using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Kits
	{
		const string TableName = "KITS";

		// collection of record fields
		public class TableData
		{
			public int KitIid;
			public string KitId;
			public string KitName;
			public int AllAreas;
			public int KitSource;
			public string KitEditorName;
			public string KitEditorId;
			public int KitType;
			public int IsBillable;
			public int CreditItems;

			public TableData(int KitIid, string KitId, string KitName, int AllAreas, int KitSource, string KitEditorName, string KitEditorId, int KitType, int IsBillable, int CreditItems)
			{
				this.KitIid = KitIid;
				this.KitId = KitId;
				this.KitName = KitName;
				this.AllAreas = AllAreas;
				this.KitSource = KitSource;
				this.KitEditorName = KitEditorName;
				this.KitEditorId = KitEditorId;
				this.KitType = KitType;
				this.IsBillable = IsBillable;
				this.CreditItems = CreditItems;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int KitIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from KITS WHERE KitIid='{0}'", 
				(int)KitIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Kits", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["KitIid"])
				, myDataReader["KitId"].ToString()
				, myDataReader["KitName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["AllAreas"])
				, MainClass.ToInt(TableName, myDataReader["KitSource"])
				, myDataReader["KitEditorName"].ToString()
				, myDataReader["KitEditorId"].ToString()
				, MainClass.ToInt(TableName, myDataReader["KitType"])
				, MainClass.ToInt(TableName, myDataReader["IsBillable"])
				, MainClass.ToInt(TableName, myDataReader["CreditItems"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO KITS (KitIid, KitId, KitName, AllAreas, KitSource, KitEditorName, KitEditorId, KitType, IsBillable, CreditItems) VALUES ("
				+ (int)data.KitIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.KitId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.KitName) + "'" + ", " + (int)data.AllAreas + ", " + (int)data.KitSource + ", " + "'" + MainClass.FixStringForSingleQuote(data.KitEditorName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.KitEditorId) + "'" + ", " + (int)data.KitType + ", " + (int)data.IsBillable + ", " + (int)data.CreditItems + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Kits", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int KitIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE KITS WHERE KitIid='{0}'", 
				(int)KitIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Kits", "DeleteRecord");
			return Retval;
		}


	}
}
