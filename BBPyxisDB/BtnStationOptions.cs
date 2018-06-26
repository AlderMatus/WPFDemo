using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class BtnStationOptions
	{
		const string TableName = "BTN_STATION_OPTIONS";

		// collection of record fields
		public class TableData
		{
			public int DeviceIid;
			public int LoginMode;
			public int PrintVend;
			public int PrintRefill;
			public int PrintLoadUnload;
			public int PrintInventory;
			public int PrintDiscard;
			public int PrintExpireItem;
			public int PrintTransfer;
			public int PrintCancelTrx;
			public int PrintKit;
			public int IntfcStatusBanner;
			public int VerifyCount;
			public int AnchorPage;
			public int TrxKeepDays;
			public int RtsMode;
			public int PtKeepTime;
			public int MyListKeepTime;
			public int PreAdmit;
			public int PtDeleteDelayHours;
			public int TrxHrsMaxToStnPtr;
			public int MenuTimeOut;
			public int EventTimeOut;
			public int InvRefillEventTimeOut;
			public int AnchorTimeOut;
			public int AnchorTimeOutWarnDelta;
			public int AnchorRequestExtendTime;
			public int ServiceStatus;
			public int ServiceStatusReq;
			public string StnAreaType;
			public int IgnoreLotSerialTracking;
			public int ExpItemCheckLeadDays;
			public int RefillMethod;
			public int NullTrxWarningMode;
			public int EmergAccessMode;
			public int ItemDiscardMode;
			public int PostTimeMode;
			public int PatientIDCheckMode;
			public int PermitGlobalPatientList;
			public int PermitGlobalItemFind;
			public int PermitRequestItem;
			public string StnCareArea;
			public string CostCenter;
			public DateTime MaintStartTime;
			public int PermitMultiPtPick;
			public int Voice;
			public int PktSortBy;
			public int NoPatientMode;
			public int ProcedureMgmt;
			public int ItemShortName;
			public int VerifyCountRefillRequest;
			public string ORName;
			public int BillOnly;
			public int ProcedureTimes;
			public int NonRefillDays;
			public int StockoutBtn;
			public int SerialLotNumVerify;
			public int AllowTakeWithNoLogin;
			public int DefaultTakeAmount;
			public int CaseMgmt;
			public int TempUserKeepTime;
			public int AutoCommitTx;
			public int AutoCommitTxSeconds;
			public int JITrBUDTxListTimeOut;
			public int VerifyReturns;
			public int FloorStockItemWarning;

			public TableData(int DeviceIid, int LoginMode, int PrintVend, int PrintRefill, int PrintLoadUnload, int PrintInventory, int PrintDiscard, int PrintExpireItem, int PrintTransfer, int PrintCancelTrx, int PrintKit, int IntfcStatusBanner, int VerifyCount, int AnchorPage, int TrxKeepDays, int RtsMode, int PtKeepTime, int MyListKeepTime, int PreAdmit, int PtDeleteDelayHours, int TrxHrsMaxToStnPtr, int MenuTimeOut, int EventTimeOut, int InvRefillEventTimeOut, int AnchorTimeOut, int AnchorTimeOutWarnDelta, int AnchorRequestExtendTime, int ServiceStatus, int ServiceStatusReq, string StnAreaType, int IgnoreLotSerialTracking, int ExpItemCheckLeadDays, int RefillMethod, int NullTrxWarningMode, int EmergAccessMode, int ItemDiscardMode, int PostTimeMode, int PatientIDCheckMode, int PermitGlobalPatientList, int PermitGlobalItemFind, int PermitRequestItem, string StnCareArea, string CostCenter, DateTime MaintStartTime, int PermitMultiPtPick, int Voice, int PktSortBy, int NoPatientMode, int ProcedureMgmt, int ItemShortName, int VerifyCountRefillRequest, string ORName, int BillOnly, int ProcedureTimes, int NonRefillDays, int StockoutBtn, int SerialLotNumVerify, int AllowTakeWithNoLogin, int DefaultTakeAmount, int CaseMgmt, int TempUserKeepTime, int AutoCommitTx, int AutoCommitTxSeconds, int JITrBUDTxListTimeOut, int VerifyReturns, int FloorStockItemWarning)
			{
				this.DeviceIid = DeviceIid;
				this.LoginMode = LoginMode;
				this.PrintVend = PrintVend;
				this.PrintRefill = PrintRefill;
				this.PrintLoadUnload = PrintLoadUnload;
				this.PrintInventory = PrintInventory;
				this.PrintDiscard = PrintDiscard;
				this.PrintExpireItem = PrintExpireItem;
				this.PrintTransfer = PrintTransfer;
				this.PrintCancelTrx = PrintCancelTrx;
				this.PrintKit = PrintKit;
				this.IntfcStatusBanner = IntfcStatusBanner;
				this.VerifyCount = VerifyCount;
				this.AnchorPage = AnchorPage;
				this.TrxKeepDays = TrxKeepDays;
				this.RtsMode = RtsMode;
				this.PtKeepTime = PtKeepTime;
				this.MyListKeepTime = MyListKeepTime;
				this.PreAdmit = PreAdmit;
				this.PtDeleteDelayHours = PtDeleteDelayHours;
				this.TrxHrsMaxToStnPtr = TrxHrsMaxToStnPtr;
				this.MenuTimeOut = MenuTimeOut;
				this.EventTimeOut = EventTimeOut;
				this.InvRefillEventTimeOut = InvRefillEventTimeOut;
				this.AnchorTimeOut = AnchorTimeOut;
				this.AnchorTimeOutWarnDelta = AnchorTimeOutWarnDelta;
				this.AnchorRequestExtendTime = AnchorRequestExtendTime;
				this.ServiceStatus = ServiceStatus;
				this.ServiceStatusReq = ServiceStatusReq;
				this.StnAreaType = StnAreaType;
				this.IgnoreLotSerialTracking = IgnoreLotSerialTracking;
				this.ExpItemCheckLeadDays = ExpItemCheckLeadDays;
				this.RefillMethod = RefillMethod;
				this.NullTrxWarningMode = NullTrxWarningMode;
				this.EmergAccessMode = EmergAccessMode;
				this.ItemDiscardMode = ItemDiscardMode;
				this.PostTimeMode = PostTimeMode;
				this.PatientIDCheckMode = PatientIDCheckMode;
				this.PermitGlobalPatientList = PermitGlobalPatientList;
				this.PermitGlobalItemFind = PermitGlobalItemFind;
				this.PermitRequestItem = PermitRequestItem;
				this.StnCareArea = StnCareArea;
				this.CostCenter = CostCenter;
				this.MaintStartTime = MaintStartTime;
				this.PermitMultiPtPick = PermitMultiPtPick;
				this.Voice = Voice;
				this.PktSortBy = PktSortBy;
				this.NoPatientMode = NoPatientMode;
				this.ProcedureMgmt = ProcedureMgmt;
				this.ItemShortName = ItemShortName;
				this.VerifyCountRefillRequest = VerifyCountRefillRequest;
				this.ORName = ORName;
				this.BillOnly = BillOnly;
				this.ProcedureTimes = ProcedureTimes;
				this.NonRefillDays = NonRefillDays;
				this.StockoutBtn = StockoutBtn;
				this.SerialLotNumVerify = SerialLotNumVerify;
				this.AllowTakeWithNoLogin = AllowTakeWithNoLogin;
				this.DefaultTakeAmount = DefaultTakeAmount;
				this.CaseMgmt = CaseMgmt;
				this.TempUserKeepTime = TempUserKeepTime;
				this.AutoCommitTx = AutoCommitTx;
				this.AutoCommitTxSeconds = AutoCommitTxSeconds;
				this.JITrBUDTxListTimeOut = JITrBUDTxListTimeOut;
				this.VerifyReturns = VerifyReturns;
				this.FloorStockItemWarning = FloorStockItemWarning;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int DeviceIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from BTN_STATION_OPTIONS WHERE DeviceIid='{0}'", 
				(int)DeviceIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "BtnStationOptions", "GetRecord", out _conn, out myDataReader))
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

		// make a TableData object from a SADataReader record
#if !NO_ASA
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
			data = new TableData(
				MainClass.ToInt(TableName, myDataReader["DeviceIid"])
				, MainClass.ToInt(TableName, myDataReader["LoginMode"])
				, MainClass.ToInt(TableName, myDataReader["PrintVend"])
				, MainClass.ToInt(TableName, myDataReader["PrintRefill"])
				, MainClass.ToInt(TableName, myDataReader["PrintLoadUnload"])
				, MainClass.ToInt(TableName, myDataReader["PrintInventory"])
				, MainClass.ToInt(TableName, myDataReader["PrintDiscard"])
				, MainClass.ToInt(TableName, myDataReader["PrintExpireItem"])
				, MainClass.ToInt(TableName, myDataReader["PrintTransfer"])
				, MainClass.ToInt(TableName, myDataReader["PrintCancelTrx"])
				, MainClass.ToInt(TableName, myDataReader["PrintKit"])
				, MainClass.ToInt(TableName, myDataReader["IntfcStatusBanner"])
				, MainClass.ToInt(TableName, myDataReader["VerifyCount"])
				, MainClass.ToInt(TableName, myDataReader["AnchorPage"])
				, MainClass.ToInt(TableName, myDataReader["TrxKeepDays"])
				, MainClass.ToInt(TableName, myDataReader["RtsMode"])
				, MainClass.ToInt(TableName, myDataReader["PtKeepTime"])
				, MainClass.ToInt(TableName, myDataReader["MyListKeepTime"])
				, MainClass.ToInt(TableName, myDataReader["PreAdmit"])
				, MainClass.ToInt(TableName, myDataReader["PtDeleteDelayHours"])
				, MainClass.ToInt(TableName, myDataReader["TrxHrsMaxToStnPtr"])
				, MainClass.ToInt(TableName, myDataReader["MenuTimeOut"])
				, MainClass.ToInt(TableName, myDataReader["EventTimeOut"])
				, MainClass.ToInt(TableName, myDataReader["InvRefillEventTimeOut"])
				, MainClass.ToInt(TableName, myDataReader["AnchorTimeOut"])
				, MainClass.ToInt(TableName, myDataReader["AnchorTimeOutWarnDelta"])
				, MainClass.ToInt(TableName, myDataReader["AnchorRequestExtendTime"])
				, MainClass.ToInt(TableName, myDataReader["ServiceStatus"])
				, MainClass.ToInt(TableName, myDataReader["ServiceStatusReq"])
				, myDataReader["StnAreaType"].ToString()
				, MainClass.ToInt(TableName, myDataReader["IgnoreLotSerialTracking"])
				, MainClass.ToInt(TableName, myDataReader["ExpItemCheckLeadDays"])
				, MainClass.ToInt(TableName, myDataReader["RefillMethod"])
				, MainClass.ToInt(TableName, myDataReader["NullTrxWarningMode"])
				, MainClass.ToInt(TableName, myDataReader["EmergAccessMode"])
				, MainClass.ToInt(TableName, myDataReader["ItemDiscardMode"])
				, MainClass.ToInt(TableName, myDataReader["PostTimeMode"])
				, MainClass.ToInt(TableName, myDataReader["PatientIDCheckMode"])
				, MainClass.ToInt(TableName, myDataReader["PermitGlobalPatientList"])
				, MainClass.ToInt(TableName, myDataReader["PermitGlobalItemFind"])
				, MainClass.ToInt(TableName, myDataReader["PermitRequestItem"])
				, myDataReader["StnCareArea"].ToString()
				, myDataReader["CostCenter"].ToString()
				, MainClass.ToDate(TableName, myDataReader["MaintStartTime"])
				, MainClass.ToInt(TableName, myDataReader["PermitMultiPtPick"])
				, MainClass.ToInt(TableName, myDataReader["Voice"])
				, MainClass.ToInt(TableName, myDataReader["PktSortBy"])
				, MainClass.ToInt(TableName, myDataReader["NoPatientMode"])
				, MainClass.ToInt(TableName, myDataReader["ProcedureMgmt"])
				, MainClass.ToInt(TableName, myDataReader["ItemShortName"])
				, MainClass.ToInt(TableName, myDataReader["VerifyCountRefillRequest"])
				, myDataReader["ORName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["BillOnly"])
				, MainClass.ToInt(TableName, myDataReader["ProcedureTimes"])
				, MainClass.ToInt(TableName, myDataReader["NonRefillDays"])
				, MainClass.ToInt(TableName, myDataReader["StockoutBtn"])
				, MainClass.ToInt(TableName, myDataReader["SerialLotNumVerify"])
				, MainClass.ToInt(TableName, myDataReader["AllowTakeWithNoLogin"])
				, MainClass.ToInt(TableName, myDataReader["DefaultTakeAmount"])
				, MainClass.ToInt(TableName, myDataReader["CaseMgmt"])
				, MainClass.ToInt(TableName, myDataReader["TempUserKeepTime"])
				, MainClass.ToInt(TableName, myDataReader["AutoCommitTx"])
				, MainClass.ToInt(TableName, myDataReader["AutoCommitTxSeconds"])
				, MainClass.ToInt(TableName, myDataReader["JITrBUDTxListTimeOut"])
				, MainClass.ToInt(TableName, myDataReader["VerifyReturns"])
				, MainClass.ToInt(TableName, myDataReader["FloorStockItemWarning"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO BTN_STATION_OPTIONS (DeviceIid, LoginMode, PrintVend, PrintRefill, PrintLoadUnload, PrintInventory, PrintDiscard, PrintExpireItem, PrintTransfer, PrintCancelTrx, PrintKit, IntfcStatusBanner, VerifyCount, AnchorPage, TrxKeepDays, RtsMode, PtKeepTime, MyListKeepTime, PreAdmit, PtDeleteDelayHours, TrxHrsMaxToStnPtr, MenuTimeOut, EventTimeOut, InvRefillEventTimeOut, AnchorTimeOut, AnchorTimeOutWarnDelta, AnchorRequestExtendTime, ServiceStatus, ServiceStatusReq, StnAreaType, IgnoreLotSerialTracking, ExpItemCheckLeadDays, RefillMethod, NullTrxWarningMode, EmergAccessMode, ItemDiscardMode, PostTimeMode, PatientIDCheckMode, PermitGlobalPatientList, PermitGlobalItemFind, PermitRequestItem, StnCareArea, CostCenter, MaintStartTime, PermitMultiPtPick, Voice, PktSortBy, NoPatientMode, ProcedureMgmt, ItemShortName, VerifyCountRefillRequest, ORName, BillOnly, ProcedureTimes, NonRefillDays, StockoutBtn, SerialLotNumVerify, AllowTakeWithNoLogin, DefaultTakeAmount, CaseMgmt, TempUserKeepTime, AutoCommitTx, AutoCommitTxSeconds, JITrBUDTxListTimeOut, VerifyReturns, FloorStockItemWarning) VALUES ("
				+ (int)data.DeviceIid + ", " + (int)data.LoginMode + ", " + (int)data.PrintVend + ", " + (int)data.PrintRefill + ", " + (int)data.PrintLoadUnload + ", " + (int)data.PrintInventory + ", " + (int)data.PrintDiscard + ", " + (int)data.PrintExpireItem + ", " + (int)data.PrintTransfer + ", " + (int)data.PrintCancelTrx + ", " + (int)data.PrintKit + ", " + (int)data.IntfcStatusBanner + ", " + (int)data.VerifyCount + ", " + (int)data.AnchorPage + ", " + (int)data.TrxKeepDays + ", " + (int)data.RtsMode + ", " + (int)data.PtKeepTime + ", " + (int)data.MyListKeepTime + ", " + (int)data.PreAdmit + ", " + (int)data.PtDeleteDelayHours + ", " + (int)data.TrxHrsMaxToStnPtr + ", " + (int)data.MenuTimeOut + ", " + (int)data.EventTimeOut + ", " + (int)data.InvRefillEventTimeOut + ", " + (int)data.AnchorTimeOut + ", " + (int)data.AnchorTimeOutWarnDelta + ", " + (int)data.AnchorRequestExtendTime + ", " + (int)data.ServiceStatus + ", " + (int)data.ServiceStatusReq + ", " + "'" + MainClass.FixStringForSingleQuote(data.StnAreaType) + "'" + ", " + (int)data.IgnoreLotSerialTracking + ", " + (int)data.ExpItemCheckLeadDays + ", " + (int)data.RefillMethod + ", " + (int)data.NullTrxWarningMode + ", " + (int)data.EmergAccessMode + ", " + (int)data.ItemDiscardMode + ", " + (int)data.PostTimeMode + ", " + (int)data.PatientIDCheckMode + ", " + (int)data.PermitGlobalPatientList + ", " + (int)data.PermitGlobalItemFind + ", " + (int)data.PermitRequestItem + ", " + "'" + MainClass.FixStringForSingleQuote(data.StnCareArea) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CostCenter) + "'" + ", " + MainClass.DateTimeToTimestamp(data.MaintStartTime) + ", " + (int)data.PermitMultiPtPick + ", " + (int)data.Voice + ", " + (int)data.PktSortBy + ", " + (int)data.NoPatientMode + ", " + (int)data.ProcedureMgmt + ", " + (int)data.ItemShortName + ", " + (int)data.VerifyCountRefillRequest + ", " + "'" + MainClass.FixStringForSingleQuote(data.ORName) + "'" + ", " + (int)data.BillOnly + ", " + (int)data.ProcedureTimes + ", " + (int)data.NonRefillDays + ", " + (int)data.StockoutBtn + ", " + (int)data.SerialLotNumVerify + ", " + (int)data.AllowTakeWithNoLogin + ", " + (int)data.DefaultTakeAmount + ", " + (int)data.CaseMgmt + ", " + (int)data.TempUserKeepTime + ", " + (int)data.AutoCommitTx + ", " + (int)data.AutoCommitTxSeconds + ", " + (int)data.JITrBUDTxListTimeOut + ", " + (int)data.VerifyReturns + ", " + (int)data.FloorStockItemWarning + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "BtnStationOptions", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int DeviceIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE BTN_STATION_OPTIONS WHERE DeviceIid='{0}'", 
				(int)DeviceIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "BtnStationOptions", "DeleteRecord");
			return Retval;
		}


	}
}
