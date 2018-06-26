using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class PocketPerishable
	{
		const string TableName = "POCKET_PERISHABLE";

		// collection of record fields
		public class TableData
		{
			public int ItemIsPerishable;
			public string ItemIsPerishableText;

			public TableData(int ItemIsPerishable, string ItemIsPerishableText)
			{
				this.ItemIsPerishable = ItemIsPerishable;
				this.ItemIsPerishableText = ItemIsPerishableText;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int ItemIsPerishable, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from POCKET_PERISHABLE WHERE ItemIsPerishable='{0}'", 
				(int)ItemIsPerishable);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PocketPerishable", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["ItemIsPerishable"])
				, myDataReader["ItemIsPerishableText"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO POCKET_PERISHABLE (ItemIsPerishable, ItemIsPerishableText) VALUES ("
				+ (int)data.ItemIsPerishable + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemIsPerishableText) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PocketPerishable", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int ItemIsPerishable)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE POCKET_PERISHABLE WHERE ItemIsPerishable='{0}'", 
				(int)ItemIsPerishable);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PocketPerishable", "DeleteRecord");
			return Retval;
		}


	}
}
