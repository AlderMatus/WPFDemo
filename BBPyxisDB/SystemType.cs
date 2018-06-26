using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class SystemType
	{
		const string TableName = "SYSTEM_TYPE";

		// collection of record fields
		public class TableData
		{
			public int SystemType;
			public string SystemTypeName;

			public TableData(int SystemType, string SystemTypeName)
			{
				this.SystemType = SystemType;
				this.SystemTypeName = SystemTypeName;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int SystemType, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from SYSTEM_TYPE WHERE SystemType='{0}'", 
				(int)SystemType);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "SystemType", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["SystemType"])
				, myDataReader["SystemTypeName"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO SYSTEM_TYPE (SystemTypeName) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.SystemTypeName) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "SystemType", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int SystemType)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE SYSTEM_TYPE WHERE SystemType='{0}'", 
				(int)SystemType);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "SystemType", "DeleteRecord");
			return Retval;
		}


	}
}
