using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class UserAccessType
	{
		const string TableName = "USER_ACCESS_TYPE";

		// collection of record fields
		public class TableData
		{
			public int AccessTypeIid;
			public string AccessType;

			public TableData(int AccessTypeIid, string AccessType)
			{
				this.AccessTypeIid = AccessTypeIid;
				this.AccessType = AccessType;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int AccessTypeIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from USER_ACCESS_TYPE WHERE AccessTypeIid='{0}'", 
				(int)AccessTypeIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "UserAccessType", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["AccessTypeIid"])
				, myDataReader["AccessType"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO USER_ACCESS_TYPE (AccessType) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.AccessType) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserAccessType", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int AccessTypeIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE USER_ACCESS_TYPE WHERE AccessTypeIid='{0}'", 
				(int)AccessTypeIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserAccessType", "DeleteRecord");
			return Retval;
		}


	}
}
