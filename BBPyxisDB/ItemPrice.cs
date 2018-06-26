using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ItemPrice
	{
		const string TableName = "ITEM_PRICE";

		// collection of record fields
		public class TableData
		{
			public int PriceIid;
			public string ItemID;
			public string CostCenter;
			public decimal SellPrice;
			public int IsDefault;
			public int CDM;

			public TableData(int PriceIid, string ItemID, string CostCenter, decimal SellPrice, int IsDefault, int CDM)
			{
				this.PriceIid = PriceIid;
				this.ItemID = ItemID;
				this.CostCenter = CostCenter;
				this.SellPrice = SellPrice;
				this.IsDefault = IsDefault;
				this.CDM = CDM;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(string ItemID, string CostCenter, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ITEM_PRICE WHERE ItemID='{0}' AND CostCenter='{1}'", 
				MainClass.FixStringForSingleQuote(ItemID), MainClass.FixStringForSingleQuote(CostCenter));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ItemPrice", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["PriceIid"])
				, myDataReader["ItemID"].ToString()
				, myDataReader["CostCenter"].ToString()
				, MainClass.ToDecimal(TableName, myDataReader["SellPrice"])
				, MainClass.ToInt(TableName, myDataReader["IsDefault"])
				, MainClass.ToInt(TableName, myDataReader["CDM"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO ITEM_PRICE (ItemID, CostCenter, SellPrice, IsDefault, CDM) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.ItemID) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CostCenter) + "'" + ", " + data.SellPrice + ", " + (int)data.IsDefault + ", " + (int)data.CDM + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemPrice", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(string ItemID, string CostCenter)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ITEM_PRICE WHERE ItemID='{0}' AND CostCenter='{1}'", 
				MainClass.FixStringForSingleQuote(ItemID), MainClass.FixStringForSingleQuote(CostCenter));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemPrice", "DeleteRecord");
			return Retval;
		}


	}
}
