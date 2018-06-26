using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{
    public class EmailListToEmail
    {
        public enum EmailType
        {
            User = 0,          // email ID is an individuals email ID
            List = 1,           // email ID is a List of individual emails - Custom list
        }                       // List supports Custom list on report, which can include list of emails

        const string TableName = "EMAIL_LIST_TO_EMAIL";

        // collection of record fields
        public class TableData
        {
            public int listIid;
            public int emailId;
            public int emailType;

            public TableData(int listIid, int emailId, int emailType)
            {
                this.listIid = listIid;
                this.emailId = emailId;
                this.emailType = emailType;
            }
        }

        // return record given its emailId
        public static bool GetRecord(int listIid, int emailId, out TableData data)
        {
            bool Retval = true;
            data = null;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from EMAIL_LIST_TO_EMAIL WHERE ListIid ='{0}' AND EmailId = '(1)'",
                (int)listIid, (int)emailId);
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "EmailListToEmail", "GetRecord", out _conn, out myDataReader))
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
                MainClass.ToInt(TableName, myDataReader["ListIid"]),
                MainClass.ToInt(TableName, myDataReader["emailId"]),
                MainClass.ToInt(TableName, myDataReader["emailType"]));
        }
#endif

        // insert record and return its primary key
        public static bool InsertRecord(TableData data)
        {
            bool Retval = false;

            string SqlStatement = "INSERT INTO EMAIL_LIST_TO_EMAIL (listIid, emailId, EmailType) VALUES ("
                + data.listIid + ", " + data.emailId + "," + data.emailType + ")";
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "EmailListToEmail", "InsertRecord");
            return Retval;
        }


        // Insert a list of email IDs into the table.
        // NOTE:  All email Ids have to be of the SAME TYPE... see overload below for different types
        public static bool InsertEmailList(int listIid, List<int> emailIds, int emailType)
        {
            bool Retval = false;

            string SqlStatement = "INSERT INTO EMAIL_LIST_TO_EMAIL (listIid, emailId, EmailType) VALUES (";

            for (int i = 0; i <  emailIds.Count; i++)
            {
                SqlStatement += "(" + listIid + "," + emailIds[i] + "," + emailType + ")";
                if (i + 1 < emailIds.Count)
                    SqlStatement += ",";
            }
            SqlStatement += ")";
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "EmailListToEmail", "InsertRecord");
            return Retval;
        }

        // Insert a list into the EmailList table - supports both email Ids and List Ids in the same statement
        public static bool InsertEmailList(int listIid, List<int> emailIds, List<int> emailTypes)
        {
            bool Retval = false;

            Debug.Assert(emailIds.Count == emailTypes.Count);

            string SqlStatement = "INSERT INTO EMAIL_LIST_TO_EMAIL (listIid, emailId, EmailType) VALUES (";

            for (int i = 0; i < emailIds.Count; i++)
            {
                SqlStatement += "(" + listIid + "," + emailIds[i] + "," + emailTypes[i] + ")";
                if (i + 1 < emailIds.Count)
                    SqlStatement += ",";
            }
            SqlStatement += ")";

            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "EmailListToEmail", "InsertRecord");
            return Retval;
        }


        // delete record given its primary key
        public static bool DeleteRecord(int listIid, int emailId)
        {
            bool Retval = true;
            string SqlStatement = string.Format("DELETE EMAIL_LIST_TO_EMAIL WHERE ListIid ='{0}' AND EmailId = '(1)'",
                (int)listIid, (int)emailId);
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "EmailListToEmail", "DeleteRecord");
            return Retval;
        }

        public static bool GetEmailAddressesInList(int listIid, List<string> emailAddresses)
        {
            bool Retval = true;
            SAConnection _conn;
            SADataReader myDataReader;

            string SqlStatement = string.Format("SELECT E.EMAILADDRESS FROM EMAIL E,  EMAIL_LIST_TO_EMAIL L WHERE E.EMAILID = L.EMAILID AND L.LISTIID = '{0}' AND L.EMAILTYPE = 0 ORDER BY EMAILADDRESS", listIid);
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "EmailListToEmail", "GetSortedEmailAddressesForList", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        emailAddresses.Add(myDataReader["EMAILADDRESS"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "EmailListToEmail", "GetSortedEmailAddressList", err);
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
            return Retval;
        }

        // Get the list of email IDs associated with a given list id
        public static bool GetEmailIdsForList(int listIid, List<int> emailList, List<int> listList)
        {
            bool Retval = true;
            SAConnection _conn;
            SADataReader myDataReader;
            
            string SqlStatement = string.Format("Select EmailId, emailType  from EMAIL_LIST_TO_EMAIL WHERE ListIid = '{0}'", listIid);
            
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "EmailListToEmail", "GetEmailIdsForList", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        if (MainClass.ToInt(TableName, myDataReader["emailType"]) == (int)EmailType.User)
                            emailList.Add(MainClass.ToInt(TableName, myDataReader["emailId"]));
                        else
                            listList.Add(MainClass.ToInt(TableName, myDataReader["emailId"]));
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "EmailListToEmail", "GetEmailIdsForList", err);
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
            return Retval;
        }

        // check if a batch report or custom list is using the group list or email
        // return true if record found
        public static bool ReportLinkExists(int nIid, EmailType type)
        {
            bool Retval = false;
            string SqlStatement;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;

            if(type == EmailType.User)
            {
                SqlStatement = "SELECT * from EMAIL_LIST_TO_EMAIL where emailtype=0 and emailId=" + nIid.ToString();
            }
            else
            {
                SqlStatement = "SELECT * from EMAIL_LIST_TO_EMAIL where emailtype=1 and emailId=" + nIid.ToString();
            }

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "EmailListToEmail", "ReportLinkExists", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                        Retval = true;
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        "Report", ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "EmailListToEmail", "ReportLinkExists", err);
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

        public static bool DeleteEmailsFromList(int listIid)
        {
            bool Retval = true;
            string SqlStatement = string.Format("DELETE EMAIL_LIST_TO_EMAIL WHERE ListIid='{0}'", listIid);
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "EMAIL_LIST_TO_EMAIL", "DeleteEmailsFromList");

            return Retval;
        }

        public static bool DeleteOrphans()
        {
            bool Retval = true;
            string SqlStatement = string.Format("DELETE EMAIL_LIST_TO_EMAIL WHERE ListIid NOT IN (SELECT listIid FROM EMAIL_LIST)");
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "EMAIL_LIST_TO_EMAIL", "DeleteOrphans");

            return Retval;
        }

    }
}
