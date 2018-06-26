using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class StationAreaType
	{
		const string TableName = "STATION_AREA_TYPE";

		// collection of record fields
		public class TableData
		{
			public int StnAreaTypeIid;
			public string StnAreaType;

			public TableData(int StnAreaTypeIid, string StnAreaType)
			{
				this.StnAreaTypeIid = StnAreaTypeIid;
				this.StnAreaType = StnAreaType;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int StnAreaTypeIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from STATION_AREA_TYPE WHERE StnAreaTypeIid='{0}'", 
				(int)StnAreaTypeIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "StationAreaType", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["StnAreaTypeIid"])
				, myDataReader["StnAreaType"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO STATION_AREA_TYPE (StnAreaType) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.StnAreaType) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "StationAreaType", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int StnAreaTypeIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE STATION_AREA_TYPE WHERE StnAreaTypeIid='{0}'", 
				(int)StnAreaTypeIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "StationAreaType", "DeleteRecord");
			return Retval;
		}


	}
}
