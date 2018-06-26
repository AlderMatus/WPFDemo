using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Msgtypes
	{
		const string TableName = "MSGTYPES";

		// collection of record fields
		public class TableData
		{
			public string MsgType;
			public int MsgTypeId;
			public string MsgDesc;
			public int SelectedIn;
			public int SelectedOut;
			public int IsV13Msg;
			public int ProcarMsgFwdGroup;

			public TableData(string MsgType, int MsgTypeId, string MsgDesc, int SelectedIn, int SelectedOut, int IsV13Msg, int ProcarMsgFwdGroup)
			{
				this.MsgType = MsgType;
				this.MsgTypeId = MsgTypeId;
				this.MsgDesc = MsgDesc;
				this.SelectedIn = SelectedIn;
				this.SelectedOut = SelectedOut;
				this.IsV13Msg = IsV13Msg;
				this.ProcarMsgFwdGroup = ProcarMsgFwdGroup;
			}
		}

		// return record given its primary key
		public static bool GetRecord(string MsgType, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from MSGTYPES WHERE MsgType='{0}'", 
				MainClass.FixStringForSingleQuote(MsgType));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Msgtypes", "GetRecord", out _conn, out myDataReader))
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
				myDataReader["MsgType"].ToString()
				, MainClass.ToInt(TableName, myDataReader["MsgTypeId"])
				, myDataReader["MsgDesc"].ToString()
				, MainClass.ToInt(TableName, myDataReader["SelectedIn"])
				, MainClass.ToInt(TableName, myDataReader["SelectedOut"])
				, MainClass.ToInt(TableName, myDataReader["IsV13Msg"])
				, MainClass.ToInt(TableName, myDataReader["ProcarMsgFwdGroup"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO MSGTYPES (MsgType, MsgTypeId, MsgDesc, SelectedIn, SelectedOut, IsV13Msg, ProcarMsgFwdGroup) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.MsgType) + "'" + ", " + (int)data.MsgTypeId + ", " + "'" + MainClass.FixStringForSingleQuote(data.MsgDesc) + "'" + ", " + (int)data.SelectedIn + ", " + (int)data.SelectedOut + ", " + (int)data.IsV13Msg + ", " + (int)data.ProcarMsgFwdGroup + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Msgtypes", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(string MsgType)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE MSGTYPES WHERE MsgType='{0}'", 
				MainClass.FixStringForSingleQuote(MsgType));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Msgtypes", "DeleteRecord");
			return Retval;
		}


	}
}
