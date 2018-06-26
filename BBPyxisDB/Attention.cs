using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Attention
	{
		const string TableName = "ATTENTION";

		// collection of record fields
		public class TableData
		{
			public int AttnIid;
			public int AttnCategory;
			public DateTime Message_time;
			public int AttnAction;
			public string Source;
			public string AttnMsg;
			public int Printed;
			public string ProcessBox;

			public TableData(int AttnIid, int AttnCategory, DateTime Message_time, int AttnAction, string Source, string AttnMsg, int Printed, string ProcessBox)
			{
				this.AttnIid = AttnIid;
				this.AttnCategory = AttnCategory;
				this.Message_time = Message_time;
				this.AttnAction = AttnAction;
				this.Source = Source;
				this.AttnMsg = AttnMsg;
				this.Printed = Printed;
				this.ProcessBox = ProcessBox;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int AttnIid, int AttnCategory, DateTime Message_time, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ATTENTION WHERE AttnIid='{0}' AND AttnCategory='{1}' AND Message_time='{2}'", 
				(int)AttnIid, (int)AttnCategory, MainClass.DateTimeToTimestamp(Message_time));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Attention", "GetRecord", out _conn, out myDataReader))
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
						myDataReader.Close();
					if (_conn != null)
						_conn.Close();
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
				MainClass.ToInt(TableName, myDataReader["AttnIid"])
				, MainClass.ToInt(TableName, myDataReader["AttnCategory"])
				, MainClass.ToDate(TableName, myDataReader["Message_time"])
				, MainClass.ToInt(TableName, myDataReader["AttnAction"])
				, myDataReader["Source"].ToString()
				, myDataReader["AttnMsg"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Printed"])
				, myDataReader["ProcessBox"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO ATTENTION (AttnCategory, Message_time, AttnAction, Source, AttnMsg, Printed, ProcessBox) VALUES ("
				+ (int)data.AttnCategory + ", " + MainClass.DateTimeToTimestamp(data.Message_time) + ", " + (int)data.AttnAction + ", " + "'" + MainClass.FixStringForSingleQuote(data.Source) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.AttnMsg) + "'" + ", " + (int)data.Printed + ", " + "'" + MainClass.FixStringForSingleQuote(data.ProcessBox) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Attention", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int AttnIid, int AttnCategory, DateTime Message_time)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ATTENTION WHERE AttnIid='{0}' AND AttnCategory='{1}' AND Message_time='{2}'", 
				(int)AttnIid, (int)AttnCategory, MainClass.DateTimeToTimestamp(Message_time));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Attention", "DeleteRecord");
			return Retval;
		}

        public static int FindNotices(string message, DateTime timeframe)
        {
            int count = 0;
#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ATTENTION WHERE attnMsg = '{0}' AND Message_time > {1}", 
				MainClass.FixStringForSingleQuote(message), MainClass.DateTimeToTimestamp(timeframe));

			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Attention", "FindNotices", out _conn, out myDataReader))
			{
				try
				{
					while (myDataReader.Read())
					{
                        ++count;
					}
				}
				catch (Exception ex)
				{
					string err = String.Format(MainClass.StringTable.GetString("DatabaseError"), TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
					ServiceMessages.InsertRec(MainClass.AppName, TableName, "GetRecord", err);
				}
				finally
				{
					if (myDataReader != null)
						myDataReader.Close();
					if (_conn != null)
						_conn.Close();
				}
			}
#endif
			return count;
        }

	}
}
