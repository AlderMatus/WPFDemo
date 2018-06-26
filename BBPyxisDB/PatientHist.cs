using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class PatientHist
	{
		const string TableName = "PATIENT_HIST";

		// collection of record fields
		public class TableData
		{
			public int PtIid;
			public DateTime EffectTime;
			public int PtHistIid;
			public int Action;
			public string PtLastName;
			public string PtFirstName;
			public string PtMiddleName;
			public string PtId;
			public string PtMergeId;
			public string PtAltId1;
			public string PtAltId2;
			public string Room;
			public string Bed;
			public int PtType;
			public int TempPatient;
			public DateTime FirstPtActivityTime;
			public DateTime LastPtActivityTime;
			public DateTime AdmitTime;
			public DateTime PtAutoDischargeDate;
			public DateTime PtDischargeDate;
			public string AdmitDrName;
			public string AttendDrName;
			public string SourceDev;
			public string CreatorId;
			public string CreatorName;
			public string NUnitName;
			public string FacilityCode;
			public int Private;
			public int HasBeenArchived;

			public TableData(int PtIid, DateTime EffectTime, int PtHistIid, int Action, string PtLastName, string PtFirstName, string PtMiddleName, string PtId, string PtMergeId, string PtAltId1, string PtAltId2, string Room, string Bed, int PtType, int TempPatient, DateTime FirstPtActivityTime, DateTime LastPtActivityTime, DateTime AdmitTime, DateTime PtAutoDischargeDate, DateTime PtDischargeDate, string AdmitDrName, string AttendDrName, string SourceDev, string CreatorId, string CreatorName, string NUnitName, string FacilityCode, int Private, int HasBeenArchived)
			{
				this.PtIid = PtIid;
				this.EffectTime = EffectTime;
				this.PtHistIid = PtHistIid;
				this.Action = Action;
				this.PtLastName = PtLastName;
				this.PtFirstName = PtFirstName;
				this.PtMiddleName = PtMiddleName;
				this.PtId = PtId;
				this.PtMergeId = PtMergeId;
				this.PtAltId1 = PtAltId1;
				this.PtAltId2 = PtAltId2;
				this.Room = Room;
				this.Bed = Bed;
				this.PtType = PtType;
				this.TempPatient = TempPatient;
				this.FirstPtActivityTime = FirstPtActivityTime;
				this.LastPtActivityTime = LastPtActivityTime;
				this.AdmitTime = AdmitTime;
				this.PtAutoDischargeDate = PtAutoDischargeDate;
				this.PtDischargeDate = PtDischargeDate;
				this.AdmitDrName = AdmitDrName;
				this.AttendDrName = AttendDrName;
				this.SourceDev = SourceDev;
				this.CreatorId = CreatorId;
				this.CreatorName = CreatorName;
				this.NUnitName = NUnitName;
				this.FacilityCode = FacilityCode;
				this.Private = Private;
				this.HasBeenArchived = HasBeenArchived;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int PtIid, DateTime EffectTime, int PtHistIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PATIENT_HIST WHERE PtIid='{0}' AND EffectTime='{1}' AND PtHistIid='{2}'", 
				(int)PtIid, MainClass.DateTimeToTimestamp(EffectTime), (int)PtHistIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PatientHist", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["PtIid"])
				, MainClass.ToDate(TableName, myDataReader["EffectTime"])
				, MainClass.ToInt(TableName, myDataReader["PtHistIid"])
				, MainClass.ToInt(TableName, myDataReader["Action"])
				, myDataReader["PtLastName"].ToString()
				, myDataReader["PtFirstName"].ToString()
				, myDataReader["PtMiddleName"].ToString()
				, myDataReader["PtId"].ToString()
				, myDataReader["PtMergeId"].ToString()
				, myDataReader["PtAltId1"].ToString()
				, myDataReader["PtAltId2"].ToString()
				, myDataReader["Room"].ToString()
				, myDataReader["Bed"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PtType"])
				, MainClass.ToInt(TableName, myDataReader["TempPatient"])
				, MainClass.ToDate(TableName, myDataReader["FirstPtActivityTime"])
				, MainClass.ToDate(TableName, myDataReader["LastPtActivityTime"])
				, MainClass.ToDate(TableName, myDataReader["AdmitTime"])
				, MainClass.ToDate(TableName, myDataReader["PtAutoDischargeDate"])
				, MainClass.ToDate(TableName, myDataReader["PtDischargeDate"])
				, myDataReader["AdmitDrName"].ToString()
				, myDataReader["AttendDrName"].ToString()
				, myDataReader["SourceDev"].ToString()
				, myDataReader["CreatorId"].ToString()
				, myDataReader["CreatorName"].ToString()
				, myDataReader["NUnitName"].ToString()
				, myDataReader["FacilityCode"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Private"])
				, MainClass.ToInt(TableName, myDataReader["HasBeenArchived"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO PATIENT_HIST (PtIid, EffectTime, Action, PtLastName, PtFirstName, PtMiddleName, PtId, PtMergeId, PtAltId1, PtAltId2, Room, Bed, PtType, TempPatient, FirstPtActivityTime, LastPtActivityTime, AdmitTime, PtAutoDischargeDate, PtDischargeDate, AdmitDrName, AttendDrName, SourceDev, CreatorId, CreatorName, NUnitName, FacilityCode, Private, HasBeenArchived) VALUES ("
				+ (int)data.PtIid + ", " + MainClass.DateTimeToTimestamp(data.EffectTime) + ", " + (int)data.Action + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtLastName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtFirstName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtMiddleName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtMergeId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtAltId1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtAltId2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Room) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Bed) + "'" + ", " + (int)data.PtType + ", " + (int)data.TempPatient + ", " + MainClass.DateTimeToTimestamp(data.FirstPtActivityTime) + ", " + MainClass.DateTimeToTimestamp(data.LastPtActivityTime) + ", " + MainClass.DateTimeToTimestamp(data.AdmitTime) + ", " + MainClass.DateTimeToTimestamp(data.PtAutoDischargeDate) + ", " + MainClass.DateTimeToTimestamp(data.PtDischargeDate) + ", " + "'" + MainClass.FixStringForSingleQuote(data.AdmitDrName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.AttendDrName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SourceDev) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CreatorId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CreatorName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.NUnitName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.FacilityCode) + "'" + ", " + (int)data.Private + ", " + (int)data.HasBeenArchived + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PatientHist", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int PtIid, DateTime EffectTime, int PtHistIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PATIENT_HIST WHERE PtIid='{0}' AND EffectTime='{1}' AND PtHistIid='{2}'", 
				(int)PtIid, MainClass.DateTimeToTimestamp(EffectTime), (int)PtHistIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PatientHist", "DeleteRecord");
			return Retval;
		}


	}
}
