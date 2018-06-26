using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class PickArea
	{
		const string TableName = "PICK_AREA";

		// collection of record fields
		public class TableData
		{
			public int PickAreaIid;
			public string PickAreaName;

			public TableData(int PickAreaIid, string PickAreaName)
			{
				this.PickAreaIid = PickAreaIid;
				this.PickAreaName = PickAreaName;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int PickAreaIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PICK_AREA WHERE PickAreaIid='{0}'", 
				(int)PickAreaIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PickArea", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["PickAreaIid"])
				, myDataReader["PickAreaName"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO PICK_AREA (PickAreaName) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.PickAreaName) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PickArea", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int PickAreaIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PICK_AREA WHERE PickAreaIid='{0}'", 
				(int)PickAreaIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PickArea", "DeleteRecord");
			return Retval;
		}


	}
}
