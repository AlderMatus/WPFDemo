using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ProcMgmtService
	{
		const string TableName = "PROC_MGMT_SERVICE";

		// collection of record fields
		public class TableData
		{
			public int ServiceIid;
			public string ServiceName;

			public TableData(int ServiceIid, string ServiceName)
			{
				this.ServiceIid = ServiceIid;
				this.ServiceName = ServiceName;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int ServiceIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PROC_MGMT_SERVICE WHERE ServiceIid='{0}'", 
				(int)ServiceIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ProcMgmtService", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["ServiceIid"])
				, myDataReader["ServiceName"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO PROC_MGMT_SERVICE (ServiceName) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.ServiceName) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcMgmtService", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int ServiceIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PROC_MGMT_SERVICE WHERE ServiceIid='{0}'", 
				(int)ServiceIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcMgmtService", "DeleteRecord");
			return Retval;
		}


	}
}
