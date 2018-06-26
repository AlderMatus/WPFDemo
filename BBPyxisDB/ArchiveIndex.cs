using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ArchiveIndex
	{
		const string TableName = "ARCHIVE_INDEX";

		// collection of record fields
		public class TableData
		{
			public DateTime DateOfArchRecs;
			public string ArchiveId;
			public int ArchiveFileExists;
			public int ArchiveYearFormat;
			public int NbrOfActivityRecs;
			public DateTime DtTmOfArchiveAction;
			public string TypeOfArchiveAction;
			public string UserName;
			public string UserId;
			public string ArchiveFileName;

			public TableData(DateTime DateOfArchRecs, string ArchiveId, int ArchiveFileExists, int ArchiveYearFormat, int NbrOfActivityRecs, DateTime DtTmOfArchiveAction, string TypeOfArchiveAction, string UserName, string UserId, string ArchiveFileName)
			{
				this.DateOfArchRecs = DateOfArchRecs;
				this.ArchiveId = ArchiveId;
				this.ArchiveFileExists = ArchiveFileExists;
				this.ArchiveYearFormat = ArchiveYearFormat;
				this.NbrOfActivityRecs = NbrOfActivityRecs;
				this.DtTmOfArchiveAction = DtTmOfArchiveAction;
				this.TypeOfArchiveAction = TypeOfArchiveAction;
				this.UserName = UserName;
				this.UserId = UserId;
				this.ArchiveFileName = ArchiveFileName;
			}
		}

		// return record given its primary key
		public static bool GetRecord(DateTime DateOfArchRecs, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from ARCHIVE_INDEX WHERE DateOfArchRecs='{0}'", 
				MainClass.DateTimeToTimestamp(DateOfArchRecs));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ArchiveIndex", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToDate(TableName, myDataReader["DateOfArchRecs"])
				, myDataReader["ArchiveId"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ArchiveFileExists"])
				, MainClass.ToInt(TableName, myDataReader["ArchiveYearFormat"])
				, MainClass.ToInt(TableName, myDataReader["NbrOfActivityRecs"])
				, MainClass.ToDate(TableName, myDataReader["DtTmOfArchiveAction"])
				, myDataReader["TypeOfArchiveAction"].ToString()
				, myDataReader["UserName"].ToString()
				, myDataReader["UserId"].ToString()
				, myDataReader["ArchiveFileName"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO ARCHIVE_INDEX (DateOfArchRecs, ArchiveId, ArchiveFileExists, ArchiveYearFormat, NbrOfActivityRecs, DtTmOfArchiveAction, TypeOfArchiveAction, UserName, UserId, ArchiveFileName) VALUES ("
				+ MainClass.DateTimeToTimestamp(data.DateOfArchRecs) + ", " + "'" + MainClass.FixStringForSingleQuote(data.ArchiveId) + "'" + ", " + (int)data.ArchiveFileExists + ", " + (int)data.ArchiveYearFormat + ", " + (int)data.NbrOfActivityRecs + ", " + MainClass.DateTimeToTimestamp(data.DtTmOfArchiveAction) + ", " + "'" + MainClass.FixStringForSingleQuote(data.TypeOfArchiveAction) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ArchiveFileName) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ArchiveIndex", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(DateTime DateOfArchRecs)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE ARCHIVE_INDEX WHERE DateOfArchRecs='{0}'", 
				MainClass.DateTimeToTimestamp(DateOfArchRecs));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ArchiveIndex", "DeleteRecord");
			return Retval;
		}


	}
}
