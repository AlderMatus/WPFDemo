using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class HHShadowSubUnit
	{
		const string TableName = "HH_SHADOW_SUBUNIT";

		// collection of record fields
		public class TableData
		{
			public int SubUnitIid;
			public string SubUnitName;
			public DateTime Last_modified;

			public TableData(int SubUnitIid, string SubUnitName, DateTime Last_modified)
			{
				this.SubUnitIid = SubUnitIid;
				this.SubUnitName = SubUnitName;
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
			string SqlStatement = string.Format("SELECT * from HH_SHADOW_SUBUNIT WHERE SubUnitIid='{0}'", 
				(int)SubUnitIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "HHShadowSubUnit", "GetRecord", out _conn, out myDataReader))
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
				, MainClass.ToDate(TableName, myDataReader["Last_modified"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO HH_SHADOW_SUBUNIT (SubUnitIid, SubUnitName) VALUES ("
				+ (int)data.SubUnitIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.SubUnitName) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHShadowSubUnit", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int SubUnitIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE HH_SHADOW_SUBUNIT WHERE SubUnitIid='{0}'", 
				(int)SubUnitIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHShadowSubUnit", "DeleteRecord");
			return Retval;
		}


	}
}
