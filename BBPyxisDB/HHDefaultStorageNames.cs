using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class HHDefaultStorageNames
	{
		const string TableName = "HH_DEFAULT_STORAGE_NAMES";

		// collection of record fields
		public class TableData
		{
			public int DefaultStorageIid;
			public string DefaultStorageName;
			public int StorageTable;

			public TableData(int DefaultStorageIid, string DefaultStorageName, int StorageTable)
			{
				this.DefaultStorageIid = DefaultStorageIid;
				this.DefaultStorageName = DefaultStorageName;
				this.StorageTable = StorageTable;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int DefaultstorageIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from HH_DEFAULT_STORAGE_NAMES WHERE DefaultstorageIid='{0}'", 
				(int)DefaultstorageIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "HHDefaultStorageNames", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["DefaultStorageIid"])
				, myDataReader["DefaultStorageName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["StorageTable"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO HH_DEFAULT_STORAGE_NAMES (DefaultStorageName, StorageTable) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.DefaultStorageName) + "'" + ", " + (int)data.StorageTable + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHDefaultStorageNames", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int DefaultstorageIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE HH_DEFAULT_STORAGE_NAMES WHERE DefaultstorageIid='{0}'", 
				(int)DefaultstorageIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHDefaultStorageNames", "DeleteRecord");
			return Retval;
		}


	}
}
