using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class PatientVisit
	{
		const string TableName = "PATIENT_VISIT";

		// collection of record fields
		public class TableData
		{
			public int PtIid;
			public DateTime PtDischargeDate;

			public TableData(int PtIid, DateTime PtDischargeDate)
			{
				this.PtIid = PtIid;
				this.PtDischargeDate = PtDischargeDate;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int PtIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PATIENT_VISIT WHERE PtIid='{0}'", 
				(int)PtIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PatientVisit", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["PtIid"])
				, MainClass.ToDate(TableName, myDataReader["PtDischargeDate"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO PATIENT_VISIT (PtIid, PtDischargeDate) VALUES ("
				+ (int)data.PtIid + ", " + MainClass.DateTimeToTimestamp(data.PtDischargeDate) + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PatientVisit", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int PtIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PATIENT_VISIT WHERE PtIid='{0}'", 
				(int)PtIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PatientVisit", "DeleteRecord");
			return Retval;
		}


	}
}
