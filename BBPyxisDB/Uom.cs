using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Uom
	{
		const string TableName = "UOM";

		// collection of record fields
		public class TableData
		{
			public string UnitOfMeasure;
			public DateTime Last_modified;

			public TableData(string UnitOfMeasure, DateTime Last_modified)
			{
				this.UnitOfMeasure = UnitOfMeasure;
				this.Last_modified = Last_modified;
			}
		}

		// return record given its primary key
		public static bool GetRecord(string UnitOfMeasure, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from UOM WHERE UnitOfMeasure='{0}'", 
				MainClass.FixStringForSingleQuote(UnitOfMeasure));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Uom", "GetRecord", out _conn, out myDataReader))
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
				myDataReader["UnitOfMeasure"].ToString()
				, MainClass.ToDate(TableName, myDataReader["Last_modified"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO UOM (UnitOfMeasure) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.UnitOfMeasure) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Uom", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(string UnitOfMeasure)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE UOM WHERE UnitOfMeasure='{0}'", 
				MainClass.FixStringForSingleQuote(UnitOfMeasure));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Uom", "DeleteRecord");
			return Retval;
		}


	}
}
