using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ItemsToPickArea
	{
		const string TableName = "ITEMS_TO_PICK_AREA";

		// collection of record fields
		public class TableData
		{
			public int PickAreaIid;
			public int ItemIid;
			public int PickAreaPriority;
			public int VendorIid;
			public string PickAreaLoc;

			public TableData(int PickAreaIid, int ItemIid, int PickAreaPriority, int VendorIid, string PickAreaLoc)
			{
				this.PickAreaIid = PickAreaIid;
				this.ItemIid = ItemIid;
				this.PickAreaPriority = PickAreaPriority;
				this.VendorIid = VendorIid;
				this.PickAreaLoc = PickAreaLoc;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int ItemIid, int PickAreaIid, int PickAreaPriority, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ITEMS_TO_PICK_AREA WHERE ItemIid='{0}' AND PickAreaIid='{1}' AND PickAreaPriority='{2}'", 
				(int)ItemIid, (int)PickAreaIid, (int)PickAreaPriority);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ItemsToPickArea", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["PickAreaIid"])
				, MainClass.ToInt(TableName, myDataReader["ItemIid"])
				, MainClass.ToInt(TableName, myDataReader["PickAreaPriority"])
				, MainClass.ToInt(TableName, myDataReader["VendorIid"])
				, myDataReader["PickAreaLoc"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO ITEMS_TO_PICK_AREA (PickAreaIid, ItemIid, PickAreaPriority, VendorIid, PickAreaLoc) VALUES ("
				+ (int)data.PickAreaIid + ", " + (int)data.ItemIid + ", " + (int)data.PickAreaPriority + ", " + (int)data.VendorIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.PickAreaLoc) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemsToPickArea", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int ItemIid, int PickAreaIid, int PickAreaPriority)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ITEMS_TO_PICK_AREA WHERE ItemIid='{0}' AND PickAreaIid='{1}' AND PickAreaPriority='{2}'", 
				(int)ItemIid, (int)PickAreaIid, (int)PickAreaPriority);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemsToPickArea", "DeleteRecord");
			return Retval;
		}


	}
}
