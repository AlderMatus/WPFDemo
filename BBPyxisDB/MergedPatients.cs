using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class MergedPatients
	{
		const string TableName = "MERGED_PATIENTS";

		// collection of record fields
		public class TableData
		{
			public int MpIid;
			public DateTime MergeDateTime;
			public string PermPtLastName;
			public string PermPtFirstName;
			public string PermPtMiddleName;
			public string PermPtId;
			public string PermPtAltId1;
			public string PermPtAltId2;
			public string PermRoom;
			public string PermBed;
			public DateTime PermFirstPtActivityTime;
			public DateTime PermAdmitTime;
			public string PermSourceDevName;
			public int PermNurseUnitIid;
			public string TempPtLastName;
			public string TempPtFirstName;
			public string TempPtMiddleName;
			public string TempPtId;
			public string TempPtAltId1;
			public string TempPtAltId2;
			public string TempRoom;
			public string TempBed;
			public DateTime TempFirstPtActivityTime;
			public DateTime TempLastPtActivityTime;
			public DateTime TempAdmitTime;
			public string TempSourceDevName;
			public int TempNurseUnitIid;
			public string MergeUserId;
			public string MergeUserName;
			public string FacilityCode;

			public TableData(int MpIid, DateTime MergeDateTime, string PermPtLastName, string PermPtFirstName, string PermPtMiddleName, string PermPtId, string PermPtAltId1, string PermPtAltId2, string PermRoom, string PermBed, DateTime PermFirstPtActivityTime, DateTime PermAdmitTime, string PermSourceDevName, int PermNurseUnitIid, string TempPtLastName, string TempPtFirstName, string TempPtMiddleName, string TempPtId, string TempPtAltId1, string TempPtAltId2, string TempRoom, string TempBed, DateTime TempFirstPtActivityTime, DateTime TempLastPtActivityTime, DateTime TempAdmitTime, string TempSourceDevName, int TempNurseUnitIid, string MergeUserId, string MergeUserName, string FacilityCode)
			{
				this.MpIid = MpIid;
				this.MergeDateTime = MergeDateTime;
				this.PermPtLastName = PermPtLastName;
				this.PermPtFirstName = PermPtFirstName;
				this.PermPtMiddleName = PermPtMiddleName;
				this.PermPtId = PermPtId;
				this.PermPtAltId1 = PermPtAltId1;
				this.PermPtAltId2 = PermPtAltId2;
				this.PermRoom = PermRoom;
				this.PermBed = PermBed;
				this.PermFirstPtActivityTime = PermFirstPtActivityTime;
				this.PermAdmitTime = PermAdmitTime;
				this.PermSourceDevName = PermSourceDevName;
				this.PermNurseUnitIid = PermNurseUnitIid;
				this.TempPtLastName = TempPtLastName;
				this.TempPtFirstName = TempPtFirstName;
				this.TempPtMiddleName = TempPtMiddleName;
				this.TempPtId = TempPtId;
				this.TempPtAltId1 = TempPtAltId1;
				this.TempPtAltId2 = TempPtAltId2;
				this.TempRoom = TempRoom;
				this.TempBed = TempBed;
				this.TempFirstPtActivityTime = TempFirstPtActivityTime;
				this.TempLastPtActivityTime = TempLastPtActivityTime;
				this.TempAdmitTime = TempAdmitTime;
				this.TempSourceDevName = TempSourceDevName;
				this.TempNurseUnitIid = TempNurseUnitIid;
				this.MergeUserId = MergeUserId;
				this.MergeUserName = MergeUserName;
				this.FacilityCode = FacilityCode;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int MpIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from MERGED_PATIENTS WHERE MpIid='{0}'", 
				(int)MpIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "MergedPatients", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["MpIid"])
				, MainClass.ToDate(TableName, myDataReader["MergeDateTime"])
				, myDataReader["PermPtLastName"].ToString()
				, myDataReader["PermPtFirstName"].ToString()
				, myDataReader["PermPtMiddleName"].ToString()
				, myDataReader["PermPtId"].ToString()
				, myDataReader["PermPtAltId1"].ToString()
				, myDataReader["PermPtAltId2"].ToString()
				, myDataReader["PermRoom"].ToString()
				, myDataReader["PermBed"].ToString()
				, MainClass.ToDate(TableName, myDataReader["PermFirstPtActivityTime"])
				, MainClass.ToDate(TableName, myDataReader["PermAdmitTime"])
				, myDataReader["PermSourceDevName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PermNurseUnitIid"])
				, myDataReader["TempPtLastName"].ToString()
				, myDataReader["TempPtFirstName"].ToString()
				, myDataReader["TempPtMiddleName"].ToString()
				, myDataReader["TempPtId"].ToString()
				, myDataReader["TempPtAltId1"].ToString()
				, myDataReader["TempPtAltId2"].ToString()
				, myDataReader["TempRoom"].ToString()
				, myDataReader["TempBed"].ToString()
				, MainClass.ToDate(TableName, myDataReader["TempFirstPtActivityTime"])
				, MainClass.ToDate(TableName, myDataReader["TempLastPtActivityTime"])
				, MainClass.ToDate(TableName, myDataReader["TempAdmitTime"])
				, myDataReader["TempSourceDevName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["TempNurseUnitIid"])
				, myDataReader["MergeUserId"].ToString()
				, myDataReader["MergeUserName"].ToString()
				, myDataReader["FacilityCode"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO MERGED_PATIENTS (MergeDateTime, PermPtLastName, PermPtFirstName, PermPtMiddleName, PermPtId, PermPtAltId1, PermPtAltId2, PermRoom, PermBed, PermFirstPtActivityTime, PermAdmitTime, PermSourceDevName, PermNurseUnitIid, TempPtLastName, TempPtFirstName, TempPtMiddleName, TempPtId, TempPtAltId1, TempPtAltId2, TempRoom, TempBed, TempFirstPtActivityTime, TempLastPtActivityTime, TempAdmitTime, TempSourceDevName, TempNurseUnitIid, MergeUserId, MergeUserName, FacilityCode) VALUES ("
				+ MainClass.DateTimeToTimestamp(data.MergeDateTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.PermPtLastName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PermPtFirstName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PermPtMiddleName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PermPtId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PermPtAltId1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PermPtAltId2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PermRoom) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PermBed) + "'" + ", " + MainClass.DateTimeToTimestamp(data.PermFirstPtActivityTime) + ", " + MainClass.DateTimeToTimestamp(data.PermAdmitTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.PermSourceDevName) + "'" + ", " + (int)data.PermNurseUnitIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.TempPtLastName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.TempPtFirstName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.TempPtMiddleName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.TempPtId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.TempPtAltId1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.TempPtAltId2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.TempRoom) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.TempBed) + "'" + ", " + MainClass.DateTimeToTimestamp(data.TempFirstPtActivityTime) + ", " + MainClass.DateTimeToTimestamp(data.TempLastPtActivityTime) + ", " + MainClass.DateTimeToTimestamp(data.TempAdmitTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.TempSourceDevName) + "'" + ", " + (int)data.TempNurseUnitIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.MergeUserId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.MergeUserName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.FacilityCode) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "MergedPatients", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int MpIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE MERGED_PATIENTS WHERE MpIid='{0}'", 
				(int)MpIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "MergedPatients", "DeleteRecord");
			return Retval;
		}


	}
}
