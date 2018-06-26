using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class PrinterData
	{
		const string TableName = "PRINTER_DATA";

		// collection of record fields
		public class TableData
		{
			public int printerIid;
            public string description;
			public string printerName;
            public string printerDriver;
            public string printerPort;

            public TableData(int printerIid, string description, string printerName, string printerDriver, string printerPort)
			{
                this.printerIid     = printerIid;
                this.description    = description;
                this.printerName    = printerName;
                this.printerDriver  = printerDriver;
                this.printerPort    = printerPort;
			}

            public TableData(TableData prevTblData)
            {
                Copy(prevTblData);
            }

            public TableData Copy(TableData prevTblData)
            {
                this.printerIid     = prevTblData.printerIid;
                this.description    = prevTblData.description;
                this.printerName    = prevTblData.printerName;
                this.printerDriver  = prevTblData.printerDriver;
                this.printerPort    = prevTblData.printerPort;

                return this;
            }
		}

		// return record given its primary key
        public static bool GetRecord(int printerIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from PRINTER_DATA WHERE printerIid='{0}'",
                (int)printerIid);
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PRINTER_DATA", "GetRecord", out _conn, out myDataReader))
			{
				try
				{
					if (myDataReader.Read())
					{
						MakeDataRec(myDataReader, out data);
					}
				}
				catch (Exception ex)
				{
					Retval = false;
					string err = String.Format(MainClass.StringTable.GetString("DatabaseError"), TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
					ServiceMessages.InsertRec(MainClass.AppName, TableName, "GetRecord", err);
				}
				finally
				{
                    if (myDataReader != null)
                    {
                        myDataReader.Close();
                        myDataReader.Dispose();
                        myDataReader = null;
                    }
                    if (_conn != null)
                    {
                        _conn.Close();
                        _conn.Dispose();
                        _conn = null;
                    }
				}
			}
#endif
			return Retval;
		}

        // get all records
        public static bool GetRecords(out List<TableData> list)
        {
            bool Retval = true;
            list = new List<TableData>();
            TableData data;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from PRINTER_DATA";
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PRINTER_DATA", "GetRecords", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        MakeDataRec(myDataReader, out data);
                        list.Add(data);
                    }
                }

                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "GetRecords", err);
                }
                finally
                {
                    if (myDataReader != null)
                    {
                        myDataReader.Close();
                        myDataReader.Dispose();
                        myDataReader = null;
                    }
                    if (_conn != null)
                    {
                        _conn.Close();
                        _conn.Dispose();
                        _conn = null;
                    }
                }
            }
#endif
            return Retval;
        }

		// make a TableData object from a SADataReader record
#if !NO_ASA
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
			data = new TableData(
                MainClass.ToInt(TableName, myDataReader["printerIid"])
                , myDataReader["description"].ToString()
                , myDataReader["printerName"].ToString()
                , myDataReader["printerDriver"].ToString()
                , myDataReader["printerPort"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

            string SqlStatement = "INSERT INTO PRINTER_DATA (description, printerName, printerDriver, printerPort) VALUES ("
                + "'" + MainClass.FixStringForSingleQuote(data.description) + "', "
                + "'" + MainClass.FixStringForSingleQuote(data.printerName) + "', "
                + "'" + MainClass.FixStringForSingleQuote(data.printerDriver) + "', "
                + "'" + MainClass.FixStringForSingleQuote(data.printerPort) + "'" + ")";
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PRINTER_DATA", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
        public static bool DeleteRecord(int printerIid)
		{
			bool Retval = true;
            string SqlStatement = string.Format("DELETE PRINTER_DATA WHERE printerIid='{0}'",
                (int)printerIid);
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PRINTER_DATA", "DeleteRecord");
			return Retval;
		}

        // check for duplicate printer descriptions, ignoring the one we are working on
        public static bool IsDuplicatedPrinterDescription(string printerDescription, string excludedPrinterName)
        {
            bool descExists = false;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from PRINTER_DATA WHERE description='" + printerDescription + "' AND printerName <> '" + excludedPrinterName + "'";

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PRINTER_DATA", "IsDuplicatedPrinterDescription", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                        descExists = true;
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "IsDuplicatedPrinterDescription", err);
                }
                finally
                {
                    if (myDataReader != null)
                    {
                        myDataReader.Close();
                        myDataReader.Dispose();
                        myDataReader = null;
                    }
                    if (_conn != null)
                    {
                        _conn.Close();
                        _conn.Dispose();
                        _conn = null;
                    }
                }
            }
#endif
            return descExists;
        }
        // check for duplicate printer descriptions, ignoring the one we are working on
        public static bool IsDuplicatedPrinterDescription(string printerDescription, int excludedPrinterIid)
        {
            bool descExists = false;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from PRINTER_DATA WHERE description='" + printerDescription + "' AND printerIid <> " + excludedPrinterIid.ToString();

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PRINTER_DATA", "IsDuplicatedPrinterDescription", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                        descExists = true;
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "IsDuplicatedPrinterDescription", err);
                }
                finally
                {
                    if (myDataReader != null)
                    {
                        myDataReader.Close();
                        myDataReader.Dispose();
                        myDataReader = null;
                    }
                    if (_conn != null)
                    {
                        _conn.Close();
                        _conn.Dispose();
                        _conn = null;
                    }
                }
            }
#endif
            return descExists;
        }

        // check for duplicate printer names
        public static bool IsDuplicatedPrinterName(string printerName)
        {
            bool descExists = false;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            //Need special processing for name starting with "\\"
            bool networkPath = (printerName.IndexOf(@"\") == 0);
            string SqlStatement;
            if (networkPath)
            {
                SqlStatement = "SELECT * from PRINTER_DATA";
            }
            else
            {
                SqlStatement = "SELECT * from PRINTER_DATA WHERE printerName=" + "'" + printerName + "'";
            }

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PRINTER_DATA", "IsDuplicatedPrinterName", out _conn, out myDataReader))
            {
                try
                {
                    if (!networkPath)
                    {
                        if (myDataReader.Read())
                        {
                            descExists = true;
                        }
                    }
                    else //loop through for name with \\
                    {
                        while (myDataReader.Read())
                        {
                            if (myDataReader["printerName"].ToString() == printerName)
                            {
                                descExists = true;
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "IsDuplicateDescription", err);
                }
                finally
                {
                    if (myDataReader != null)
                    {
                        myDataReader.Close();
                        myDataReader.Dispose();
                        myDataReader = null;
                    }
                    if (_conn != null)
                    {
                        _conn.Close();
                        _conn.Dispose();
                        _conn = null;
                    }
                }
            }
#endif
            return descExists;
        }

        // find Iid for printer name
        public static int GetIidFromPrinterName(string printerName)
        {
            int printerIid = 0;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from PRINTER_DATA WHERE printerName=" + "'" + printerName + "'";

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PRINTER_DATA", "GetIidFromPrinterName", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                        printerIid = (int)myDataReader["printerIid"];
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "GetIidFromPrinterName", err);
                }
                finally
                {
                    if (myDataReader != null)
                    {
                        myDataReader.Close();
                        myDataReader.Dispose();
                        myDataReader = null;
                    }
                    if (_conn != null)
                    {
                        _conn.Close();
                        _conn.Dispose();
                        _conn = null;
                    }
                }
            }
#endif
            return printerIid;
        }
	}
}
