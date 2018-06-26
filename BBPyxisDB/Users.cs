using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Users
	{
		const string TableName = "USERS";

		// collection of record fields
		public class TableData
		{
			public int UserIid;
			public string UserName;
			public string UserId;
            //Rename to clarify this is already encrypted
			public string EncryptedPass;
			public int AllAreas;
			public DateTime UserExpires;
			public DateTime PwExpired;
			public DateTime LastUserActTime;
			public string CardReaderId;
			public string UserDescription;
			public string SourceDevName;
			public string CreatorId;
			public string CreatorName;
			public string UserCredentials;
			public int AccessTypeIid;
			public int TempUser;
			public string AdminRightsMask;
			public string StockRightsMask;
			public int TemplateUser;
			public int StnPriv;
			public int RxPriv;
			public int NoBioID;
			public int Owner;
			public DateTime Last_modified;
			public int EmailId;
			public string PasswordSalt;
            public int DomainIid;
            public string DomainUserName;
            public string EncryptedBlock;
            public int ActiveDirectory;

			public TableData(int UserIid, string UserName, string UserId, string EncryptedPass, int AllAreas, DateTime UserExpires, DateTime PwExpired, DateTime LastUserActTime, string CardReaderId, string UserDescription, string SourceDevName, string CreatorId, string CreatorName, string UserCredentials, int AccessTypeIid, int TempUser, string AdminRightsMask, string StockRightsMask, int TemplateUser, int StnPriv, int RxPriv, int NoBioID, int Owner, DateTime Last_modified, int EmailId, string PasswordSalt, int DomainIid, string DomainUserName, string EncryptedBlock, int ActiveDirectory)
			{
				this.UserIid = UserIid;
				this.UserName = UserName;
				this.UserId = UserId;
				this.EncryptedPass = EncryptedPass;
				this.AllAreas = AllAreas;
				this.UserExpires = UserExpires;
				this.PwExpired = PwExpired;
				this.LastUserActTime = LastUserActTime;
				this.CardReaderId = CardReaderId;
				this.UserDescription = UserDescription;
				this.SourceDevName = SourceDevName;
				this.CreatorId = CreatorId;
				this.CreatorName = CreatorName;
				this.UserCredentials = UserCredentials;
				this.AccessTypeIid = AccessTypeIid;
				this.TempUser = TempUser;
				this.AdminRightsMask = AdminRightsMask;
				this.StockRightsMask = StockRightsMask;
				this.TemplateUser = TemplateUser;
				this.StnPriv = StnPriv;
				this.RxPriv = RxPriv;
				this.NoBioID = NoBioID;
				this.Owner = Owner;
				this.Last_modified = Last_modified;
				this.EmailId = EmailId;
				this.PasswordSalt = PasswordSalt;
                this.DomainIid = DomainIid;
                this.DomainUserName = DomainUserName;
                this.EncryptedBlock = EncryptedBlock;
                this.ActiveDirectory = ActiveDirectory;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int UserIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from USERS WHERE UserIid='{0}'", 
				(int)UserIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Users", "GetRecord", out _conn, out myDataReader))
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

        // return record given its primary key
        public static bool GetRecord(string UserId, out TableData data)
        {
            bool Retval = true;
            data = null;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from USERS WHERE UserId='{0}'", UserId);

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Users", "GetRecord", out _conn, out myDataReader))
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
				, myDataReader["UserName"].ToString()
				, myDataReader["UserId"].ToString()
				, myDataReader["PassWord"].ToString()
				, MainClass.ToInt(TableName, myDataReader["AllAreas"])
				, MainClass.ToDate(TableName, myDataReader["UserExpires"])
				, MainClass.ToDate(TableName, myDataReader["PwExpired"])
				, MainClass.ToDate(TableName, myDataReader["LastUserActTime"])
				, myDataReader["CardReaderId"].ToString()
				, myDataReader["UserDescription"].ToString()
				, myDataReader["SourceDevName"].ToString()
				, myDataReader["CreatorId"].ToString()
				, myDataReader["CreatorName"].ToString()
				, myDataReader["UserCredentials"].ToString()
				, MainClass.ToInt(TableName, myDataReader["AccessTypeIid"])
				, MainClass.ToInt(TableName, myDataReader["TempUser"])
				, myDataReader["AdminRightsMask"].ToString()
				, myDataReader["StockRightsMask"].ToString()
				, MainClass.ToInt(TableName, myDataReader["TemplateUser"])
				, MainClass.ToInt(TableName, myDataReader["StnPriv"])
				, MainClass.ToInt(TableName, myDataReader["RxPriv"])
				, MainClass.ToInt(TableName, myDataReader["NoBioID"])
				, MainClass.ToInt(TableName, myDataReader["Owner"])
				, MainClass.ToDate(TableName, myDataReader["Last_modified"])
				, MainClass.ToInt(TableName, myDataReader["EmailId"])
				, myDataReader["PasswordSalt"].ToString()
                , MainClass.ToInt(TableName, myDataReader["DomainIid"])
                , myDataReader["DomainUserName"].ToString()
                , myDataReader["EncryptedBlock"].ToString()
                , MainClass.ToInt(TableName, myDataReader["ActiveDirectory"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

            string SqlStatement = "INSERT INTO USERS (UserName, UserId, PassWord, AllAreas, UserExpires, PwExpired, LastUserActTime, CardReaderId, UserDescription, SourceDevName, CreatorId, CreatorName, UserCredentials, AccessTypeIid, TempUser, AdminRightsMask, StockRightsMask, TemplateUser, StnPriv, RxPriv, NoBioID, Owner, EmailId, PasswordSalt, DomainIid, DomainUserName, EncryptedBlock, ActiveDirectory) VALUES ("
                + "'" + MainClass.FixStringForSingleQuote(data.UserName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.EncryptedPass) + "'" + ", " + (int)data.AllAreas + ", " + MainClass.DateTimeToTimestamp(data.UserExpires) + ", " + MainClass.DateTimeToTimestamp(data.PwExpired) + ", " + MainClass.DateTimeToTimestamp(data.LastUserActTime) + ", " + "'" + MainClass.FixStringForSingleQuote(data.CardReaderId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserDescription) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SourceDevName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CreatorId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CreatorName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserCredentials) + "'" + ", " + (int)data.AccessTypeIid + ", " + (int)data.TempUser + ", " + "'" + MainClass.FixStringForSingleQuote(data.AdminRightsMask) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.StockRightsMask) + "'" + ", " + (int)data.TemplateUser + ", " + (int)data.StnPriv + ", " + (int)data.RxPriv + ", " + (int)data.NoBioID + ", " + (int)data.Owner + ", " + (int)data.EmailId + ", " + "'" + MainClass.FixStringForSingleQuote(data.PasswordSalt) + "'" + ", " + (int)data.DomainIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.DomainUserName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.EncryptedBlock) + "'" + ", " + (int)data.ActiveDirectory + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Users", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int UserIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE USERS WHERE UserIid='{0}'", 
				(int)UserIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Users", "DeleteRecord");
			return Retval;
		}

        public static bool GetDomainUsers(List<TableData> users, int domainIid)
        {
            bool Retval = true;
            users.Clear();
#if !NO_ASA
            TableData data;
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement;

            SqlStatement = string.Format("SELECT * from USERS WHERE DomainIid='{0}'", domainIid);
            
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Users", "GetAllDomains", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        MakeDataRec(myDataReader, out data);
                        users.Add(data);
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "Users", "GetDomainUsers", err);
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

        public static bool DoesDomainUserExists(string domainUserName, int domainIid)
        {
            int cnt = 0;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement;

            SqlStatement = string.Format("SELECT * from USERS WHERE domainUserName = '{0}' AND DomainIid='{1}'", domainUserName, domainIid);

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Users", "GetAllDomains", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        cnt++;
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "Users", "GetDomainUsers", err);
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
            return (cnt > 0);
        }
        // Remove the domain from all users that have indicated dommainIid
        public static bool RemoveDomain(int domainIid)
        {
            bool Retval = true;
            string SqlStatement = string.Format("UPDATE USERS SET DomainIid = -1, DomainUserName = '' WHERE DomainIid='{0}'", (int)domainIid);
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Users", "RemoveDomain");
            return Retval;
        }

        public static bool RemoveActiveDirectory()
        {
            bool Retval = true;
            string SqlStatement = string.Format("UPDATE USERS SET ActiveDirectory = 0, DomainIid = -1, DomainUserName = ''");
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Users", "RemoveActiveDirectory");
            return Retval;
        }

        public static bool SaveDomainCredentials(string userId, string DomainUserName, int DomainIid, string encryptedPass, string salt)
        {
            bool Retval = true;
            string SqlStatement;
            if (string.IsNullOrEmpty(salt))
                SqlStatement = string.Format("UPDATE USERS SET DomainUserName = '{0}', DomainIid = {1}, EncryptedBlock = '{2}' WHERE  UserId = '{3}'",
                                                DomainUserName, DomainIid, encryptedPass, userId);
            else
                SqlStatement = string.Format("UPDATE USERS SET DomainUserName = '{0}', DomainIid = {1}, EncryptedBlock = '{2}', PasswordSalt = '{3}'  WHERE  UserId = '{4}'",
                                                DomainUserName, DomainIid, encryptedPass, salt, userId);

            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Users", "SaveDomainCredentials");
            
            return Retval;
        }

        public static bool UpdateEncryptedBlock(string userid, string encryptedBlock, string salt)
        {
            bool Retval = true;
            string SqlStatement;

            if (string.IsNullOrEmpty(salt))
                SqlStatement = string.Format("UPDATE USERS SET EncryptedBlock = '{0}' WHERE  UserId = '{1}'", encryptedBlock, userid);
            else
                SqlStatement = string.Format("UPDATE USERS SET EncryptedBlock = '{0}', PasswordSalt = '{1}' WHERE  UserId = '{2}'", encryptedBlock, salt, userid);

            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Users", "UpdateEncryptedBlock");
            
            return Retval;
        }
	}
}
