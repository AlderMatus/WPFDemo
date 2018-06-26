using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Refill
	{
		const string TableName = "REFILL";

		// collection of record fields
		public class TableData
		{
			public string RefillId;
			public DateTime RefillQueTime;
			public string StnName;
			public string ItemId;
			public string AreaName;
			public int Zone;
			public int BtnBoardNbr;
			public int SubDrawer;
			public string PktDescriptor;
			public int BtnPocketNbr;
			public string ItemName;
			public string ItemShortDesc;
			public string ItemAltId;
			public string ItemAltId2;
			public int ItemClass;
			public string ItemClassName;
			public string PrimPickAreaName;
			public string SecPickAreaName;
			public int ItemType;
			public decimal CostPerIssue;
			public decimal CostPerUnitRefill;
			public string ItemUnitOfIssue;
			public int ItemConsign;
			public int ItemStock;
			public string ItemUnitOfRefill;
			public int UOIPerUOR;
			public int ItemRefillRatio;
			public int PktIid;
			public int PktPhysMaxQty;
			public int PktParQty;
			public int PktRefillPoint;
			public int PktCurQty;
			public int PktCritLow;
			public string VendorName;
			public string VendorPartNumber;
			public int SendRefillMsg;
			public int ItemIsStandard;
			public int RefillOrderedQty;
			public int RefillShippedQty;
			public int RefillBackOrderQty;
			public int RefillDone;
			public DateTime RefillDoneTime;
			public DateTime DeliveryDate;
			public int OrderMethod;
			public string RefillUserName;
			public string RefillUserId;
			public string RefillOrderStatus;
			public DateTime RefillBackOrderEta;
			public string PrimPickAreaLoc;
			public string SecPickAreaLoc;
			public int BtnBoardSeq;
			public int BtnDoorNbr;
			public string PocketUnitOfIssue;
			public int PocketUOIRatio;
			public int SystemType;
			public int CabinetId;
			public int Drawer;
			public string StorageUnitName;
			public string SubUnitName;
			public int BinNumber;
			public string ItemCatName;
			public int PoIsComplete;

			public TableData(string RefillId, DateTime RefillQueTime, string StnName, string ItemId, string AreaName, int Zone, int BtnBoardNbr, int SubDrawer, string PktDescriptor, int BtnPocketNbr, string ItemName, string ItemShortDesc, string ItemAltId, string ItemAltId2, int ItemClass, string ItemClassName, string PrimPickAreaName, string SecPickAreaName, int ItemType, decimal CostPerIssue, decimal CostPerUnitRefill, string ItemUnitOfIssue, int ItemConsign, int ItemStock, string ItemUnitOfRefill, int UOIPerUOR, int ItemRefillRatio, int PktIid, int PktPhysMaxQty, int PktParQty, int PktRefillPoint, int PktCurQty, int PktCritLow, string VendorName, string VendorPartNumber, int SendRefillMsg, int ItemIsStandard, int RefillOrderedQty, int RefillShippedQty, int RefillBackOrderQty, int RefillDone, DateTime RefillDoneTime, DateTime DeliveryDate, int OrderMethod, string RefillUserName, string RefillUserId, string RefillOrderStatus, DateTime RefillBackOrderEta, string PrimPickAreaLoc, string SecPickAreaLoc, int BtnBoardSeq, int BtnDoorNbr, string PocketUnitOfIssue, int PocketUOIRatio, int SystemType, int CabinetId, int Drawer, string StorageUnitName, string SubUnitName, int BinNumber, string ItemCatName, int PoIsComplete)
			{
				this.RefillId = RefillId;
				this.RefillQueTime = RefillQueTime;
				this.StnName = StnName;
				this.ItemId = ItemId;
				this.AreaName = AreaName;
				this.Zone = Zone;
				this.BtnBoardNbr = BtnBoardNbr;
				this.SubDrawer = SubDrawer;
				this.PktDescriptor = PktDescriptor;
				this.BtnPocketNbr = BtnPocketNbr;
				this.ItemName = ItemName;
				this.ItemShortDesc = ItemShortDesc;
				this.ItemAltId = ItemAltId;
				this.ItemAltId2 = ItemAltId2;
				this.ItemClass = ItemClass;
				this.ItemClassName = ItemClassName;
				this.PrimPickAreaName = PrimPickAreaName;
				this.SecPickAreaName = SecPickAreaName;
				this.ItemType = ItemType;
				this.CostPerIssue = CostPerIssue;
				this.CostPerUnitRefill = CostPerUnitRefill;
				this.ItemUnitOfIssue = ItemUnitOfIssue;
				this.ItemConsign = ItemConsign;
				this.ItemStock = ItemStock;
				this.ItemUnitOfRefill = ItemUnitOfRefill;
				this.UOIPerUOR = UOIPerUOR;
				this.ItemRefillRatio = ItemRefillRatio;
				this.PktIid = PktIid;
				this.PktPhysMaxQty = PktPhysMaxQty;
				this.PktParQty = PktParQty;
				this.PktRefillPoint = PktRefillPoint;
				this.PktCurQty = PktCurQty;
				this.PktCritLow = PktCritLow;
				this.VendorName = VendorName;
				this.VendorPartNumber = VendorPartNumber;
				this.SendRefillMsg = SendRefillMsg;
				this.ItemIsStandard = ItemIsStandard;
				this.RefillOrderedQty = RefillOrderedQty;
				this.RefillShippedQty = RefillShippedQty;
				this.RefillBackOrderQty = RefillBackOrderQty;
				this.RefillDone = RefillDone;
				this.RefillDoneTime = RefillDoneTime;
				this.DeliveryDate = DeliveryDate;
				this.OrderMethod = OrderMethod;
				this.RefillUserName = RefillUserName;
				this.RefillUserId = RefillUserId;
				this.RefillOrderStatus = RefillOrderStatus;
				this.RefillBackOrderEta = RefillBackOrderEta;
				this.PrimPickAreaLoc = PrimPickAreaLoc;
				this.SecPickAreaLoc = SecPickAreaLoc;
				this.BtnBoardSeq = BtnBoardSeq;
				this.BtnDoorNbr = BtnDoorNbr;
				this.PocketUnitOfIssue = PocketUnitOfIssue;
				this.PocketUOIRatio = PocketUOIRatio;
				this.SystemType = SystemType;
				this.CabinetId = CabinetId;
				this.Drawer = Drawer;
				this.StorageUnitName = StorageUnitName;
				this.SubUnitName = SubUnitName;
				this.BinNumber = BinNumber;
				this.ItemCatName = ItemCatName;
				this.PoIsComplete = PoIsComplete;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(string RefillId, int SystemType, string StnName, string ItemId, int PktIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from REFILL WHERE RefillId='{0}' AND SystemType='{1}' AND StnName='{2}' AND ItemId='{3}' AND PktIid='{4}'", 
				MainClass.FixStringForSingleQuote(RefillId), (int)SystemType, MainClass.FixStringForSingleQuote(StnName), MainClass.FixStringForSingleQuote(ItemId), (int)PktIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Refill", "GetRecord", out _conn, out myDataReader))
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
				myDataReader["RefillId"].ToString()
				, MainClass.ToDate(TableName, myDataReader["RefillQueTime"])
				, myDataReader["StnName"].ToString()
				, myDataReader["ItemId"].ToString()
				, myDataReader["AreaName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Zone"])
				, MainClass.ToInt(TableName, myDataReader["BtnBoardNbr"])
				, MainClass.ToInt(TableName, myDataReader["SubDrawer"])
				, myDataReader["PktDescriptor"].ToString()
				, MainClass.ToInt(TableName, myDataReader["BtnPocketNbr"])
				, myDataReader["ItemName"].ToString()
				, myDataReader["ItemShortDesc"].ToString()
				, myDataReader["ItemAltId"].ToString()
				, myDataReader["ItemAltId2"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ItemClass"])
				, myDataReader["ItemClassName"].ToString()
				, myDataReader["PrimPickAreaName"].ToString()
				, myDataReader["SecPickAreaName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ItemType"])
				, MainClass.ToDecimal(TableName, myDataReader["CostPerIssue"])
				, MainClass.ToDecimal(TableName, myDataReader["CostPerUnitRefill"])
				, myDataReader["ItemUnitOfIssue"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ItemConsign"])
				, MainClass.ToInt(TableName, myDataReader["ItemStock"])
				, myDataReader["ItemUnitOfRefill"].ToString()
				, MainClass.ToInt(TableName, myDataReader["UOIPerUOR"])
				, MainClass.ToInt(TableName, myDataReader["ItemRefillRatio"])
				, MainClass.ToInt(TableName, myDataReader["PktIid"])
				, MainClass.ToInt(TableName, myDataReader["PktPhysMaxQty"])
				, MainClass.ToInt(TableName, myDataReader["PktParQty"])
				, MainClass.ToInt(TableName, myDataReader["PktRefillPoint"])
				, MainClass.ToInt(TableName, myDataReader["PktCurQty"])
				, MainClass.ToInt(TableName, myDataReader["PktCritLow"])
				, myDataReader["VendorName"].ToString()
				, myDataReader["VendorPartNumber"].ToString()
				, MainClass.ToInt(TableName, myDataReader["SendRefillMsg"])
				, MainClass.ToInt(TableName, myDataReader["ItemIsStandard"])
				, MainClass.ToInt(TableName, myDataReader["RefillOrderedQty"])
				, MainClass.ToInt(TableName, myDataReader["RefillShippedQty"])
				, MainClass.ToInt(TableName, myDataReader["RefillBackOrderQty"])
				, MainClass.ToInt(TableName, myDataReader["RefillDone"])
				, MainClass.ToDate(TableName, myDataReader["RefillDoneTime"])
				, MainClass.ToDate(TableName, myDataReader["DeliveryDate"])
				, MainClass.ToInt(TableName, myDataReader["OrderMethod"])
				, myDataReader["RefillUserName"].ToString()
				, myDataReader["RefillUserId"].ToString()
				, myDataReader["RefillOrderStatus"].ToString()
				, MainClass.ToDate(TableName, myDataReader["RefillBackOrderEta"])
				, myDataReader["PrimPickAreaLoc"].ToString()
				, myDataReader["SecPickAreaLoc"].ToString()
				, MainClass.ToInt(TableName, myDataReader["BtnBoardSeq"])
				, MainClass.ToInt(TableName, myDataReader["BtnDoorNbr"])
				, myDataReader["PocketUnitOfIssue"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PocketUOIRatio"])
				, MainClass.ToInt(TableName, myDataReader["SystemType"])
				, MainClass.ToInt(TableName, myDataReader["CabinetId"])
				, MainClass.ToInt(TableName, myDataReader["Drawer"])
				, myDataReader["StorageUnitName"].ToString()
				, myDataReader["SubUnitName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["BinNumber"])
				, myDataReader["ItemCatName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PoIsComplete"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO REFILL (RefillId, RefillQueTime, StnName, ItemId, AreaName, Zone, BtnBoardNbr, SubDrawer, PktDescriptor, BtnPocketNbr, ItemName, ItemShortDesc, ItemAltId, ItemAltId2, ItemClass, ItemClassName, PrimPickAreaName, SecPickAreaName, ItemType, CostPerIssue, CostPerUnitRefill, ItemUnitOfIssue, ItemConsign, ItemStock, ItemUnitOfRefill, UOIPerUOR, ItemRefillRatio, PktIid, PktPhysMaxQty, PktParQty, PktRefillPoint, PktCurQty, PktCritLow, VendorName, VendorPartNumber, SendRefillMsg, ItemIsStandard, RefillOrderedQty, RefillShippedQty, RefillBackOrderQty, RefillDone, RefillDoneTime, DeliveryDate, OrderMethod, RefillUserName, RefillUserId, RefillOrderStatus, RefillBackOrderEta, PrimPickAreaLoc, SecPickAreaLoc, BtnBoardSeq, BtnDoorNbr, PocketUnitOfIssue, PocketUOIRatio, SystemType, CabinetId, Drawer, StorageUnitName, SubUnitName, BinNumber, ItemCatName, PoIsComplete) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.RefillId) + "'" + ", " + MainClass.DateTimeToTimestamp(data.RefillQueTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.StnName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.AreaName) + "'" + ", " + (int)data.Zone + ", " + (int)data.BtnBoardNbr + ", " + (int)data.SubDrawer + ", " + "'" + MainClass.FixStringForSingleQuote(data.PktDescriptor) + "'" + ", " + (int)data.BtnPocketNbr + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemShortDesc) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemAltId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemAltId2) + "'" + ", " + (int)data.ItemClass + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemClassName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PrimPickAreaName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecPickAreaName) + "'" + ", " + (int)data.ItemType + ", " + data.CostPerIssue + ", " + data.CostPerUnitRefill + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemUnitOfIssue) + "'" + ", " + (int)data.ItemConsign + ", " + (int)data.ItemStock + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemUnitOfRefill) + "'" + ", " + (int)data.UOIPerUOR + ", " + (int)data.ItemRefillRatio + ", " + (int)data.PktIid + ", " + (int)data.PktPhysMaxQty + ", " + (int)data.PktParQty + ", " + (int)data.PktRefillPoint + ", " + (int)data.PktCurQty + ", " + (int)data.PktCritLow + ", " + "'" + MainClass.FixStringForSingleQuote(data.VendorName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.VendorPartNumber) + "'" + ", " + (int)data.SendRefillMsg + ", " + (int)data.ItemIsStandard + ", " + (int)data.RefillOrderedQty + ", " + (int)data.RefillShippedQty + ", " + (int)data.RefillBackOrderQty + ", " + (int)data.RefillDone + ", " + MainClass.DateTimeToTimestamp(data.RefillDoneTime) + ", " + MainClass.DateTimeToTimestamp(data.DeliveryDate) + ", " + (int)data.OrderMethod + ", " + "'" + MainClass.FixStringForSingleQuote(data.RefillUserName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.RefillUserId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.RefillOrderStatus) + "'" + ", " + MainClass.DateTimeToTimestamp(data.RefillBackOrderEta) + ", " + "'" + MainClass.FixStringForSingleQuote(data.PrimPickAreaLoc) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecPickAreaLoc) + "'" + ", " + (int)data.BtnBoardSeq + ", " + (int)data.BtnDoorNbr + ", " + "'" + MainClass.FixStringForSingleQuote(data.PocketUnitOfIssue) + "'" + ", " + (int)data.PocketUOIRatio + ", " + (int)data.SystemType + ", " + (int)data.CabinetId + ", " + (int)data.Drawer + ", " + "'" + MainClass.FixStringForSingleQuote(data.StorageUnitName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SubUnitName) + "'" + ", " + (int)data.BinNumber + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemCatName) + "'" + ", " + (int)data.PoIsComplete + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Refill", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(string RefillId, int SystemType, string StnName, string ItemId, int PktIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE REFILL WHERE RefillId='{0}' AND SystemType='{1}' AND StnName='{2}' AND ItemId='{3}' AND PktIid='{4}'", 
				MainClass.FixStringForSingleQuote(RefillId), (int)SystemType, MainClass.FixStringForSingleQuote(StnName), MainClass.FixStringForSingleQuote(ItemId), (int)PktIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Refill", "DeleteRecord");
			return Retval;
		}


	}
}
