using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ItemHazardDefs
	{
		const string TableName = "ITEM_HAZARD_DEFS";

		// collection of record fields
		public class TableData
		{
			public int ItemHazardIid;
			public string ItemHazardName;
			public string ItemHazardWarningText;
			public int ItemWarningTextColor;
			public int ItemWarningAudibleAlarm;

			public TableData(int ItemHazardIid, string ItemHazardName, string ItemHazardWarningText, int ItemWarningTextColor, int ItemWarningAudibleAlarm)
			{
				this.ItemHazardIid = ItemHazardIid;
				this.ItemHazardName = ItemHazardName;
				this.ItemHazardWarningText = ItemHazardWarningText;
				this.ItemWarningTextColor = ItemWarningTextColor;
				this.ItemWarningAudibleAlarm = ItemWarningAudibleAlarm;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int ItemHazardIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ITEM_HAZARD_DEFS WHERE ItemHazardIid='{0}'", 
				(int)ItemHazardIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ItemHazardDefs", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["ItemHazardIid"])
				, myDataReader["ItemHazardName"].ToString()
				, myDataReader["ItemHazardWarningText"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ItemWarningTextColor"])
				, MainClass.ToInt(TableName, myDataReader["ItemWarningAudibleAlarm"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO ITEM_HAZARD_DEFS (ItemHazardName, ItemHazardWarningText, ItemWarningTextColor, ItemWarningAudibleAlarm) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.ItemHazardName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemHazardWarningText) + "'" + ", " + (int)data.ItemWarningTextColor + ", " + (int)data.ItemWarningAudibleAlarm + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemHazardDefs", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int ItemHazardIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ITEM_HAZARD_DEFS WHERE ItemHazardIid='{0}'", 
				(int)ItemHazardIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemHazardDefs", "DeleteRecord");
			return Retval;
		}


	}
}
