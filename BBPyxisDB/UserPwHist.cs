using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class UserPwHist
	{
		const string TableName = "USER_PW_HIST";

		// collection of record fields
		public class TableData
		{
			public int UserIid;
            //Rename to clarify this is already encrypted
			public string EncryptedPass;
			public string PasswordSalt;
			public DateTime StoredTS;

			public TableData(int UserIid, string EncryptedPass, string PasswordSalt, DateTime StoredTS)
			{
				this.UserIid = UserIid;
                this.EncryptedPass = EncryptedPass;
				this.PasswordSalt = PasswordSalt;
				this.StoredTS = StoredTS;
			}
		}

		// make a TableData object from a SADataReader record
#if !NO_ASA
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
			data = new TableData(
				MainClass.ToInt(TableName, myDataReader["UserIid"])
				, myDataReader["Password"].ToString()
				, myDataReader["PasswordSalt"].ToString()
				, MainClass.ToDate(TableName, myDataReader["StoredTS"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO USER_PW_HIST (UserIid, Password, PasswordSalt, StoredTS) VALUES ("
				+ (int)data.UserIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.EncryptedPass) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PasswordSalt) + "'" + ", " + MainClass.DateTimeToTimestamp(data.StoredTS) + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserPwHist", "InsertRecord");
			return Retval;
		}


	}
}
