using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif


namespace BBPyxisDB
{
    public class EmailOnlyUsers
    {
        const string TableName = "EMAIL_ONLY_USERS";
        const string ModuleName = "EmailOnlyUsers";

        // collection of record fields
        public class TableData
        {
            public string Description;
            public int emailId;
            public int creatorIid;
            public DateTime addedTime;

            public TableData(string Description, int emailId, int creatorIid, DateTime addedTime)
            {
                this.Description = Description;
                this.emailId = emailId;
                this.creatorIid = creatorIid;
                this.addedTime = addedTime;
            }
        }

        public class EmailData
        {
            public int emailId;
            public string Description;
            public string emailAddress;

            public EmailData(int emailId, string Description, string emailAddress)
            {
                this.emailId = emailId;
                this.Description = Description;
                this.emailAddress = emailAddress;
            }
        }

        // return record given its emailId
        public static bool GetRecord(int eMailId, out TableData data)
        {
            bool Retval = true;
            data = null;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from EMAIL_ONLY_USERS WHERE emailId ='{0}'",
                (int)eMailId);
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, ModuleName, "GetRecord", out _conn, out myDataReader))
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

        public static bool GetAllEmailAddresses(List<EmailData> list, string search)
        {
            bool Retval = true;
            list.Clear();
#if !NO_ASA
            EmailData data;
            SAConnection _conn;
            SADataReader myDataReader;
            
            string SqlStatement;
            if (string.IsNullOrEmpty(search))
                SqlStatement = "SELECT EMAIL_ONLY_USERS.emailId, EMAIL_ONLY_USERS.description, EMAIL.emailAddress from EMAIL_ONLY_USERS, EMAIL  where EMAIL.emailid = EMAIL_ONLY_USERS.emailId ORDER BY EMAIL_ONLY_USERS.description";
            else
                SqlStatement = string.Format("SELECT EMAIL_ONLY_USERS.emailId, EMAIL_ONLY_USERS.description, EMAIL.emailAddress from EMAIL_ONLY_USERS, EMAIL  where EMAIL.emailid = EMAIL_ONLY_USERS.emailId and EMAIL_ONLY_USERS.Description like '{0}' ORDER BY EMAIL_ONLY_USERS.description", search);
            
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, ModuleName, "GetAllEmailAddresses", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        MakeEmailRec(myDataReader, out data);
                        list.Add(data);
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, ModuleName, "GetAllEmailAddresses", err);
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

        public static bool GetEmailByAddresses(List<EmailData> list, string search)
        {
            bool Retval = true;
            list.Clear();
#if !NO_ASA
            EmailData data;
            SAConnection _conn;
            SADataReader myDataReader;

            string SqlStatement;
            if (string.IsNullOrEmpty(search))
                SqlStatement = "SELECT EMAIL_ONLY_USERS.emailId, EMAIL_ONLY_USERS.description, EMAIL.emailAddress from EMAIL_ONLY_USERS, EMAIL  where EMAIL.emailid = EMAIL_ONLY_USERS.emailId";
            else
                SqlStatement = string.Format("SELECT EMAIL_ONLY_USERS.emailId, EMAIL_ONLY_USERS.description, EMAIL.emailAddress from EMAIL_ONLY_USERS, EMAIL  where EMAIL.emailid = EMAIL_ONLY_USERS.emailId and EMAIL.emailAddress like '{0}'", search);

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, ModuleName, "GetEmailByAddresses", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        MakeEmailRec(myDataReader, out data);
                        list.Add(data);
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, ModuleName, "GetEmailByAddresses", err);
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
                myDataReader["Description"].ToString(),
                MainClass.ToInt(TableName, myDataReader["emailId"]),
                MainClass.ToInt(TableName, myDataReader["CreatorIid"]),
                MainClass.ToDate(TableName, myDataReader["AddedTime"]));
        }

        static void MakeEmailRec(SADataReader myDataReader, out EmailData data)
        {
            data = new EmailData(MainClass.ToInt(TableName, myDataReader["emailId"]),
                 myDataReader["Description"].ToString(),
                 myDataReader["emailAddress"].ToString());
        }
#endif

        // insert record and return its primary key
        public static bool InsertRecord(TableData data, out int NewIid)
        {
            bool Retval = false;
            NewIid = -1;

            string SqlStatement = "INSERT INTO EMAIL_ONLY_USERS (description, emailId, CreatorIid) VALUES ("
                + "'" + MainClass.FixStringForSingleQuote(data.Description) + "', " + data.emailId + "," + data.creatorIid + ")";
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, ModuleName, "InsertRecord", out NewIid);
            return Retval;
        }

        // update email description based on emailId
        public static bool UpdateEmailDescription(string emailDescription, int emailId)
        {
            bool Retval = false;
            string SqlStatement = "UPDATE EMAIL_ONLY_USERS SET description = '" + MainClass.FixStringForSingleQuote(emailDescription) 
                + "' WHERE emailId = " + emailId;
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, ModuleName, "UpdateEmailDescription");
            return Retval;
        }

        // delete record given its primary key
        public static bool DeleteRecord(int emailId)
        {
            bool Retval = true;
            string SqlStatement = string.Format("DELETE EMAIL_ONLY_USERS WHERE EMAILID ='{0}'",
                (int)emailId);
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, ModuleName, "DeleteRecord");
            return Retval;
        }

    }
}
