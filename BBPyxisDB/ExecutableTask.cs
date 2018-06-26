using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

// interface to Executable_Task table

namespace BBPyxisDB
{
	public class ExecutableTask
	{
        const string TableName = "EXECUTABLE_TASK";

        // apps we can execute. These are the taskIds of the entries the Executable_Task table.
		public enum Application
		{
			Maintenance = 1,
			Backup = 2,
			ReportService = 3,
			RefillToMax = 4,
            PocketTableCycleCount = 5
		}

		public struct TableData
		{
			public int TaskId;
			public string Directory;
			public string FileName;
			public bool AllowMultipleFlag;
            public string MsgQueue;

            public TableData(int taskId, string directory, string fileName,
                bool allowMultipleFlag, string msgQueue)
			{
				TaskId = taskId;
				Directory = directory;
				FileName = fileName;
				AllowMultipleFlag = allowMultipleFlag;
                MsgQueue = msgQueue;
			}
		}

        // get all tasks from EXECUTABLE_TASK table for specified taskID
        // integer vals are returned as -1 if they're null
        public static bool GetRecs(int TaskId, List<TableData> list)
        {
            bool Retval = true;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            TableData esd;
            string SqlStatement = "SELECT * from EXECUTABLE_TASK WHERE taskId=" + TaskId;
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ExecutableTask", "GetRecs", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        MakeDataRec(myDataReader, out esd);
                        list.Add(esd);
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "ExecutableTask", "GetRecs", err);
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

        // get all message queues from EXECUTABLE_TASK table
        public static bool GetQueues(List<string> list)
        {
            bool Retval = true;
#if !NO_ASA
            string msgQueue;
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT msgQueue from EXECUTABLE_TASK";
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ExecutableTask", "GetQueues", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        msgQueue = myDataReader["msgQueue"].ToString();

                        if (msgQueue != "")
                            list.Add(msgQueue);
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "ExecutableTask", "GetQueues", err);
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
                MainClass.ToInt(TableName, myDataReader["TaskId"])
                , myDataReader["Directory"].ToString()
                , myDataReader["FileName"].ToString()
                , MainClass.ToBool(TableName, myDataReader["AllowMultipleFlag"])
                , myDataReader["MsgQueue"].ToString());
        }
#endif


	}
}
