using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class UserComplience
	{
		const string TableName = "USER_COMPLIANCE";

		// collection of record fields
		public class TableData
		{
			public int EventIid;
			public string UserName;
			public string UserId;
			public int UserType;
			public string DeviceName;
			public int DeviceType;
			public int ProcedureStation;
			public int ActivityType;
			public string AreaName;
			public DateTime LoginTime;
			public DateTime LogoutTime;
			public int DiscrepancyCount;
			public int OutsideExpectedUsage;
			public int DisruptSec;
			public int DoorNumber;
			public int IsDoorOpenedStartState;
			public DateTime DoorOpenedTime;
			public int DoorDurationSec;
			public int IsDoorClosedEndState;
			public DateTime DoorClosedTime;
			public int DoorTransactionCount;
			public int DoorComplianceType;
			public string SessionID;
			public int SessionContext;
			public int AnchorPage;

			public TableData(int EventIid, string UserName, string UserId, int UserType, string DeviceName, int DeviceType, int ProcedureStation, int ActivityType, string AreaName, DateTime LoginTime, DateTime LogoutTime, int DiscrepancyCount, int OutsideExpectedUsage, int DisruptSec, int DoorNumber, int IsDoorOpenedStartState, DateTime DoorOpenedTime, int DoorDurationSec, int IsDoorClosedEndState, DateTime DoorClosedTime, int DoorTransactionCount, int DoorComplianceType, string SessionID, int SessionContext, int AnchorPage)
			{
				this.EventIid = EventIid;
				this.UserName = UserName;
				this.UserId = UserId;
				this.UserType = UserType;
				this.DeviceName = DeviceName;
				this.DeviceType = DeviceType;
				this.ProcedureStation = ProcedureStation;
				this.ActivityType = ActivityType;
				this.AreaName = AreaName;
				this.LoginTime = LoginTime;
				this.LogoutTime = LogoutTime;
				this.DiscrepancyCount = DiscrepancyCount;
				this.OutsideExpectedUsage = OutsideExpectedUsage;
				this.DisruptSec = DisruptSec;
				this.DoorNumber = DoorNumber;
				this.IsDoorOpenedStartState = IsDoorOpenedStartState;
				this.DoorOpenedTime = DoorOpenedTime;
				this.DoorDurationSec = DoorDurationSec;
				this.IsDoorClosedEndState = IsDoorClosedEndState;
				this.DoorClosedTime = DoorClosedTime;
				this.DoorTransactionCount = DoorTransactionCount;
				this.DoorComplianceType = DoorComplianceType;
				this.SessionID = SessionID;
				this.SessionContext = SessionContext;
				this.AnchorPage = AnchorPage;
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
			string SqlStatement = string.Format("SELECT * from USER_COMPLIANCE WHERE EventIid='{0}'", 
				(int)EventIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "UserComplience", "GetRecord", out _conn, out myDataReader))
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
				, MainClass.ToInt(TableName, myDataReader["UserType"])
				, myDataReader["DeviceName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["DeviceType"])
				, MainClass.ToInt(TableName, myDataReader["ProcedureStation"])
				, MainClass.ToInt(TableName, myDataReader["ActivityType"])
				, myDataReader["AreaName"].ToString()
				, MainClass.ToDate(TableName, myDataReader["LoginTime"])
				, MainClass.ToDate(TableName, myDataReader["LogoutTime"])
				, MainClass.ToInt(TableName, myDataReader["DiscrepancyCount"])
				, MainClass.ToInt(TableName, myDataReader["OutsideExpectedUsage"])
				, MainClass.ToInt(TableName, myDataReader["DisruptSec"])
				, MainClass.ToInt(TableName, myDataReader["DoorNumber"])
				, MainClass.ToInt(TableName, myDataReader["IsDoorOpenedStartState"])
				, MainClass.ToDate(TableName, myDataReader["DoorOpenedTime"])
				, MainClass.ToInt(TableName, myDataReader["DoorDurationSec"])
				, MainClass.ToInt(TableName, myDataReader["IsDoorClosedEndState"])
				, MainClass.ToDate(TableName, myDataReader["DoorClosedTime"])
				, MainClass.ToInt(TableName, myDataReader["DoorTransactionCount"])
				, MainClass.ToInt(TableName, myDataReader["DoorComplianceType"])
				, myDataReader["SessionID"].ToString()
				, MainClass.ToInt(TableName, myDataReader["SessionContext"])
				, MainClass.ToInt(TableName, myDataReader["AnchorPage"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO USER_COMPLIANCE (UserName, UserId, UserType, DeviceName, DeviceType, ProcedureStation, ActivityType, AreaName, LoginTime, LogoutTime, DiscrepancyCount, OutsideExpectedUsage, DisruptSec, DoorNumber, IsDoorOpenedStartState, DoorOpenedTime, DoorDurationSec, IsDoorClosedEndState, DoorClosedTime, DoorTransactionCount, DoorComplianceType, SessionID, SessionContext, AnchorPage) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.UserName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserId) + "'" + ", " + (int)data.UserType + ", " + "'" + MainClass.FixStringForSingleQuote(data.DeviceName) + "'" + ", " + (int)data.DeviceType + ", " + (int)data.ProcedureStation + ", " + (int)data.ActivityType + ", " + "'" + MainClass.FixStringForSingleQuote(data.AreaName) + "'" + ", " + MainClass.DateTimeToTimestamp(data.LoginTime) + ", " + MainClass.DateTimeToTimestamp(data.LogoutTime) + ", " + (int)data.DiscrepancyCount + ", " + (int)data.OutsideExpectedUsage + ", " + (int)data.DisruptSec + ", " + (int)data.DoorNumber + ", " + (int)data.IsDoorOpenedStartState + ", " + MainClass.DateTimeToTimestamp(data.DoorOpenedTime) + ", " + (int)data.DoorDurationSec + ", " + (int)data.IsDoorClosedEndState + ", " + MainClass.DateTimeToTimestamp(data.DoorClosedTime) + ", " + (int)data.DoorTransactionCount + ", " + (int)data.DoorComplianceType + ", " + "'" + MainClass.FixStringForSingleQuote(data.SessionID) + "'" + ", " + (int)data.SessionContext + ", " + (int)data.AnchorPage + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserComplience", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int EventIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE USER_COMPLIANCE WHERE EventIid='{0}'", 
				(int)EventIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserComplience", "DeleteRecord");
			return Retval;
		}


	}
}
