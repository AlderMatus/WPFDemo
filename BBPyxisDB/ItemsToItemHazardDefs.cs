using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ItemsToItemHazardDefs
	{
		const string TableName = "ITEMS_TO_ITEM_HAZARD_DEFS";

		// collection of record fields
		public class TableData
		{
			public int ItemIid;
			public int ItemHazardIid;

			public TableData(int ItemIid, int ItemHazardIid)
			{
				this.ItemIid = ItemIid;
				this.ItemHazardIid = ItemHazardIid;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int Itemiid, int ItemHazardIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ITEMS_TO_ITEM_HAZARD_DEFS WHERE Itemiid='{0}' AND ItemHazardIid='{1}'", 
				(int)Itemiid, (int)ItemHazardIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ItemsToItemHazardDefs", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["ItemIid"])
				, MainClass.ToInt(TableName, myDataReader["ItemHazardIid"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO ITEMS_TO_ITEM_HAZARD_DEFS (ItemIid, ItemHazardIid) VALUES ("
				+ (int)data.ItemIid + ", " + (int)data.ItemHazardIid + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemsToItemHazardDefs", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int Itemiid, int ItemHazardIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ITEMS_TO_ITEM_HAZARD_DEFS WHERE Itemiid='{0}' AND ItemHazardIid='{1}'", 
				(int)Itemiid, (int)ItemHazardIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemsToItemHazardDefs", "DeleteRecord");
			return Retval;
		}


	}
}
