using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class reportindexes
	{
		const string TableName = "POCKET_ACCESS_DEVICENAME_RPTINDX";

		// collection of record fields
		public class TableData
		{
			public string DeviceName;
			public DateTime PurgeDate;
			public int SystemType;

			public TableData(string DeviceName, DateTime PurgeDate, int SystemType)
			{
				this.DeviceName = DeviceName;
				this.PurgeDate = PurgeDate;
				this.SystemType = SystemType;
			}
		}

		// make a TableData object from a SADataReader record
#if !NO_ASA
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
			data = new TableData(
				myDataReader["DeviceName"].ToString()
				, MainClass.ToDate(TableName, myDataReader["PurgeDate"])
				, MainClass.ToInt(TableName, myDataReader["SystemType"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO POCKET_ACCESS_DEVICENAME_RPTINDX (DeviceName, PurgeDate, SystemType) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.DeviceName) + "'" + ", " + MainClass.DateTimeToTimestamp(data.PurgeDate) + ", " + (int)data.SystemType + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "reportindexes", "InsertRecord");
			return Retval;
		}


	}
}
