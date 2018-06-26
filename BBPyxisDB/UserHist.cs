using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class UserHist
	{
		const string TableName = "USER_HIST";

		// collection of record fields
		public class TableData
		{
			public int UserIid;
			public DateTime EffectTime;
			public int UserHistIid;
			public int Action;
			public string UserName;
			public string UserId;
			//Rename to clarify this is already encrypted
            public string EncryptedPass;
            public int AllAreas;
			public DateTime UserExpires;
			public DateTime PwExpired;
			public string CardReaderId;
			public string SourceDev;
			public string CreatorId;
			public string CreatorName;
			public int StnPriv;
			public int RxPriv;
			public string AccessType;
			public string AdminRightsMask;
			public string StockRightsMask;
			public int HasBeenArchived;
			public int TempUser;

			public TableData(int UserIid, DateTime EffectTime, int UserHistIid, int Action, string UserName, string UserId, string EncryptedPass, int AllAreas, DateTime UserExpires, DateTime PwExpired, string CardReaderId, string SourceDev, string CreatorId, string CreatorName, int StnPriv, int RxPriv, string AccessType, string AdminRightsMask, string StockRightsMask, int HasBeenArchived, int TempUser)
			{
				this.UserIid = UserIid;
				this.EffectTime = EffectTime;
				this.UserHistIid = UserHistIid;
				this.Action = Action;
				this.UserName = UserName;
				this.UserId = UserId;
				this.EncryptedPass = EncryptedPass;
				this.AllAreas = AllAreas;
				this.UserExpires = UserExpires;
				this.PwExpired = PwExpired;
				this.CardReaderId = CardReaderId;
				this.SourceDev = SourceDev;
				this.CreatorId = CreatorId;
				this.CreatorName = CreatorName;
				this.StnPriv = StnPriv;
				this.RxPriv = RxPriv;
				this.AccessType = AccessType;
				this.AdminRightsMask = AdminRightsMask;
				this.StockRightsMask = StockRightsMask;
				this.HasBeenArchived = HasBeenArchived;
				this.TempUser = TempUser;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(int UserIid, DateTime EffectTime, int UserHistIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from USER_HIST WHERE UserIid='{0}' AND EffectTime='{1}' AND UserHistIid='{2}'", 
				(int)UserIid, MainClass.DateTimeToTimestamp(EffectTime), (int)UserHistIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "UserHist", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["UserIid"])
				, MainClass.ToDate(TableName, myDataReader["EffectTime"])
				, MainClass.ToInt(TableName, myDataReader["UserHistIid"])
				, MainClass.ToInt(TableName, myDataReader["Action"])
				, myDataReader["UserName"].ToString()
				, myDataReader["UserId"].ToString()
				, myDataReader["PassWord"].ToString()
				, MainClass.ToInt(TableName, myDataReader["AllAreas"])
				, MainClass.ToDate(TableName, myDataReader["UserExpires"])
				, MainClass.ToDate(TableName, myDataReader["PwExpired"])
				, myDataReader["CardReaderId"].ToString()
				, myDataReader["SourceDev"].ToString()
				, myDataReader["CreatorId"].ToString()
				, myDataReader["CreatorName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["StnPriv"])
				, MainClass.ToInt(TableName, myDataReader["RxPriv"])
				, myDataReader["AccessType"].ToString()
				, myDataReader["AdminRightsMask"].ToString()
				, myDataReader["StockRightsMask"].ToString()
				, MainClass.ToInt(TableName, myDataReader["HasBeenArchived"])
				, MainClass.ToInt(TableName, myDataReader["TempUser"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO USER_HIST (UserIid, EffectTime, Action, UserName, UserId, PassWord, AllAreas, UserExpires, PwExpired, CardReaderId, SourceDev, CreatorId, CreatorName, StnPriv, RxPriv, AccessType, AdminRightsMask, StockRightsMask, HasBeenArchived, TempUser) VALUES ("
				+ (int)data.UserIid + ", " + MainClass.DateTimeToTimestamp(data.EffectTime) + ", " + (int)data.Action + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.EncryptedPass) + "'" + ", " + (int)data.AllAreas + ", " + MainClass.DateTimeToTimestamp(data.UserExpires) + ", " + MainClass.DateTimeToTimestamp(data.PwExpired) + ", " + "'" + MainClass.FixStringForSingleQuote(data.CardReaderId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SourceDev) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CreatorId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CreatorName) + "'" + ", " + (int)data.StnPriv + ", " + (int)data.RxPriv + ", " + "'" + MainClass.FixStringForSingleQuote(data.AccessType) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.AdminRightsMask) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.StockRightsMask) + "'" + ", " + (int)data.HasBeenArchived + ", " + (int)data.TempUser + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserHist", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int UserIid, DateTime EffectTime, int UserHistIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE USER_HIST WHERE UserIid='{0}' AND EffectTime='{1}' AND UserHistIid='{2}'", 
				(int)UserIid, MainClass.DateTimeToTimestamp(EffectTime), (int)UserHistIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "UserHist", "DeleteRecord");
			return Retval;
		}


	}
}
