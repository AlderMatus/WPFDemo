using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class DiscResolutionReason
	{
		const string TableName = "DISC_RESOLUTION_REASON";

		// collection of record fields
		public class TableData
		{
			public int ResolutionNum;
			public string ResolutionReason;

			public TableData(int ResolutionNum, string ResolutionReason)
			{
				this.ResolutionNum = ResolutionNum;
				this.ResolutionReason = ResolutionReason;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int ResolutionNum, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from DISC_RESOLUTION_REASON WHERE ResolutionNum='{0}'", 
				(int)ResolutionNum);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "DiscResolutionReason", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["ResolutionNum"])
				, myDataReader["ResolutionReason"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO DISC_RESOLUTION_REASON (ResolutionNum, ResolutionReason) VALUES ("
				+ (int)data.ResolutionNum + ", " + "'" + MainClass.FixStringForSingleQuote(data.ResolutionReason) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "DiscResolutionReason", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int ResolutionNum)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE DISC_RESOLUTION_REASON WHERE ResolutionNum='{0}'", 
				(int)ResolutionNum);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "DiscResolutionReason", "DeleteRecord");
			return Retval;
		}


	}
}
