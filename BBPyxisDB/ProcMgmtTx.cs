using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ProcMgmtTx
	{
		const string TableName = "PROC_MGMT_TX";

		// collection of record fields
		public class TableData
		{
			public int MgmtIid;
			public int PatientIid;
			public int ProcedureIid;
			public int ServiceIid;
			public int PhysicianIid;
			public DateTime TxTime;
			public int CaseIid;
			public DateTime ProcedureTime;
			public string CaseProcId;
			//public string OrderNum;

			public TableData(int MgmtIid, int PatientIid, int ProcedureIid, int ServiceIid, int PhysicianIid, DateTime TxTime, int CaseIid, DateTime ProcedureTime, string CaseProcId /*, string OrderNum */)
			{
				this.MgmtIid = MgmtIid;
				this.PatientIid = PatientIid;
				this.ProcedureIid = ProcedureIid;
				this.ServiceIid = ServiceIid;
				this.PhysicianIid = PhysicianIid;
				this.TxTime = TxTime;
				this.CaseIid = CaseIid;
				this.ProcedureTime = ProcedureTime;
				this.CaseProcId = CaseProcId;
				//this.OrderNum = OrderNum;
			}
		}

		// make a TableData object from a SADataReader record
#if !NO_ASA
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
			data = new TableData(
				MainClass.ToInt(TableName, myDataReader["MgmtIid"])
				, MainClass.ToInt(TableName, myDataReader["PatientIid"])
				, MainClass.ToInt(TableName, myDataReader["ProcedureIid"])
				, MainClass.ToInt(TableName, myDataReader["ServiceIid"])
				, MainClass.ToInt(TableName, myDataReader["PhysicianIid"])
				, MainClass.ToDate(TableName, myDataReader["TxTime"])
				, MainClass.ToInt(TableName, myDataReader["CaseIid"])
				, MainClass.ToDate(TableName, myDataReader["ProcedureTime"])
				, myDataReader["CaseProcId"].ToString() );
				//, myDataReader["OrderNum"].ToString());
		}
#endif

        /// <summary>
        /// Get all records with given case iid
        /// </summary>
        /// <param name="CaseIid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool GetRecords(int CaseIid, out List<TableData> data)
        {
            bool Retval = true;
            data = new List<TableData>();

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from PROC_MGMT_TX WHERE caseIid={0}", CaseIid);
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ProcMgmtTx", "GetFirstRecord", out _conn, out myDataReader))
            {
                try
                {
                    while(myDataReader.Read())
                    {
                        TableData td;
                        MakeDataRec(myDataReader, out td);
                        data.Add(td);
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"), TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "GetFirstRecord", err);
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

        /// <summary>
        /// Get the record with given case iid and procedure iid.
        /// </summary>
        /// <param name="CaseIid"></param>
        /// <param name="ProcedureIid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool GetRecord(int CaseIid, int ProcedureIid, out TableData data)
        {
            bool Retval = true;
            data = null;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from PROC_MGMT_TX WHERE caseIid={0} AND procedureIid={1}", CaseIid, ProcedureIid);
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ProcMgmtTx", "GetFirstRecord", out _conn, out myDataReader))
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

        /// <summary>
        /// Get the first record with given case id
        /// </summary>
        /// <param name="CaseIid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool GetFirstRecord(int CaseIid, out TableData data)
        {
            bool Retval = true;
            data = null;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from PROC_MGMT_TX WHERE caseIid={0}", CaseIid);
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ProcMgmtTx", "GetFirstRecord", out _conn, out myDataReader))
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
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "GetFirstRecord", err);
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

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO PROC_MGMT_TX (PatientIid, ProcedureIid, ServiceIid, PhysicianIid, TxTime, CaseIid, ProcedureTime, CaseProcId) VALUES ("
				+ (int)data.PatientIid + ", " + (int)data.ProcedureIid + ", " + (int)data.ServiceIid + ", " + (int)data.PhysicianIid + ", " + MainClass.DateTimeToTimestamp(data.TxTime) + ", " + (int)data.CaseIid + ", " + MainClass.DateTimeToTimestamp(data.ProcedureTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseProcId) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcMgmtTx", "InsertRecord", out NewIid);
			return Retval;
		}


	}
}
