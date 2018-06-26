//#define TESTDB

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif
using Carefusion.Supply.CSharpResource;
using System.Resources;

/// <summary>
/// This class contains general support functions to facilitate interacting with the
/// console database. The reason it's called BB PyxisDB is to honor the genius of the author
/// who, due to his unremitting humility, wishes to remain anonymous, other than the vague
/// initials, which could indicate practically anyone, for example Boris Becker or 
/// Bridget Bardot.
/// </summary>

namespace BBPyxisDB
{
    public abstract class MainClass
    {
        //public static readonly System.Resources.ResourceManager StringTable = new System.Resources.ResourceManager("BBPyxisDB.Resource1", System.Reflection.Assembly.GetExecutingAssembly());
        public static readonly ResourceManager StringTable = SupplyResource.GetResource();
        public static readonly string AppName = "BBPyxisDB";
        private static string[] source = 
                {
                    "SupplyConsole30",
                    "\x1d\x02\xf4\xcd\xe1\xec\x24\xf2\xf5\xee\xf7\xb8\x00\x30\x09",
                    "SupplyConsole30",
                    "\x1d\x02\xf4\xcd\xe1\xec\x24\xf2\xf5\xee\xf7\xb8\x00\x30\x09",
                };
        public static string PWD       
        {
           get{
               string[] values = null;
               if(values == null)
               {
                    List<string> all = new List<string>();
                    for (int i = 0; i < source.Length / 2; ++i)
                    {
                        string hr = "";
                        string s1 = source[i + i];
                        string s2 = source[i + i + 1];
                        for (int j = 0; j < Math.Min(source[i * 2].Length, source[i * 2 + 1].Length); ++j)
                        {
                            hr += (char)(0xff & (s1[j] + s2[j]));
                        }
                        all.Add(hr);
                    }
                    values = all.ToArray();
               }
                return values[0].Substring(4);
            }
        }           

        public static string OEMAUTHENTICATION = "SET TEMPORARY OPTION CONNECTION_AUTHENTICATION='Company=CareFusion;Application=Pyxis Dispensing;Signature=000fa55157edb8e14d818eb4fe3db41447146f1571g46263b1e7a23368d91d850a1ec22bb8f7a9f0809'";		// Sybase 16 OEM connectiion authentication
     
        public enum DataTypeEnum
        {
            String,
            Int,
            DateTime
        }

#if !NO_ASA
        // Return an SAConnection to the database
		public static SAConnection GetConnection()
		{
			SAConnection RetConnection = null;
            string strDSN = "DSN=PSTAR";
	
			RetConnection = new SAConnection(strDSN);
            //Sybase 16 OEM
            RetConnection.InitString = OEMAUTHENTICATION;
			return RetConnection;
				
		}

        public static SAConnection GetMasterConnection()
        {
            SAConnection RetConnection = null;
            RetConnection = new SAConnection("DSN=pSTARMAIN");
            RetConnection.InitString = OEMAUTHENTICATION;
            return RetConnection;
        }

		// Open a DB connection
		static bool TryOpenDB(out SAConnection _conn, out string ErrMsg)
		{
			bool RetVal=false;
			ErrMsg = "";
			_conn = null;

            try
			{
				_conn = GetConnection();
				_conn.Open();
				RetVal = (ConnectionState.Open == _conn.State);
			}
			catch (Exception ex)
			{
				ErrMsg = ex.Message.ToString();
			}

			return RetVal;
		}

		// Open a DB connection
		static bool OpenDB(string CallingClass, string CallingFunction, out SAConnection _conn)
		{
			bool RetVal=false;
			_conn = null;
			string ErrMsg = "";

            // GHB 6/23/08: Removed retry on DB for performance reasons on DB Down event
            RetVal = TryOpenDB(out _conn, out ErrMsg);

			return RetVal;
		}

        //Added for CSharp Station Report Wizard CR13
        public static bool TestDBConnection(bool bLocalDB)
        {
            SAConnection _conn = null;
            bool RetVal = false;

            try
            {
                _conn = bLocalDB ? MainClass.GetConnection() : MainClass.GetMasterConnection();
                _conn.Open();
                RetVal = (ConnectionState.Open == _conn.State);
            }
            catch (Exception ex)
            {
                RetVal = false;
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }

            return RetVal;
        }
#endif

