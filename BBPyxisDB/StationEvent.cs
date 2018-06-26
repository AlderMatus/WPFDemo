using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class StationEvent
	{
		const string TableName = "STATION_EVENT";

		// collection of record fields
		public class TableData
		{
			public int EventIid;
			public string DeviceName;
			public int ShutdownType;
			public DateTime StationStopTime;
			public DateTime StationStartTime;
			public string EventMsg;

			public TableData(int EventIid, string DeviceName, int ShutdownType, DateTime StationStopTime, DateTime StationStartTime, string EventMsg)
			{
				this.EventIid = EventIid;
				this.DeviceName = DeviceName;
				this.ShutdownType = ShutdownType;
				this.StationStopTime = StationStopTime;
				this.StationStartTime = StationStartTime;
				this.EventMsg = EventMsg;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int EventIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from STATION_EVENT WHERE EventIid='{0}'", 
				(int)EventIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "StationEvent", "GetRecord", out _conn, out myDataReader))
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
				, myDataReader["DeviceName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ShutdownType"])
				, MainClass.ToDate(TableName, myDataReader["StationStopTime"])
				, MainClass.ToDate(TableName, myDataReader["StationStartTime"])
				, myDataReader["EventMsg"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO STATION_EVENT (DeviceName, ShutdownType, StationStopTime, StationStartTime, EventMsg) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.DeviceName) + "'" + ", " + (int)data.ShutdownType + ", " + MainClass.DateTimeToTimestamp(data.StationStopTime) + ", " + MainClass.DateTimeToTimestamp(data.StationStartTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.EventMsg) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "StationEvent", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int EventIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE STATION_EVENT WHERE EventIid='{0}'", 
				(int)EventIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "StationEvent", "DeleteRecord");
			return Retval;
		}


	}
}
