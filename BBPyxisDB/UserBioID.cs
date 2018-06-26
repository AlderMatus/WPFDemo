using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class UserBioID
	{
		const string TableName = "USER_BIOID";

		// collection of record fields
		public class TableData
		{
			public int UserIid;
			public int BioDevType;
			public byte[] BioData;
			public DateTime BioDate;
			public int DeviceIid;
			public int BioClass;

			public TableData(int UserIid, int BioDevType, byte[] BioData, DateTime BioDate, int DeviceIid, int BioClass)
			{
				this.UserIid = UserIid;
				this.BioDevType = BioDevType;
				this.BioData = BioData;
				this.BioDate = BioDate;
				this.DeviceIid = DeviceIid;
				this.BioClass = BioClass;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int UserIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from USER_BIOID WHERE UserIid='{0}'", 
				(int)UserIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "UserBioID", "GetRecord", out _conn, out myDataReader))
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
				, MainClass.ToInt(TableName, myDataReader["BioDevType"])
				, MainClass.ToByteArray(TableName, myDataReader["BioData"])
				, MainClass.ToDate(TableName, myDataReader["BioDate"])
				, MainClass.ToInt(TableName, myDataReader["DeviceIid"])
				, MainClass.ToInt(TableName, myDataReader["BioClass"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO USER_BIOID (UserIid, BioDevType, BioData, BioDate, DeviceIid, BioClass) VALUES ("
				+ (int)data.UserIid + ", " + (int)data.BioDevType + ", " + data.BioData + ", " + MainClass.DateTimeToTimestamp(data.BioDate) + ", " + (int)data.DeviceIid + ", " + (int)data.BioClass + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserBioID", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int UserIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE USER_BIOID WHERE UserIid='{0}'", 
				(int)UserIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserBioID", "DeleteRecord");
			return Retval;
		}


	}
}
