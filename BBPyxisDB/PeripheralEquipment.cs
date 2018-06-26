using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class PeripheralEquipment
	{
		const string TableName = "PERIPHERAL_EQUIPMENT";

		// collection of record fields
		public class TableData
		{
			public int EquipmentIid;
			public int EquipmentType;
			public string SerialNumber;
			public DateTime InstallDate;
			public DateTime LastModDate;

			public TableData(int EquipmentIid, int EquipmentType, string SerialNumber, DateTime InstallDate, DateTime LastModDate)
			{
				this.EquipmentIid = EquipmentIid;
				this.EquipmentType = EquipmentType;
				this.SerialNumber = SerialNumber;
				this.InstallDate = InstallDate;
				this.LastModDate = LastModDate;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int EquipmentIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PERIPHERAL_EQUIPMENT WHERE EquipmentIid='{0}'", 
				(int)EquipmentIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PeripheralEquipment", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["EquipmentIid"])
				, MainClass.ToInt(TableName, myDataReader["EquipmentType"])
				, myDataReader["SerialNumber"].ToString()
				, MainClass.ToDate(TableName, myDataReader["InstallDate"])
				, MainClass.ToDate(TableName, myDataReader["LastModDate"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO PERIPHERAL_EQUIPMENT (EquipmentType, SerialNumber, InstallDate, LastModDate) VALUES ("
				+ (int)data.EquipmentType + ", " + "'" + MainClass.FixStringForSingleQuote(data.SerialNumber) + "'" + ", " + MainClass.DateTimeToTimestamp(data.InstallDate) + ", " + MainClass.DateTimeToTimestamp(data.LastModDate) + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PeripheralEquipment", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int EquipmentIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PERIPHERAL_EQUIPMENT WHERE EquipmentIid='{0}'", 
				(int)EquipmentIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PeripheralEquipment", "DeleteRecord");
			return Retval;
		}


	}
}
