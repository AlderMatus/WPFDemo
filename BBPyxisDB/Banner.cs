using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Banner
	{
		const string TableName = "BANNER";

		// collection of record fields
		public class TableData
		{
			public int DeviceIid;
			public string DeviceName;
			public int ColorRef;
			public int Speed;
			public int Font;
			public string Msg;

			public TableData(int DeviceIid, string DeviceName, int ColorRef, int Speed, int Font, string Msg)
			{
				this.DeviceIid = DeviceIid;
				this.DeviceName = DeviceName;
				this.ColorRef = ColorRef;
				this.Speed = Speed;
				this.Font = Font;
				this.Msg = Msg;
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
			string SqlStatement = string.Format("SELECT * from BANNER WHERE DeviceIid='{0}'", 
				(int)DeviceIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Banner", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["DeviceIid"])
				, myDataReader["DeviceName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ColorRef"])
				, MainClass.ToInt(TableName, myDataReader["Speed"])
				, MainClass.ToInt(TableName, myDataReader["Font"])
				, myDataReader["Msg"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO BANNER (DeviceIid, DeviceName, ColorRef, Speed, Font, Msg) VALUES ("
				+ (int)data.DeviceIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.DeviceName) + "'" + ", " + (int)data.ColorRef + ", " + (int)data.Speed + ", " + (int)data.Font + ", " + "'" + MainClass.FixStringForSingleQuote(data.Msg) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Banner", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int DeviceIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE BANNER WHERE DeviceIid='{0}'", 
				(int)DeviceIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Banner", "DeleteRecord");
			return Retval;
		}


	}
}
