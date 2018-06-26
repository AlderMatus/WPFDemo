using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class HHDeviceStatus
	{
		const string TableName = "HH_DEVICE_STATUS";

		// collection of record fields
		public class TableData
		{
			public string DeviceName;
			public int TempField;
			public int HasCommFailure;
			public DateTime Last_modified;

			public TableData(string DeviceName, int TempField, int HasCommFailure, DateTime Last_modified)
			{
				this.DeviceName = DeviceName;
				this.TempField = TempField;
				this.HasCommFailure = HasCommFailure;
				this.Last_modified = Last_modified;
			}
		}

		// return record given its primary key
		public static bool GetRecord(string DeviceName, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from HH_DEVICE_STATUS WHERE DeviceName='{0}'", 
				MainClass.FixStringForSingleQuote(DeviceName));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "HHDeviceStatus", "GetRecord", out _conn, out myDataReader))
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
				myDataReader["DeviceName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["TempField"])
				, MainClass.ToInt(TableName, myDataReader["HasCommFailure"])
				, MainClass.ToDate(TableName, myDataReader["Last_modified"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO HH_DEVICE_STATUS (DeviceName, TempField, HasCommFailure) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.DeviceName) + "'" + ", " + (int)data.TempField + ", " + (int)data.HasCommFailure + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHDeviceStatus", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(string DeviceName)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE HH_DEVICE_STATUS WHERE DeviceName='{0}'", 
				MainClass.FixStringForSingleQuote(DeviceName));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHDeviceStatus", "DeleteRecord");
			return Retval;
		}


	}
}
