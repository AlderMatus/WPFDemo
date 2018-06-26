using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class RequestItem
	{
		const string TableName = "REQUEST_ITEM";

		// collection of record fields
		public class TableData
		{
			public int ReqIid;
			public string DeviceName;
			public DateTime MsgTime;
			public string UserId;
			public string MsgText;
			public string ItemId;
			public int Qty;
			public string UnitOfIssue;
			public int Status;

			public TableData(int ReqIid, string DeviceName, DateTime MsgTime, string UserId, string MsgText, string ItemId, int Qty, string UnitOfIssue, int Status)
			{
				this.ReqIid = ReqIid;
				this.DeviceName = DeviceName;
				this.MsgTime = MsgTime;
				this.UserId = UserId;
				this.MsgText = MsgText;
				this.ItemId = ItemId;
				this.Qty = Qty;
				this.UnitOfIssue = UnitOfIssue;
				this.Status = Status;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int ReqIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from REQUEST_ITEM WHERE ReqIid='{0}'", 
				(int)ReqIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "RequestItem", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["ReqIid"])
				, myDataReader["DeviceName"].ToString()
				, MainClass.ToDate(TableName, myDataReader["MsgTime"])
				, myDataReader["UserId"].ToString()
				, myDataReader["MsgText"].ToString()
				, myDataReader["ItemId"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Qty"])
				, myDataReader["UnitOfIssue"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Status"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO REQUEST_ITEM (DeviceName, MsgTime, UserId, MsgText, ItemId, Qty, UnitOfIssue, Status) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.DeviceName) + "'" + ", " + MainClass.DateTimeToTimestamp(data.MsgTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.MsgText) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemId) + "'" + ", " + (int)data.Qty + ", " + "'" + MainClass.FixStringForSingleQuote(data.UnitOfIssue) + "'" + ", " + (int)data.Status + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "RequestItem", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int ReqIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE REQUEST_ITEM WHERE ReqIid='{0}'", 
				(int)ReqIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "RequestItem", "DeleteRecord");
			return Retval;
		}


	}
}
