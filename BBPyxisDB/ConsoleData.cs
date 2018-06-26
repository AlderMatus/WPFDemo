using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ConsoleData
	{
		const string TableName = "CONSOLE_DATA";

		// collection of record fields
		public class TableData
		{
			public int consoleIid;
			public string consoleName;
            public int pathId;
            public int printerIid;

			public TableData(int consoleIid, string consoleName, int pathId, int printerIid)
			{
                this.consoleIid = consoleIid;
                this.consoleName = consoleName;
                this.pathId = pathId;
                this.printerIid = printerIid;
			}
		}

		// return record given its primary key
        public static bool GetRecord(int consoleIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from CONSOLE_DATA WHERE consoleIid='{0}'",
                (int)consoleIid);
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "CONSOLE_DATA", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["consoleIid"]),
                myDataReader["consoleName"].ToString(),               
                MainClass.ToInt(TableName, myDataReader["pathId"]),
                MainClass.ToInt(TableName, myDataReader["printerIid"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

            string SqlStatement = "INSERT INTO CONSOLE_DATA (consoleName, pathId, printerIid) VALUES ("
            + "'" + MainClass.FixStringForSingleQuote(data.consoleName) + "', '"  + (int)data.pathId + ", " + (int)data.printerIid + ")";
	
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "CONSOLE_DATA", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int AreaIid)
		{
			bool Retval = true;
            string SqlStatement = string.Format("DELETE CONSOLE_DATA WHERE AreaIid='{0}'", 
				(int)AreaIid);
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "CONSOLE_DATA", "DeleteRecord");
			return Retval;
		}

        // Get PrinterIid for a given Console name
        public static bool GetRecordFromConsoleName(string ConsoleName, out TableData data)
        {
           bool Retval = false;
            data = null;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from CONSOLE_DATA where consoleName='" + MainClass.FixStringForSingleQuote(ConsoleName) + "' ";
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ConsoleData", "GetRecordFromConsoleName", out _conn, out myDataReader))
            {
              
                try
                {
                    if (myDataReader.Read())
                    {
                        MakeDataRec(myDataReader, out data);
                        Retval = true;
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        "ConsoleData", ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "ConsoleData", "GetRecordFromConsoleName", err);
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
	}
}
