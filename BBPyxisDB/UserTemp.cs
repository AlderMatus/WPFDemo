using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class UserTemp
	{
		const string TableName = "USER_TEMP";

		// collection of record fields
		public class TableData
		{
			public int UserIsTemp;
			public string UserIsTempText;

			public TableData(int UserIsTemp, string UserIsTempText)
			{
				this.UserIsTemp = UserIsTemp;
				this.UserIsTempText = UserIsTempText;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int UserIsTemp, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from USER_TEMP WHERE UserIsTemp='{0}'", 
				(int)UserIsTemp);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "UserTemp", "GetRecord", out _conn, out myDataReader))
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
                    {
                        myDataReader.Close();
                        myDataReader.Dispose();
                        myDataReader = null;
                    }
                    if (_conn != null)
                    {
                        _conn.Close();
                        _conn.Dispose();
                        _conn = null;
                    }
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
				MainClass.ToInt(TableName, myDataReader["UserIsTemp"])
				, myDataReader["UserIsTempText"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO USER_TEMP (UserIsTemp, UserIsTempText) VALUES ("
				+ (int)data.UserIsTemp + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserIsTempText) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserTemp", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int UserIsTemp)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE USER_TEMP WHERE UserIsTemp='{0}'", 
				(int)UserIsTemp);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserTemp", "DeleteRecord");
			return Retval;
		}


	}
}
