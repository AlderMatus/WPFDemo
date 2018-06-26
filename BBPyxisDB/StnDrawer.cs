using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class StnDrawer
	{
		const string TableName = "STN_DRAWER";

		// collection of record fields
		public class TableData
		{
			public int DeviceIid;
			public int Drawer;
			public int SubDrawer;
			public int PktTotal;
			public int DrawerStatus;
			public int DrawerAddress;
			public int DrawerType;

			public TableData(int DeviceIid, int Drawer, int SubDrawer, int PktTotal, int DrawerStatus, int DrawerAddress, int DrawerType)
			{
				this.DeviceIid = DeviceIid;
				this.Drawer = Drawer;
				this.SubDrawer = SubDrawer;
				this.PktTotal = PktTotal;
				this.DrawerStatus = DrawerStatus;
				this.DrawerAddress = DrawerAddress;
				this.DrawerType = DrawerType;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int DeviceIid, int Drawer, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from STN_DRAWER WHERE DeviceIid='{0}' AND Drawer='{1}'", 
				(int)DeviceIid, (int)Drawer);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "StnDrawer", "GetRecord", out _conn, out myDataReader))
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
				, MainClass.ToInt(TableName, myDataReader["Drawer"])
				, MainClass.ToInt(TableName, myDataReader["SubDrawer"])
				, MainClass.ToInt(TableName, myDataReader["PktTotal"])
				, MainClass.ToInt(TableName, myDataReader["DrawerStatus"])
				, MainClass.ToInt(TableName, myDataReader["DrawerAddress"])
				, MainClass.ToInt(TableName, myDataReader["DrawerType"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO STN_DRAWER (DeviceIid, Drawer, SubDrawer, PktTotal, DrawerStatus, DrawerAddress, DrawerType) VALUES ("
				+ (int)data.DeviceIid + ", " + (int)data.Drawer + ", " + (int)data.SubDrawer + ", " + (int)data.PktTotal + ", " + (int)data.DrawerStatus + ", " + (int)data.DrawerAddress + ", " + (int)data.DrawerType + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "StnDrawer", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int DeviceIid, int Drawer)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE STN_DRAWER WHERE DeviceIid='{0}' AND Drawer='{1}'", 
				(int)DeviceIid, (int)Drawer);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "StnDrawer", "DeleteRecord");
			return Retval;
		}


	}
}
