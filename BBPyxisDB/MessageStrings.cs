using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class MessageStrings
	{
		const string TableName = "MESSAGE_STRINGS";

		// collection of record fields
		public class TableData
		{
			public int Lcid;
			public int MsgNbr;
			public string MsgText;

			public TableData(int Lcid, int MsgNbr, string MsgText)
			{
				this.Lcid = Lcid;
				this.MsgNbr = MsgNbr;
				this.MsgText = MsgText;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int Lcid, int MsgNbr, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from MESSAGE_STRINGS WHERE Lcid='{0}' AND MsgNbr='{1}'", 
				(int)Lcid, (int)MsgNbr);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "MessageStrings", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["Lcid"])
				, MainClass.ToInt(TableName, myDataReader["MsgNbr"])
				, myDataReader["MsgText"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO MESSAGE_STRINGS (Lcid, MsgNbr, MsgText) VALUES ("
				+ (int)data.Lcid + ", " + (int)data.MsgNbr + ", " + "'" + MainClass.FixStringForSingleQuote(data.MsgText) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "MessageStrings", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int Lcid, int MsgNbr)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE MESSAGE_STRINGS WHERE Lcid='{0}' AND MsgNbr='{1}'", 
				(int)Lcid, (int)MsgNbr);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "MessageStrings", "DeleteRecord");
			return Retval;
		}


	}
}
