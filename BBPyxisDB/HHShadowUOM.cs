using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class HHShadowUOM
	{
		const string TableName = "HH_SHADOW_UOM";

		// collection of record fields
		public class TableData
		{
			public string UnitOfMeasure;
			public DateTime Last_modified;

			public TableData(string UnitOfMeasure, DateTime Last_modified)
			{
				this.UnitOfMeasure = UnitOfMeasure;
				this.Last_modified = Last_modified;
			}
		}

		// make a TableData object from a SADataReader record
#if !NO_ASA
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
			data = new TableData(
				myDataReader["UnitOfMeasure"].ToString()
				, MainClass.ToDate(TableName, myDataReader["Last_modified"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO HH_SHADOW_UOM (UnitOfMeasure) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.UnitOfMeasure) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHShadowUOM", "InsertRecord");
			return Retval;
		}


	}
}
