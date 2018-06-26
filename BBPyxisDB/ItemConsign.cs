using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ItemConsign
	{
		const string TableName = "ITEM_CONSIGN";

		// collection of record fields
		public class TableData
		{
			public int ItemConsign;
			public string ItemConsignText;

			public TableData(int ItemConsign, string ItemConsignText)
			{
				this.ItemConsign = ItemConsign;
				this.ItemConsignText = ItemConsignText;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int ItemConsign, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ITEM_CONSIGN WHERE ItemConsign='{0}'", 
				(int)ItemConsign);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ItemConsign", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["ItemConsign"])
				, myDataReader["ItemConsignText"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO ITEM_CONSIGN (ItemConsign, ItemConsignText) VALUES ("
				+ (int)data.ItemConsign + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemConsignText) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemConsign", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int ItemConsign)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ITEM_CONSIGN WHERE ItemConsign='{0}'", 
				(int)ItemConsign);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemConsign", "DeleteRecord");
			return Retval;
		}


	}
}
