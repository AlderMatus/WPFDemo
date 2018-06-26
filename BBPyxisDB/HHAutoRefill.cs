using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class HHAutoRefill
	{
		const string TableName = "HH_AUTO_REFILL";

		public enum ItemsToRefillEnum
		{
			StockItemsOnly = 1,
			NonstockItemsOnly = 2,
			StockAndNonstock = 3
		}

		public enum WhenRefillEnum
		{
			CurLessPar = 1,
			CurLessRefillPt = 2,
			CurLessCritlow = 3
		}

        public class TableData
		{
			public int AutoRefillIid;
			public WhenRefillEnum WhenRefill;
			public ItemsToRefillEnum ItemsToRefill;
            public string UserName;
            public string UserId;

			public TableData(int autoRefillIid, WhenRefillEnum whenRefill,
                ItemsToRefillEnum itemsToRefill, string userName, string userId)
			{
				AutoRefillIid = autoRefillIid;
				WhenRefill = whenRefill;
				ItemsToRefill = itemsToRefill;
                UserName = userName;
                UserId = userId;
			}
		}

		// get a record
		// integer vals are returned as -1 if they're null
        // return false if no record found
		public static bool GetRecord(int autoRefillIid, out TableData ard)
		{
			bool Retval = false;
			ard = null;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from HH_AUTO_REFILL WHERE autoRefillIid=" + autoRefillIid;
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "HHAutoRefill", "GetRecord", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                        ard = new TableData(
                            autoRefillIid,
                            (WhenRefillEnum)(Int16)myDataReader["whenRefill"],
                            (ItemsToRefillEnum)(Int16)myDataReader["itemsToRefill"],
                            myDataReader["userName"].ToString(),
                            myDataReader["userId"].ToString());
                        Retval = true;
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "HHAutoRefill", "GetRecord", err);
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
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool RetVal = false;
			NewIid = -1;

            string strSqlStatement = "INSERT INTO HH_AUTO_REFILL (whenRefill, itemsToRefill, userName, userId) VALUES ("
                + (int)data.WhenRefill + ", " + (int)data.ItemsToRefill + ", '" + MainClass.FixStringForSingleQuote(data.UserName) + "', '" + MainClass.FixStringForSingleQuote(data.UserId) + "')";
			RetVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "HHAutoRefill", "InsertRec", out NewIid);

			return RetVal;
		}

		// update fields of specified record
		public static bool UpdateRecord(int AutoRefillIid, TableData data)
		{
			bool Retval = true;
			string sql = "UPDATE HH_AUTO_REFILL SET whenRefill=" + (int)data.WhenRefill
                + ", itemsToRefill=" + (int)data.ItemsToRefill
                + ", userName='" + MainClass.FixStringForSingleQuote(data.UserName)
                + "', UserId='" + MainClass.FixStringForSingleQuote(data.UserId)
                + "' WHERE autoRefillIid=" + AutoRefillIid;
			Retval = MainClass.ExecuteSql(sql, true, TableName, "HHAutoRefill", "UpdateRecord");

			return Retval;
		}


        // delete specified record
        public static bool DeleteRecord(int Iid)
        {
            bool Retval = true;
            string sql = "DELETE HH_AUTO_REFILL WHERE autoRefillIid = " + Iid;
            Retval = MainClass.ExecuteSql(sql, true, TableName, "HHAutoRefill", "DeleteRecord");

            return Retval;
        }

	}
}

