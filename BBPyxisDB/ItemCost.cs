using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ItemCost
	{
		const string TableName = "ITEM_COST";

		// collection of record fields
		public class TableData
		{
			public string ItemId;
			public DateTime EffectTime;
			public decimal CostPerIssue;
			public decimal CostPerUnitOrder;

			public TableData(string ItemId, DateTime EffectTime, decimal CostPerIssue, decimal CostPerUnitOrder)
			{
				this.ItemId = ItemId;
				this.EffectTime = EffectTime;
				this.CostPerIssue = CostPerIssue;
				this.CostPerUnitOrder = CostPerUnitOrder;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(string ItemId, DateTime EffectTime, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ITEM_COST WHERE ItemId='{0}' AND EffectTime='{1}'", 
				MainClass.FixStringForSingleQuote(ItemId), MainClass.DateTimeToTimestamp(EffectTime));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ItemCost", "GetRecord", out _conn, out myDataReader))
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
				myDataReader["ItemId"].ToString()
				, MainClass.ToDate(TableName, myDataReader["EffectTime"])
				, MainClass.ToDecimal(TableName, myDataReader["CostPerIssue"])
				, MainClass.ToDecimal(TableName, myDataReader["CostPerUnitOrder"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO ITEM_COST (ItemId, EffectTime, CostPerIssue, CostPerUnitOrder) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.ItemId) + "'" + ", " + MainClass.DateTimeToTimestamp(data.EffectTime) + ", " + data.CostPerIssue + ", " + data.CostPerUnitOrder + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemCost", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(string ItemId, DateTime EffectTime)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ITEM_COST WHERE ItemId='{0}' AND EffectTime='{1}'", 
				MainClass.FixStringForSingleQuote(ItemId), MainClass.DateTimeToTimestamp(EffectTime));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ItemCost", "DeleteRecord");
			return Retval;
		}


	}
}
