using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class HHAutoRefillToParloc
	{
		const string TableName = "HH_AUTO_REFILL_TO_PARLOC";

		// get par locations associated with this auto refill
        // return false if error or no data found
		public static bool GetParLocs(int autoRefillIid, List<int> ParLocList)
		{
			bool Retval = false;
			ParLocList.Clear();
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from HH_AUTOREFILL_TO_PARLOC WHERE autoRefillIid=" + autoRefillIid;
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "HHAutoRefillToParloc", "GetParLocs", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        ParLocList.Add(MainClass.ToInt(TableName, myDataReader["parLocationIid"]));
                        Retval = true;
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "GetParLocs", err);
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

		// insert record
		// OUTPUT: NewIid - IID of inserted record
        public static bool InsertRecord(int AutoRefillIid, int ParLocationIid, out int NewIid)
		{
			bool RetVal = false;
			NewIid = -1;

            string strSqlStatement = "INSERT INTO HH_AUTOREFILL_TO_PARLOC (autoRefillIid, parLocationIid) VALUES (" + AutoRefillIid + ", " + ParLocationIid + ")";
			RetVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "HHAutoRefillToParloc", "InsertRec", out NewIid);

			return RetVal;
		}

		// delete specified record
		public static bool DeleteRecord(int Iid)
		{
			bool Retval = true;
			string sql = "DELETE HH_AUTOREFILL_TO_PARLOC WHERE autoRefillIid = " + Iid;
			Retval = MainClass.ExecuteSql(sql, true, TableName, "HHAutoRefillToParloc", "DeleteRecord");

			return Retval;
		}


	}
}

