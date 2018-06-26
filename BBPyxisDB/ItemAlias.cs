using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ItemAlias
	{
		const string TableName = "ITEM_ALIAS";

		// collection of record fields
		public class TableData
		{
			public string ItemAlias;
			public string ItemId;

			public TableData(string ItemAlias, string ItemId)
			{
				this.ItemAlias = ItemAlias;
				this.ItemId = ItemId;
			}
		}

		// return record given its primary key
		public static bool GetRecord(string ItemAlias, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ITEM_ALIAS WHERE ItemAlias='{0}'", 
				MainClass.FixStringForSingleQuote(ItemAlias));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ItemAlias", "GetRecord", out _conn, out myDataReader))
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
				myDataReader["ItemAlias"].ToString()
				, myDataReader["ItemId"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO ITEM_ALIAS (ItemAlias, ItemId) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.ItemAlias) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemId) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemAlias", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(string ItemAlias)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ITEM_ALIAS WHERE ItemAlias='{0}'", 
				MainClass.FixStringForSingleQuote(ItemAlias));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemAlias", "DeleteRecord");
			return Retval;
		}


	}
}
