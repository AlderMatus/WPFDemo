using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class UsersToUserCat
	{
		const string TableName = "USERS_TO_USER_CAT";

		// collection of record fields
		public class TableData
		{
			public int UserIid;
			public int CategoryIid;

			public TableData(int UserIid, int CategoryIid)
			{
				this.UserIid = UserIid;
				this.CategoryIid = CategoryIid;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int CategoryIid, int UserIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from USERS_TO_USER_CAT WHERE CategoryIid='{0}' AND UserIid='{1}'", 
				(int)CategoryIid, (int)UserIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "UsersToUserCat", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["UserIid"])
				, MainClass.ToInt(TableName, myDataReader["CategoryIid"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO USERS_TO_USER_CAT (UserIid, CategoryIid) VALUES ("
				+ (int)data.UserIid + ", " + (int)data.CategoryIid + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UsersToUserCat", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int CategoryIid, int UserIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE USERS_TO_USER_CAT WHERE CategoryIid='{0}' AND UserIid='{1}'", 
				(int)CategoryIid, (int)UserIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UsersToUserCat", "DeleteRecord");
			return Retval;
		}


	}
}
