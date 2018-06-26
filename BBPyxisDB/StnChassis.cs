using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class StnChassis
	{
		const string TableName = "STN_CHASSIS";

		// collection of record fields
		public class TableData
		{
			public int StnChassisIid;
			public int DeviceIid;
			public int ChassisSeqNbr;
			public string SerialNumber;
			public int ChassisType;
			public string ChassisDescrip;
			public int StartLocation;
			public int EndLocation;
			public DateTime InstallDate;
			public DateTime LastServiceDate;
			public int DoorBoardsRight;
			public int DoorBoardsLeft;
			public string CustAdminProdNbr;

			public TableData(int StnChassisIid, int DeviceIid, int ChassisSeqNbr, string SerialNumber, int ChassisType, string ChassisDescrip, int StartLocation, int EndLocation, DateTime InstallDate, DateTime LastServiceDate, int DoorBoardsRight, int DoorBoardsLeft, string CustAdminProdNbr)
			{
				this.StnChassisIid = StnChassisIid;
				this.DeviceIid = DeviceIid;
				this.ChassisSeqNbr = ChassisSeqNbr;
				this.SerialNumber = SerialNumber;
				this.ChassisType = ChassisType;
				this.ChassisDescrip = ChassisDescrip;
				this.StartLocation = StartLocation;
				this.EndLocation = EndLocation;
				this.InstallDate = InstallDate;
				this.LastServiceDate = LastServiceDate;
				this.DoorBoardsRight = DoorBoardsRight;
				this.DoorBoardsLeft = DoorBoardsLeft;
				this.CustAdminProdNbr = CustAdminProdNbr;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int StnChassisIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from STN_CHASSIS WHERE StnChassisIid='{0}'", 
				(int)StnChassisIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "StnChassis", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["StnChassisIid"])
				, MainClass.ToInt(TableName, myDataReader["DeviceIid"])
				, MainClass.ToInt(TableName, myDataReader["ChassisSeqNbr"])
				, myDataReader["SerialNumber"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ChassisType"])
				, myDataReader["ChassisDescrip"].ToString()
				, MainClass.ToInt(TableName, myDataReader["StartLocation"])
				, MainClass.ToInt(TableName, myDataReader["EndLocation"])
				, MainClass.ToDate(TableName, myDataReader["InstallDate"])
				, MainClass.ToDate(TableName, myDataReader["LastServiceDate"])
				, MainClass.ToInt(TableName, myDataReader["DoorBoardsRight"])
				, MainClass.ToInt(TableName, myDataReader["DoorBoardsLeft"])
				, myDataReader["CustAdminProdNbr"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO STN_CHASSIS (DeviceIid, ChassisSeqNbr, SerialNumber, ChassisType, ChassisDescrip, StartLocation, EndLocation, InstallDate, LastServiceDate, DoorBoardsRight, DoorBoardsLeft, CustAdminProdNbr) VALUES ("
				+ (int)data.DeviceIid + ", " + (int)data.ChassisSeqNbr + ", " + "'" + MainClass.FixStringForSingleQuote(data.SerialNumber) + "'" + ", " + (int)data.ChassisType + ", " + "'" + MainClass.FixStringForSingleQuote(data.ChassisDescrip) + "'" + ", " + (int)data.StartLocation + ", " + (int)data.EndLocation + ", " + MainClass.DateTimeToTimestamp(data.InstallDate) + ", " + MainClass.DateTimeToTimestamp(data.LastServiceDate) + ", " + (int)data.DoorBoardsRight + ", " + (int)data.DoorBoardsLeft + ", " + "'" + MainClass.FixStringForSingleQuote(data.CustAdminProdNbr) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "StnChassis", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int StnChassisIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE STN_CHASSIS WHERE StnChassisIid='{0}'", 
				(int)StnChassisIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "StnChassis", "DeleteRecord");
			return Retval;
		}


	}
}
