using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class HHMessageLog
	{
		const string TableName = "HH_MESSAGE_LOG";

		// collection of record fields
		public class TableData
		{
			public int MsgIid;
			public DateTime MessageTime;
			public string HHMessage;
			public string DeviceName;
			public string UserId;
			public string Class;
			public string Method;

			public TableData(int MsgIid, DateTime MessageTime, string HHMessage, string DeviceName, string UserId, string Class, string Method)
			{
				this.MsgIid = MsgIid;
				this.MessageTime = MessageTime;
				this.HHMessage = HHMessage;
				this.DeviceName = DeviceName;
				this.UserId = UserId;
				this.Class = Class;
				this.Method = Method;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int MsgIid, string DeviceName, DateTime MessageTime, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from HH_MESSAGE_LOG WHERE MsgIid='{0}' AND DeviceName='{1}' AND MessageTime='{2}'", 
				(int)MsgIid, MainClass.FixStringForSingleQuote(DeviceName), MainClass.DateTimeToTimestamp(MessageTime));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "HHMessageLog", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["MsgIid"])
				, MainClass.ToDate(TableName, myDataReader["MessageTime"])
				, myDataReader["HHMessage"].ToString()
				, myDataReader["DeviceName"].ToString()
				, myDataReader["UserId"].ToString()
				, myDataReader["Class"].ToString()
				, myDataReader["Method"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO HH_MESSAGE_LOG (MsgIid, MessageTime, HHMessage, DeviceName, UserId, Class, Method) VALUES ("
				+ (int)data.MsgIid + ", " + MainClass.DateTimeToTimestamp(data.MessageTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.HHMessage) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.DeviceName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Class) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Method) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHMessageLog", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int MsgIid, string DeviceName, DateTime MessageTime)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE HH_MESSAGE_LOG WHERE MsgIid='{0}' AND DeviceName='{1}' AND MessageTime='{2}'", 
				(int)MsgIid, MainClass.FixStringForSingleQuote(DeviceName), MainClass.DateTimeToTimestamp(MessageTime));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHMessageLog", "DeleteRecord");
			return Retval;
		}


	}
}
