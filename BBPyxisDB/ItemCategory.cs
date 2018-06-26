using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ItemCategory
	{
		const string TableName = "ITEM_CATEGORY";

		// collection of record fields
		public class TableData
		{
			public int ItemCatIid;
			public string ItemCatName;

			public TableData(int ItemCatIid, string ItemCatName)
			{
				this.ItemCatIid = ItemCatIid;
				this.ItemCatName = ItemCatName;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int ItemCatIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ITEM_CATEGORY WHERE ItemCatIid='{0}'", 
				(int)ItemCatIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ItemCategory", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["ItemCatIid"])
				, myDataReader["ItemCatName"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO ITEM_CATEGORY (ItemCatName) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.ItemCatName) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemCategory", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int ItemCatIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ITEM_CATEGORY WHERE ItemCatIid='{0}'", 
				(int)ItemCatIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemCategory", "DeleteRecord");
			return Retval;
		}


	}
}