        // Take care of the single quote problem: replace ' with ''
        public static string FixStringForSingleQuote(string s)
		{
			string sOut="";
			if (s != null)
			{
				if (s != "")
					sOut = s.Replace("'", "''");
			}
			return sOut;
		}

        // Take care of formatting braces problem: replace {} with {{}}
        public static string FixStringForBraces(string s)
        {
            string sOut = "";
            if (s != null)
            {
                if (s != "")
                {
                    sOut = s.Replace("{", "{{");
                    sOut = sOut.Replace("}", "}}");
                }
            }
            return sOut;
        }

        // Execute an insert, delete, or update SQL statement
        // and return the Iid of the last inserted record
        public static bool ExecuteSql(string SqlStatement, bool LogErrMsg,
			string table, string CallingClass, string CallingFunction, out int Iid)
		{
			bool RetVal = false;
			Iid = 0;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            RetVal = ExecuteSql(true, SqlStatement, LogErrMsg, table, CallingClass, CallingFunction, out _conn);
            if (RetVal && ExecuteSelect(_conn, "SELECT @@identity", true, table, CallingClass, CallingFunction, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                        RetVal = int.TryParse(myDataReader["@@identity"].ToString(), out Iid);
                }
                catch (Exception ex)
                {
                    if (LogErrMsg)
                    {
                        string err = String.Format(StringTable.GetString("DatabaseError"),
                            table,
                            ex.Message.ToString() + "(" + SqlStatement + ")");
                        ServiceMessages.InsertRec(MainClass.AppName, CallingClass, CallingFunction, err);
                    }
                }
                finally
                {
                    if (myDataReader != null)
                        myDataReader.Close();
                    if (_conn != null)
                        _conn.Close();
                }
            }
#endif

            return RetVal;
        }

		// Execute an insert, delete, or update SQL statement
		// 	Select statements should be done by the ExecuteSelect method
		// INPUT:
		// 		SqlStatement - the SQL statement to execute
		// 		LogErrMsg - if true, write to error table if error occurs. Should always be true unless caller is LogErrorMsg itself.
		// 		table - name of DB table being accessed (or something like "pocket join location")
		// 		CallingClass - the calling class
		// 		CallingFunction - the calling function
		// Return value: true if successful
		public static bool ExecuteSql(string SqlStatement, bool LogErrMsg,
			string table, string CallingClass, string CallingFunction)
		{
			bool RetVal = true;
#if !NO_ASA
			SAConnection _conn = null;
			RetVal = ExecuteSql(false, SqlStatement, LogErrMsg, table, CallingClass, CallingFunction, out _conn);
#endif
			return RetVal;
		}

#if !NO_ASA
		// Execute a SQL statement & return the connection
        // Return value: true if successful
        static bool ExecuteSql(bool ReturnConn, string SqlStatement, bool LogErrMsg,
            string table, string CallingClass, string CallingFunction,
            out SAConnection _conn)
        {
            bool RetVal = true;
            _conn = null;

            try
            {
                if (OpenDB(CallingClass, CallingFunction, out _conn))
                {
                    SACommand cmd = new SACommand(SqlStatement, _conn);
                    cmd.ExecuteNonQuery();
                }
                else
                    RetVal = false;
            }
            catch (Exception ex)
            {
                RetVal = false;
                if (LogErrMsg)
                {
                    string err = String.Format(StringTable.GetString("DatabaseError"),
                        table,
                        ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, CallingClass, CallingFunction, err);
                }
            }
            finally
            {
                if (!ReturnConn && _conn != null)
                    _conn.Close();
            }

            return RetVal;
        }
#endif

