using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class UserPatients
	{
		const string TableName = "USER_PATIENTS";

		// collection of record fields
		public class TableData
		{
			public int UserIid;
			public int PtIid;
			public DateTime AddedTime;

			public TableData(int UserIid, int PtIid, DateTime AddedTime)
			{
				this.UserIid = UserIid;
				this.PtIid = PtIid;
				this.AddedTime = AddedTime;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int UserIid, int PtIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from USER_PATIENTS WHERE UserIid='{0}' AND PtIid='{1}'", 
				(int)UserIid, (int)PtIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "UserPatients", "GetRecord", out _conn, out myDataReader))
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
				, MainClass.ToInt(TableName, myDataReader["PtIid"])
				, MainClass.ToDate(TableName, myDataReader["AddedTime"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO USER_PATIENTS (UserIid, PtIid, AddedTime) VALUES ("
				+ (int)data.UserIid + ", " + (int)data.PtIid + ", " + MainClass.DateTimeToTimestamp(data.AddedTime) + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserPatients", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int UserIid, int PtIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE USER_PATIENTS WHERE UserIid='{0}' AND PtIid='{1}'", 
				(int)UserIid, (int)PtIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserPatients", "DeleteRecord");
			return Retval;
		}


	}
}
