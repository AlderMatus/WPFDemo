using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ProcMgmtProcedure
	{
		const string TableName = "PROC_MGMT_PROCEDURE";

		// collection of record fields
		public class TableData
		{
			public int ProcedureIid;
			public string ProcedureName;
			public string ProcedureCode;

			public TableData(int ProcedureIid, string ProcedureName, string ProcedureCode)
			{
				this.ProcedureIid = ProcedureIid;
				this.ProcedureName = ProcedureName;
				this.ProcedureCode = ProcedureCode;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int ProcedureIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PROC_MGMT_PROCEDURE WHERE ProcedureIid={0}", 
				(int)ProcedureIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ProcMgmtProcedure", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["ProcedureIid"])
				, myDataReader["ProcedureName"].ToString()
				, myDataReader["ProcedureCode"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO PROC_MGMT_PROCEDURE (ProcedureName, ProcedureCode) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.ProcedureName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ProcedureCode) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcMgmtProcedure", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int ProcedureIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PROC_MGMT_PROCEDURE WHERE ProcedureIid='{0}'", 
				(int)ProcedureIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcMgmtProcedure", "DeleteRecord");
			return Retval;
		}


	}
}
