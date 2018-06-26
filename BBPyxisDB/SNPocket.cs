using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class SNPocket
	{
		const string TableName = "SN_POCKET";

		// collection of record fields
		public class TableData
		{
			public int PktSNIid;
			public int CabinetId;
			public int MicroId;
			public int Drawer;
			public int SubDrawer;
			public string PktDescriptor;
			public int PktState;
			public int PktNumber;
			public int ItemIid;
			public int PktPhysMaxQty;
			public int PktParQty;
			public int PktRefillPoint;
			public int PktCurQty;
			public int PktCritLow;
			public int ItemIsStandard;
			public int ItemIsChargeable;
			public DateTime LastVend;
			public DateTime LastRefill;
			public DateTime LastStockOut;
			public DateTime LastInventory;
			public string PocketUnitOfIssue;
			public int PocketUOIRatio;
			public int PktPosLo;
			public int PktPosHi;
			public int IncludeInDOP;

			public TableData(int PktSNIid, int CabinetId, int MicroId, int Drawer, int SubDrawer, string PktDescriptor, int PktState, int PktNumber, int ItemIid, int PktPhysMaxQty, int PktParQty, int PktRefillPoint, int PktCurQty, int PktCritLow, int ItemIsStandard, int ItemIsChargeable, DateTime LastVend, DateTime LastRefill, DateTime LastStockOut, DateTime LastInventory, string PocketUnitOfIssue, int PocketUOIRatio, int PktPosLo, int PktPosHi, int IncludeInDOP)
			{
				this.PktSNIid = PktSNIid;
				this.CabinetId = CabinetId;
				this.MicroId = MicroId;
				this.Drawer = Drawer;
				this.SubDrawer = SubDrawer;
				this.PktDescriptor = PktDescriptor;
				this.PktState = PktState;
				this.PktNumber = PktNumber;
				this.ItemIid = ItemIid;
				this.PktPhysMaxQty = PktPhysMaxQty;
				this.PktParQty = PktParQty;
				this.PktRefillPoint = PktRefillPoint;
				this.PktCurQty = PktCurQty;
				this.PktCritLow = PktCritLow;
				this.ItemIsStandard = ItemIsStandard;
				this.ItemIsChargeable = ItemIsChargeable;
				this.LastVend = LastVend;
				this.LastRefill = LastRefill;
				this.LastStockOut = LastStockOut;
				this.LastInventory = LastInventory;
				this.PocketUnitOfIssue = PocketUnitOfIssue;
				this.PocketUOIRatio = PocketUOIRatio;
				this.PktPosLo = PktPosLo;
				this.PktPosHi = PktPosHi;
				this.IncludeInDOP = IncludeInDOP;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int PktSNIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from SN_POCKET WHERE PktSNIid='{0}'", 
				(int)PktSNIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "SNPocket", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["PktSNIid"])
				, MainClass.ToInt(TableName, myDataReader["CabinetId"])
				, MainClass.ToInt(TableName, myDataReader["MicroId"])
				, MainClass.ToInt(TableName, myDataReader["Drawer"])
				, MainClass.ToInt(TableName, myDataReader["SubDrawer"])
				, myDataReader["PktDescriptor"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PktState"])
				, MainClass.ToInt(TableName, myDataReader["PktNumber"])
				, MainClass.ToInt(TableName, myDataReader["ItemIid"])
				, MainClass.ToInt(TableName, myDataReader["PktPhysMaxQty"])
				, MainClass.ToInt(TableName, myDataReader["PktParQty"])
				, MainClass.ToInt(TableName, myDataReader["PktRefillPoint"])
				, MainClass.ToInt(TableName, myDataReader["PktCurQty"])
				, MainClass.ToInt(TableName, myDataReader["PktCritLow"])
				, MainClass.ToInt(TableName, myDataReader["ItemIsStandard"])
				, MainClass.ToInt(TableName, myDataReader["ItemIsChargeable"])
				, MainClass.ToDate(TableName, myDataReader["LastVend"])
				, MainClass.ToDate(TableName, myDataReader["LastRefill"])
				, MainClass.ToDate(TableName, myDataReader["LastStockOut"])
				, MainClass.ToDate(TableName, myDataReader["LastInventory"])
				, myDataReader["PocketUnitOfIssue"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PocketUOIRatio"])
				, MainClass.ToInt(TableName, myDataReader["PktPosLo"])
				, MainClass.ToInt(TableName, myDataReader["PktPosHi"])
				, MainClass.ToInt(TableName, myDataReader["IncludeInDOP"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO SN_POCKET (CabinetId, MicroId, Drawer, SubDrawer, PktDescriptor, PktState, PktNumber, ItemIid, PktPhysMaxQty, PktParQty, PktRefillPoint, PktCurQty, PktCritLow, ItemIsStandard, ItemIsChargeable, LastVend, LastRefill, LastStockOut, LastInventory, PocketUnitOfIssue, PocketUOIRatio, PktPosLo, PktPosHi, IncludeInDOP) VALUES ("
				+ (int)data.CabinetId + ", " + (int)data.MicroId + ", " + (int)data.Drawer + ", " + (int)data.SubDrawer + ", " + "'" + MainClass.FixStringForSingleQuote(data.PktDescriptor) + "'" + ", " + (int)data.PktState + ", " + (int)data.PktNumber + ", " + (int)data.ItemIid + ", " + (int)data.PktPhysMaxQty + ", " + (int)data.PktParQty + ", " + (int)data.PktRefillPoint + ", " + (int)data.PktCurQty + ", " + (int)data.PktCritLow + ", " + (int)data.ItemIsStandard + ", " + (int)data.ItemIsChargeable + ", " + MainClass.DateTimeToTimestamp(data.LastVend) + ", " + MainClass.DateTimeToTimestamp(data.LastRefill) + ", " + MainClass.DateTimeToTimestamp(data.LastStockOut) + ", " + MainClass.DateTimeToTimestamp(data.LastInventory) + ", " + "'" + MainClass.FixStringForSingleQuote(data.PocketUnitOfIssue) + "'" + ", " + (int)data.PocketUOIRatio + ", " + (int)data.PktPosLo + ", " + (int)data.PktPosHi + ", " + (int)data.IncludeInDOP + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "SNPocket", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int PktSNIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE SN_POCKET WHERE PktSNIid='{0}'", 
				(int)PktSNIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "SNPocket", "DeleteRecord");
			return Retval;
		}


	}
}
