using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class UserCat
	{
		const string TableName = "USER_CAT";

		// collection of record fields
		public class TableData
		{
			public int CategoryIid;
			public string CategoryDesc;

			public TableData(int CategoryIid, string CategoryDesc)
			{
				this.CategoryIid = CategoryIid;
				this.CategoryDesc = CategoryDesc;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int CategoryIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from USER_CAT WHERE CategoryIid='{0}'", 
				(int)CategoryIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "UserCat", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["CategoryIid"])
				, myDataReader["CategoryDesc"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO USER_CAT (CategoryDesc) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.CategoryDesc) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserCat", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int CategoryIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE USER_CAT WHERE CategoryIid='{0}'", 
				(int)CategoryIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserCat", "DeleteRecord");
			return Retval;
		}


	}
}
