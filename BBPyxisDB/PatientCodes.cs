using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class PatientCodes
	{
		const string TableName = "PATIENT_CODES";

		// collection of record fields
		public class TableData
		{
			public int PtIid;
			public string CodeMethod;
			public string CodeValue;
			public DateTime EffectTime;
			public string CodeDescription;
			public string Physician1;
			public string Physician2;
			public string Physician3;
			public string AssocCodeValue;
			public string Drg;

			public TableData(int PtIid, string CodeMethod, string CodeValue, DateTime EffectTime, string CodeDescription, string Physician1, string Physician2, string Physician3, string AssocCodeValue, string Drg)
			{
				this.PtIid = PtIid;
				this.CodeMethod = CodeMethod;
				this.CodeValue = CodeValue;
				this.EffectTime = EffectTime;
				this.CodeDescription = CodeDescription;
				this.Physician1 = Physician1;
				this.Physician2 = Physician2;
				this.Physician3 = Physician3;
				this.AssocCodeValue = AssocCodeValue;
				this.Drg = Drg;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int PtIid, string CodeMethod, string CodeValue, DateTime EffectTime, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PATIENT_CODES WHERE PtIid='{0}' AND CodeMethod='{1}' AND CodeValue='{2}' AND EffectTime='{3}'", 
				(int)PtIid, MainClass.FixStringForSingleQuote(CodeMethod), MainClass.FixStringForSingleQuote(CodeValue), MainClass.DateTimeToTimestamp(EffectTime));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PatientCodes", "GetRecord", out _conn, out myDataReader))
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
				, myDataReader["CodeMethod"].ToString()
				, myDataReader["CodeValue"].ToString()
				, MainClass.ToDate(TableName, myDataReader["EffectTime"])
				, myDataReader["CodeDescription"].ToString()
				, myDataReader["Physician1"].ToString()
				, myDataReader["Physician2"].ToString()
				, myDataReader["Physician3"].ToString()
				, myDataReader["AssocCodeValue"].ToString()
				, myDataReader["Drg"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO PATIENT_CODES (PtIid, CodeMethod, CodeValue, EffectTime, CodeDescription, Physician1, Physician2, Physician3, AssocCodeValue, Drg) VALUES ("
				+ (int)data.PtIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.CodeMethod) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CodeValue) + "'" + ", " + MainClass.DateTimeToTimestamp(data.EffectTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.CodeDescription) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Physician1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Physician2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Physician3) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.AssocCodeValue) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Drg) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PatientCodes", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int PtIid, string CodeMethod, string CodeValue, DateTime EffectTime)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PATIENT_CODES WHERE PtIid='{0}' AND CodeMethod='{1}' AND CodeValue='{2}' AND EffectTime='{3}'", 
				(int)PtIid, MainClass.FixStringForSingleQuote(CodeMethod), MainClass.FixStringForSingleQuote(CodeValue), MainClass.DateTimeToTimestamp(EffectTime));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PatientCodes", "DeleteRecord");
			return Retval;
		}


	}
}