		// Execute a SQL select statement
        // note on accessibility: if we really have to we can make this public, 
        // but let's try hard not to, so we can maintain a strict separation
        // between GUI/business rules and DB layer. Nobody should need a 
        // reference to ianywhere except bbpyxisdb.
		// INPUT:
		// 			SqlStatement - the SQL select statement to execute
		// 			ErrMsg - if true, write to error table if error occurs. Should always be true unless caller is LogErrorMsg itself.
		// 			table - name of DB table being accessed (or something like "pocket join location") - used for error reporting
		// 			CallingClass - the calling class
		// 			CallingFunction - the calling function
		// OUTPUT:
		// 			myDataReader - the selected records
		// Return value: true if successful
        /* Example:
            SAConnection _conn;
            SADataReader myDataReader;
            if (BBPyxisDB.MainClass.ExecuteSelect("SELECT xx from yy", true, "SCANCODE", "ItemInfoClass", "GetItemIdFromBarCode", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read()) // or "while" instead of "if"
                    {
                        strItemId = myDataReader["medId"].ToString();   // varchar
                        nItemIid = BBPyxisDB.MainClass.ToInt(tablename, myDataReader["medId"]);   // integer
                        MyDate = BBPyxisDB.MainClass.ToDate(tablename, myDataReader["lastInventory"]);   // timestamp
                        MyBool = BBPyxisDB.MainClass.ToBool(tablename, myDataReader["isBatch"]);   // smallint -> bool
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    if (myDataReader != null)
                        myDataReader.Close();
                    if (_conn != null)
                        _conn.Close();
                }
            }
        */
#if !NO_ASA
        internal static bool ExecuteSelect(string SqlStatement, bool ErrMsg,
			string table, string CallingClass, string CallingFunction,
			out SAConnection _conn, out SADataReader myDataReader)
		{
			bool RetVal=true;
            myDataReader = null;
			_conn=null;

			try
			{
				if (OpenDB(CallingClass, CallingFunction, out _conn))
				{
					SACommand cmd = new SACommand(SqlStatement, _conn);
					myDataReader = cmd.ExecuteReader();
				}
				else
					RetVal = false; 
			}			
			catch (Exception ex)
			{
				RetVal = false; 
				if (ErrMsg)
				{
					string err = String.Format(StringTable.GetString("DatabaseError"),
						table, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, CallingClass, CallingFunction, err);
				}
			}

            return RetVal;
		}

