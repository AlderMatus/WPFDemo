using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Areas
	{
		const string TableName = "AREAS";

		// collection of record fields
		public class TableData
		{
			public int AreaIid;
			public string AreaName;

			public TableData(int AreaIid, string AreaName)
			{
				this.AreaIid = AreaIid;
				this.AreaName = AreaName;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int AreaIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from AREAS WHERE AreaIid='{0}'", 
				(int)AreaIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Areas", "GetRecord", out _conn, out myDataReader))
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
						myDataReader.Close();
					if (_conn != null)
						_conn.Close();
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
				MainClass.ToInt(TableName, myDataReader["AreaIid"])
				, myDataReader["AreaName"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO AREAS (AreaName) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.AreaName) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Areas", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int AreaIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE AREAS WHERE AreaIid='{0}'", 
				(int)AreaIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Areas", "DeleteRecord");
			return Retval;
		}


	}
}
