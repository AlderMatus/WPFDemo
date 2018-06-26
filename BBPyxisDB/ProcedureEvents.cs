using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ProcedureEvents
	{
		const string TableName = "PROCEDURE_EVENTS";

		// collection of record fields
		public class TableData
		{
			public int EventIid;
			public string EventName;

			public TableData(int EventIid, string EventName)
			{
				this.EventIid = EventIid;
				this.EventName = EventName;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int EventIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PROCEDURE_EVENTS WHERE EventIid='{0}'", 
				(int)EventIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ProcedureEvents", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["EventIid"])
				, myDataReader["EventName"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO PROCEDURE_EVENTS (EventName) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.EventName) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcedureEvents", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int EventIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PROCEDURE_EVENTS WHERE EventIid='{0}'", 
				(int)EventIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ProcedureEvents", "DeleteRecord");
			return Retval;
		}


	}
}
