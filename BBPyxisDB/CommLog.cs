using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class CommLog
	{
		const string TableName = "COMM_LOG";

		// collection of record fields
		public class TableData
		{
			public DateTime Message_time;
			public string Destination;
			public string Source;
			public int Message_number;
			public string Q_message;
			public string ProcessBox;
			public int MsgInOut;

			public TableData(DateTime Message_time, string Destination, string Source, int Message_number, string Q_message, string ProcessBox, int MsgInOut)
			{
				this.Message_time = Message_time;
				this.Destination = Destination;
				this.Source = Source;
				this.Message_number = Message_number;
				this.Q_message = Q_message;
				this.ProcessBox = ProcessBox;
				this.MsgInOut = MsgInOut;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(DateTime Message_time, int Message_number, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from COMM_LOG WHERE Message_time='{0}' AND Message_number='{1}'", 
				MainClass.DateTimeToTimestamp(Message_time), (int)Message_number);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "CommLog", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToDate(TableName, myDataReader["Message_time"])
				, myDataReader["Destination"].ToString()
				, myDataReader["Source"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Message_number"])
				, myDataReader["Q_message"].ToString()
				, myDataReader["ProcessBox"].ToString()
				, MainClass.ToInt(TableName, myDataReader["MsgInOut"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO COMM_LOG (Message_time, Destination, Source, Q_message, ProcessBox, MsgInOut) VALUES ("
				+ MainClass.DateTimeToTimestamp(data.Message_time) + ", " + "'" + MainClass.FixStringForSingleQuote(data.Destination) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Source) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Q_message) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ProcessBox) + "'" + ", " + (int)data.MsgInOut + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "CommLog", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(DateTime Message_time, int Message_number)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE COMM_LOG WHERE Message_time='{0}' AND Message_number='{1}'", 
				MainClass.DateTimeToTimestamp(Message_time), (int)Message_number);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "CommLog", "DeleteRecord");
			return Retval;
		}


	}
}
