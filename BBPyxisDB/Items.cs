using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Items
	{
		const string TableName = "ITEMS";

		// collection of record fields
		public class TableData
		{
			public int ItemIid;
			public string ItemName;
			public string ItemShortDesc;
			public string ItemId;
			public string ItemAltId;
			public string ItemAltId2;
			public int ItemClass;
			public int ItemConsign;
			public int ItemStock;
			public int ItemTrackLot;
			public int ItemTrackSerial;
			public int ItemBillOnly;
			public int ItemType;
			public string ItemUnitOfIssue;
			public string ItemUnitOfRefill;
			public string ItemUnitOfOrder;
			public int ItemReOrderRatio;
			public int ItemRefillRatio;
			public int UOIPerUOO;
			public int UOIPerUOR;
			public int AutoResolveDiscrep;
			public decimal CostPerIssue;
			public decimal CostPerUnitRefill;
			public decimal CostPerUnitOrder;
			public int ItemIsChargeable;
			public int LeadTime;
			public int OrderCycle;
			public int SafetyDays;
			public int InclusionDays;
			public DateTime Last_modified;
            public int TempItem;
            public int IsPerishable;

            public TableData(int ItemIid, string ItemName, string ItemShortDesc, string ItemId, string ItemAltId, string ItemAltId2, int ItemClass, int ItemConsign, int ItemStock, int ItemTrackLot, int ItemTrackSerial, int ItemBillOnly, int ItemType, string ItemUnitOfIssue, string ItemUnitOfRefill, string ItemUnitOfOrder, int ItemReOrderRatio, int ItemRefillRatio, int UOIPerUOO, int UOIPerUOR, int AutoResolveDiscrep, decimal CostPerIssue, decimal CostPerUnitRefill, decimal CostPerUnitOrder, int ItemIsChargeable, int LeadTime, int OrderCycle, int SafetyDays, int InclusionDays, DateTime Last_modified, int TempItem, int isPerishable)
			{
				this.ItemIid = ItemIid;
				this.ItemName = ItemName;
				this.ItemShortDesc = ItemShortDesc;
				this.ItemId = ItemId;
				this.ItemAltId = ItemAltId;
				this.ItemAltId2 = ItemAltId2;
				this.ItemClass = ItemClass;
				this.ItemConsign = ItemConsign;
				this.ItemStock = ItemStock;
				this.ItemTrackLot = ItemTrackLot;
				this.ItemTrackSerial = ItemTrackSerial;
				this.ItemBillOnly = ItemBillOnly;
				this.ItemType = ItemType;
				this.ItemUnitOfIssue = ItemUnitOfIssue;
				this.ItemUnitOfRefill = ItemUnitOfRefill;
				this.ItemUnitOfOrder = ItemUnitOfOrder;
				this.ItemReOrderRatio = ItemReOrderRatio;
				this.ItemRefillRatio = ItemRefillRatio;
				this.UOIPerUOO = UOIPerUOO;
				this.UOIPerUOR = UOIPerUOR;
				this.AutoResolveDiscrep = AutoResolveDiscrep;
				this.CostPerIssue = CostPerIssue;
				this.CostPerUnitRefill = CostPerUnitRefill;
				this.CostPerUnitOrder = CostPerUnitOrder;
				this.ItemIsChargeable = ItemIsChargeable;
				this.LeadTime = LeadTime;
				this.OrderCycle = OrderCycle;
				this.SafetyDays = SafetyDays;
				this.InclusionDays = InclusionDays;
				this.Last_modified = Last_modified;
                this.TempItem = TempItem;
                this.IsPerishable = isPerishable;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int ItemIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ITEMS WHERE ItemIid='{0}'", 
				(int)ItemIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Items", "GetRecord", out _conn, out myDataReader))
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

        // return record based on the itemId (not primary key)
        public static bool GetRecord(string ItemId, out TableData data)
        {
            bool Retval = true;
            data = null;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from ITEMS WHERE Itemid='{0}'", ItemId);
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Items", "GetRecord", out _conn, out myDataReader))
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
                MainClass.ToInt(TableName, myDataReader["itemIid"])
				, myDataReader["itemName"].ToString()
				, myDataReader["itemShortDesc"].ToString()
				, myDataReader["itemId"].ToString()
				, myDataReader["itemAltId"].ToString()
				, myDataReader["itemAltId2"].ToString()
				, MainClass.ToInt(TableName, myDataReader["itemClass"])
				, MainClass.ToInt(TableName, myDataReader["itemConsign"])
				, MainClass.ToInt(TableName, myDataReader["itemStock"])
				, MainClass.ToInt(TableName, myDataReader["itemTrackLot"])
				, MainClass.ToInt(TableName, myDataReader["itemTrackSerial"])
				, MainClass.ToInt(TableName, myDataReader["itemBillOnly"])
				, MainClass.ToInt(TableName, myDataReader["itemType"])
				, myDataReader["itemUnitOfIssue"].ToString()
				, myDataReader["itemUnitOfRefill"].ToString()
				, myDataReader["itemUnitOfOrder"].ToString()
				, MainClass.ToInt(TableName, myDataReader["itemReOrderRatio"])
				, MainClass.ToInt(TableName, myDataReader["itemRefillRatio"])
				, MainClass.ToInt(TableName, myDataReader["UOIPerUOO"])
				, MainClass.ToInt(TableName, myDataReader["UOIPerUOR"])
				, MainClass.ToInt(TableName, myDataReader["autoResolveDiscrep"])
				, MainClass.ToDecimal(TableName, myDataReader["CostPerIssue"])
				, MainClass.ToDecimal(TableName, myDataReader["costPerUnitRefill"])
				, MainClass.ToDecimal(TableName, myDataReader["costPerUnitOrder"])
				, MainClass.ToInt(TableName, myDataReader["itemIsChargeable"])
				, MainClass.ToInt(TableName, myDataReader["leadTime"])
				, MainClass.ToInt(TableName, myDataReader["orderCycle"])
				, MainClass.ToInt(TableName, myDataReader["safetyDays"])
				, MainClass.ToInt(TableName, myDataReader["inclusionDays"])
				, MainClass.ToDate(TableName, myDataReader["last_modified"])
                , MainClass.ToInt(TableName, myDataReader["tempItem"])
                , MainClass.ToInt(TableName, myDataReader["isPerishable"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO ITEMS (ItemName, ItemShortDesc, ItemId, ItemAltId, ItemAltId2, ItemClass, ItemConsign, ItemStock, ItemTrackLot, ItemTrackSerial, ItemBillOnly, ItemType, ItemUnitOfIssue, ItemUnitOfRefill, ItemUnitOfOrder, ItemReOrderRatio, ItemRefillRatio, UOIPerUOO, UOIPerUOR, AutoResolveDiscrep, CostPerIssue, CostPerUnitRefill, CostPerUnitOrder, ItemIsChargeable, LeadTime, OrderCycle, SafetyDays, InclusionDays, tempItem, isPerishable) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.ItemName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemShortDesc) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemAltId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemAltId2) + "'" + ", " + (int)data.ItemClass + ", " + (int)data.ItemConsign + ", " + (int)data.ItemStock + ", " + (int)data.ItemTrackLot + ", " + (int)data.ItemTrackSerial + ", " + (int)data.ItemBillOnly + ", " + (int)data.ItemType + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemUnitOfIssue) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemUnitOfRefill) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemUnitOfOrder) + "'" + ", " + (int)data.ItemReOrderRatio + ", " + (int)data.ItemRefillRatio + ", " + (int)data.UOIPerUOO + ", " + (int)data.UOIPerUOR + ", " + (int)data.AutoResolveDiscrep + ", " + data.CostPerIssue + ", " + data.CostPerUnitRefill + ", " + data.CostPerUnitOrder + ", " + (int)data.ItemIsChargeable + ", " + (int)data.LeadTime + ", " + (int)data.OrderCycle + ", " + (int)data.SafetyDays + ", " + (int)data.InclusionDays + ", " + (int)data.TempItem + ", " + (int)data.IsPerishable+ ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Items", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int ItemIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ITEMS WHERE ItemIid='{0}'", 
				(int)ItemIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Items", "DeleteRecord");
			return Retval;
		}


	}
}
