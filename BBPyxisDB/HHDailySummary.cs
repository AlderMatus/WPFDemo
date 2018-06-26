using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class HHDailySummary
	{
		const string TableName = "HH_DAILY_SUMMARY";

		// collection of record fields
		public class TableData
		{
			public string ParLocationName;
			public string StorageUnitName;
			public string SubUnitName;
			public int BinNumber;
			public DateTime DayOfMonth;
			public int PktIid;
			public int PktParQty;
			public int PktRefillPoint;
			public int PktCritLow;
			public int PktCurQty;
			public string ItemId;
			public string ItemName;
			public string ItemAltId;
			public string ItemAltId2;
			public int UOIperUOR;
			public int UOIperUOO;
			public int NetUseQty;
			public DateTime LoadDate;
			public int CritLowCounter;
			public int RefillCounter;
			public int RefillTotalQty;
			public int InventoryCounter;
			public int AddedToMonthly;
			public int StockOutCounter;
			public int StillLoaded;

			public TableData(string ParLocationName, string StorageUnitName, string SubUnitName, int BinNumber, DateTime DayOfMonth, int PktIid, int PktParQty, int PktRefillPoint, int PktCritLow, int PktCurQty, string ItemId, string ItemName, string ItemAltId, string ItemAltId2, int UOIperUOR, int UOIperUOO, int NetUseQty, DateTime LoadDate, int CritLowCounter, int RefillCounter, int RefillTotalQty, int InventoryCounter, int AddedToMonthly, int StockOutCounter, int StillLoaded)
			{
				this.ParLocationName = ParLocationName;
				this.StorageUnitName = StorageUnitName;
				this.SubUnitName = SubUnitName;
				this.BinNumber = BinNumber;
				this.DayOfMonth = DayOfMonth;
				this.PktIid = PktIid;
				this.PktParQty = PktParQty;
				this.PktRefillPoint = PktRefillPoint;
				this.PktCritLow = PktCritLow;
				this.PktCurQty = PktCurQty;
				this.ItemId = ItemId;
				this.ItemName = ItemName;
				this.ItemAltId = ItemAltId;
				this.ItemAltId2 = ItemAltId2;
				this.UOIperUOR = UOIperUOR;
				this.UOIperUOO = UOIperUOO;
				this.NetUseQty = NetUseQty;
				this.LoadDate = LoadDate;
				this.CritLowCounter = CritLowCounter;
				this.RefillCounter = RefillCounter;
				this.RefillTotalQty = RefillTotalQty;
				this.InventoryCounter = InventoryCounter;
				this.AddedToMonthly = AddedToMonthly;
				this.StockOutCounter = StockOutCounter;
				this.StillLoaded = StillLoaded;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(string ParLocationName, string StorageUnitName, string SubUnitName, int BinNumber, DateTime DayOfMonth, string ItemId, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from HH_DAILY_SUMMARY WHERE ParLocationName='{0}' AND StorageUnitName='{1}' AND SubUnitName='{2}' AND BinNumber='{3}' AND DayOfMonth='{4}' AND ItemId='{5}'", 
				MainClass.FixStringForSingleQuote(ParLocationName), MainClass.FixStringForSingleQuote(StorageUnitName), MainClass.FixStringForSingleQuote(SubUnitName), (int)BinNumber, MainClass.DateTimeToTimestamp(DayOfMonth), MainClass.FixStringForSingleQuote(ItemId));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "HHDailySummary", "GetRecord", out _conn, out myDataReader))
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
				myDataReader["ParLocationName"].ToString()
				, myDataReader["StorageUnitName"].ToString()
				, myDataReader["SubUnitName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["BinNumber"])
				, MainClass.ToDate(TableName, myDataReader["DayOfMonth"])
				, MainClass.ToInt(TableName, myDataReader["PktIid"])
				, MainClass.ToInt(TableName, myDataReader["PktParQty"])
				, MainClass.ToInt(TableName, myDataReader["PktRefillPoint"])
				, MainClass.ToInt(TableName, myDataReader["PktCritLow"])
				, MainClass.ToInt(TableName, myDataReader["PktCurQty"])
				, myDataReader["ItemId"].ToString()
				, myDataReader["ItemName"].ToString()
				, myDataReader["ItemAltId"].ToString()
				, myDataReader["ItemAltId2"].ToString()
				, MainClass.ToInt(TableName, myDataReader["UOIperUOR"])
				, MainClass.ToInt(TableName, myDataReader["UOIperUOO"])
				, MainClass.ToInt(TableName, myDataReader["NetUseQty"])
				, MainClass.ToDate(TableName, myDataReader["LoadDate"])
				, MainClass.ToInt(TableName, myDataReader["CritLowCounter"])
				, MainClass.ToInt(TableName, myDataReader["RefillCounter"])
				, MainClass.ToInt(TableName, myDataReader["RefillTotalQty"])
				, MainClass.ToInt(TableName, myDataReader["InventoryCounter"])
				, MainClass.ToInt(TableName, myDataReader["AddedToMonthly"])
				, MainClass.ToInt(TableName, myDataReader["StockOutCounter"])
				, MainClass.ToInt(TableName, myDataReader["StillLoaded"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO HH_DAILY_SUMMARY (ParLocationName, StorageUnitName, SubUnitName, BinNumber, DayOfMonth, PktIid, PktParQty, PktRefillPoint, PktCritLow, PktCurQty, ItemId, ItemName, ItemAltId, ItemAltId2, UOIperUOR, UOIperUOO, NetUseQty, LoadDate, CritLowCounter, RefillCounter, RefillTotalQty, InventoryCounter, AddedToMonthly, StockOutCounter, StillLoaded) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.ParLocationName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.StorageUnitName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SubUnitName) + "'" + ", " + (int)data.BinNumber + ", " + MainClass.DateTimeToTimestamp(data.DayOfMonth) + ", " + (int)data.PktIid + ", " + (int)data.PktParQty + ", " + (int)data.PktRefillPoint + ", " + (int)data.PktCritLow + ", " + (int)data.PktCurQty + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemAltId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemAltId2) + "'" + ", " + (int)data.UOIperUOR + ", " + (int)data.UOIperUOO + ", " + (int)data.NetUseQty + ", " + MainClass.DateTimeToTimestamp(data.LoadDate) + ", " + (int)data.CritLowCounter + ", " + (int)data.RefillCounter + ", " + (int)data.RefillTotalQty + ", " + (int)data.InventoryCounter + ", " + (int)data.AddedToMonthly + ", " + (int)data.StockOutCounter + ", " + (int)data.StillLoaded + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHDailySummary", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(string ParLocationName, string StorageUnitName, string SubUnitName, int BinNumber, DateTime DayOfMonth, string ItemId)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE HH_DAILY_SUMMARY WHERE ParLocationName='{0}' AND StorageUnitName='{1}' AND SubUnitName='{2}' AND BinNumber='{3}' AND DayOfMonth='{4}' AND ItemId='{5}'", 
				MainClass.FixStringForSingleQuote(ParLocationName), MainClass.FixStringForSingleQuote(StorageUnitName), MainClass.FixStringForSingleQuote(SubUnitName), (int)BinNumber, MainClass.DateTimeToTimestamp(DayOfMonth), MainClass.FixStringForSingleQuote(ItemId));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHDailySummary", "DeleteRecord");
			return Retval;
		}


	}
}
