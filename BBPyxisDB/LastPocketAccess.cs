using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class LastPocketAccess
	{
		const string TableName = "LAST_POCKET_ACCESS";

		// collection of record fields
		public class TableData
		{
			public string DeviceName;
			public DateTime TxTime;
			public int BtnDoorNbr;
			public int BtnBoardNbr;
			public int BtnBoardSeq;
			public int SubDrawer;
			public string PktDescriptor;
			public int TrxSeq;
			public int ChassisSeqNbr;
			public int BtnColumnNbr;
			public int MsgTypeId;
			public string ItemName;
			public string ItemShortDesc;
			public string ItemAlias;
			public string ItemId;
			public string ItemAltId;
			public string ItemAltId2;
			public int ItemClass;
			public string ItemClassName;
			public int ItemType;
			public int ExpBeginCount;
			public int ActualBeginCount;
			public int TrxQty;
			public int PktPhysMaxQty;
			public int PktParQty;
			public int PktRefillPoint;
			public int PktCritLow;
			public int StnTotal;
			public DateTime PostDateTime;
			public string LotNumber;
			public string SerialNumber;
			public string VendorName;
			public string UserName;
			public string UserId;
			public int TempUser;
			public string WitnName;
			public string WitnId;
			public string PtLastName;
			public string PtFirstName;
			public string PtMiddleName;
			public string PtId;
			public string PtAltId1;
			public string PtAltId2;
			public string PtComment1;
			public string PtComment2;
			public string NUnitName;
			public string Room;
			public string Bed;
			public int PtType;
			public int EADT;
			public int TempPatient;
			public string Waste;
			public string PtMergeId;
			public int HasBeenArchived;
			public DateTime NextExpireTime;
			public decimal CostPerIssue;
			public decimal CostPerUnitRefill;
			public decimal CostPerUnitOrder;
			public string AreaName;
			public string ItemUnitOfIssue;
			public string ItemUnitOfRefill;
			public string ItemUnitOfOrder;
			public int TrxUOM;
			public string AdmitDrName;
			public string AttendDrName;
			public string KitId;
			public string FacilityCode;
			public int OrigVendSeq;
			public DateTime OrigVendDate;
			public int IsBillable;
			public string CostCenter;
			public string VendorPartNumber;
			public int TimeBias;
			public int ItemIsChargeable;
			public int ItemStock;
			public string RefillId;
			public int RefillOrderedQty;
			public int RefillShippedQty;
			public int BtnPocketNbr;
			public string XfrToStation;
			public string CaseId;
			public string CaseName;
			public string CaseCart;

			public TableData(string DeviceName, DateTime TxTime, int BtnDoorNbr, int BtnBoardNbr, int BtnBoardSeq, int SubDrawer, string PktDescriptor, int TrxSeq, int ChassisSeqNbr, int BtnColumnNbr, int MsgTypeId, string ItemName, string ItemShortDesc, string ItemAlias, string ItemId, string ItemAltId, string ItemAltId2, int ItemClass, string ItemClassName, int ItemType, int ExpBeginCount, int ActualBeginCount, int TrxQty, int PktPhysMaxQty, int PktParQty, int PktRefillPoint, int PktCritLow, int StnTotal, DateTime PostDateTime, string LotNumber, string SerialNumber, string VendorName, string UserName, string UserId, int TempUser, string WitnName, string WitnId, string PtLastName, string PtFirstName, string PtMiddleName, string PtId, string PtAltId1, string PtAltId2, string PtComment1, string PtComment2, string NUnitName, string Room, string Bed, int PtType, int EADT, int TempPatient, string Waste, string PtMergeId, int HasBeenArchived, DateTime NextExpireTime, decimal CostPerIssue, decimal CostPerUnitRefill, decimal CostPerUnitOrder, string AreaName, string ItemUnitOfIssue, string ItemUnitOfRefill, string ItemUnitOfOrder, int TrxUOM, string AdmitDrName, string AttendDrName, string KitId, string FacilityCode, int OrigVendSeq, DateTime OrigVendDate, int IsBillable, string CostCenter, string VendorPartNumber, int TimeBias, int ItemIsChargeable, int ItemStock, string RefillId, int RefillOrderedQty, int RefillShippedQty, int BtnPocketNbr, string XfrToStation, string CaseId, string CaseName, string CaseCart)
			{
				this.DeviceName = DeviceName;
				this.TxTime = TxTime;
				this.BtnDoorNbr = BtnDoorNbr;
				this.BtnBoardNbr = BtnBoardNbr;
				this.BtnBoardSeq = BtnBoardSeq;
				this.SubDrawer = SubDrawer;
				this.PktDescriptor = PktDescriptor;
				this.TrxSeq = TrxSeq;
				this.ChassisSeqNbr = ChassisSeqNbr;
				this.BtnColumnNbr = BtnColumnNbr;
				this.MsgTypeId = MsgTypeId;
				this.ItemName = ItemName;
				this.ItemShortDesc = ItemShortDesc;
				this.ItemAlias = ItemAlias;
				this.ItemId = ItemId;
				this.ItemAltId = ItemAltId;
				this.ItemAltId2 = ItemAltId2;
				this.ItemClass = ItemClass;
				this.ItemClassName = ItemClassName;
				this.ItemType = ItemType;
				this.ExpBeginCount = ExpBeginCount;
				this.ActualBeginCount = ActualBeginCount;
				this.TrxQty = TrxQty;
				this.PktPhysMaxQty = PktPhysMaxQty;
				this.PktParQty = PktParQty;
				this.PktRefillPoint = PktRefillPoint;
				this.PktCritLow = PktCritLow;
				this.StnTotal = StnTotal;
				this.PostDateTime = PostDateTime;
				this.LotNumber = LotNumber;
				this.SerialNumber = SerialNumber;
				this.VendorName = VendorName;
				this.UserName = UserName;
				this.UserId = UserId;
				this.TempUser = TempUser;
				this.WitnName = WitnName;
				this.WitnId = WitnId;
				this.PtLastName = PtLastName;
				this.PtFirstName = PtFirstName;
				this.PtMiddleName = PtMiddleName;
				this.PtId = PtId;
				this.PtAltId1 = PtAltId1;
				this.PtAltId2 = PtAltId2;
				this.PtComment1 = PtComment1;
				this.PtComment2 = PtComment2;
				this.NUnitName = NUnitName;
				this.Room = Room;
				this.Bed = Bed;
				this.PtType = PtType;
				this.EADT = EADT;
				this.TempPatient = TempPatient;
				this.Waste = Waste;
				this.PtMergeId = PtMergeId;
				this.HasBeenArchived = HasBeenArchived;
				this.NextExpireTime = NextExpireTime;
				this.CostPerIssue = CostPerIssue;
				this.CostPerUnitRefill = CostPerUnitRefill;
				this.CostPerUnitOrder = CostPerUnitOrder;
				this.AreaName = AreaName;
				this.ItemUnitOfIssue = ItemUnitOfIssue;
				this.ItemUnitOfRefill = ItemUnitOfRefill;
				this.ItemUnitOfOrder = ItemUnitOfOrder;
				this.TrxUOM = TrxUOM;
				this.AdmitDrName = AdmitDrName;
				this.AttendDrName = AttendDrName;
				this.KitId = KitId;
				this.FacilityCode = FacilityCode;
				this.OrigVendSeq = OrigVendSeq;
				this.OrigVendDate = OrigVendDate;
				this.IsBillable = IsBillable;
				this.CostCenter = CostCenter;
				this.VendorPartNumber = VendorPartNumber;
				this.TimeBias = TimeBias;
				this.ItemIsChargeable = ItemIsChargeable;
				this.ItemStock = ItemStock;
				this.RefillId = RefillId;
				this.RefillOrderedQty = RefillOrderedQty;
				this.RefillShippedQty = RefillShippedQty;
				this.BtnPocketNbr = BtnPocketNbr;
				this.XfrToStation = XfrToStation;
				this.CaseId = CaseId;
				this.CaseName = CaseName;
				this.CaseCart = CaseCart;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(string DeviceName, int BtnBoardNbr, int SubDrawer, string PktDescriptor, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from LAST_POCKET_ACCESS WHERE DeviceName='{0}' AND BtnBoardNbr='{1}' AND SubDrawer='{2}' AND PktDescriptor='{3}'", 
				MainClass.FixStringForSingleQuote(DeviceName), (int)BtnBoardNbr, (int)SubDrawer, MainClass.FixStringForSingleQuote(PktDescriptor));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "LastPocketAccess", "GetRecord", out _conn, out myDataReader))
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
				myDataReader["DeviceName"].ToString()
				, MainClass.ToDate(TableName, myDataReader["TxTime"])
				, MainClass.ToInt(TableName, myDataReader["BtnDoorNbr"])
				, MainClass.ToInt(TableName, myDataReader["BtnBoardNbr"])
				, MainClass.ToInt(TableName, myDataReader["BtnBoardSeq"])
				, MainClass.ToInt(TableName, myDataReader["SubDrawer"])
				, myDataReader["PktDescriptor"].ToString()
				, MainClass.ToInt(TableName, myDataReader["TrxSeq"])
				, MainClass.ToInt(TableName, myDataReader["ChassisSeqNbr"])
				, MainClass.ToInt(TableName, myDataReader["BtnColumnNbr"])
				, MainClass.ToInt(TableName, myDataReader["MsgTypeId"])
				, myDataReader["ItemName"].ToString()
				, myDataReader["ItemShortDesc"].ToString()
				, myDataReader["ItemAlias"].ToString()
				, myDataReader["ItemId"].ToString()
				, myDataReader["ItemAltId"].ToString()
				, myDataReader["ItemAltId2"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ItemClass"])
				, myDataReader["ItemClassName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ItemType"])
				, MainClass.ToInt(TableName, myDataReader["ExpBeginCount"])
				, MainClass.ToInt(TableName, myDataReader["ActualBeginCount"])
				, MainClass.ToInt(TableName, myDataReader["TrxQty"])
				, MainClass.ToInt(TableName, myDataReader["PktPhysMaxQty"])
				, MainClass.ToInt(TableName, myDataReader["PktParQty"])
				, MainClass.ToInt(TableName, myDataReader["PktRefillPoint"])
				, MainClass.ToInt(TableName, myDataReader["PktCritLow"])
				, MainClass.ToInt(TableName, myDataReader["StnTotal"])
				, MainClass.ToDate(TableName, myDataReader["PostDateTime"])
				, myDataReader["LotNumber"].ToString()
				, myDataReader["SerialNumber"].ToString()
				, myDataReader["VendorName"].ToString()
				, myDataReader["UserName"].ToString()
				, myDataReader["UserId"].ToString()
				, MainClass.ToInt(TableName, myDataReader["TempUser"])
				, myDataReader["WitnName"].ToString()
				, myDataReader["WitnId"].ToString()
				, myDataReader["PtLastName"].ToString()
				, myDataReader["PtFirstName"].ToString()
				, myDataReader["PtMiddleName"].ToString()
				, myDataReader["PtId"].ToString()
				, myDataReader["PtAltId1"].ToString()
				, myDataReader["PtAltId2"].ToString()
				, myDataReader["PtComment1"].ToString()
				, myDataReader["PtComment2"].ToString()
				, myDataReader["NUnitName"].ToString()
				, myDataReader["Room"].ToString()
				, myDataReader["Bed"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PtType"])
				, MainClass.ToInt(TableName, myDataReader["EADT"])
				, MainClass.ToInt(TableName, myDataReader["TempPatient"])
				, myDataReader["Waste"].ToString()
				, myDataReader["PtMergeId"].ToString()
				, MainClass.ToInt(TableName, myDataReader["HasBeenArchived"])
				, MainClass.ToDate(TableName, myDataReader["NextExpireTime"])
				, MainClass.ToDecimal(TableName, myDataReader["CostPerIssue"])
				, MainClass.ToDecimal(TableName, myDataReader["CostPerUnitRefill"])
				, MainClass.ToDecimal(TableName, myDataReader["CostPerUnitOrder"])
				, myDataReader["AreaName"].ToString()
				, myDataReader["ItemUnitOfIssue"].ToString()
				, myDataReader["ItemUnitOfRefill"].ToString()
				, myDataReader["ItemUnitOfOrder"].ToString()
				, MainClass.ToInt(TableName, myDataReader["TrxUOM"])
				, myDataReader["AdmitDrName"].ToString()
				, myDataReader["AttendDrName"].ToString()
				, myDataReader["KitId"].ToString()
				, myDataReader["FacilityCode"].ToString()
				, MainClass.ToInt(TableName, myDataReader["OrigVendSeq"])
				, MainClass.ToDate(TableName, myDataReader["OrigVendDate"])
				, MainClass.ToInt(TableName, myDataReader["IsBillable"])
				, myDataReader["CostCenter"].ToString()
				, myDataReader["VendorPartNumber"].ToString()
				, MainClass.ToInt(TableName, myDataReader["TimeBias"])
				, MainClass.ToInt(TableName, myDataReader["ItemIsChargeable"])
				, MainClass.ToInt(TableName, myDataReader["ItemStock"])
				, myDataReader["RefillId"].ToString()
				, MainClass.ToInt(TableName, myDataReader["RefillOrderedQty"])
				, MainClass.ToInt(TableName, myDataReader["RefillShippedQty"])
				, MainClass.ToInt(TableName, myDataReader["BtnPocketNbr"])
				, myDataReader["XfrToStation"].ToString()
				, myDataReader["CaseId"].ToString()
				, myDataReader["CaseName"].ToString()
				, myDataReader["CaseCart"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO LAST_POCKET_ACCESS (DeviceName, TxTime, BtnDoorNbr, BtnBoardNbr, BtnBoardSeq, SubDrawer, PktDescriptor, ChassisSeqNbr, BtnColumnNbr, MsgTypeId, ItemName, ItemShortDesc, ItemAlias, ItemId, ItemAltId, ItemAltId2, ItemClass, ItemClassName, ItemType, ExpBeginCount, ActualBeginCount, TrxQty, PktPhysMaxQty, PktParQty, PktRefillPoint, PktCritLow, StnTotal, PostDateTime, LotNumber, SerialNumber, VendorName, UserName, UserId, TempUser, WitnName, WitnId, PtLastName, PtFirstName, PtMiddleName, PtId, PtAltId1, PtAltId2, PtComment1, PtComment2, NUnitName, Room, Bed, PtType, EADT, TempPatient, Waste, PtMergeId, HasBeenArchived, NextExpireTime, CostPerIssue, CostPerUnitRefill, CostPerUnitOrder, AreaName, ItemUnitOfIssue, ItemUnitOfRefill, ItemUnitOfOrder, TrxUOM, AdmitDrName, AttendDrName, KitId, FacilityCode, OrigVendSeq, OrigVendDate, IsBillable, CostCenter, VendorPartNumber, TimeBias, ItemIsChargeable, ItemStock, RefillId, RefillOrderedQty, RefillShippedQty, BtnPocketNbr, XfrToStation, CaseId, CaseName, CaseCart) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.DeviceName) + "'" + ", " + MainClass.DateTimeToTimestamp(data.TxTime) + ", " + (int)data.BtnDoorNbr + ", " + (int)data.BtnBoardNbr + ", " + (int)data.BtnBoardSeq + ", " + (int)data.SubDrawer + ", " + "'" + MainClass.FixStringForSingleQuote(data.PktDescriptor) + "'" + ", " + (int)data.ChassisSeqNbr + ", " + (int)data.BtnColumnNbr + ", " + (int)data.MsgTypeId + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemShortDesc) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemAlias) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemAltId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemAltId2) + "'" + ", " + (int)data.ItemClass + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemClassName) + "'" + ", " + (int)data.ItemType + ", " + (int)data.ExpBeginCount + ", " + (int)data.ActualBeginCount + ", " + (int)data.TrxQty + ", " + (int)data.PktPhysMaxQty + ", " + (int)data.PktParQty + ", " + (int)data.PktRefillPoint + ", " + (int)data.PktCritLow + ", " + (int)data.StnTotal + ", " + MainClass.DateTimeToTimestamp(data.PostDateTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.LotNumber) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SerialNumber) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.VendorName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserId) + "'" + ", " + (int)data.TempUser + ", " + "'" + MainClass.FixStringForSingleQuote(data.WitnName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.WitnId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtLastName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtFirstName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtMiddleName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtAltId1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtAltId2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtComment1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtComment2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.NUnitName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Room) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Bed) + "'" + ", " + (int)data.PtType + ", " + (int)data.EADT + ", " + (int)data.TempPatient + ", " + "'" + MainClass.FixStringForSingleQuote(data.Waste) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtMergeId) + "'" + ", " + (int)data.HasBeenArchived + ", " + MainClass.DateTimeToTimestamp(data.NextExpireTime) + ", " + data.CostPerIssue + ", " + data.CostPerUnitRefill + ", " + data.CostPerUnitOrder + ", " + "'" + MainClass.FixStringForSingleQuote(data.AreaName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemUnitOfIssue) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemUnitOfRefill) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemUnitOfOrder) + "'" + ", " + (int)data.TrxUOM + ", " + "'" + MainClass.FixStringForSingleQuote(data.AdmitDrName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.AttendDrName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.KitId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.FacilityCode) + "'" + ", " + (int)data.OrigVendSeq + ", " + MainClass.DateTimeToTimestamp(data.OrigVendDate) + ", " + (int)data.IsBillable + ", " + "'" + MainClass.FixStringForSingleQuote(data.CostCenter) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.VendorPartNumber) + "'" + ", " + (int)data.TimeBias + ", " + (int)data.ItemIsChargeable + ", " + (int)data.ItemStock + ", " + "'" + MainClass.FixStringForSingleQuote(data.RefillId) + "'" + ", " + (int)data.RefillOrderedQty + ", " + (int)data.RefillShippedQty + ", " + (int)data.BtnPocketNbr + ", " + "'" + MainClass.FixStringForSingleQuote(data.XfrToStation) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseCart) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "LastPocketAccess", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(string DeviceName, int BtnBoardNbr, int SubDrawer, string PktDescriptor)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE LAST_POCKET_ACCESS WHERE DeviceName='{0}' AND BtnBoardNbr='{1}' AND SubDrawer='{2}' AND PktDescriptor='{3}'", 
				MainClass.FixStringForSingleQuote(DeviceName), (int)BtnBoardNbr, (int)SubDrawer, MainClass.FixStringForSingleQuote(PktDescriptor));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "LastPocketAccess", "DeleteRecord");
			return Retval;
		}


	}
}
