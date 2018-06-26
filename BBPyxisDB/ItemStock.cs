using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ItemStock
	{
		const string TableName = "ITEM_STOCK";

		// collection of record fields
		public class TableData
		{
			public int ItemStock;
			public string ItemStockText;

			public TableData(int ItemStock, string ItemStockText)
			{
				this.ItemStock = ItemStock;
				this.ItemStockText = ItemStockText;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int ItemStock, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ITEM_STOCK WHERE ItemStock='{0}'", 
				(int)ItemStock);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ItemStock", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["ItemStock"])
				, myDataReader["ItemStockText"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO ITEM_STOCK (ItemStock, ItemStockText) VALUES ("
				+ (int)data.ItemStock + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemStockText) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemStock", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int ItemStock)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ITEM_STOCK WHERE ItemStock='{0}'", 
				(int)ItemStock);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemStock", "DeleteRecord");
			return Retval;
		}


	}
}
