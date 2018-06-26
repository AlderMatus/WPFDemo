using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class PatientTempPerm
	{
		const string TableName = "PATIENT_TEMP_PERM";

		// collection of record fields
		public class TableData
		{
			public int TempPatient;
			public string TempPatientText;

			public TableData(int TempPatient, string TempPatientText)
			{
				this.TempPatient = TempPatient;
				this.TempPatientText = TempPatientText;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int TempPatient, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PATIENT_TEMP_PERM WHERE TempPatient='{0}'", 
				(int)TempPatient);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PatientTempPerm", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["TempPatient"])
				, myDataReader["TempPatientText"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO PATIENT_TEMP_PERM (TempPatient, TempPatientText) VALUES ("
				+ (int)data.TempPatient + ", " + "'" + MainClass.FixStringForSingleQuote(data.TempPatientText) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PatientTempPerm", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int TempPatient)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PATIENT_TEMP_PERM WHERE TempPatient='{0}'", 
				(int)TempPatient);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PatientTempPerm", "DeleteRecord");
			return Retval;
		}


	}
}
