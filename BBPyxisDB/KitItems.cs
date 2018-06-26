using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class KitItems
	{
		const string TableName = "KIT_ITEMS";

		// collection of record fields
		public class TableData
		{
			public int KitIid;
			public int ItemIid;
			public int KitQty;
			public string KitEditorName;
			public string KitEditorId;

			public TableData(int KitIid, int ItemIid, int KitQty, string KitEditorName, string KitEditorId)
			{
				this.KitIid = KitIid;
				this.ItemIid = ItemIid;
				this.KitQty = KitQty;
				this.KitEditorName = KitEditorName;
				this.KitEditorId = KitEditorId;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int KitIid, int ItemIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from KIT_ITEMS WHERE KitIid='{0}' AND ItemIid='{1}'", 
				(int)KitIid, (int)ItemIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "KitItems", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["KitIid"])
				, MainClass.ToInt(TableName, myDataReader["ItemIid"])
				, MainClass.ToInt(TableName, myDataReader["KitQty"])
				, myDataReader["KitEditorName"].ToString()
				, myDataReader["KitEditorId"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO KIT_ITEMS (KitIid, ItemIid, KitQty, KitEditorName, KitEditorId) VALUES ("
				+ (int)data.KitIid + ", " + (int)data.ItemIid + ", " + (int)data.KitQty + ", " + "'" + MainClass.FixStringForSingleQuote(data.KitEditorName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.KitEditorId) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "KitItems", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int KitIid, int ItemIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE KIT_ITEMS WHERE KitIid='{0}' AND ItemIid='{1}'", 
				(int)KitIid, (int)ItemIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "KitItems", "DeleteRecord");
			return Retval;
		}


	}
}
