using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class BillingTbl
	{
		const string TableName = "BILLING_TBL";

		// collection of record fields
		public class TableData
		{
			public int PktAccIid;
			public int BillingStatus;

			public TableData(int PktAccIid, int BillingStatus)
			{
				this.PktAccIid = PktAccIid;
				this.BillingStatus = BillingStatus;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int PktAccIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from BILLING_TBL WHERE PktAccIid='{0}'", 
				(int)PktAccIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "BillingTbl", "GetRecord", out _conn, out myDataReader))
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
						myDataReader.Close();
					if (_conn != null)
						_conn.Close();
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
				MainClass.ToInt(TableName, myDataReader["PktAccIid"])
				, MainClass.ToInt(TableName, myDataReader["BillingStatus"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO BILLING_TBL (PktAccIid, BillingStatus) VALUES ("
				+ (int)data.PktAccIid + ", " + (int)data.BillingStatus + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "BillingTbl", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int PktAccIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE BILLING_TBL WHERE PktAccIid='{0}'", 
				(int)PktAccIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "BillingTbl", "DeleteRecord");
			return Retval;
		}


	}
}
