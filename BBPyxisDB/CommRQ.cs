using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class CommRQ
	{
		const string TableName = "COMM_RQ";

		// collection of record fields
		public class TableData
		{
			public string Destination;
			public int Message_number;
			public string Source;
			public DateTime Message_time;
			public string Rq_message;
			public string ProcessBox;

			public TableData(string Destination, int Message_number, string Source, DateTime Message_time, string Rq_message, string ProcessBox)
			{
				this.Destination = Destination;
				this.Message_number = Message_number;
				this.Source = Source;
				this.Message_time = Message_time;
				this.Rq_message = Rq_message;
				this.ProcessBox = ProcessBox;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(string Destination, int Message_number, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from COMM_RQ WHERE Destination='{0}' AND Message_number='{1}'", 
				MainClass.FixStringForSingleQuote(Destination), (int)Message_number);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "CommRQ", "GetRecord", out _conn, out myDataReader))
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
				myDataReader["Destination"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Message_number"])
				, myDataReader["Source"].ToString()
				, MainClass.ToDate(TableName, myDataReader["Message_time"])
				, myDataReader["Rq_message"].ToString()
				, myDataReader["ProcessBox"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO COMM_RQ (Destination, Source, Message_time, Rq_message, ProcessBox) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.Destination) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Source) + "'" + ", " + MainClass.DateTimeToTimestamp(data.Message_time) + ", " + "'" + MainClass.FixStringForSingleQuote(data.Rq_message) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ProcessBox) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "CommRQ", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(string Destination, int Message_number)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE COMM_RQ WHERE Destination='{0}' AND Message_number='{1}'", 
				MainClass.FixStringForSingleQuote(Destination), (int)Message_number);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "CommRQ", "DeleteRecord");
			return Retval;
		}


	}
}
