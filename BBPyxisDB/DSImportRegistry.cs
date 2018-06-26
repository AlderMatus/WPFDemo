using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class DSImportRegistry
	{
		const string TableName = "D_S_IMPORT_REGISTRY";

		// collection of record fields
		public class TableData
		{
			public string ImportFileName;
			public DateTime ImportTime;
			public string ImportStatus;
			public string LastKeyLine;
			public string ImportHeader;

			public TableData(string ImportFileName, DateTime ImportTime, string ImportStatus, string LastKeyLine, string ImportHeader)
			{
				this.ImportFileName = ImportFileName;
				this.ImportTime = ImportTime;
				this.ImportStatus = ImportStatus;
				this.LastKeyLine = LastKeyLine;
				this.ImportHeader = ImportHeader;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(string ImportFileName, DateTime ImportTime, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from D_S_IMPORT_REGISTRY WHERE ImportFileName='{0}' AND ImportTime='{1}'", 
				MainClass.FixStringForSingleQuote(ImportFileName), MainClass.DateTimeToTimestamp(ImportTime));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "DSImportRegistry", "GetRecord", out _conn, out myDataReader))
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
				myDataReader["ImportFileName"].ToString()
				, MainClass.ToDate(TableName, myDataReader["ImportTime"])
				, myDataReader["ImportStatus"].ToString()
				, myDataReader["LastKeyLine"].ToString()
				, myDataReader["ImportHeader"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO D_S_IMPORT_REGISTRY (ImportFileName, ImportTime, ImportStatus, LastKeyLine, ImportHeader) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.ImportFileName) + "'" + ", " + MainClass.DateTimeToTimestamp(data.ImportTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.ImportStatus) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.LastKeyLine) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ImportHeader) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "DSImportRegistry", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(string ImportFileName, DateTime ImportTime)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE D_S_IMPORT_REGISTRY WHERE ImportFileName='{0}' AND ImportTime='{1}'", 
				MainClass.FixStringForSingleQuote(ImportFileName), MainClass.DateTimeToTimestamp(ImportTime));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "DSImportRegistry", "DeleteRecord");
			return Retval;
		}


	}
}
