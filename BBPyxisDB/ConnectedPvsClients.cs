using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif


namespace BBPyxisDB
{
    public class ConnectedPvsClients
    {
        const string TableName = "CONNECTED_PVSCLIENTS";

        // collection of record fields
        public class TableData
        {
            public int ClientIid;
            public string ClientName;
            public string ClientIP;
            public DateTime LastActivityTime;

            public TableData(int ClientIid, string ClientName, string ClientIP, DateTime LastActivityTime)
            {
                this.ClientIid = ClientIid;
                this.ClientName = ClientName;
                this.ClientIP = ClientIP;
                this.LastActivityTime = LastActivityTime;
            }
        }

        	// make a TableData object from a SADataReader record
#if !NO_ASA
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
			data = new TableData(
                MainClass.ToInt(TableName, myDataReader["ClientIid"]),
                myDataReader["ClientName"].ToString(),
                myDataReader["ClientIP"].ToString(),
                MainClass.ToDate(TableName, myDataReader["LastActivityTime"]));
		}
#endif

        		// return record given its primary key
		public static bool GetRecord(int ClientIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from CONNECTED_PVSCLIENTS WHERE ClientIid='{0}'", (int)ClientIid);

			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ConnectedPvsClients", "GetRecord", out _conn, out myDataReader))
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

        // insert record and return its primary key
        public static bool InsertRecord(TableData data, out int NewIid)
        {
            bool Retval = false;
            NewIid = -1;

            string SqlStatement = "INSERT INTO CONNECTED_PVSCLIENTS (ClientName, ClientIP) VALUES ('" 
                + MainClass.FixStringForSingleQuote(data.ClientName) + "', '" + MainClass.FixStringForSingleQuote(data.ClientIP) + "')";

            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ConnectedPvsClients", "InsertRecord", out NewIid);
            return Retval;
        }

        public static bool UpdateClientConnectTime(int ClientIid)
        {
            bool retVal = false;

            string SqlStatement = string.Format("UPDATE Connected_PVSClients SET LastActivityTime = GetDate() where clientIid = '{0}'", (int)ClientIid);
            retVal = MainClass.ExecuteSql(SqlStatement, true, TableName, "ConnectedPvsClients", "UpdateClientConnectTime");

            return retVal;
        }

		// delete record given its primary key
		public static bool DeleteRecord(int ClientIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE CONNECTED_PVSCLIENTS WHERE ClientIid='{0}'", (int)ClientIid);

			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ConnectedPvsClients", "DeleteRecord");
			return Retval;
		}

        public static bool DeleteRecord(string ClientName)
        {
            bool Retval = true;
            string SqlStatement = string.Format("DELETE CONNECTED_PVSCLIENTS WHERE ClientName='{0}'", ClientName);

            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ConnectedPvsClients", "DeleteRecord");
            return Retval;
        }

    }
}
