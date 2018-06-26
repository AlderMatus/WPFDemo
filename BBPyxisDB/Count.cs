using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Count
	{
		const string TableName = "COUNT";

		// collection of record fields
		public class TableData
		{
			public int CountIid;
			public int Count;

			public TableData(int CountIid, int Count)
			{
				this.CountIid = CountIid;
				this.Count = Count;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int CountIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from COUNT WHERE CountIid='{0}'", 
				(int)CountIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Count", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["CountIid"])
				, MainClass.ToInt(TableName, myDataReader["Count"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO COUNT (CountIid, Count) VALUES ("
				+ (int)data.CountIid + ", " + (int)data.Count + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Count", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int CountIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE COUNT WHERE CountIid='{0}'", 
				(int)CountIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Count", "DeleteRecord");
			return Retval;
		}


	}
}
