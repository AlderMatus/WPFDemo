using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class DeviceBlockedMsgs
	{
		const string TableName = "DEVICE_BLOCKED_MSGS";

		// collection of record fields
		public class TableData
		{
			public int DeviceIid;
			public string MsgType;
			public int BlockIn;
			public int BlockOut;

			public TableData(int DeviceIid, string MsgType, int BlockIn, int BlockOut)
			{
				this.DeviceIid = DeviceIid;
				this.MsgType = MsgType;
				this.BlockIn = BlockIn;
				this.BlockOut = BlockOut;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int DeviceIid, string MsgType, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from DEVICE_BLOCKED_MSGS WHERE DeviceIid='{0}' AND MsgType='{1}'", 
				(int)DeviceIid, MainClass.FixStringForSingleQuote(MsgType));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "DeviceBlockedMsgs", "GetRecord", out _conn, out myDataReader))
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
						myDataReader.Close();
					if (_conn != null)
						_conn.Close();
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
				MainClass.ToInt(TableName, myDataReader["DeviceIid"])
				, myDataReader["MsgType"].ToString()
				, MainClass.ToInt(TableName, myDataReader["BlockIn"])
				, MainClass.ToInt(TableName, myDataReader["BlockOut"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO DEVICE_BLOCKED_MSGS (DeviceIid, MsgType, BlockIn, BlockOut) VALUES ("
				+ (int)data.DeviceIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.MsgType) + "'" + ", " + (int)data.BlockIn + ", " + (int)data.BlockOut + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "DeviceBlockedMsgs", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int DeviceIid, string MsgType)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE DEVICE_BLOCKED_MSGS WHERE DeviceIid='{0}' AND MsgType='{1}'", 
				(int)DeviceIid, MainClass.FixStringForSingleQuote(MsgType));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "DeviceBlockedMsgs", "DeleteRecord");
			return Retval;
		}


	}
}
