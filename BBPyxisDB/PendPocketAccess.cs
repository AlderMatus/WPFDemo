using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class PendPocketAccess
	{
		const string TableName = "PEND_POCKET_ACCESS";

		// collection of record fields
		public class TableData
		{
			public DateTime TxTime;
			public int NSequenceNum;
			public int TxType;
			public int NRetryCount;
			public int NAddress;
			public int NBin;
			public int NAmount;
			public int NPktFinal;
			public int NPktBegin;
			public int NPktEdit;
			public int NPktIid;
			public int NPatientIid;
			public int NItemIid;
			public int NStationIid;
			public int NStationAreaIid;
			public int TempUser;
			public int NStationTotal;
			public string Descriptor;
			public string StrSerial;
			public string StrLot;
			public string UserName;
			public string UserId;
			public decimal TxCost;
			public DateTime PostDate;
			public int LProcedureIid;
			public int LServiceIid;
			public int LPhysicianIid;
			public int BProcedureMgmt;
			public int NewLotSerial;
			public int BVendforCase;
			public int NKitIid;
			public int BForeignVend;
			public int DoorNo;
			public int DevNo;
			public int ChassisId;
			public int Column;
			public int PocketIid;
			public string ItemID;
			public string ForeignStationName;
			public int RefillOrderedQty;
			public int RefillShippedQty;
			public int NMessageType;
			public string RefillId;
			public string StrReason;

			public TableData(DateTime TxTime, int NSequenceNum, int TxType, int NRetryCount, int NAddress, int NBin, int NAmount, int NPktFinal, int NPktBegin, int NPktEdit, int NPktIid, int NPatientIid, int NItemIid, int NStationIid, int NStationAreaIid, int TempUser, int NStationTotal, string Descriptor, string StrSerial, string StrLot, string UserName, string UserId, decimal TxCost, DateTime PostDate, int LProcedureIid, int LServiceIid, int LPhysicianIid, int BProcedureMgmt, int NewLotSerial, int BVendforCase, int NKitIid, int BForeignVend, int DoorNo, int DevNo, int ChassisId, int Column, int PocketIid, string ItemID, string ForeignStationName, int RefillOrderedQty, int RefillShippedQty, int NMessageType, string RefillId, string StrReason)
			{
				this.TxTime = TxTime;
				this.NSequenceNum = NSequenceNum;
				this.TxType = TxType;
				this.NRetryCount = NRetryCount;
				this.NAddress = NAddress;
				this.NBin = NBin;
				this.NAmount = NAmount;
				this.NPktFinal = NPktFinal;
				this.NPktBegin = NPktBegin;
				this.NPktEdit = NPktEdit;
				this.NPktIid = NPktIid;
				this.NPatientIid = NPatientIid;
				this.NItemIid = NItemIid;
				this.NStationIid = NStationIid;
				this.NStationAreaIid = NStationAreaIid;
				this.TempUser = TempUser;
				this.NStationTotal = NStationTotal;
				this.Descriptor = Descriptor;
				this.StrSerial = StrSerial;
				this.StrLot = StrLot;
				this.UserName = UserName;
				this.UserId = UserId;
				this.TxCost = TxCost;
				this.PostDate = PostDate;
				this.LProcedureIid = LProcedureIid;
				this.LServiceIid = LServiceIid;
				this.LPhysicianIid = LPhysicianIid;
				this.BProcedureMgmt = BProcedureMgmt;
				this.NewLotSerial = NewLotSerial;
				this.BVendforCase = BVendforCase;
				this.NKitIid = NKitIid;
				this.BForeignVend = BForeignVend;
				this.DoorNo = DoorNo;
				this.DevNo = DevNo;
				this.ChassisId = ChassisId;
				this.Column = Column;
				this.PocketIid = PocketIid;
				this.ItemID = ItemID;
				this.ForeignStationName = ForeignStationName;
				this.RefillOrderedQty = RefillOrderedQty;
				this.RefillShippedQty = RefillShippedQty;
				this.NMessageType = NMessageType;
				this.RefillId = RefillId;
				this.StrReason = StrReason;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(DateTime TxTime, int NSequenceNum, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PEND_POCKET_ACCESS WHERE TxTime='{0}' AND NSequenceNum='{1}'", 
				MainClass.DateTimeToTimestamp(TxTime), (int)NSequenceNum);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PendPocketAccess", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToDate(TableName, myDataReader["TxTime"])
				, MainClass.ToInt(TableName, myDataReader["NSequenceNum"])
				, MainClass.ToInt(TableName, myDataReader["TxType"])
				, MainClass.ToInt(TableName, myDataReader["NRetryCount"])
				, MainClass.ToInt(TableName, myDataReader["NAddress"])
				, MainClass.ToInt(TableName, myDataReader["NBin"])
				, MainClass.ToInt(TableName, myDataReader["NAmount"])
				, MainClass.ToInt(TableName, myDataReader["NPktFinal"])
				, MainClass.ToInt(TableName, myDataReader["NPktBegin"])
				, MainClass.ToInt(TableName, myDataReader["NPktEdit"])
				, MainClass.ToInt(TableName, myDataReader["NPktIid"])
				, MainClass.ToInt(TableName, myDataReader["NPatientIid"])
				, MainClass.ToInt(TableName, myDataReader["NItemIid"])
				, MainClass.ToInt(TableName, myDataReader["NStationIid"])
				, MainClass.ToInt(TableName, myDataReader["NStationAreaIid"])
				, MainClass.ToInt(TableName, myDataReader["TempUser"])
				, MainClass.ToInt(TableName, myDataReader["NStationTotal"])
				, myDataReader["Descriptor"].ToString()
				, myDataReader["StrSerial"].ToString()
				, myDataReader["StrLot"].ToString()
				, myDataReader["UserName"].ToString()
				, myDataReader["UserId"].ToString()
				, MainClass.ToDecimal(TableName, myDataReader["TxCost"])
				, MainClass.ToDate(TableName, myDataReader["PostDate"])
				, MainClass.ToInt(TableName, myDataReader["LProcedureIid"])
				, MainClass.ToInt(TableName, myDataReader["LServiceIid"])
				, MainClass.ToInt(TableName, myDataReader["LPhysicianIid"])
				, MainClass.ToInt(TableName, myDataReader["BProcedureMgmt"])
				, MainClass.ToInt(TableName, myDataReader["NewLotSerial"])
				, MainClass.ToInt(TableName, myDataReader["BVendforCase"])
				, MainClass.ToInt(TableName, myDataReader["NKitIid"])
				, MainClass.ToInt(TableName, myDataReader["BForeignVend"])
				, MainClass.ToInt(TableName, myDataReader["DoorNo"])
				, MainClass.ToInt(TableName, myDataReader["DevNo"])
				, MainClass.ToInt(TableName, myDataReader["ChassisId"])
				, MainClass.ToInt(TableName, myDataReader["Column"])
				, MainClass.ToInt(TableName, myDataReader["PocketIid"])
				, myDataReader["ItemID"].ToString()
				, myDataReader["ForeignStationName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["RefillOrderedQty"])
				, MainClass.ToInt(TableName, myDataReader["RefillShippedQty"])
				, MainClass.ToInt(TableName, myDataReader["NMessageType"])
				, myDataReader["RefillId"].ToString()
				, myDataReader["StrReason"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO PEND_POCKET_ACCESS (TxTime, NSequenceNum, TxType, NRetryCount, NAddress, NBin, NAmount, NPktFinal, NPktBegin, NPktEdit, NPktIid, NPatientIid, NItemIid, NStationIid, NStationAreaIid, TempUser, NStationTotal, Descriptor, StrSerial, StrLot, UserName, UserId, TxCost, PostDate, LProcedureIid, LServiceIid, LPhysicianIid, BProcedureMgmt, NewLotSerial, BVendforCase, NKitIid, BForeignVend, DoorNo, DevNo, ChassisId, Column, PocketIid, ItemID, ForeignStationName, RefillOrderedQty, RefillShippedQty, NMessageType, RefillId, StrReason) VALUES ("
				+ MainClass.DateTimeToTimestamp(data.TxTime) + ", " + (int)data.NSequenceNum + ", " + (int)data.TxType + ", " + (int)data.NRetryCount + ", " + (int)data.NAddress + ", " + (int)data.NBin + ", " + (int)data.NAmount + ", " + (int)data.NPktFinal + ", " + (int)data.NPktBegin + ", " + (int)data.NPktEdit + ", " + (int)data.NPktIid + ", " + (int)data.NPatientIid + ", " + (int)data.NItemIid + ", " + (int)data.NStationIid + ", " + (int)data.NStationAreaIid + ", " + (int)data.TempUser + ", " + (int)data.NStationTotal + ", " + "'" + MainClass.FixStringForSingleQuote(data.Descriptor) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.StrSerial) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.StrLot) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserId) + "'" + ", " + data.TxCost + ", " + MainClass.DateTimeToTimestamp(data.PostDate) + ", " + (int)data.LProcedureIid + ", " + (int)data.LServiceIid + ", " + (int)data.LPhysicianIid + ", " + (int)data.BProcedureMgmt + ", " + (int)data.NewLotSerial + ", " + (int)data.BVendforCase + ", " + (int)data.NKitIid + ", " + (int)data.BForeignVend + ", " + (int)data.DoorNo + ", " + (int)data.DevNo + ", " + (int)data.ChassisId + ", " + (int)data.Column + ", " + (int)data.PocketIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemID) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ForeignStationName) + "'" + ", " + (int)data.RefillOrderedQty + ", " + (int)data.RefillShippedQty + ", " + (int)data.NMessageType + ", " + "'" + MainClass.FixStringForSingleQuote(data.RefillId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.StrReason) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PendPocketAccess", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(DateTime TxTime, int NSequenceNum)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PEND_POCKET_ACCESS WHERE TxTime='{0}' AND NSequenceNum='{1}'", 
				MainClass.DateTimeToTimestamp(TxTime), (int)NSequenceNum);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PendPocketAccess", "DeleteRecord");
			return Retval;
		}


	}
}
