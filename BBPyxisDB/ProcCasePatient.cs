using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ProcCasePatient
	{
		const string TableName = "PROC_CASE_PATIENT";

		// collection of record fields
		public class TableData
		{
			public int CaseIid;
			public int ProcedureIid;
			public int PtIid;
			public int PhysicianIid;
			public int ServiceIid;
			public string OrderNum;
			public DateTime ProcedureTime;
			public string CaseProcId;

			public TableData(int CaseIid, int ProcedureIid, int PtIid, int PhysicianIid, int ServiceIid, string OrderNum, DateTime ProcedureTime, string CaseProcId)
			{
				this.CaseIid = CaseIid;
				this.ProcedureIid = ProcedureIid;
				this.PtIid = PtIid;
				this.PhysicianIid = PhysicianIid;
				this.ServiceIid = ServiceIid;
				this.OrderNum = OrderNum;
				this.ProcedureTime = ProcedureTime;
				this.CaseProcId = CaseProcId;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int CaseIid, int ProcedureIid, int PtIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PROC_CASE_PATIENT WHERE CaseIid='{0}' AND ProcedureIid='{1}' AND PtIid='{2}'", 
				(int)CaseIid, (int)ProcedureIid, (int)PtIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ProcCasePatient", "GetRecord", out _conn, out myDataReader))
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
				, MainClass.ToInt(TableName, myDataReader["ProcedureIid"])
				, MainClass.ToInt(TableName, myDataReader["PtIid"])
				, MainClass.ToInt(TableName, myDataReader["PhysicianIid"])
				, MainClass.ToInt(TableName, myDataReader["ServiceIid"])
				, myDataReader["OrderNum"].ToString()
				, MainClass.ToDate(TableName, myDataReader["ProcedureTime"])
				, myDataReader["CaseProcId"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO PROC_CASE_PATIENT (CaseIid, ProcedureIid, PtIid, PhysicianIid, ServiceIid, OrderNum, ProcedureTime, CaseProcId) VALUES ("
				+ (int)data.CaseIid + ", " + (int)data.ProcedureIid + ", " + (int)data.PtIid + ", " + (int)data.PhysicianIid + ", " + (int)data.ServiceIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.OrderNum) + "'" + ", " + MainClass.DateTimeToTimestamp(data.ProcedureTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseProcId) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcCasePatient", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int CaseIid, int ProcedureIid, int PtIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PROC_CASE_PATIENT WHERE CaseIid='{0}' AND ProcedureIid='{1}' AND PtIid='{2}'", 
				(int)CaseIid, (int)ProcedureIid, (int)PtIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcCasePatient", "DeleteRecord");
			return Retval;
		}


	}
}
