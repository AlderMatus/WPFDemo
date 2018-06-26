using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class DoorAccessExclusion
	{
		const string TableName = "DOOR_ACCESS_EXCLUSION";

		// collection of record fields
		public class TableData
		{
			public int UserIid;
			public int AccessTypeIid;
			public int DeviceIid;
			public int BtnBoardNbr;

			public TableData(int UserIid, int AccessTypeIid, int DeviceIid, int BtnBoardNbr)
			{
				this.UserIid = UserIid;
				this.AccessTypeIid = AccessTypeIid;
				this.DeviceIid = DeviceIid;
				this.BtnBoardNbr = BtnBoardNbr;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int UserIid, int AccessTypeIid, int DeviceIid, int BtnBoardNbr, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from DOOR_ACCESS_EXCLUSION WHERE UserIid='{0}' AND AccessTypeIid='{1}' AND DeviceIid='{2}' AND BtnBoardNbr='{3}'", 
				(int)UserIid, (int)AccessTypeIid, (int)DeviceIid, (int)BtnBoardNbr);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "DoorAccessExclusion", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["UserIid"])
				, MainClass.ToInt(TableName, myDataReader["AccessTypeIid"])
				, MainClass.ToInt(TableName, myDataReader["DeviceIid"])
				, MainClass.ToInt(TableName, myDataReader["BtnBoardNbr"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO DOOR_ACCESS_EXCLUSION (UserIid, AccessTypeIid, DeviceIid, BtnBoardNbr) VALUES ("
				+ (int)data.UserIid + ", " + (int)data.AccessTypeIid + ", " + (int)data.DeviceIid + ", " + (int)data.BtnBoardNbr + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "DoorAccessExclusion", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int UserIid, int AccessTypeIid, int DeviceIid, int BtnBoardNbr)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE DOOR_ACCESS_EXCLUSION WHERE UserIid='{0}' AND AccessTypeIid='{1}' AND DeviceIid='{2}' AND BtnBoardNbr='{3}'", 
				(int)UserIid, (int)AccessTypeIid, (int)DeviceIid, (int)BtnBoardNbr);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "DoorAccessExclusion", "DeleteRecord");
			return Retval;
		}


	}
}
