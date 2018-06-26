using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ProcMgmtPhysician
	{
		const string TableName = "PROC_MGMT_PHYSICIAN";

		// collection of record fields
		public class TableData
		{
			public int PhysicianIid;
			public string PhysicianName;

			public TableData(int PhysicianIid, string PhysicianName)
			{
				this.PhysicianIid = PhysicianIid;
				this.PhysicianName = PhysicianName;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int PhysicianIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PROC_MGMT_PHYSICIAN WHERE PhysicianIid='{0}'", 
				(int)PhysicianIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ProcMgmtPhysician", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["PhysicianIid"])
				, myDataReader["PhysicianName"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO PROC_MGMT_PHYSICIAN (PhysicianName) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.PhysicianName) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcMgmtPhysician", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int PhysicianIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PROC_MGMT_PHYSICIAN WHERE PhysicianIid='{0}'", 
				(int)PhysicianIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcMgmtPhysician", "DeleteRecord");
			return Retval;
		}


	}
}
