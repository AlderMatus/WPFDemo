using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class NUnitLocation
	{
		const string TableName = "NUNIT_LOCATION";

		// collection of record fields
		public class TableData
		{
			public int AreaIid;
			public int NUnitIid;

			public TableData(int AreaIid, int NUnitIid)
			{
				this.AreaIid = AreaIid;
				this.NUnitIid = NUnitIid;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int AreaIid, int NUnitIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from NUNIT_LOCATION WHERE AreaIid='{0}' AND NUnitIid='{1}'", 
				(int)AreaIid, (int)NUnitIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "NUnitLocation", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["AreaIid"])
				, MainClass.ToInt(TableName, myDataReader["NUnitIid"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO NUNIT_LOCATION (AreaIid, NUnitIid) VALUES ("
				+ (int)data.AreaIid + ", " + (int)data.NUnitIid + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "NUnitLocation", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int AreaIid, int NUnitIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE NUNIT_LOCATION WHERE AreaIid='{0}' AND NUnitIid='{1}'", 
				(int)AreaIid, (int)NUnitIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "NUnitLocation", "DeleteRecord");
			return Retval;
		}


	}
}
