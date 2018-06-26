using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class BtnBoard
	{
		const string TableName = "BTN_BOARD";

		// collection of record fields
		public class TableData
		{
			public int BtnBoardNbr;
			public int DeviceIid;
			public int MicroId;
			public int StnChassisIid;
			public int BtnDoorNbr;
			public string BtnBoardLocDescrip;
			public int BtnBoardStatus;
			public int BtnBoardType;
			public int BtnAccessoryType;
			public int BtnBoardSeq;
			public int BtnBarMap;
			public int BtnColumnNbr;
			public int BtnDrawerNbr;
			public int BtnShortBars;
			public int Shared;

			public TableData(int BtnBoardNbr, int DeviceIid, int MicroId, int StnChassisIid, int BtnDoorNbr, string BtnBoardLocDescrip, int BtnBoardStatus, int BtnBoardType, int BtnAccessoryType, int BtnBoardSeq, int BtnBarMap, int BtnColumnNbr, int BtnDrawerNbr, int BtnShortBars, int Shared)
			{
				this.BtnBoardNbr = BtnBoardNbr;
				this.DeviceIid = DeviceIid;
				this.MicroId = MicroId;
				this.StnChassisIid = StnChassisIid;
				this.BtnDoorNbr = BtnDoorNbr;
				this.BtnBoardLocDescrip = BtnBoardLocDescrip;
				this.BtnBoardStatus = BtnBoardStatus;
				this.BtnBoardType = BtnBoardType;
				this.BtnAccessoryType = BtnAccessoryType;
				this.BtnBoardSeq = BtnBoardSeq;
				this.BtnBarMap = BtnBarMap;
				this.BtnColumnNbr = BtnColumnNbr;
				this.BtnDrawerNbr = BtnDrawerNbr;
				this.BtnShortBars = BtnShortBars;
				this.Shared = Shared;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int BtnBoardNbr, int DeviceIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from BTN_BOARD WHERE BtnBoardNbr='{0}' AND DeviceIid='{1}'", 
				(int)BtnBoardNbr, (int)DeviceIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "BtnBoard", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["BtnBoardNbr"])
				, MainClass.ToInt(TableName, myDataReader["DeviceIid"])
				, MainClass.ToInt(TableName, myDataReader["MicroId"])
				, MainClass.ToInt(TableName, myDataReader["StnChassisIid"])
				, MainClass.ToInt(TableName, myDataReader["BtnDoorNbr"])
				, myDataReader["BtnBoardLocDescrip"].ToString()
				, MainClass.ToInt(TableName, myDataReader["BtnBoardStatus"])
				, MainClass.ToInt(TableName, myDataReader["BtnBoardType"])
				, MainClass.ToInt(TableName, myDataReader["BtnAccessoryType"])
				, MainClass.ToInt(TableName, myDataReader["BtnBoardSeq"])
				, MainClass.ToInt(TableName, myDataReader["BtnBarMap"])
				, MainClass.ToInt(TableName, myDataReader["BtnColumnNbr"])
				, MainClass.ToInt(TableName, myDataReader["BtnDrawerNbr"])
				, MainClass.ToInt(TableName, myDataReader["BtnShortBars"])
				, MainClass.ToInt(TableName, myDataReader["Shared"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO BTN_BOARD (BtnBoardNbr, DeviceIid, MicroId, StnChassisIid, BtnDoorNbr, BtnBoardLocDescrip, BtnBoardStatus, BtnBoardType, BtnAccessoryType, BtnBoardSeq, BtnBarMap, BtnColumnNbr, BtnDrawerNbr, BtnShortBars, Shared) VALUES ("
				+ (int)data.BtnBoardNbr + ", " + (int)data.DeviceIid + ", " + (int)data.MicroId + ", " + (int)data.StnChassisIid + ", " + (int)data.BtnDoorNbr + ", " + "'" + MainClass.FixStringForSingleQuote(data.BtnBoardLocDescrip) + "'" + ", " + (int)data.BtnBoardStatus + ", " + (int)data.BtnBoardType + ", " + (int)data.BtnAccessoryType + ", " + (int)data.BtnBoardSeq + ", " + (int)data.BtnBarMap + ", " + (int)data.BtnColumnNbr + ", " + (int)data.BtnDrawerNbr + ", " + (int)data.BtnShortBars + ", " + (int)data.Shared + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "BtnBoard", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int BtnBoardNbr, int DeviceIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE BTN_BOARD WHERE BtnBoardNbr='{0}' AND DeviceIid='{1}'", 
				(int)BtnBoardNbr, (int)DeviceIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "BtnBoard", "DeleteRecord");
			return Retval;
		}


	}
}
