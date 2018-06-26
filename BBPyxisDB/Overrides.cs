using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Overrides
	{
		const string TableName = "OVERRIDES";

		// collection of record fields
		public class TableData
		{
			public int OverIid;
			public string OverID;
			public string Reason;
			public int Type;

			public TableData(int OverIid, string OverID, string Reason, int Type)
			{
				this.OverIid = OverIid;
				this.OverID = OverID;
				this.Reason = Reason;
				this.Type = Type;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int OverIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from OVERRIDES WHERE OverIid='{0}'", 
				(int)OverIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Overrides", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["OverIid"])
				, myDataReader["OverID"].ToString()
				, myDataReader["Reason"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Type"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO OVERRIDES (OverID, Reason, Type) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.OverID) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Reason) + "'" + ", " + (int)data.Type + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Overrides", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int OverIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE OVERRIDES WHERE OverIid='{0}'", 
				(int)OverIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Overrides", "DeleteRecord");
			return Retval;
		}


	}
}
