using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class CasesTransferred
	{
		const string TableName = "CASES_TRANSFERRED";

		// collection of record fields
		public class TableData
		{
			public int CaseTransIid;
			public int PatientIid;
			public int TempPatient;
			public string CaseId;
			public string CaseName;
			public string CaseCart;
			public string CaseCreatorId;
			public string CaseCreatorName;
			public DateTime CaseCreationTime;
			public DateTime LastCaseActTime;
			public DateTime ProcedureTime;
			public string CaseSourceDevName;
			public string CaseTransDevName;
			public DateTime CaseTransDateTime;

            // for c2c
            public string NewCaseID;
            public string OldProcedureCode;
            public string NewProcedureCode;
            public string PhysicianName;
            public string ServiceName;
            public int OldCaseType;
            public int NewCaseType;
            public string NewProcedureName;

            // for 9.2 Patch
            public string OldProcedureName;
            public string PatientLastName;
            public string PatientAltID1;
            public string PatientAltID2;
            public int PatientType;
            public string PatientNurseUnitName;
            public string PatientRoom;
            public string PatientBed;
            public string PatientComment1;
            public string PatientComment2;
            public string PatientAdmitDrName;
            public string PatientAttendDrName;
            public int PatientEADTFlag;
            public string PatientEquivalentPatID;


			public TableData(int CaseTransIid, int PatientIid, int TempPatient, string CaseId, string CaseName, string CaseCart, string CaseCreatorId, string CaseCreatorName, DateTime CaseCreationTime, DateTime LastCaseActTime, DateTime ProcedureTime, string CaseSourceDevName, string CaseTransDevName, DateTime CaseTransDateTime,
                string newCaseID, string oldProcedureCode, string newProcedureCode, string physicianName, string serviceName, int oldCaseType, int newCaseType, string newProcedureName,
                string oldProcedureName, string patientLastName, string patientAltID1, string patientAltID2, int patientType, string patientNurseUnitName,
                string patientRoom, string patientBed, string patientComment1, string patientComment2, string patientAdmitDrName, string patientAttendDrName,
                int patientEADTFlag, string patientEquivalentPatID)
			{
				this.CaseTransIid = CaseTransIid;
				this.PatientIid = PatientIid;
				this.TempPatient = TempPatient;
				this.CaseId = CaseId;
				this.CaseName = CaseName;
				this.CaseCart = CaseCart;
				this.CaseCreatorId = CaseCreatorId;
				this.CaseCreatorName = CaseCreatorName;
				this.CaseCreationTime = CaseCreationTime;
				this.LastCaseActTime = LastCaseActTime;
				this.ProcedureTime = ProcedureTime;
				this.CaseSourceDevName = CaseSourceDevName;
				this.CaseTransDevName = CaseTransDevName;
				this.CaseTransDateTime = CaseTransDateTime;

                this.NewCaseID = newCaseID;
                this.OldProcedureCode = oldProcedureCode;
                this.NewProcedureCode = newProcedureCode;
                this.PhysicianName = physicianName;
                this.ServiceName = serviceName;
                this.OldCaseType = oldCaseType;
                this.NewCaseType = newCaseType;
                this.NewProcedureName = newProcedureName;

                this.OldProcedureName = oldProcedureName;
                this.PatientLastName = patientLastName;
                this.PatientAltID1 = patientAltID1;
                this.PatientAltID2 = patientAltID2;
                this.PatientType = patientType;
                this.PatientNurseUnitName = patientNurseUnitName;
                this.PatientRoom = patientRoom;
                this.PatientBed = patientBed;
                this.PatientComment1 = patientComment1;
                this.PatientComment2 = patientComment2;
                this.PatientAdmitDrName = patientAdmitDrName;
                this.PatientAttendDrName = patientAttendDrName;
                this.PatientEADTFlag = patientEADTFlag;
                this.PatientEquivalentPatID = patientEquivalentPatID;
            }
		}

		// return record given its primary key
		public static bool GetRecord(int CaseTransIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from CASES_TRANSFERRED WHERE CaseTransIid='{0}'", 
				(int)CaseTransIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "CasesTransferred", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["CaseTransIid"])
				, MainClass.ToInt(TableName, myDataReader["PatientIid"])
				, MainClass.ToInt(TableName, myDataReader["TempPatient"])
				, myDataReader["CaseId"].ToString()
				, myDataReader["CaseName"].ToString()
				, myDataReader["CaseCart"].ToString()
				, myDataReader["CaseCreatorId"].ToString()
				, myDataReader["CaseCreatorName"].ToString()
				, MainClass.ToDate(TableName, myDataReader["CaseCreationTime"])
				, MainClass.ToDate(TableName, myDataReader["LastCaseActTime"])
				, MainClass.ToDate(TableName, myDataReader["ProcedureTime"])
				, myDataReader["CaseSourceDevName"].ToString()
				, myDataReader["CaseTransDevName"].ToString()
				, MainClass.ToDate(TableName, myDataReader["CaseTransDateTime"])
                , myDataReader["newCaseID"].ToString()
                , myDataReader["oldProcedureCode"].ToString()
                , myDataReader["newProcedureCode"].ToString()
                , myDataReader["physicianName"].ToString()
                , myDataReader["serviceName"].ToString()
                , MainClass.ToInt(TableName, myDataReader["oldCaseType"])
                , MainClass.ToInt(TableName, myDataReader["newCaseType"])
                , myDataReader["newProcedureName"].ToString()
                , myDataReader["oldProcedureName"].ToString()
                , myDataReader["patientLastName"].ToString()
                , myDataReader["patientAltID1"].ToString()
                , myDataReader["patientAltID2"].ToString()
                , MainClass.ToInt(TableName, myDataReader["patientType"])
                , myDataReader["patientNurseUnitName"].ToString()
                , myDataReader["patientRoom"].ToString()
                , myDataReader["patientBed"].ToString()
                , myDataReader["patientComment1"].ToString()
                , myDataReader["patientComment2"].ToString()
                , myDataReader["patientAdmitDrName"].ToString()
                , myDataReader["patientAttendDrName"].ToString()
                , MainClass.ToInt(TableName, myDataReader["patientEADTFlag"])
                , myDataReader["patientEquivalentPatID"].ToString()
                );
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

            string SqlStatement = "INSERT INTO CASES_TRANSFERRED (PatientIid, TempPatient, CaseId, CaseName, CaseCart, CaseCreatorId, CaseCreatorName, CaseCreationTime, LastCaseActTime, ProcedureTime, CaseSourceDevName, CaseTransDevName, CaseTransDateTime, newCaseID, oldProcedureCode, newProcedureCode, physicianName, serviceName, oldCaseType, newCaseType, newProcedureName, OldProcedureName, PatientLastName, PatientAltID1, PatientAltID2, PatientType, PatientNurseUnitName, PatientRoom, PatientBed, PatientComment1, PatientComment2, PatientAdmitDrName, PatientAttendDrName, PatientEADTFlag, PatientEquivalentPatID) VALUES ("
				+ (int)data.PatientIid + ", " + (int)data.TempPatient + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseCart) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseCreatorId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseCreatorName) + "'" + ", " + MainClass.DateTimeToTimestamp(data.CaseCreationTime) + ", " + MainClass.DateTimeToTimestamp(data.LastCaseActTime) + ", " + MainClass.DateTimeToTimestamp(data.ProcedureTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseSourceDevName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseTransDevName) + "'" + ", " + MainClass.DateTimeToTimestamp(data.CaseTransDateTime)
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.NewCaseID) + "'"
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.OldProcedureCode) + "'"
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.NewProcedureCode) + "'"
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.PhysicianName) + "'"
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.ServiceName) + "'"
                + ", " + (int)data.OldCaseType
                + ", " + (int)data.NewCaseType
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.NewProcedureName) + "'"
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.OldProcedureName) + "'"
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.PatientLastName) + "'"
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.PatientAltID1) + "'"
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.PatientAltID2) + "'"
                + ", " + (int)data.PatientType
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.PatientNurseUnitName) + "'"
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.PatientRoom) + "'"
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.PatientBed) + "'"
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.PatientComment1) + "'"
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.PatientComment2) + "'"
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.PatientAdmitDrName) + "'"
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.PatientAttendDrName) + "'"
                + ", " + (int)data.PatientEADTFlag
                + ", " + "'" + MainClass.FixStringForSingleQuote(data.PatientEquivalentPatID) + "'"
                + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "CasesTransferred", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int CaseTransIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE CASES_TRANSFERRED WHERE CaseTransIid='{0}'", 
				(int)CaseTransIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "CasesTransferred", "DeleteRecord");
			return Retval;
		}


	}
}
