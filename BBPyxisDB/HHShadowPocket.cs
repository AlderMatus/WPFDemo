using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class HHShadowPocket
	{
		const string TableName = "HH_SHADOW_POCKET";

		// collection of record fields
		public class TableData
		{
			public int ParLocationIid;
			public string StorageUnitName;
			public int SubUnitIid;
			public int BinNumber;
			public DateTime Last_modified;

			public TableData(int ParLocationIid, string StorageUnitName, int SubUnitIid, int BinNumber, DateTime Last_modified)
			{
				this.ParLocationIid = ParLocationIid;
				this.StorageUnitName = StorageUnitName;
				this.SubUnitIid = SubUnitIid;
				this.BinNumber = BinNumber;
				this.Last_modified = Last_modified;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int ParLocationIid, string StorageUnitName, int SubUnitIid, int BinNumber, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from HH_SHADOW_POCKET WHERE ParLocationIid='{0}' AND StorageUnitName='{1}' AND SubUnitIid='{2}' AND BinNumber='{3}'", 
				(int)ParLocationIid, MainClass.FixStringForSingleQuote(StorageUnitName), (int)SubUnitIid, (int)BinNumber);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "HHShadowPocket", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["ParLocationIid"])
				, myDataReader["StorageUnitName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["SubUnitIid"])
				, MainClass.ToInt(TableName, myDataReader["BinNumber"])
				, MainClass.ToDate(TableName, myDataReader["Last_modified"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO HH_SHADOW_POCKET (ParLocationIid, StorageUnitName, SubUnitIid, BinNumber) VALUES ("
				+ (int)data.ParLocationIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.StorageUnitName) + "'" + ", " + (int)data.SubUnitIid + ", " + (int)data.BinNumber + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHShadowPocket", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int ParLocationIid, string StorageUnitName, int SubUnitIid, int BinNumber)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE HH_SHADOW_POCKET WHERE ParLocationIid='{0}' AND StorageUnitName='{1}' AND SubUnitIid='{2}' AND BinNumber='{3}'", 
				(int)ParLocationIid, MainClass.FixStringForSingleQuote(StorageUnitName), (int)SubUnitIid, (int)BinNumber);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHShadowPocket", "DeleteRecord");
			return Retval;
		}


	}
}
