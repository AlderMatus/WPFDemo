using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ProcedureEventTimes
	{
		const string TableName = "PROCEDURE_EVENT_TIMES";

		// collection of record fields
		public class TableData
		{
			public int EventTimesIid;
			public string EventName;
			public DateTime EventTime;
			public string UserName;
			public string UserId;
			public string PtLastName;
			public string PtFirstName;
			public string PtMiddleName;
			public string PtId;
			public string OrNumber;
			public string ProcedureName;
			public string ProcedureCode;
			public string ServiceName;
			public string PhysicianName;
			public string DeviceName;
			public string CaseId;

			public TableData(int EventTimesIid, string EventName, DateTime EventTime, string UserName, string UserId, string PtLastName, string PtFirstName, string PtMiddleName, string PtId, string OrNumber, string ProcedureName, string ProcedureCode, string ServiceName, string PhysicianName, string DeviceName, string CaseId)
			{
				this.EventTimesIid = EventTimesIid;
				this.EventName = EventName;
				this.EventTime = EventTime;
				this.UserName = UserName;
				this.UserId = UserId;
				this.PtLastName = PtLastName;
				this.PtFirstName = PtFirstName;
				this.PtMiddleName = PtMiddleName;
				this.PtId = PtId;
				this.OrNumber = OrNumber;
				this.ProcedureName = ProcedureName;
				this.ProcedureCode = ProcedureCode;
				this.ServiceName = ServiceName;
				this.PhysicianName = PhysicianName;
				this.DeviceName = DeviceName;
				this.CaseId = CaseId;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int EventTimesIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PROCEDURE_EVENT_TIMES WHERE EventTimesIid='{0}'", 
				(int)EventTimesIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ProcedureEventTimes", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["EventTimesIid"])
				, myDataReader["EventName"].ToString()
				, MainClass.ToDate(TableName, myDataReader["EventTime"])
				, myDataReader["UserName"].ToString()
				, myDataReader["UserId"].ToString()
				, myDataReader["PtLastName"].ToString()
				, myDataReader["PtFirstName"].ToString()
				, myDataReader["PtMiddleName"].ToString()
				, myDataReader["PtId"].ToString()
				, myDataReader["OrNumber"].ToString()
				, myDataReader["ProcedureName"].ToString()
				, myDataReader["ProcedureCode"].ToString()
				, myDataReader["ServiceName"].ToString()
				, myDataReader["PhysicianName"].ToString()
				, myDataReader["DeviceName"].ToString()
				, myDataReader["CaseId"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO PROCEDURE_EVENT_TIMES (EventName, EventTime, UserName, UserId, PtLastName, PtFirstName, PtMiddleName, PtId, OrNumber, ProcedureName, ProcedureCode, ServiceName, PhysicianName, DeviceName, CaseId) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.EventName) + "'" + ", " + MainClass.DateTimeToTimestamp(data.EventTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtLastName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtFirstName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtMiddleName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.OrNumber) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ProcedureName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ProcedureCode) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ServiceName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PhysicianName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.DeviceName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseId) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcedureEventTimes", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int EventTimesIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PROCEDURE_EVENT_TIMES WHERE EventTimesIid='{0}'", 
				(int)EventTimesIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcedureEventTimes", "DeleteRecord");
			return Retval;
		}


	}
}
