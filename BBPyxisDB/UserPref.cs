using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class UserPref
	{
		const string TableName = "USER_PREF";

		// collection of record fields
		public class TableData
		{
			public int UserIid;
			public int UserPrefId;
			public int UserPrefValue;

			public TableData(int UserIid, int UserPrefId, int UserPrefValue)
			{
				this.UserIid = UserIid;
				this.UserPrefId = UserPrefId;
				this.UserPrefValue = UserPrefValue;
			}
		}

		// make a TableData object from a SADataReader record
#if !NO_ASA
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
			data = new TableData(
				MainClass.ToInt(TableName, myDataReader["UserIid"])
				, MainClass.ToInt(TableName, myDataReader["UserPrefId"])
				, MainClass.ToInt(TableName, myDataReader["UserPrefValue"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO USER_PREF (UserIid, UserPrefId, UserPrefValue) VALUES ("
				+ (int)data.UserIid + ", " + (int)data.UserPrefId + ", " + (int)data.UserPrefValue + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserPref", "InsertRecord");
			return Retval;
		}


	}
}
