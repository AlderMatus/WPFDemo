using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class SNDrawer
	{
		const string TableName = "SN_DRAWER";

		// collection of record fields
		public class TableData
		{
			public int CabinetId;
			public int MicroId;
			public string DrawerName;
			public int Drawer;
			public int SubDrawer;
			public int BtnBoardNbr;
			public int BtnBoardType;
			public int MaxPockets;

			public TableData(int CabinetId, int MicroId, string DrawerName, int Drawer, int SubDrawer, int BtnBoardNbr, int BtnBoardType, int MaxPockets)
			{
				this.CabinetId = CabinetId;
				this.MicroId = MicroId;
				this.DrawerName = DrawerName;
				this.Drawer = Drawer;
				this.SubDrawer = SubDrawer;
				this.BtnBoardNbr = BtnBoardNbr;
				this.BtnBoardType = BtnBoardType;
				this.MaxPockets = MaxPockets;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int MicroId, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from SN_DRAWER WHERE MicroId='{0}'", 
				(int)MicroId);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "SNDrawer", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["CabinetId"])
				, MainClass.ToInt(TableName, myDataReader["MicroId"])
				, myDataReader["DrawerName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Drawer"])
				, MainClass.ToInt(TableName, myDataReader["SubDrawer"])
				, MainClass.ToInt(TableName, myDataReader["BtnBoardNbr"])
				, MainClass.ToInt(TableName, myDataReader["BtnBoardType"])
				, MainClass.ToInt(TableName, myDataReader["MaxPockets"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO SN_DRAWER (CabinetId, MicroId, DrawerName, Drawer, SubDrawer, BtnBoardNbr, BtnBoardType, MaxPockets) VALUES ("
				+ (int)data.CabinetId + ", " + (int)data.MicroId + ", " + "'" + MainClass.FixStringForSingleQuote(data.DrawerName) + "'" + ", " + (int)data.Drawer + ", " + (int)data.SubDrawer + ", " + (int)data.BtnBoardNbr + ", " + (int)data.BtnBoardType + ", " + (int)data.MaxPockets + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "SNDrawer", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int MicroId)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE SN_DRAWER WHERE MicroId='{0}'", 
				(int)MicroId);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "SNDrawer", "DeleteRecord");
			return Retval;
		}


	}
}
