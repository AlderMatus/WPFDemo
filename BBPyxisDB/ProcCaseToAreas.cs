using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ProcCaseToAreas
	{
		const string TableName = "PROC_CASE_TO_AREAS";

		// collection of record fields
		public class TableData
		{
			public int CaseIid;
			public int AreaIid;

			public TableData(int CaseIid, int AreaIid)
			{
				this.CaseIid = CaseIid;
				this.AreaIid = AreaIid;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int CaseIid, int AreaIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PROC_CASE_TO_AREAS WHERE CaseIid='{0}' AND AreaIid='{1}'", 
				(int)CaseIid, (int)AreaIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ProcCaseToAreas", "GetRecord", out _conn, out myDataReader))
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
				, MainClass.ToInt(TableName, myDataReader["AreaIid"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO PROC_CASE_TO_AREAS (CaseIid, AreaIid) VALUES ("
				+ (int)data.CaseIid + ", " + (int)data.AreaIid + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcCaseToAreas", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int CaseIid, int AreaIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PROC_CASE_TO_AREAS WHERE CaseIid='{0}' AND AreaIid='{1}'", 
				(int)CaseIid, (int)AreaIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcCaseToAreas", "DeleteRecord");
			return Retval;
		}


	}
}
