using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class KitsToAreas
	{
		const string TableName = "KITS_TO_AREAS";

		// collection of record fields
		public class TableData
		{
			public int KitIid;
			public int AreaIid;

			public TableData(int KitIid, int AreaIid)
			{
				this.KitIid = KitIid;
				this.AreaIid = AreaIid;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int KitIid, int AreaIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from KITS_TO_AREAS WHERE KitIid='{0}' AND AreaIid='{1}'", 
				(int)KitIid, (int)AreaIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "KitsToAreas", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["KitIid"])
				, MainClass.ToInt(TableName, myDataReader["AreaIid"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO KITS_TO_AREAS (KitIid, AreaIid) VALUES ("
				+ (int)data.KitIid + ", " + (int)data.AreaIid + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "KitsToAreas", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int KitIid, int AreaIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE KITS_TO_AREAS WHERE KitIid='{0}' AND AreaIid='{1}'", 
				(int)KitIid, (int)AreaIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "KitsToAreas", "DeleteRecord");
			return Retval;
		}


	}
}
