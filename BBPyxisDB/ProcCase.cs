using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ProcCase
	{
		const string TableName = "PROC_CASE";

		// collection of record fields
		public class TableData
		{
			public int CaseIid;
			public string CaseId;
			public string CaseName;
			public string CaseCart;
			public string CaseCreatorId;
			public string CaseCreatorName;
			public DateTime CaseCreationTime;
			public DateTime LastCaseActTime;
			public string SourceDevName;
            public int CaseType;
            public int CaseStatus;

            public TableData(int CaseIid, string CaseId, string CaseName, string CaseCart, string CaseCreatorId, string CaseCreatorName, DateTime CaseCreationTime, DateTime LastCaseActTime, string SourceDevName, int caseType, int caseStatus)
			{
				this.CaseIid = CaseIid;
				this.CaseId = CaseId;
				this.CaseName = CaseName;
				this.CaseCart = CaseCart;
				this.CaseCreatorId = CaseCreatorId;
				this.CaseCreatorName = CaseCreatorName;
				this.CaseCreationTime = CaseCreationTime;
				this.LastCaseActTime = LastCaseActTime;
				this.SourceDevName = SourceDevName;
                this.CaseType = caseType;
                this.CaseStatus = caseStatus;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int CaseIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PROC_CASE WHERE CaseIid='{0}'", 
				(int)CaseIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ProcCase", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["CaseIid"])
				, myDataReader["CaseId"].ToString()
				, myDataReader["CaseName"].ToString()
				, myDataReader["CaseCart"].ToString()
				, myDataReader["CaseCreatorId"].ToString()
				, myDataReader["CaseCreatorName"].ToString()
				, MainClass.ToDate(TableName, myDataReader["CaseCreationTime"])
				, MainClass.ToDate(TableName, myDataReader["LastCaseActTime"])
				, myDataReader["SourceDevName"].ToString()
                , MainClass.ToInt(TableName, myDataReader["caseType"])
                , MainClass.ToInt(TableName, myDataReader["caseStatus"])
                );
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

            string SqlStatement = "INSERT INTO PROC_CASE (CaseId, CaseName, CaseCart, CaseCreatorId, CaseCreatorName, CaseCreationTime, LastCaseActTime, SourceDevName, caseType, caseStatus) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.CaseId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseCart) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseCreatorId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseCreatorName) + "'" + ", " + MainClass.DateTimeToTimestamp(data.CaseCreationTime) + ", " + MainClass.DateTimeToTimestamp(data.LastCaseActTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.SourceDevName) + "'"
                + ", " + (int)data.CaseType
                + ", " + (int)data.CaseStatus
                + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcCase", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int CaseIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PROC_CASE WHERE CaseIid='{0}'", 
				(int)CaseIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcCase", "DeleteRecord");
			return Retval;
		}


	}
}
