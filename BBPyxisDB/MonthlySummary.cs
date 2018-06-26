using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class MonthlySummary
	{
		const string TableName = "MONTHLY_SUMMARY";

		// collection of record fields
		public class TableData
		{
			public DateTime MonthOfYear;
			public string DeviceName;
			public int BtnBoardNbr;
			public int BtnPocketNbr;
			public int BtnColumnNbr;
			public int BtnDoorNbr;
			public int ChassisSeqNbr;
			public int SubDrawer;
			public int PktIid;
			public string PktDescriptor;
			public string ItemId;
			public string ItemName;
			public string ItemAltId;
			public string ItemAltId2;
			public int NetUseQty;
			public int StillLoaded;
			public DateTime LoadDate;
			public int StockOutCounter;
			public int CritLowCounter;
			public int RefillCounter;
			public int RefillTotalQty;
			public int VendCounter;
			public int VendTotalQty;
			public int ReturnCounter;
			public int ReturnTotalQty;
			public int ExpireItemCounter;
			public int ExpireItemTotalQty;
			public int DiscrepCounter;
			public int DiscrepOverQty;
			public int DiscrepShortQty;
			public int RemRetDiscCounter;
			public int RemRetDiscQty;
			public int TransferCounter;
			public int TransferTotalQty;
			public int NullTrxCounter;
			public int InventoryCounter;
			public int CancelTrxCounter;
			public decimal TxTotalCost;
			public decimal SellPrice;
			public int CDM;

			public TableData(DateTime MonthOfYear, string DeviceName, int BtnBoardNbr, int BtnPocketNbr, int BtnColumnNbr, int BtnDoorNbr, int ChassisSeqNbr, int SubDrawer, int PktIid, string PktDescriptor, string ItemId, string ItemName, string ItemAltId, string ItemAltId2, int NetUseQty, int StillLoaded, DateTime LoadDate, int StockOutCounter, int CritLowCounter, int RefillCounter, int RefillTotalQty, int VendCounter, int VendTotalQty, int ReturnCounter, int ReturnTotalQty, int ExpireItemCounter, int ExpireItemTotalQty, int DiscrepCounter, int DiscrepOverQty, int DiscrepShortQty, int RemRetDiscCounter, int RemRetDiscQty, int TransferCounter, int TransferTotalQty, int NullTrxCounter, int InventoryCounter, int CancelTrxCounter, decimal TxTotalCost, decimal SellPrice, int CDM)
			{
				this.MonthOfYear = MonthOfYear;
				this.DeviceName = DeviceName;
				this.BtnBoardNbr = BtnBoardNbr;
				this.BtnPocketNbr = BtnPocketNbr;
				this.BtnColumnNbr = BtnColumnNbr;
				this.BtnDoorNbr = BtnDoorNbr;
				this.ChassisSeqNbr = ChassisSeqNbr;
				this.SubDrawer = SubDrawer;
				this.PktIid = PktIid;
				this.PktDescriptor = PktDescriptor;
				this.ItemId = ItemId;
				this.ItemName = ItemName;
				this.ItemAltId = ItemAltId;
				this.ItemAltId2 = ItemAltId2;
				this.NetUseQty = NetUseQty;
				this.StillLoaded = StillLoaded;
				this.LoadDate = LoadDate;
				this.StockOutCounter = StockOutCounter;
				this.CritLowCounter = CritLowCounter;
				this.RefillCounter = RefillCounter;
				this.RefillTotalQty = RefillTotalQty;
				this.VendCounter = VendCounter;
				this.VendTotalQty = VendTotalQty;
				this.ReturnCounter = ReturnCounter;
				this.ReturnTotalQty = ReturnTotalQty;
				this.ExpireItemCounter = ExpireItemCounter;
				this.ExpireItemTotalQty = ExpireItemTotalQty;
				this.DiscrepCounter = DiscrepCounter;
				this.DiscrepOverQty = DiscrepOverQty;
				this.DiscrepShortQty = DiscrepShortQty;
				this.RemRetDiscCounter = RemRetDiscCounter;
				this.RemRetDiscQty = RemRetDiscQty;
				this.TransferCounter = TransferCounter;
				this.TransferTotalQty = TransferTotalQty;
				this.NullTrxCounter = NullTrxCounter;
				this.InventoryCounter = InventoryCounter;
				this.CancelTrxCounter = CancelTrxCounter;
				this.TxTotalCost = TxTotalCost;
				this.SellPrice = SellPrice;
				this.CDM = CDM;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(DateTime MonthOfYear, string DeviceName, int BtnBoardNbr, int BtnPocketNbr, string ItemId, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from MONTHLY_SUMMARY WHERE MonthOfYear='{0}' AND DeviceName='{1}' AND BtnBoardNbr='{2}' AND BtnPocketNbr='{3}' AND ItemId='{4}'", 
				MainClass.DateTimeToTimestamp(MonthOfYear), MainClass.FixStringForSingleQuote(DeviceName), (int)BtnBoardNbr, (int)BtnPocketNbr, MainClass.FixStringForSingleQuote(ItemId));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "MonthlySummary", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToDate(TableName, myDataReader["MonthOfYear"])
				, myDataReader["DeviceName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["BtnBoardNbr"])
				, MainClass.ToInt(TableName, myDataReader["BtnPocketNbr"])
				, MainClass.ToInt(TableName, myDataReader["BtnColumnNbr"])
				, MainClass.ToInt(TableName, myDataReader["BtnDoorNbr"])
				, MainClass.ToInt(TableName, myDataReader["ChassisSeqNbr"])
				, MainClass.ToInt(TableName, myDataReader["SubDrawer"])
				, MainClass.ToInt(TableName, myDataReader["PktIid"])
				, myDataReader["PktDescriptor"].ToString()
				, myDataReader["ItemId"].ToString()
				, myDataReader["ItemName"].ToString()
				, myDataReader["ItemAltId"].ToString()
				, myDataReader["ItemAltId2"].ToString()
				, MainClass.ToInt(TableName, myDataReader["NetUseQty"])
				, MainClass.ToInt(TableName, myDataReader["StillLoaded"])
				, MainClass.ToDate(TableName, myDataReader["LoadDate"])
				, MainClass.ToInt(TableName, myDataReader["StockOutCounter"])
				, MainClass.ToInt(TableName, myDataReader["CritLowCounter"])
				, MainClass.ToInt(TableName, myDataReader["RefillCounter"])
				, MainClass.ToInt(TableName, myDataReader["RefillTotalQty"])
				, MainClass.ToInt(TableName, myDataReader["VendCounter"])
				, MainClass.ToInt(TableName, myDataReader["VendTotalQty"])
				, MainClass.ToInt(TableName, myDataReader["ReturnCounter"])
				, MainClass.ToInt(TableName, myDataReader["ReturnTotalQty"])
				, MainClass.ToInt(TableName, myDataReader["ExpireItemCounter"])
				, MainClass.ToInt(TableName, myDataReader["ExpireItemTotalQty"])
				, MainClass.ToInt(TableName, myDataReader["DiscrepCounter"])
				, MainClass.ToInt(TableName, myDataReader["DiscrepOverQty"])
				, MainClass.ToInt(TableName, myDataReader["DiscrepShortQty"])
				, MainClass.ToInt(TableName, myDataReader["RemRetDiscCounter"])
				, MainClass.ToInt(TableName, myDataReader["RemRetDiscQty"])
				, MainClass.ToInt(TableName, myDataReader["TransferCounter"])
				, MainClass.ToInt(TableName, myDataReader["TransferTotalQty"])
				, MainClass.ToInt(TableName, myDataReader["NullTrxCounter"])
				, MainClass.ToInt(TableName, myDataReader["InventoryCounter"])
				, MainClass.ToInt(TableName, myDataReader["CancelTrxCounter"])
				, MainClass.ToDecimal(TableName, myDataReader["TxTotalCost"])
				, MainClass.ToDecimal(TableName, myDataReader["SellPrice"])
				, MainClass.ToInt(TableName, myDataReader["CDM"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO MONTHLY_SUMMARY (MonthOfYear, DeviceName, BtnBoardNbr, BtnPocketNbr, BtnColumnNbr, BtnDoorNbr, ChassisSeqNbr, SubDrawer, PktIid, PktDescriptor, ItemId, ItemName, ItemAltId, ItemAltId2, NetUseQty, StillLoaded, LoadDate, StockOutCounter, CritLowCounter, RefillCounter, RefillTotalQty, VendCounter, VendTotalQty, ReturnCounter, ReturnTotalQty, ExpireItemCounter, ExpireItemTotalQty, DiscrepCounter, DiscrepOverQty, DiscrepShortQty, RemRetDiscCounter, RemRetDiscQty, TransferCounter, TransferTotalQty, NullTrxCounter, InventoryCounter, CancelTrxCounter, TxTotalCost, SellPrice, CDM) VALUES ("
				+ MainClass.DateTimeToTimestamp(data.MonthOfYear) + ", " + "'" + MainClass.FixStringForSingleQuote(data.DeviceName) + "'" + ", " + (int)data.BtnBoardNbr + ", " + (int)data.BtnPocketNbr + ", " + (int)data.BtnColumnNbr + ", " + (int)data.BtnDoorNbr + ", " + (int)data.ChassisSeqNbr + ", " + (int)data.SubDrawer + ", " + (int)data.PktIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.PktDescriptor) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemAltId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemAltId2) + "'" + ", " + (int)data.NetUseQty + ", " + (int)data.StillLoaded + ", " + MainClass.DateTimeToTimestamp(data.LoadDate) + ", " + (int)data.StockOutCounter + ", " + (int)data.CritLowCounter + ", " + (int)data.RefillCounter + ", " + (int)data.RefillTotalQty + ", " + (int)data.VendCounter + ", " + (int)data.VendTotalQty + ", " + (int)data.ReturnCounter + ", " + (int)data.ReturnTotalQty + ", " + (int)data.ExpireItemCounter + ", " + (int)data.ExpireItemTotalQty + ", " + (int)data.DiscrepCounter + ", " + (int)data.DiscrepOverQty + ", " + (int)data.DiscrepShortQty + ", " + (int)data.RemRetDiscCounter + ", " + (int)data.RemRetDiscQty + ", " + (int)data.TransferCounter + ", " + (int)data.TransferTotalQty + ", " + (int)data.NullTrxCounter + ", " + (int)data.InventoryCounter + ", " + (int)data.CancelTrxCounter + ", " + data.TxTotalCost + ", " + data.SellPrice + ", " + (int)data.CDM + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "MonthlySummary", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(DateTime MonthOfYear, string DeviceName, int BtnBoardNbr, int BtnPocketNbr, string ItemId)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE MONTHLY_SUMMARY WHERE MonthOfYear='{0}' AND DeviceName='{1}' AND BtnBoardNbr='{2}' AND BtnPocketNbr='{3}' AND ItemId='{4}'", 
				MainClass.DateTimeToTimestamp(MonthOfYear), MainClass.FixStringForSingleQuote(DeviceName), (int)BtnBoardNbr, (int)BtnPocketNbr, MainClass.FixStringForSingleQuote(ItemId));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "MonthlySummary", "DeleteRecord");
			return Retval;
		}


	}
}
