using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Devices
	{
		const string TableName = "DEVICES";

		// collection of record fields
		public class TableData
		{
			public int DeviceIid;
			public int AreaIid;
			public string DeviceName;
			public int DeviceType;
			public string SiteID;
			public string FacilityCode;
			public string Version;
			public int Zone;
			public int CallStart0;
			public int CallStop0;
			public int CallStart1;
			public int CallStop1;
			public int CallStart2;
			public int CallStop2;
			public int CallStart3;
			public int CallStop3;
			public int CallStart4;
			public int CallStop4;
			public int CallStart5;
			public int CallStop5;
			public int CallStart6;
			public int CallStop6;
			public string Token0;
			public string Token1;
			public string Token2;
			public string Method;
			public int Polling;
			public int PollFail;
			public int PollData;
			public int PollNoData;
			public string Route0;
			public string Route1;
			public string Route2;
			public string Route3;
			public string CommBox;
			public string ProcessBox;
			public int ProcarMsgFwdGroup;
			public string BulletinPrinter;
			public int ProcedureStation;
            public int bulletinPrinterIid;
            public string deviceGUID;
            public int useGLCodeTransactions;


            public TableData(int DeviceIid, int AreaIid, string DeviceName, int DeviceType, string SiteID, string FacilityCode, string Version, int Zone, int CallStart0, int CallStop0, int CallStart1, int CallStop1, int CallStart2, int CallStop2, int CallStart3, int CallStop3, int CallStart4, int CallStop4, int CallStart5, int CallStop5, int CallStart6, int CallStop6, string Token0, string Token1, string Token2, string Method, int Polling, int PollFail, int PollData, int PollNoData, string Route0, string Route1, string Route2, string Route3, string CommBox, string ProcessBox, int ProcarMsgFwdGroup, string BulletinPrinter, int ProcedureStation, int bulletinPrinterIid, string deviceGUID, int useGLCodeTransactions)
			{
				this.DeviceIid = DeviceIid;
				this.AreaIid = AreaIid;
				this.DeviceName = DeviceName;
				this.DeviceType = DeviceType;
				this.SiteID = SiteID;
				this.FacilityCode = FacilityCode;
				this.Version = Version;
				this.Zone = Zone;
				this.CallStart0 = CallStart0;
				this.CallStop0 = CallStop0;
				this.CallStart1 = CallStart1;
				this.CallStop1 = CallStop1;
				this.CallStart2 = CallStart2;
				this.CallStop2 = CallStop2;
				this.CallStart3 = CallStart3;
				this.CallStop3 = CallStop3;
				this.CallStart4 = CallStart4;
				this.CallStop4 = CallStop4;
				this.CallStart5 = CallStart5;
				this.CallStop5 = CallStop5;
				this.CallStart6 = CallStart6;
				this.CallStop6 = CallStop6;
				this.Token0 = Token0;
				this.Token1 = Token1;
				this.Token2 = Token2;
				this.Method = Method;
				this.Polling = Polling;
				this.PollFail = PollFail;
				this.PollData = PollData;
				this.PollNoData = PollNoData;
				this.Route0 = Route0;
				this.Route1 = Route1;
				this.Route2 = Route2;
				this.Route3 = Route3;
				this.CommBox = CommBox;
				this.ProcessBox = ProcessBox;
				this.ProcarMsgFwdGroup = ProcarMsgFwdGroup;
				this.BulletinPrinter = BulletinPrinter;
				this.ProcedureStation = ProcedureStation;
                this.bulletinPrinterIid = bulletinPrinterIid;
                this.deviceGUID = deviceGUID;
                this.useGLCodeTransactions = useGLCodeTransactions;
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
			string SqlStatement = string.Format("SELECT * from DEVICES WHERE DeviceIid='{0}'", 
				(int)DeviceIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Devices", "GetRecord", out _conn, out myDataReader))
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
        // return record given its device name
        public static bool GetRecord(string DeviceName, out TableData data)
        {
            bool Retval = true;
            data = null;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from DEVICES WHERE deviceName='{0}'",
                DeviceName);
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Devices", "GetRecord", out _conn, out myDataReader))
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
				, MainClass.ToInt(TableName, myDataReader["AreaIid"])
				, myDataReader["DeviceName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["DeviceType"])
				, myDataReader["SiteID"].ToString()
				, myDataReader["FacilityCode"].ToString()
				, myDataReader["Version"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Zone"])
				, MainClass.ToInt(TableName, myDataReader["CallStart0"])
				, MainClass.ToInt(TableName, myDataReader["CallStop0"])
				, MainClass.ToInt(TableName, myDataReader["CallStart1"])
				, MainClass.ToInt(TableName, myDataReader["CallStop1"])
				, MainClass.ToInt(TableName, myDataReader["CallStart2"])
				, MainClass.ToInt(TableName, myDataReader["CallStop2"])
				, MainClass.ToInt(TableName, myDataReader["CallStart3"])
				, MainClass.ToInt(TableName, myDataReader["CallStop3"])
				, MainClass.ToInt(TableName, myDataReader["CallStart4"])
				, MainClass.ToInt(TableName, myDataReader["CallStop4"])
				, MainClass.ToInt(TableName, myDataReader["CallStart5"])
				, MainClass.ToInt(TableName, myDataReader["CallStop5"])
				, MainClass.ToInt(TableName, myDataReader["CallStart6"])
				, MainClass.ToInt(TableName, myDataReader["CallStop6"])
				, myDataReader["Token0"].ToString()
				, myDataReader["Token1"].ToString()
				, myDataReader["Token2"].ToString()
				, myDataReader["Method"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Polling"])
				, MainClass.ToInt(TableName, myDataReader["PollFail"])
				, MainClass.ToInt(TableName, myDataReader["PollData"])
				, MainClass.ToInt(TableName, myDataReader["PollNoData"])
				, myDataReader["Route0"].ToString()
				, myDataReader["Route1"].ToString()
				, myDataReader["Route2"].ToString()
				, myDataReader["Route3"].ToString()
				, myDataReader["CommBox"].ToString()
				, myDataReader["ProcessBox"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ProcarMsgFwdGroup"])
				, myDataReader["BulletinPrinter"].ToString()
                , MainClass.ToInt(TableName, myDataReader["ProcedureStation"])
                , MainClass.ToInt(TableName, myDataReader["bulletinPrinterIid"])
                , myDataReader["deviceGUID"].ToString()
                , MainClass.ToInt(TableName, myDataReader["useGLCodeTransactions"])
                );
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

            string SqlStatement = "INSERT INTO DEVICES (AreaIid, DeviceName, DeviceType, SiteID, FacilityCode, Version, Zone, CallStart0, CallStop0, CallStart1, CallStop1, CallStart2, CallStop2, CallStart3, CallStop3, CallStart4, CallStop4, CallStart5, CallStop5, CallStart6, CallStop6, Token0, Token1, Token2, Method, Polling, PollFail, PollData, PollNoData, Route0, Route1, Route2, Route3, CommBox, ProcessBox, ProcarMsgFwdGroup, BulletinPrinter, ProcedureStation, bulletinPrinterIid, deviceGUID, bulletinPrinterIid, useGLCodeTransactions) VALUES ("
				+ (int)data.AreaIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.DeviceName) + "'" + ", " + (int)data.DeviceType + ", " + "'" + MainClass.FixStringForSingleQuote(data.SiteID) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.FacilityCode) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Version) + "'" + ", " + (int)data.Zone + ", " + (int)data.CallStart0 + ", " + (int)data.CallStop0 + ", " + (int)data.CallStart1 + ", " + (int)data.CallStop1 + ", " + (int)data.CallStart2 
                + ", " + (int)data.CallStop2 + ", " + (int)data.CallStart3 + ", " + (int)data.CallStop3 + ", " + (int)data.CallStart4 + ", " + (int)data.CallStop4 + ", " + (int)data.CallStart5 + ", " + (int)data.CallStop5 + ", " + (int)data.CallStart6 + ", " + (int)data.CallStop6 + ", " + "'" + MainClass.FixStringForSingleQuote(data.Token0) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Token1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Token2) + "'" + ", " + "'" 
                + MainClass.FixStringForSingleQuote(data.Method) + "'" + ", " + (int)data.Polling + ", " + (int)data.PollFail + ", " + (int)data.PollData + ", " + (int)data.PollNoData + ", " + "'" + MainClass.FixStringForSingleQuote(data.Route0) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Route1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Route2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Route3) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CommBox) 
                + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ProcessBox) + "'" + ", " + (int)data.ProcarMsgFwdGroup + ", " + "'" + MainClass.FixStringForSingleQuote(data.BulletinPrinter) + "'" + ", " + (int)data.ProcedureStation
                + "," + (int)data.bulletinPrinterIid + "," + "'" + MainClass.FixStringForSingleQuote(data.deviceGUID) + "'" + ", " + (int)data.useGLCodeTransactions + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Devices", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int DeviceIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE DEVICES WHERE DeviceIid='{0}'", 
				(int)DeviceIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Devices", "DeleteRecord");
			return Retval;
		}


	}
}
