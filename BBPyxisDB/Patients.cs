using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Patients
	{
		const string TableName = "PATIENTS";

		// collection of record fields
		public class TableData
		{
			public int PtIid;
			public string PtLastName;
			public string PtFirstName;
			public string PtMiddleName;
			public string PtId;
			public string PtMergeId;
			public string PtAltId1;
			public string PtAltId2;
			public string PtComment1;
			public string PtComment2;
			public string Room;
			public string Bed;
			public int PtType;
			public int TempPatient;
			public int EADT;
			public DateTime FirstPtActivityTime;
			public DateTime LastPtActivityTime;
			public DateTime AdmitTime;
			public DateTime PtDischargeDate;
			public string AdmitDrName;
			public string AttendDrName;
			public string PtSex;
			public string PtDOB;
			public string SourceDevName;
			public string CreatorId;
			public string CreatorName;
			public int HtWtEstimated;
			public string PtHeight;
			public string PtHtUnits;
			public string PtWeight;
			public string PtWtUnits;
			public int NUnitIid;
			public string FacilityCode;
			public int Private;
            public int FloorStock;
            public int isGLCode;


			public TableData(int PtIid, string PtLastName, string PtFirstName, string PtMiddleName, string PtId, string PtMergeId, string PtAltId1, string PtAltId2, string PtComment1, string PtComment2, string Room, string Bed, int PtType, int TempPatient, int EADT, DateTime FirstPtActivityTime, DateTime LastPtActivityTime, DateTime AdmitTime, DateTime PtDischargeDate, string AdmitDrName, string AttendDrName, string PtSex, string PtDOB, string SourceDevName, string CreatorId, string CreatorName, int HtWtEstimated, string PtHeight, string PtHtUnits, string PtWeight, string PtWtUnits, int NUnitIid, string FacilityCode, int Private, int FloorStock, int isGLCode)
			{
				this.PtIid = PtIid;
				this.PtLastName = PtLastName;
				this.PtFirstName = PtFirstName;
				this.PtMiddleName = PtMiddleName;
				this.PtId = PtId;
				this.PtMergeId = PtMergeId;
				this.PtAltId1 = PtAltId1;
				this.PtAltId2 = PtAltId2;
				this.PtComment1 = PtComment1;
				this.PtComment2 = PtComment2;
				this.Room = Room;
				this.Bed = Bed;
				this.PtType = PtType;
				this.TempPatient = TempPatient;
				this.EADT = EADT;
				this.FirstPtActivityTime = FirstPtActivityTime;
				this.LastPtActivityTime = LastPtActivityTime;
				this.AdmitTime = AdmitTime;
				this.PtDischargeDate = PtDischargeDate;
				this.AdmitDrName = AdmitDrName;
				this.AttendDrName = AttendDrName;
				this.PtSex = PtSex;
				this.PtDOB = PtDOB;
				this.SourceDevName = SourceDevName;
				this.CreatorId = CreatorId;
				this.CreatorName = CreatorName;
				this.HtWtEstimated = HtWtEstimated;
				this.PtHeight = PtHeight;
				this.PtHtUnits = PtHtUnits;
				this.PtWeight = PtWeight;
				this.PtWtUnits = PtWtUnits;
				this.NUnitIid = NUnitIid;
				this.FacilityCode = FacilityCode;
				this.Private = Private;
                this.FloorStock = FloorStock;
                this.isGLCode = isGLCode;
            }
		}

		// return record given its primary key
		public static bool GetRecord(int PtIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PATIENTS WHERE PtIid='{0}'", 
				(int)PtIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Patients", "GetRecord", out _conn, out myDataReader))
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
				, myDataReader["PtLastName"].ToString()
				, myDataReader["PtFirstName"].ToString()
				, myDataReader["PtMiddleName"].ToString()
				, myDataReader["PtId"].ToString()
				, myDataReader["PtMergeId"].ToString()
				, myDataReader["PtAltId1"].ToString()
				, myDataReader["PtAltId2"].ToString()
				, myDataReader["PtComment1"].ToString()
				, myDataReader["PtComment2"].ToString()
				, myDataReader["Room"].ToString()
				, myDataReader["Bed"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PtType"])
				, MainClass.ToInt(TableName, myDataReader["TempPatient"])
				, MainClass.ToInt(TableName, myDataReader["EADT"])
				, MainClass.ToDate(TableName, myDataReader["FirstPtActivityTime"])
				, MainClass.ToDate(TableName, myDataReader["LastPtActivityTime"])
				, MainClass.ToDate(TableName, myDataReader["AdmitTime"])
				, MainClass.ToDate(TableName, myDataReader["PtDischargeDate"])
				, myDataReader["AdmitDrName"].ToString()
				, myDataReader["AttendDrName"].ToString()
				, myDataReader["PtSex"].ToString()
				, myDataReader["PtDOB"].ToString()
				, myDataReader["SourceDevName"].ToString()
				, myDataReader["CreatorId"].ToString()
				, myDataReader["CreatorName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["HtWtEstimated"])
				, myDataReader["PtHeight"].ToString()
				, myDataReader["PtHtUnits"].ToString()
				, myDataReader["PtWeight"].ToString()
				, myDataReader["PtWtUnits"].ToString()
				, MainClass.ToInt(TableName, myDataReader["NUnitIid"])
				, myDataReader["FacilityCode"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Private"])
                , MainClass.ToInt(TableName, myDataReader["FloorStock"])
                , MainClass.ToInt(TableName, myDataReader["isGLCode"])
                );
        }
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO PATIENTS (PtLastName, PtFirstName, PtMiddleName, PtId, PtMergeId, PtAltId1, PtAltId2, PtComment1, PtComment2, Room, Bed, PtType, TempPatient, EADT, FirstPtActivityTime, LastPtActivityTime, AdmitTime, PtDischargeDate, AdmitDrName, AttendDrName, PtSex, PtDOB, SourceDevName, CreatorId, CreatorName, HtWtEstimated, PtHeight, PtHtUnits, PtWeight, PtWtUnits, NUnitIid, FacilityCode, Private, FloorStock, isGLCode) VALUES ("
                + "'" + MainClass.FixStringForSingleQuote(data.PtLastName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtFirstName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtMiddleName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtMergeId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtAltId1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtAltId2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtComment1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtComment2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Room) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Bed) + "'" + ", " + (int)data.PtType + ", " + (int)data.TempPatient + ", " + (int)data.EADT + ", " + MainClass.DateTimeToTimestamp(data.FirstPtActivityTime) + ", " + MainClass.DateTimeToTimestamp(data.LastPtActivityTime) + ", " + MainClass.DateTimeToTimestamp(data.AdmitTime) + ", " + MainClass.DateTimeToTimestamp(data.PtDischargeDate) + ", " + "'" + MainClass.FixStringForSingleQuote(data.AdmitDrName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.AttendDrName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtSex) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtDOB) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SourceDevName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CreatorId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CreatorName) + "'" + ", " + (int)data.HtWtEstimated + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtHeight) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtHtUnits) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtWeight) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtWtUnits) + "'" + ", " + (int)data.NUnitIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.FacilityCode) + "'" + ", " + (int)data.Private + ", " + (int)data.FloorStock + ", " + (int)data.isGLCode + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Patients", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int PtIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PATIENTS WHERE PtIid='{0}'", 
				(int)PtIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Patients", "DeleteRecord");
			return Retval;
		}

        // delete deleted but not purged GL Code
        public static bool DeleteOldGLCode(string glCode)
        {
            bool Retval = true;
            string SqlStatement = string.Format("DELETE PATIENTS WHERE ptId='{0}' AND isGLCode=1 AND ptDischargeDate<{1}",
                glCode, MainClass.DateTimeToTimestamp(DateTime.Now));
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Patients", "DeleteOldGLCode");
            return Retval;
        }

        // check for duplicate GL code, ignoring the one we are working on
        public static bool IsDuplicatedGLCode(string glCode, int excludedIid)
        {
            bool glCodeExists = false;
            DeleteOldGLCode(glCode);
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from PATIENTS WHERE ptId='" + glCode + "' AND PtIid <> " + excludedIid.ToString();

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PATIENTS", "IsDuplicatedGLCode", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                        glCodeExists = true;
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "IsDuplicatedGLCode", err);
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
            return glCodeExists;
        }
	}
}
