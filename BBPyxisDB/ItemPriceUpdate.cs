using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using iAnywhere.Data.AsaClient;
#endif

namespace BBPyxisDB
{

	public class ItemPriceUpdate
	{
		const string TableName = "ITEM_PRICE_UPDATE";

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

		// return record given its primary key
		public static bool GetRecord(int PriceIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			AsaConnection _conn;
			AsaDataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ITEM_PRICE_UPDATE WHERE PriceIid='{0}'", 
				(int)PriceIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ItemPriceUpdate", "GetRecord", out _conn, out myDataReader))
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

		// make a TableData object from a AsaDataReader record
#if !NO_ASA
		static void MakeDataRec(AsaDataReader myDataReader, out TableData data)
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

			string SqlStatement = "INSERT INTO ITEM_PRICE_UPDATE (ItemID, CostCenter, SellPrice, IsDefault, CDM) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.ItemID) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CostCenter) + "'" + ", " + data.SellPrice + ", " + (int)data.IsDefault + ", " + (int)data.CDM + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemPriceUpdate", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int PriceIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ITEM_PRICE_UPDATE WHERE PriceIid='{0}'", 
				(int)PriceIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemPriceUpdate", "DeleteRecord");
			return Retval;
		}


	}
}
