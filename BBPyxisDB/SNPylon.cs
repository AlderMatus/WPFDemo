using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class SNPylon
	{
		const string TableName = "SN_PYLON";

		// collection of record fields
		public class TableData
		{
			public int PylonIid;
			public string PylonName;
			public int AreaIid;

			public TableData(int PylonIid, string PylonName, int AreaIid)
			{
				this.PylonIid = PylonIid;
				this.PylonName = PylonName;
				this.AreaIid = AreaIid;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int PylonIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from SN_PYLON WHERE PylonIid='{0}'", 
				(int)PylonIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "SNPylon", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["PylonIid"])
				, myDataReader["PylonName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["AreaIid"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO SN_PYLON (PylonName, AreaIid) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.PylonName) + "'" + ", " + (int)data.AreaIid + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "SNPylon", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int PylonIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE SN_PYLON WHERE PylonIid='{0}'", 
				(int)PylonIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "SNPylon", "DeleteRecord");
			return Retval;
		}


	}
}
