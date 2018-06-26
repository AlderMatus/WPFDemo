using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class SNCabinet
	{
		const string TableName = "SN_CABINET";

		// collection of record fields
		public class TableData
		{
			public int CabinetId;
			public string CabinetName;
			public int PylonIid;

			public TableData(int CabinetId, string CabinetName, int PylonIid)
			{
				this.CabinetId = CabinetId;
				this.CabinetName = CabinetName;
				this.PylonIid = PylonIid;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int CabinetId, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from SN_CABINET WHERE CabinetId='{0}'", 
				(int)CabinetId);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "SNCabinet", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["CabinetId"])
				, myDataReader["CabinetName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PylonIid"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO SN_CABINET (CabinetId, CabinetName, PylonIid) VALUES ("
				+ (int)data.CabinetId + ", " + "'" + MainClass.FixStringForSingleQuote(data.CabinetName) + "'" + ", " + (int)data.PylonIid + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "SNCabinet", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int CabinetId)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE SN_CABINET WHERE CabinetId='{0}'", 
				(int)CabinetId);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "SNCabinet", "DeleteRecord");
			return Retval;
		}


	}
}
