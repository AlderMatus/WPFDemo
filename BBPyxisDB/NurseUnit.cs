using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class NurseUnit
	{
		const string TableName = "NURSE_UNIT";

		// collection of record fields
		public class TableData
		{
			public int NUnitIid;
			public string NUnitName;
			public string FacilityCode;
			public int DischargeDeltaTime;

			public TableData(int NUnitIid, string NUnitName, string FacilityCode, int DischargeDeltaTime)
			{
				this.NUnitIid = NUnitIid;
				this.NUnitName = NUnitName;
				this.FacilityCode = FacilityCode;
				this.DischargeDeltaTime = DischargeDeltaTime;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int NUnitIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from NURSE_UNIT WHERE NUnitIid='{0}'", 
				(int)NUnitIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "NurseUnit", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["NUnitIid"])
				, myDataReader["NUnitName"].ToString()
				, myDataReader["FacilityCode"].ToString()
				, MainClass.ToInt(TableName, myDataReader["DischargeDeltaTime"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO NURSE_UNIT (NUnitName, FacilityCode, DischargeDeltaTime) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.NUnitName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.FacilityCode) + "'" + ", " + (int)data.DischargeDeltaTime + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "NurseUnit", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int NUnitIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE NURSE_UNIT WHERE NUnitIid='{0}'", 
				(int)NUnitIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "NurseUnit", "DeleteRecord");
			return Retval;
		}


	}
}
