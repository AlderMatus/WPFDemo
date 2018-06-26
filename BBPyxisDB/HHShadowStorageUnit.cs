using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class HHShadowStorageUnit
	{
		const string TableName = "HH_SHADOW_STORAGE_UNIT";

		// collection of record fields
		public class TableData
		{
			public int StorageUnitIid;
			public string StorageUnitName;
			public DateTime Last_modified;

			public TableData(int StorageUnitIid, string StorageUnitName, DateTime Last_modified)
			{
				this.StorageUnitIid = StorageUnitIid;
				this.StorageUnitName = StorageUnitName;
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
			string SqlStatement = string.Format("SELECT * from HH_SHADOW_STORAGE_UNIT WHERE StorageUnitIid='{0}'", 
				(int)StorageUnitIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "HHShadowStorageUnit", "GetRecord", out _conn, out myDataReader))
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
				, MainClass.ToDate(TableName, myDataReader["Last_modified"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO HH_SHADOW_STORAGE_UNIT (StorageUnitIid, StorageUnitName) VALUES ("
				+ (int)data.StorageUnitIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.StorageUnitName) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHShadowStorageUnit", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int StorageUnitIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE HH_SHADOW_STORAGE_UNIT WHERE StorageUnitIid='{0}'", 
				(int)StorageUnitIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHShadowStorageUnit", "DeleteRecord");
			return Retval;
		}


	}
}
