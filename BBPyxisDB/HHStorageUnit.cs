using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class HHStorageUnit
	{
		const string TableName = "HH_STORAGE_UNIT";

		// collection of record fields
		public class TableData
		{
			public int StorageUnitIid;
			public string StorageUnitName;
			public int ParLocationIid;
			public DateTime Last_modified;

			public TableData(int StorageUnitIid, string StorageUnitName, int ParLocationIid, DateTime Last_modified)
			{
				this.StorageUnitIid = StorageUnitIid;
				this.StorageUnitName = StorageUnitName;
				this.ParLocationIid = ParLocationIid;
				this.Last_modified = Last_modified;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int StorageUnitIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from HH_STORAGE_UNIT WHERE StorageUnitIid='{0}'", 
				(int)StorageUnitIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "HHStorageUnit", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["StorageUnitIid"])
				, myDataReader["StorageUnitName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ParLocationIid"])
				, MainClass.ToDate(TableName, myDataReader["Last_modified"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO HH_STORAGE_UNIT (StorageUnitName, ParLocationIid) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.StorageUnitName) + "'" + ", " + (int)data.ParLocationIid + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHStorageUnit", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int StorageUnitIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE HH_STORAGE_UNIT WHERE StorageUnitIid='{0}'", 
				(int)StorageUnitIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHStorageUnit", "DeleteRecord");
			return Retval;
		}


	}
}
