using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class HHShadowScanCode
	{
		const string TableName = "HH_SHADOW_SCANCODE";

		// collection of record fields
		public class TableData
		{
			public string MedID;
			public string Scancode;
			public DateTime Last_modified;

			public TableData(string MedID, string Scancode, DateTime Last_modified)
			{
				this.MedID = MedID;
				this.Scancode = Scancode;
				this.Last_modified = Last_modified;
			}
		}

		// make a TableData object from a SADataReader record
#if !NO_ASA
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
			data = new TableData(
				myDataReader["MedID"].ToString()
				, myDataReader["Scancode"].ToString()
				, MainClass.ToDate(TableName, myDataReader["Last_modified"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO HH_SHADOW_SCANCODE (MedID, Scancode) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.MedID) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Scancode) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHShadowScanCode", "InsertRecord");
			return Retval;
		}


	}
}
