using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class StationOptions
	{
		const string TableName = "STATION_OPTIONS";

		// collection of record fields
		public class TableData
		{
			public int DeviceIid;
			public int LoginMode;
			public int PrintTx;
			public int IntegWaste;
			public int VerifyCount;
			public int AnchorPage;
			public int PtKeepTime;
			public int TxHold;
			public int RtsMode;
			public int PtDelay;
			public int ConfirmESP;
			public int RptSortByItemId;
			public int RptSuppressUserId;
			public int MenuTimeOut;
			public int EventTimeOut;
			public int AnchorTimeOut;
			public int ServiceStatus;
			public int ServiceStatusReq;
			public int RetDrawer;
			public string RetPocket;
			public string StnAreaType;

			public TableData(int DeviceIid, int LoginMode, int PrintTx, int IntegWaste, int VerifyCount, int AnchorPage, int PtKeepTime, int TxHold, int RtsMode, int PtDelay, int ConfirmESP, int RptSortByItemId, int RptSuppressUserId, int MenuTimeOut, int EventTimeOut, int AnchorTimeOut, int ServiceStatus, int ServiceStatusReq, int RetDrawer, string RetPocket, string StnAreaType)
			{
				this.DeviceIid = DeviceIid;
				this.LoginMode = LoginMode;
				this.PrintTx = PrintTx;
				this.IntegWaste = IntegWaste;
				this.VerifyCount = VerifyCount;
				this.AnchorPage = AnchorPage;
				this.PtKeepTime = PtKeepTime;
				this.TxHold = TxHold;
				this.RtsMode = RtsMode;
				this.PtDelay = PtDelay;
				this.ConfirmESP = ConfirmESP;
				this.RptSortByItemId = RptSortByItemId;
				this.RptSuppressUserId = RptSuppressUserId;
				this.MenuTimeOut = MenuTimeOut;
				this.EventTimeOut = EventTimeOut;
				this.AnchorTimeOut = AnchorTimeOut;
				this.ServiceStatus = ServiceStatus;
				this.ServiceStatusReq = ServiceStatusReq;
				this.RetDrawer = RetDrawer;
				this.RetPocket = RetPocket;
				this.StnAreaType = StnAreaType;
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
			string SqlStatement = string.Format("SELECT * from STATION_OPTIONS WHERE DeviceIid='{0}'", 
				(int)DeviceIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "StationOptions", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["DeviceIid"])
				, MainClass.ToInt(TableName, myDataReader["LoginMode"])
				, MainClass.ToInt(TableName, myDataReader["PrintTx"])
				, MainClass.ToInt(TableName, myDataReader["IntegWaste"])
				, MainClass.ToInt(TableName, myDataReader["VerifyCount"])
				, MainClass.ToInt(TableName, myDataReader["AnchorPage"])
				, MainClass.ToInt(TableName, myDataReader["PtKeepTime"])
				, MainClass.ToInt(TableName, myDataReader["TxHold"])
				, MainClass.ToInt(TableName, myDataReader["RtsMode"])
				, MainClass.ToInt(TableName, myDataReader["PtDelay"])
				, MainClass.ToInt(TableName, myDataReader["ConfirmESP"])
				, MainClass.ToInt(TableName, myDataReader["RptSortByItemId"])
				, MainClass.ToInt(TableName, myDataReader["RptSuppressUserId"])
				, MainClass.ToInt(TableName, myDataReader["MenuTimeOut"])
				, MainClass.ToInt(TableName, myDataReader["EventTimeOut"])
				, MainClass.ToInt(TableName, myDataReader["AnchorTimeOut"])
				, MainClass.ToInt(TableName, myDataReader["ServiceStatus"])
				, MainClass.ToInt(TableName, myDataReader["ServiceStatusReq"])
				, MainClass.ToInt(TableName, myDataReader["RetDrawer"])
				, myDataReader["RetPocket"].ToString()
				, myDataReader["StnAreaType"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO STATION_OPTIONS (DeviceIid, LoginMode, PrintTx, IntegWaste, VerifyCount, AnchorPage, PtKeepTime, TxHold, RtsMode, PtDelay, ConfirmESP, RptSortByItemId, RptSuppressUserId, MenuTimeOut, EventTimeOut, AnchorTimeOut, ServiceStatus, ServiceStatusReq, RetDrawer, RetPocket, StnAreaType) VALUES ("
				+ (int)data.DeviceIid + ", " + (int)data.LoginMode + ", " + (int)data.PrintTx + ", " + (int)data.IntegWaste + ", " + (int)data.VerifyCount + ", " + (int)data.AnchorPage + ", " + (int)data.PtKeepTime + ", " + (int)data.TxHold + ", " + (int)data.RtsMode + ", " + (int)data.PtDelay + ", " + (int)data.ConfirmESP + ", " + (int)data.RptSortByItemId + ", " + (int)data.RptSuppressUserId + ", " + (int)data.MenuTimeOut + ", " + (int)data.EventTimeOut + ", " + (int)data.AnchorTimeOut + ", " + (int)data.ServiceStatus + ", " + (int)data.ServiceStatusReq + ", " + (int)data.RetDrawer + ", " + "'" + MainClass.FixStringForSingleQuote(data.RetPocket) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.StnAreaType) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "StationOptions", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int DeviceIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE STATION_OPTIONS WHERE DeviceIid='{0}'", 
				(int)DeviceIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "StationOptions", "DeleteRecord");
			return Retval;
		}


	}
}
