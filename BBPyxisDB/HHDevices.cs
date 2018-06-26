using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class HHDevices
	{
		const string TableName = "HH_DEVICES";

		// collection of record fields
		public class TableData
		{
			public int DeviceIid;
			public string DeviceName;
			public int DeviceType;
			public string Version;
			public int LoginMode;
			public int Polling;
			public int CommMode;
			public string CommBox;
			public DateTime RebootTime;
			public DateTime Last_modified;

			public TableData(int DeviceIid, string DeviceName, int DeviceType, string Version, int LoginMode, int Polling, int CommMode, string CommBox, DateTime RebootTime, DateTime Last_modified)
			{
				this.DeviceIid = DeviceIid;
				this.DeviceName = DeviceName;
				this.DeviceType = DeviceType;
				this.Version = Version;
				this.LoginMode = LoginMode;
				this.Polling = Polling;
				this.CommMode = CommMode;
				this.CommBox = CommBox;
				this.RebootTime = RebootTime;
				this.Last_modified = Last_modified;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int DeviceIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from HH_DEVICES WHERE DeviceIid='{0}'", 
				(int)DeviceIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "HHDevices", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["DeviceIid"])
				, myDataReader["DeviceName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["DeviceType"])
				, myDataReader["Version"].ToString()
				, MainClass.ToInt(TableName, myDataReader["LoginMode"])
				, MainClass.ToInt(TableName, myDataReader["Polling"])
				, MainClass.ToInt(TableName, myDataReader["CommMode"])
				, myDataReader["CommBox"].ToString()
				, MainClass.ToDate(TableName, myDataReader["RebootTime"])
				, MainClass.ToDate(TableName, myDataReader["Last_modified"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO HH_DEVICES (DeviceName, DeviceType, Version, LoginMode, Polling, CommMode, CommBox, RebootTime) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.DeviceName) + "'" + ", " + (int)data.DeviceType + ", " + "'" + MainClass.FixStringForSingleQuote(data.Version) 
                + "'" + ", " + (int)data.LoginMode + ", " + (int)data.Polling + ", " + (int)data.CommMode + ", " + "'" + MainClass.FixStringForSingleQuote(data.CommBox) 
                + "'" + ", " + MainClass.DateTimeToTimestamp(data.RebootTime) + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHDevices", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int DeviceIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE HH_DEVICES WHERE DeviceIid='{0}'", 
				(int)DeviceIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "HHDevices", "DeleteRecord");
			return Retval;
		}


	}
}
