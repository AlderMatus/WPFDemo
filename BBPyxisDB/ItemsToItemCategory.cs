using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ItemsToItemCategory
	{
		const string TableName = "ITEMS_TO_ITEM_CATEGORY";

		// collection of record fields
		public class TableData
		{
			public int ItemCatIid;
			public int ItemIid;

			public TableData(int ItemCatIid, int ItemIid)
			{
				this.ItemCatIid = ItemCatIid;
				this.ItemIid = ItemIid;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int ItemCatIid, int ItemIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ITEMS_TO_ITEM_CATEGORY WHERE ItemCatIid='{0}' AND ItemIid='{1}'", 
				(int)ItemCatIid, (int)ItemIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ItemsToItemCategory", "GetRecord", out _conn, out myDataReader))
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
				, MainClass.ToInt(TableName, myDataReader["ItemIid"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO ITEMS_TO_ITEM_CATEGORY (ItemCatIid, ItemIid) VALUES ("
				+ (int)data.ItemCatIid + ", " + (int)data.ItemIid + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemsToItemCategory", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int ItemCatIid, int ItemIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ITEMS_TO_ITEM_CATEGORY WHERE ItemCatIid='{0}' AND ItemIid='{1}'", 
				(int)ItemCatIid, (int)ItemIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemsToItemCategory", "DeleteRecord");
			return Retval;
		}


	}
}
