using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ItemBillOnly
	{
		const string TableName = "ITEM_BILL_ONLY";

		// collection of record fields
		public class TableData
		{
			public int ItemBillOnly;
			public string ItemBillOnlyText;

			public TableData(int ItemBillOnly, string ItemBillOnlyText)
			{
				this.ItemBillOnly = ItemBillOnly;
				this.ItemBillOnlyText = ItemBillOnlyText;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int ItemBillOnly, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ITEM_BILL_ONLY WHERE ItemBillOnly='{0}'", 
				(int)ItemBillOnly);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ItemBillOnly", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["ItemBillOnly"])
				, myDataReader["ItemBillOnlyText"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO ITEM_BILL_ONLY (ItemBillOnly, ItemBillOnlyText) VALUES ("
				+ (int)data.ItemBillOnly + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemBillOnlyText) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemBillOnly", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int ItemBillOnly)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ITEM_BILL_ONLY WHERE ItemBillOnly='{0}'", 
				(int)ItemBillOnly);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemBillOnly", "DeleteRecord");
			return Retval;
		}


	}
}
