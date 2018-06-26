using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class UserEvent
	{
		const string TableName = "USER_EVENT";

		// collection of record fields
		public class TableData
		{
			public int EventIid;
			public string UserName;
			public string UserId;
			public string StockRightsMask;
			public string DeviceName;
			public DateTime LoginTime;
			public DateTime LogoutTime;
			public int DurationSec;

			public TableData(int EventIid, string UserName, string UserId, string StockRightsMask, string DeviceName, DateTime LoginTime, DateTime LogoutTime, int DurationSec)
			{
				this.EventIid = EventIid;
				this.UserName = UserName;
				this.UserId = UserId;
				this.StockRightsMask = StockRightsMask;
				this.DeviceName = DeviceName;
				this.LoginTime = LoginTime;
				this.LogoutTime = LogoutTime;
				this.DurationSec = DurationSec;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(string UserName, string DeviceName, DateTime LoginTime, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from USER_EVENT WHERE UserName='{0}' AND DeviceName='{1}' AND LoginTime='{2}'", 
				MainClass.FixStringForSingleQuote(UserName), MainClass.FixStringForSingleQuote(DeviceName), MainClass.DateTimeToTimestamp(LoginTime));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "UserEvent", "GetRecord", out _conn, out myDataReader))
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

		// make a TableData object from a SADataReader record
#if !NO_ASA
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
			data = new TableData(
				MainClass.ToInt(TableName, myDataReader["EventIid"])
				, myDataReader["UserName"].ToString()
				, myDataReader["UserId"].ToString()
				, myDataReader["StockRightsMask"].ToString()
				, myDataReader["DeviceName"].ToString()
				, MainClass.ToDate(TableName, myDataReader["LoginTime"])
				, MainClass.ToDate(TableName, myDataReader["LogoutTime"])
				, MainClass.ToInt(TableName, myDataReader["DurationSec"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO USER_EVENT (UserName, UserId, StockRightsMask, DeviceName, LoginTime, LogoutTime, DurationSec) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.UserName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.StockRightsMask) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.DeviceName) + "'" + ", " + MainClass.DateTimeToTimestamp(data.LoginTime) + ", " + MainClass.DateTimeToTimestamp(data.LogoutTime) + ", " + (int)data.DurationSec + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserEvent", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(string UserName, string DeviceName, DateTime LoginTime)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE USER_EVENT WHERE UserName='{0}' AND DeviceName='{1}' AND LoginTime='{2}'", 
				MainClass.FixStringForSingleQuote(UserName), MainClass.FixStringForSingleQuote(DeviceName), MainClass.DateTimeToTimestamp(LoginTime));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserEvent", "DeleteRecord");
			return Retval;
		}


	}
}
