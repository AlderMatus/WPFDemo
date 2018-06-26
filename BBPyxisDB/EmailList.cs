using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif


namespace BBPyxisDB
{
    public class EmailList
    {
		const string TableName = "EMAIL_LIST";
        const string ModuleName = "EmailList";

        public enum ListType
        {
            List = 0,
            Report = 1,
        }
		// collection of record fields
		public class TableData
		{
			public int ListIid;
			public string Description;
            public int ListType;

			public TableData(int ListIid, string Description, int ListType)
			{
				this.ListIid = ListIid;
				this.Description = Description;
                this.ListType = ListType;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int listIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from EMAIL_LIST WHERE ListIid ='{0}'", listIid);
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
        // Return all email list records based on search string
        public static bool GetAllLists(List<TableData> list, string search)
        {
            bool Retval = true;
            list.Clear();
#if !NO_ASA
            TableData data;
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement;
            if (string.IsNullOrEmpty(search))
                SqlStatement = "SELECT * FROM EMAIL_LIST WHERE LISTTYPE = 0 ORDER BY description";
            else
                SqlStatement = string.Format("SELECT * FROM EMAIL_LIST WHERE LISTTYPE = 0 AND DESCRIPTION LIKE '{0}' ORDER BY description", search);

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, ModuleName, "GetAllLists", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        MakeDataRec(myDataReader, out data);
                        list.Add(data);
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, ModuleName, "GetAllLists", err);
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
				MainClass.ToInt(TableName, myDataReader["ListIid"])
				, myDataReader["Description"].ToString(),
                MainClass.ToInt(TableName, myDataReader["ListType"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO EMAIL_LIST (description, listType) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.Description) + "', " + data.ListType + ")";
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, ModuleName, "InsertRecord", out NewIid);
			return Retval;
		}

        public static bool UpdateRecord(String listName, int listIid)
        {
            bool Retval = false;

            string SqlStatement = string.Format("UPDATE EMAIL_LIST SET description = '{0}'  WHERE listIid = '{1}'", listName, listIid);
  
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, ModuleName, "UpdateRecord");
            return Retval;
        }

		// delete record given its primary key
		public static bool DeleteRecord(int listIid)
		{
			bool Retval = true;
            
			string SqlStatement = string.Format("DELETE EMAIL_LIST WHERE ListIid='{0}'", listIid);
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, ModuleName, "DeleteRecord");
			return Retval;
		}

        public static bool DeleteEmailList(int listIid)
        {
            bool Retval = true;

            Retval = EmailListToEmail.DeleteEmailsFromList(listIid);
            if (Retval == true)
            {
                string SqlStatement = string.Format("DELETE EMAIL_LIST WHERE ListIid='{0}'", listIid);
                Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, ModuleName, "DeleteEmailList");
            }
            return Retval;
        }

        public static bool DeleteOrphans()
        {
            bool Retval = true;

            string SqlStatement = string.Format("DELETE EMAIL_LIST WHERE listType = 1 AND listIid NOT IN (SELECT emailId from REPORT WHERE eMailType = 1)");
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, ModuleName, "DeleteEmailList");

            if (Retval == true)
            {
                Retval = EmailListToEmail.DeleteOrphans();
            }
            return Retval;
        }
	}
}
