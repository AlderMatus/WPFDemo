using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class HHSubUnit
	{
		const string TableName = "HH_SUBUNIT";

		// collection of record fields
		public class TableData
		{
			public int SubUnitIid;
			public string SubUnitName;
			public int StorageUnitIid;
			public int NumberOfBins;
			public DateTime Last_modified;

			public TableData(int SubUnitIid, string SubUnitName, int StorageUnitIid, int NumberOfBins, DateTime Last_modified)
			{
				this.SubUnitIid = SubUnitIid;
				this.SubUnitName = SubUnitName;
				this.StorageUnitIid = StorageUnitIid;
				this.NumberOfBins = NumberOfBins;
				this.Last_modified = Last_modified;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int SubUnitIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from HH_SUBUNIT WHERE SubUnitIid='{0}'", 
				(int)SubUnitIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "HHSubUnit", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["SubUnitIid"])
				, myDataReader["SubUnitName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["StorageUnitIid"])
				, MainClass.ToInt(TableName, myDataReader["NumberOfBins"])
				, MainClass.ToDate(TableName, myDataReader["Last_modified"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO HH_SUBUNIT (SubUnitName, StorageUnitIid, NumberOfBins) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.SubUnitName) + "'" + ", " + (int)data.StorageUnitIid + ", " + (int)data.NumberOfBins + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHSubUnit", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int SubUnitIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE HH_SUBUNIT WHERE SubUnitIid='{0}'", 
				(int)SubUnitIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHSubUnit", "DeleteRecord");
			return Retval;
		}


	}
}
