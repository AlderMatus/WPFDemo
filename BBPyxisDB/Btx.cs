using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Btx
	{
		const string TableName = "BTX";

		// collection of record fields
		public class TableData
		{
			public int DeviceIid;
			public DateTime Timestamp;
			public DateTime StartTime;
			public DateTime FinishTime;
			public string StatusString;
			public int ActiveFlag;

			public TableData(int DeviceIid, DateTime Timestamp, DateTime StartTime, DateTime FinishTime, string StatusString, int ActiveFlag)
			{
				this.DeviceIid = DeviceIid;
				this.Timestamp = Timestamp;
				this.StartTime = StartTime;
				this.FinishTime = FinishTime;
				this.StatusString = StatusString;
				this.ActiveFlag = ActiveFlag;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int DeviceIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from BTX WHERE DeviceIid='{0}'", 
				(int)DeviceIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Btx", "GetRecord", out _conn, out myDataReader))
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
				, MainClass.ToDate(TableName, myDataReader["Timestamp"])
				, MainClass.ToDate(TableName, myDataReader["StartTime"])
				, MainClass.ToDate(TableName, myDataReader["FinishTime"])
				, myDataReader["StatusString"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ActiveFlag"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO BTX (DeviceIid, Timestamp, StartTime, FinishTime, StatusString, ActiveFlag) VALUES ("
				+ (int)data.DeviceIid + ", " + MainClass.DateTimeToTimestamp(data.Timestamp) + ", " + MainClass.DateTimeToTimestamp(data.StartTime) + ", " + MainClass.DateTimeToTimestamp(data.FinishTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.StatusString) + "'" + ", " + (int)data.ActiveFlag + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Btx", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int DeviceIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE BTX WHERE DeviceIid='{0}'", 
				(int)DeviceIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Btx", "DeleteRecord");
			return Retval;
		}


	}
}
