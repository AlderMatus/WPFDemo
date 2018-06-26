using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ConnectionID
	{
		const string TableName = "CONNECTION_ID";

		// collection of record fields
		public class TableData
		{
			public string ConnectName;
			public int ConnectID;

			public TableData(string ConnectName, int ConnectID)
			{
				this.ConnectName = ConnectName;
				this.ConnectID = ConnectID;
			}
		}

		// make a TableData object from a SADataReader record
#if !NO_ASA
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
			data = new TableData(
				myDataReader["ConnectName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ConnectID"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO CONNECTION_ID (ConnectName, ConnectID) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.ConnectName) + "'" + ", " + (int)data.ConnectID + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ConnectionID", "InsertRecord");
			return Retval;
		}


	}
}
