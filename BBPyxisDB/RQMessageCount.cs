using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class RQMessageCount
	{
		const string TableName = "RQMESSAGE_COUNT";

		// collection of record fields
		public class TableData
		{
			public string Source;
			public int Message_count;

			public TableData(string Source, int Message_count)
			{
				this.Source = Source;
				this.Message_count = Message_count;
			}
		}

		// return record given its primary key
		public static bool GetRecord(string Source, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from RQMESSAGE_COUNT WHERE Source='{0}'", 
				MainClass.FixStringForSingleQuote(Source));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "RQMessageCount", "GetRecord", out _conn, out myDataReader))
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
				myDataReader["Source"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Message_count"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO RQMESSAGE_COUNT (Source, Message_count) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.Source) + "'" + ", " + (int)data.Message_count + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "RQMessageCount", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(string Source)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE RQMESSAGE_COUNT WHERE Source='{0}'", 
				MainClass.FixStringForSingleQuote(Source));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "RQMessageCount", "DeleteRecord");
			return Retval;
		}


	}
}
