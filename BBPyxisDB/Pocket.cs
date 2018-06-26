using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Pocket
	{
		const string TableName = "POCKET";

		// collection of record fields
		public class TableData
		{
			public int PktIid;
			public int DeviceIid;
			public int BtnBoardNbr;
			public int BtnDoorNbr;
			public int BtnBoardSeq;
			public int SubDrawer;
			public string PktDescriptor;
			public int PktState;
			public int BtnPocketNbr;
			public int ItemIid;
			public int PktPhysMaxQty;
			public int PktParQty;
			public int PktRefillPoint;
			public int PktCurQty;
			public int PktCritLow;
			public int ItemIsStandard;
			public int ItemIsChargeable;
			public int ItemIsPerishable;
			public DateTime NextExpireTime;
			public DateTime LastVend;
			public DateTime LastRefill;
			public DateTime LastStockOut;
			public DateTime LastInventory;
			public int DefaultVendUOM;
			public int DefaultRefillUOM;
			public int IncludeInDOP;
			public string PocketUnitOfIssue;
			public int PocketUOIRatio;
			public int Shared;
			public int CycleCountType;
			public int ToBeCycleCounted;

			public TableData(int PktIid, int DeviceIid, int BtnBoardNbr, int BtnDoorNbr, int BtnBoardSeq, int SubDrawer, string PktDescriptor, int PktState, int BtnPocketNbr, int ItemIid, int PktPhysMaxQty, int PktParQty, int PktRefillPoint, int PktCurQty, int PktCritLow, int ItemIsStandard, int ItemIsChargeable, int ItemIsPerishable, DateTime NextExpireTime, DateTime LastVend, DateTime LastRefill, DateTime LastStockOut, DateTime LastInventory, int DefaultVendUOM, int DefaultRefillUOM, int IncludeInDOP, string PocketUnitOfIssue, int PocketUOIRatio, int Shared, int CycleCountType, int ToBeCycleCounted)
			{
				this.PktIid = PktIid;
				this.DeviceIid = DeviceIid;
				this.BtnBoardNbr = BtnBoardNbr;
				this.BtnDoorNbr = BtnDoorNbr;
				this.BtnBoardSeq = BtnBoardSeq;
				this.SubDrawer = SubDrawer;
				this.PktDescriptor = PktDescriptor;
				this.PktState = PktState;
				this.BtnPocketNbr = BtnPocketNbr;
				this.ItemIid = ItemIid;
				this.PktPhysMaxQty = PktPhysMaxQty;
				this.PktParQty = PktParQty;
				this.PktRefillPoint = PktRefillPoint;
				this.PktCurQty = PktCurQty;
				this.PktCritLow = PktCritLow;
				this.ItemIsStandard = ItemIsStandard;
				this.ItemIsChargeable = ItemIsChargeable;
				this.ItemIsPerishable = ItemIsPerishable;
				this.NextExpireTime = NextExpireTime;
				this.LastVend = LastVend;
				this.LastRefill = LastRefill;
				this.LastStockOut = LastStockOut;
				this.LastInventory = LastInventory;
				this.DefaultVendUOM = DefaultVendUOM;
				this.DefaultRefillUOM = DefaultRefillUOM;
				this.IncludeInDOP = IncludeInDOP;
				this.PocketUnitOfIssue = PocketUnitOfIssue;
				this.PocketUOIRatio = PocketUOIRatio;
				this.Shared = Shared;
				this.CycleCountType = CycleCountType;
				this.ToBeCycleCounted = ToBeCycleCounted;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int PktIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from POCKET WHERE PktIid='{0}'", 
				(int)PktIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Pocket", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["PktIid"])
				, MainClass.ToInt(TableName, myDataReader["DeviceIid"])
				, MainClass.ToInt(TableName, myDataReader["BtnBoardNbr"])
				, MainClass.ToInt(TableName, myDataReader["BtnDoorNbr"])
				, MainClass.ToInt(TableName, myDataReader["BtnBoardSeq"])
				, MainClass.ToInt(TableName, myDataReader["SubDrawer"])
				, myDataReader["PktDescriptor"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PktState"])
				, MainClass.ToInt(TableName, myDataReader["BtnPocketNbr"])
				, MainClass.ToInt(TableName, myDataReader["ItemIid"])
				, MainClass.ToInt(TableName, myDataReader["PktPhysMaxQty"])
				, MainClass.ToInt(TableName, myDataReader["PktParQty"])
				, MainClass.ToInt(TableName, myDataReader["PktRefillPoint"])
				, MainClass.ToInt(TableName, myDataReader["PktCurQty"])
				, MainClass.ToInt(TableName, myDataReader["PktCritLow"])
				, MainClass.ToInt(TableName, myDataReader["ItemIsStandard"])
				, MainClass.ToInt(TableName, myDataReader["ItemIsChargeable"])
				, MainClass.ToInt(TableName, myDataReader["ItemIsPerishable"])
				, MainClass.ToDate(TableName, myDataReader["NextExpireTime"])
				, MainClass.ToDate(TableName, myDataReader["LastVend"])
				, MainClass.ToDate(TableName, myDataReader["LastRefill"])
				, MainClass.ToDate(TableName, myDataReader["LastStockOut"])
				, MainClass.ToDate(TableName, myDataReader["LastInventory"])
				, MainClass.ToInt(TableName, myDataReader["DefaultVendUOM"])
				, MainClass.ToInt(TableName, myDataReader["DefaultRefillUOM"])
				, MainClass.ToInt(TableName, myDataReader["IncludeInDOP"])
				, myDataReader["PocketUnitOfIssue"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PocketUOIRatio"])
				, MainClass.ToInt(TableName, myDataReader["Shared"])
				, MainClass.ToInt(TableName, myDataReader["CycleCountType"])
				, MainClass.ToInt(TableName, myDataReader["ToBeCycleCounted"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO POCKET (DeviceIid, BtnBoardNbr, BtnDoorNbr, BtnBoardSeq, SubDrawer, PktDescriptor, PktState, BtnPocketNbr, ItemIid, PktPhysMaxQty, PktParQty, PktRefillPoint, PktCurQty, PktCritLow, ItemIsStandard, ItemIsChargeable, ItemIsPerishable, NextExpireTime, LastVend, LastRefill, LastStockOut, LastInventory, DefaultVendUOM, DefaultRefillUOM, IncludeInDOP, PocketUnitOfIssue, PocketUOIRatio, Shared, CycleCountType, ToBeCycleCounted) VALUES ("
				+ (int)data.DeviceIid + ", " + (int)data.BtnBoardNbr + ", " + (int)data.BtnDoorNbr + ", " + (int)data.BtnBoardSeq + ", " + (int)data.SubDrawer + ", " + "'" + MainClass.FixStringForSingleQuote(data.PktDescriptor) + "'" + ", " + (int)data.PktState + ", " + (int)data.BtnPocketNbr + ", " + (int)data.ItemIid + ", " + (int)data.PktPhysMaxQty + ", " + (int)data.PktParQty + ", " + (int)data.PktRefillPoint + ", " + (int)data.PktCurQty + ", " + (int)data.PktCritLow + ", " + (int)data.ItemIsStandard + ", " + (int)data.ItemIsChargeable + ", " + (int)data.ItemIsPerishable + ", " + MainClass.DateTimeToTimestamp(data.NextExpireTime) + ", " + MainClass.DateTimeToTimestamp(data.LastVend) + ", " + MainClass.DateTimeToTimestamp(data.LastRefill) + ", " + MainClass.DateTimeToTimestamp(data.LastStockOut) + ", " + MainClass.DateTimeToTimestamp(data.LastInventory) + ", " + (int)data.DefaultVendUOM + ", " + (int)data.DefaultRefillUOM + ", " + (int)data.IncludeInDOP + ", " + "'" + MainClass.FixStringForSingleQuote(data.PocketUnitOfIssue) + "'" + ", " + (int)data.PocketUOIRatio + ", " + (int)data.Shared + ", " + (int)data.CycleCountType + ", " + (int)data.ToBeCycleCounted + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Pocket", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int PktIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE POCKET WHERE PktIid='{0}'", 
				(int)PktIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Pocket", "DeleteRecord");
			return Retval;
		}


	}
}
