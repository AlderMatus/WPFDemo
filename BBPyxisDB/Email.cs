using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Email
	{
		const string TableName = "EMAIL";

		// collection of record fields
		public class TableData
		{
			public int EmailId;
			public string EmailAddress;

			public TableData(int EmailId, string EmailAddress)
			{
				this.EmailId = EmailId;
				this.EmailAddress = EmailAddress;
			}
		}

        public class emailUserData
        {
            public string Name;
            public string EmailAddress;
            public int EmailId;

            public emailUserData(string name, string emailAddress, int emailId)
            {
                this.Name = name;
                this.EmailAddress = emailAddress;
                this.EmailId = emailId;
            }
        }

		// return record given its primary key
		public static bool GetRecord(int EmailId, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from EMAIL WHERE EmailId='{0}'", 
				(int)EmailId);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Email", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["EmailId"])
				, myDataReader["EmailAddress"].ToString());
		}

        static void MakeEmailDataRec(SADataReader myDataReader, out emailUserData data)
        {
            data = new emailUserData(
                myDataReader["name"].ToString(), 
                myDataReader["address"].ToString(),
                MainClass.ToInt(TableName,myDataReader["emailId"]));
        }
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO EMAIL (EmailAddress) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.EmailAddress) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Email", "InsertRecord", out NewIid);
			return Retval;
		}

        // update email record
        public static bool UpdateRecord(TableData data)
        {
            bool Retval = false;
            string SqlStatement = "UPDATE EMAIL set EmailAddress = '"
                + MainClass.FixStringForSingleQuote(data.EmailAddress) + "' WHERE emailId = " + data.EmailId;
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Email", "UpdateRecord");
            return Retval;
        }

        // delete record given its primary key
		public static bool DeleteRecord(int EmailId)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE EMAIL WHERE EmailId='{0}'", 
				(int)EmailId);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Email", "DeleteRecord");
			return Retval;
		}

        public static bool GetAllEmailUsers(List<emailUserData> list)
        {
            bool Retval = true;
            list.Clear();
#if !NO_ASA
            emailUserData data;
            SAConnection _conn;
            SADataReader myDataReader;

            string SqlStatement = "SELECT u.UserName name, e.emailaddress address, e.emailID emailID FROM Users u,  email e where e.emailID = u.emailID  union " + 
                                  "select o.description name, e.emailaddress, e.emailID FROM email_only_users o, email e where e.emailID = o.emailID order by name";

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Email", "GetAllEmailUsers", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        MakeEmailDataRec(myDataReader, out data);
                        list.Add(data);
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "Email", "GetAllEmailUsers", err);
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
	}
}
