using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class XQMessageCount
	{
		const string TableName = "XQMESSAGE_COUNT";

		// collection of record fields
		public class TableData
		{
			public string Destination;
			public int Message_count;

			public TableData(string Destination, int Message_count)
			{
				this.Destination = Destination;
				this.Message_count = Message_count;
			}
		}

		// return record given its primary key
		public static bool GetRecord(string Destination, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from XQMESSAGE_COUNT WHERE Destination='{0}'", 
				MainClass.FixStringForSingleQuote(Destination));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "XQMESSAGE_COUNT", "GetRecord", out _conn, out myDataReader))
			{
				try
				{
					if (myDataReader.Read())
					{
						data = new TableData(
						myDataReader["Destination"].ToString()
						, MainClass.ToInt(TableName, myDataReader["Message_count"]));
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

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO XQMESSAGE_COUNT (Destination, Message_count) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.Destination) + "'" + ", " + (int)data.Message_count + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "XQMESSAGE_COUNT", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(string Destination)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE XQMESSAGE_COUNT WHERE Destination='{0}'", 
				MainClass.FixStringForSingleQuote(Destination));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "XQMessageCount", "DeleteRecord");
			return Retval;
		}


	}
}