        //Added for CSharp Station Report Wizard CR13
        internal static bool ExecuteSelect(string SqlStatement, bool ErrMsg,
            string table, string CallingClass, string CallingFunction,
            out SAConnection _conn, out SADataReader myDataReader, bool bLocalDB)
        {
            bool RetVal = true;
            myDataReader = null;
            _conn = null;

            try
            {
                _conn = bLocalDB ? MainClass.GetConnection() : MainClass.GetMasterConnection();
                _conn.Open();
                RetVal = (ConnectionState.Open == _conn.State);

                if (RetVal)
                {
                    SACommand cmd = new SACommand(SqlStatement, _conn);
                    myDataReader = cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                RetVal = false;
                if (ErrMsg)
                {
                    string err = String.Format(StringTable.GetString("DatabaseError"),
                        table, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, CallingClass, CallingFunction, err);
                }
            }

            return RetVal;
        }

        // Execute a SQL select statement using an existing DB connection
        internal static bool ExecuteSelect(SAConnection _conn, string SqlStatement, bool ErrMsg,
            string table, string CallingClass, string CallingFunction,
            out SADataReader myDataReader)
        {
            bool RetVal = true;
            myDataReader = null;

            try
            {
                SACommand cmd = new SACommand(SqlStatement, _conn);
                myDataReader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                RetVal = false;
                if (ErrMsg)
                {
                    string err = String.Format(StringTable.GetString("DatabaseError"),
                        table, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, CallingClass, CallingFunction, err);
                }
            }

            return RetVal;
        }
#endif

		// return distinct list of all records for specified field/table. 
		// Looks in RPTINDX table also. Assumes field is a string
        public static bool GetDistinctVals(DataTypeEnum DataType, string table,
            string column, string filter, out List<string> data, bool bLocalDB = true)
		{
			data = new List<string>();
			bool retval = false;
			string IndxTable = string.Format("{0}_{1}_RPTINDX", table, column);
            if (GetDistinctValsFromTable(DataType, IndxTable, column, filter, false, data, bLocalDB))
				retval = true;
			else
                retval = GetDistinctValsFromTable(DataType, table, column, filter, true, data, bLocalDB);
			return retval;
		}

		// return distinct list of all records for specified field/table. Assumes field is a string
        private static bool GetDistinctValsFromTable(DataTypeEnum DataType, 
            string table, string column, string filter,bool logErr, List<string> data, bool bLocalDB = true)
        {
            string filterQuery = "";
			bool retval = false;
#if !NO_ASA
            SAConnection conn = null;
            SADataReader myReader = null;

            // build query if string is not empty
            if (filter.Length > 0)
            {
                filterQuery = string.Format("WHERE {0} ", filter);
            }
			string selectStr = string.Format("SELECT DISTINCT {0} FROM {1} {2}ORDER BY {0} ASC", column, table, filterQuery);

            if (ExecuteSelect(selectStr, logErr, table, "MainClass", "GetDistinctVals", out conn, out myReader, bLocalDB))
            {
                int i;
                DateTime dt;
                decimal dec;
                Type t;
                try
                {
                    retval = true;
                    while (myReader.Read())
                    {
                        switch (DataType)
                        {
                            case DataTypeEnum.String:
                                data.Add(myReader[column].ToString());
                                break;
                            case DataTypeEnum.Int:
                                // integers and decimal numbers are the same type in reports
                                // have to check the data type before converting
                                t = myReader[column].GetType();
                                // check for null value
                                if (Type.GetTypeCode(t) == TypeCode.DBNull)
                                {
                                    break;
                                }
                                // check for decimal or int
                                if (Type.GetTypeCode(t) == TypeCode.Decimal)
                                {
                                    dec = BBPyxisDB.MainClass.ToDecimal(table, myReader[column]);
                                    data.Add(dec.ToString());
                                }
                                else
                                {
                                    i = BBPyxisDB.MainClass.ToInt(table, myReader[column]);
                                    data.Add(i.ToString());
                                }
                                break;
                            case DataTypeEnum.DateTime:
                                dt = BBPyxisDB.MainClass.ToDate(table, myReader[column]);
                                data.Add(dt.ToString("yyyy/MM/dd  HH:mm:ss"));
                                break;
                            default:
                                System.Diagnostics.Debug.Assert(true, "Bad data type " + DataType + " in BPyxisDB.MainClass.GetDistinctValsFromTable");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(StringTable.GetString("DatabaseCastError"),
                        table, ex.Message.ToString());
                    ServiceMessages.InsertRec(MainClass.AppName, "MainClass", "GetDistinctValsFromTable", err);
                }
        }

            if (myReader != null)
                myReader.Close();

            if (conn != null)
                conn.Close();
#endif
			return retval;
        }

        // convert int to either its string representation or NULL
        public static string IntToDBString(int inval)
        {
            string Retval = "NULL";
            if (inval != -1)
                Retval = inval.ToString();
            return Retval;
        }

        // convert bool to int
        public static int BoolToInt(bool inval)
        {
            int val = 0;
            if (inval)
                val = 1;
            else
                val = 0;
            return val;
        }

        // convert integer DB field value to bool
        public static bool ToBool(string table, object inval)
        {
            int val=0;
            Type t = inval.GetType();
            try
            {
                if (Type.GetTypeCode(t) == TypeCode.Int16)
                    val = (Int16)inval;
                else if (Type.GetTypeCode(t) != TypeCode.DBNull)
                    val = (int)inval;
			}
			catch (Exception ex)
			{
                string err = String.Format(StringTable.GetString("DatabaseCastError"),
                    table, ex.Message.ToString());
                ServiceMessages.InsertRec(MainClass.AppName, "MainClass", "ToBool", err);
            }
            return (val == 1);
        }

        // convert timestamp DB field value to DateTime
        public static DateTime ToDate(string table, object inval)
        {
            DateTime Retval = DateTime.MinValue;
            try
            {
                if (Type.GetTypeCode(inval.GetType()) != TypeCode.DBNull)
                    Retval = (DateTime)inval;
                if (Retval.Year == 1 && Retval.Month == 1 && Retval.Day == 1)
                    Retval = DateTime.MinValue; // sybase translates null as time 0, but sets it to 12:00PM instead of 12:00AM
            }
            catch (Exception ex)
            {
                string err = String.Format(StringTable.GetString("DatabaseCastError"),
                    table, ex.Message.ToString());
                ServiceMessages.InsertRec(MainClass.AppName, "MainClass", "ToDate", err);
            }
            return Retval;
        }

        // convert Decimal DB field value to Decimal
        public static Decimal ToDecimal(string table, object inval)
        {
            Decimal Retval = (decimal)-1.0;
            try
            {
                if (Type.GetTypeCode(inval.GetType()) != TypeCode.DBNull)
                    Retval = (Decimal)inval;
            }
            catch (Exception ex)
            {
                string err = String.Format(StringTable.GetString("DatabaseCastError"),
                    table, ex.Message.ToString());
                ServiceMessages.InsertRec(MainClass.AppName, "MainClass", "ToDecimal", err);
            }
            return Retval;
        }

        // convert binary DB field value to byte[]
        public static byte[] ToByteArray(string table, object inval)
        {
            byte[] Retval = { };
            try
            {
                if (Type.GetTypeCode(inval.GetType()) != TypeCode.DBNull)
                    Retval = (byte[])inval;
            }
            catch (Exception ex)
            {
                string err = String.Format(StringTable.GetString("DatabaseCastError"),
                    table, ex.Message.ToString());
                ServiceMessages.InsertRec(MainClass.AppName, "MainClass", "ToByteArray", err);
            }
            return Retval;
        }

        // convert integer DB field value to int
        public static int ToInt(string table, object inval)
        {
            int Retval = -1;
            Type t = inval.GetType();
            try
            {
                if (Type.GetTypeCode(t) == TypeCode.Int16)
                    Retval = (Int16)inval;
                else if (Type.GetTypeCode(t) != TypeCode.DBNull)
                    Retval = (int)inval;
            }
            catch (Exception ex)
            {
                string err = String.Format(StringTable.GetString("DatabaseCastError"),
                    table, ex.Message.ToString());
                ServiceMessages.InsertRec(MainClass.AppName, "MainClass", "ToInt", err);
            }
            return Retval;
        }

        // convert a DateTime to a format acceptable to Sybase Timestamp field
        public static string DateTimeToTimestamp(DateTime inval)
        {
            string Retval = "NULL";
            if (inval != DateTime.MinValue)
                Retval = "'" + inval.ToString("yyyy/MM/dd  HH:mm:ss.ffffff") + "'";
            return Retval;
        }      
#if TESTDB
        public static void TestReportFilter()
        {
            bool b;
            int IID;
            List<string> sdata;
            ReportFilter.TableData data;
            data = new ReportFilter.TableData(2, "filterField2", 2, "filterItems");
            b = ReportFilter.InsertRec(data, out IID);
            b = GetFirstFields("select * from pocket_access_username_rptindx", "pocket_access_username_rptindx", out sdata);
            b = ReportFilter.GetRecord(1, "filterField", out data);
            data.FilterItems = "x";
            data.FilterType = 3;
            b = ReportFilter.UpdateRecord(data);
        }

        public static void TestReport()
        {
            Reports.TableData data;
            List<Reports.TableData> list = new List<Reports.TableData>();
            int IID;
            bool b = Reports.GetRecord(1, out data);
            b = Reports.GetRecs(list);
            data.ReportIid = 2;
            Reports.InsertRec(data, out IID);
        }

        public static void TestTaskSchedule()
        {
            int iid;
            bool b;
            TaskSchedule.TableData tsd;

            b = TaskSchedule.GetRecord(true, 221, out tsd);
            TaskSchedule.TableData data = new TaskSchedule.TableData(-1, 1, "x1", 1, "", true, TaskSchedule.TimeUnitEnum.Day, -1,
                DateTime.Now, DateTime.MinValue, 0, 0, 0, 0, 0, true, "", true, "", DateTime.MinValue, DateTime.Now);
            b = TaskSchedule.InsertRec(true, data, out iid);
            /*List<DirPathTree.DirTreeSet> TreeSet;
            BBPyxisDB.DirPathTree.GetBatchReportTree(out TreeSet);
            foreach (DirPathTree.DirTreeSet Parent in TreeSet)
            {
                PrintChildren(Parent);
            }*/
        }

        static void PrintChildren(DirPathTree.DirTreeSet Parent)
        {
            string str = "parent: " + Parent.DirDescription + " children: ";
            foreach (DirPathTree.DirTreeSet Child in Parent.ChildDir)
            {
                str += Child.DirDescription + ", ";
            }
            System.Diagnostics.Trace.WriteLine(str);
            foreach (DirPathTree.DirTreeSet Child in Parent.ChildDir)
                PrintChildren(Child);
        }
#endif

        public static bool ExecuteSql(string sql, bool throwException, params SAParameter[] list)
        {
            try
            {
                using (SAConnection con = MainClass.GetConnection())
                {
                    con.Open();
                    using (SACommand cmd = new SACommand(sql, con))
                    {
                        cmd.Parameters.AddRange(list);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                if(throwException)
                {
                    throw ex;
                }
            }
            return false;
        }
    }
}
