using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{
    // This is the version 9.0 report format.  Because of the 8.1 patch, we need two versions of the table.
    // The 8.1 version ("reports") matches the registry format, and the 9.0 version ("report") supports the features of 9.0.
    // Because the filtering in 8.1 does not use the reportfilters table, we decided to make
    // a new table instead of confusing the old fields with the new ones.

    public class Report
    {
        const string TableName = "Report";

        public enum ReportTypeEnum
        {
            Batch = 0,               // batch report is a configuration for a report to be run (usually by the task scheduler)
            Stored = 1,              // stored report is a template for a report to be copied and modified, created in <language>data scripts
            Bulletin = 2,            // bulletin report is an attention bulletin to be printed and the record deleted on completion
            CycleCount = 3,          // cycle count report is a batch report which is hidden, and only configured in the cycle count dialog
            PrintOnly = 4,           // print only report is a record holding the completed filename for a report to printed and the record deleted
            BuildToPrintOrView = 5,  // build to print or view is a report configuration copied by the view or print buttons -
                                     // to be built and then viewed, or printed/exported.  the record is kept when viewing, otherwise deleted
            BatchDeleteOnLastRun = 6 // batch report to be deleted on end of schedule, needs to be kept until queue is empty, then deleted
        }

        // All the status enums are temporary. They will be filled as we go forward.
        public enum PrintStatusEnum
        {
            New = 0,
            Queued = 1,
            Running = 2,
            Succeeded = 3,
            Failed = 4,
            Aborted = 5
        }

        public enum EmailStatusEnum
        {
            Failed = 0,
            Succeeded = 1,
            Queued = 2,
            Running = 3,
            None = 4
        }

        public enum CopyStatusEnum
        {
            Failed = 0,
            Succeeded = 1,
            Queued = 2,
            Running = 3,
            None = 4
        }

        public enum RunStatusEnum
        {
            Failed = 0,
            Completed = 1,
            New = 2,
            Queued = 3,
            Running = 4,
            Aborted = 5
        }

        public enum SortDirectionEnum
        {
            NullValue = -1,
            Ascending = 0,
            Descending = 1
        }

		public enum OutputEnum
		{
			NullValue = -1,
			Printer = 1,
			Pdf = 2,
            RptFile = 3,
            Excel = 4,
            CSV = 5
		}

        public enum EmailTypeEnum
        {
            User,
            List,
        }

        public class TableData
        {
			public int ReportIid;
			public string ReportFile;
            public ReportTypeEnum ReportType;
            public PrintStatusEnum PrintStatus;
            public int PrintPathIid;
			public int NumCopy;
			public bool SendMessage;
            public bool Graph;
            public SortDirectionEnum GroupDirection1;
            public SortDirectionEnum GroupDirection2;
            public SortDirectionEnum GroupDirection3;
            public string GroupHeader1;
            public string GroupHeader2;
            public string GroupHeader3;
            public string GroupTable1;
            public string GroupTable2;
            public string GroupTable3;
			public SortDirectionEnum SortDirection1;
			public SortDirectionEnum SortDirection2;
			public SortDirectionEnum SortDirection3;
            public string SortHeader1;
            public string SortHeader2;
            public string SortHeader3;
            public string SortTable1;
            public string SortTable2;
            public string SortTable3;
			public int BulletinNumber;
            public string BatchName;
            public int BatchUser;
            public int BatchPathId;
            public int EmailId;
            public OutputEnum OutputType;
            public int DirPathId;
            public string CompletedReportFile;
            public CopyStatusEnum CopyStatus;
            public EmailStatusEnum EmailStatus;
            public RunStatusEnum RunStatus;
            public int EmailType;

            public TableData(
                int reportIid, string reportFile, ReportTypeEnum reportType,
                PrintStatusEnum printStatus,
                int printPathIid, int numCopy, bool sendMessage, bool graph,
                SortDirectionEnum groupDirection1, SortDirectionEnum groupDirection2, SortDirectionEnum groupDirection3,
                string groupHeader1, string groupHeader2, string groupHeader3,
                string groupTable1, string groupTable2, string groupTable3,
				SortDirectionEnum sortDirection1, SortDirectionEnum sortDirection2, SortDirectionEnum sortDirection3,
                string sortHeader1, string sortHeader2, string sortHeader3,
                string sortTable1, string sortTable2, string sortTable3,
                int bulletinNumber, string batchName, int batchUser,
                int batchPathId, int emailId, OutputEnum outputType,
                int dirPathId, string CompletedReportFile, CopyStatusEnum copyStatus, EmailStatusEnum emailStatus, RunStatusEnum runStatus, int eMailType)
            {
                ReportIid = reportIid;
                ReportFile = reportFile;
                ReportType = reportType;
                PrintStatus = printStatus;
                PrintPathIid = printPathIid;
                NumCopy = numCopy;
                SendMessage = sendMessage;
                Graph = graph;
                GroupDirection1 = groupDirection1;
                GroupDirection2 = groupDirection2;
                GroupDirection3 = groupDirection3;
                GroupHeader1 = groupHeader1;
                GroupHeader2 = groupHeader2;
                GroupHeader3 = groupHeader3;
                GroupTable1 = groupTable1;
                GroupTable2 = groupTable2;
                GroupTable3 = groupTable3;
                SortDirection1 = sortDirection1;
                SortDirection2 = sortDirection2;
                SortDirection3 = sortDirection3;
                SortHeader1 = sortHeader1;
                SortHeader2 = sortHeader2;
                SortHeader3 = sortHeader3;
                SortTable1 = sortTable1;
                SortTable2 = sortTable2;
                SortTable3 = sortTable3;
                BulletinNumber = bulletinNumber;
                BatchName = batchName;
                BatchUser = batchUser;
                BatchPathId = batchPathId;
                EmailId = emailId;
                OutputType = outputType;
                DirPathId = dirPathId;
                this.CompletedReportFile = CompletedReportFile;
                CopyStatus = copyStatus;
                EmailStatus = emailStatus;
                RunStatus = runStatus;
                EmailType = eMailType;
            }

            public TableData()
            {
                ReportIid = -1;

                NumCopy = 1;
                SendMessage = false;
                Graph = false;

                GroupDirection1 = SortDirectionEnum.Ascending;
                GroupDirection2 = SortDirectionEnum.Ascending;
                GroupDirection3 = SortDirectionEnum.Ascending;

                SortDirection1 = SortDirectionEnum.Ascending;
                SortDirection2 = SortDirectionEnum.Ascending;
                SortDirection3 = SortDirectionEnum.Ascending;
            }
        }

        // return data for given IID
        // -1 for an int or enum arg means that DB field was NULL
        // DateTime.MinTime for a DateTime arg means that DB field was NULL
        public static bool GetRecord(int ReportIid, out TableData data)
        {
            bool Retval = false;
            data = null;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from Report where ReportIid=" + ReportIid;
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Report", "GetRecord", out _conn, out myDataReader))
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
                        "Report", ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "Report", "GetRecord", err);
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


        // return all records
        // -1 for an int or enum arg means that DB field was NULL
        // DateTime.MinTime for a DateTime arg means that DB field was NULL
        public static bool GetRecs(List<TableData> list)
        {
            bool Retval = true;
            list.Clear();
#if !NO_ASA
            TableData data;
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from Report";
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Report", "GetRecs", out _conn, out myDataReader))
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
                    ServiceMessages.InsertRec(MainClass.AppName, "Report", "GetRecs", err);
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

        // return data for given BatchName
        // -1 for an int or enum arg means that DB field was NULL
        // DateTime.MinTime for a DateTime arg means that DB field was NULL
        // return true if record found
        public static bool GetRecordFromBatchName(string BatchName, out TableData data)
        {
            bool Retval = false;
            data = null;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from Report where BatchName='" + MainClass.FixStringForSingleQuote(BatchName) + "'";
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Report", "GetRecordFromBatchName", out _conn, out myDataReader))
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
                        "Report", ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "Report", "GetRecordFromBatchName", err);
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



        // check if a batch report of the given batch name exists
        // return true if record found
        public static bool BatchReportExists(string BatchName)
        {
            bool Retval = false;
          //  data = null;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from Report where BatchName='" + MainClass.FixStringForSingleQuote(BatchName) + "'" + " AND ReportType= " + (int)ReportTypeEnum.Batch;
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Report", "BatchReportExists", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                       // MakeDataRec(myDataReader, out data);
                        Retval = true;
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        "Report", ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "Report", "BatchReportExists", err);
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





#if !NO_ASA
        // make a TableData object from a SADataReader object
        static void MakeDataRec(SADataReader myDataReader, out TableData data)
        {
            data = new TableData(
                MainClass.ToInt(TableName, myDataReader["ReportIid"]),
                myDataReader["reportFile"].ToString(),
                (ReportTypeEnum)MainClass.ToInt(TableName, myDataReader["reportType"]),
                (PrintStatusEnum)MainClass.ToInt(TableName, myDataReader["printStatus"]),
                MainClass.ToInt(TableName, myDataReader["PrintPathIid"]),
                MainClass.ToInt(TableName, myDataReader["numCopy"]),
                MainClass.ToBool(TableName, myDataReader["sendMessage"]),
                MainClass.ToBool(TableName, myDataReader["graph"]),
                (SortDirectionEnum)MainClass.ToInt(TableName, myDataReader["groupDirection1"]),
                (SortDirectionEnum)MainClass.ToInt(TableName, myDataReader["groupDirection2"]),
                (SortDirectionEnum)MainClass.ToInt(TableName, myDataReader["groupDirection3"]),
                myDataReader["groupHeader1"].ToString(),
                myDataReader["groupHeader2"].ToString(),
                myDataReader["groupHeader3"].ToString(),
                myDataReader["groupTable1"].ToString(),
                myDataReader["groupTable2"].ToString(),
                myDataReader["groupTable3"].ToString(),
                (SortDirectionEnum)MainClass.ToInt(TableName, myDataReader["sortDirection1"]),
                (SortDirectionEnum)MainClass.ToInt(TableName, myDataReader["sortDirection2"]),
                (SortDirectionEnum)MainClass.ToInt(TableName, myDataReader["sortDirection3"]),
                myDataReader["sortHeader1"].ToString(),
                myDataReader["sortHeader2"].ToString(),
                myDataReader["sortHeader3"].ToString(),
                myDataReader["sortTable1"].ToString(),
                myDataReader["sortTable2"].ToString(),
                myDataReader["sortTable3"].ToString(),
                MainClass.ToInt(TableName, myDataReader["bulletinNumber"]),
                myDataReader["batchName"].ToString(),
                MainClass.ToInt(TableName, myDataReader["batchUser"]),
                MainClass.ToInt(TableName, myDataReader["batchPathId"]),
                MainClass.ToInt(TableName, myDataReader["emailId"]),
                (OutputEnum)MainClass.ToInt(TableName, myDataReader["outputType"]),
                MainClass.ToInt(TableName, myDataReader["dirPathId"]),
                myDataReader["completedReportFile"].ToString(),
                (CopyStatusEnum)MainClass.ToInt(TableName, myDataReader["copyStatus"]),
                (EmailStatusEnum)MainClass.ToInt(TableName, myDataReader["emailStatus"]),
                (RunStatusEnum)MainClass.ToInt(TableName, myDataReader["runStatus"]),
                MainClass.ToInt(TableName, myDataReader["eMailType"])
            );
        }
#endif
        // insert record
        // -1 for an int or enum arg means that DB field should be NULL
        // DateTime.MinTime for a DateTime arg means that DB field should be NULL
        // OUTPUT: NewIid - IID of inserted record
        public static bool InsertRecord(TableData data, out int NewIid)
        {
            bool RetVal = false;
            string FieldString = "";
            string ValueString = "";
            NewIid = -1;

            if (data.ReportIid != -1)
            {
                FieldString += "ReportIid, ";
				ValueString += data.ReportIid + ", ";
            }
            FieldString += "reportFile, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.ReportFile) + "', ";
			if ((int)data.ReportType != -1)
            {
                FieldString += "reportType, ";
				ValueString += (int)data.ReportType + ", ";
            }
			if ((int)data.PrintStatus != -1)
            {
                FieldString += "printStatus, ";
				ValueString += (int)data.PrintStatus + ", ";
            }
            FieldString += "PrintPathIid, ";
            ValueString += (int)data.PrintPathIid + ", ";

			if (data.NumCopy != -1)
            {
                FieldString += "numCopy, ";
				ValueString += data.NumCopy + ", ";
            }
            FieldString += "sendMessage, ";
            ValueString += MainClass.BoolToInt(data.SendMessage) + ", ";
            FieldString += "graph, ";
            ValueString += MainClass.BoolToInt(data.Graph) + ", ";
            if (data.GroupDirection1 != SortDirectionEnum.NullValue)
            {
                FieldString += "groupDirection1, ";
                ValueString += (int)data.GroupDirection1 + ", ";
            }
            if (data.GroupDirection2 != SortDirectionEnum.NullValue)
            {
                FieldString += "groupDirection2, ";
                ValueString += (int)data.GroupDirection2 + ", ";
            }
            if (data.GroupDirection3 != SortDirectionEnum.NullValue)
            {
                FieldString += "groupDirection3, ";
                ValueString += (int)data.GroupDirection3 + ", ";
            }
            FieldString += "groupHeader1, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.GroupHeader1) + "', ";
            FieldString += "groupHeader2, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.GroupHeader2) + "', ";
            FieldString += "groupHeader3, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.GroupHeader3) + "', ";
            FieldString += "groupTable1, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.GroupTable1) + "', ";
            FieldString += "groupTable2, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.GroupTable2) + "', ";
            FieldString += "groupTable3, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.GroupTable3) + "', ";
            if (data.SortDirection1 != SortDirectionEnum.NullValue)
            {
                FieldString += "sortDirection1, ";
				ValueString += (int)data.SortDirection1 + ", ";
            }
			if (data.SortDirection2 != SortDirectionEnum.NullValue)
            {
                FieldString += "sortDirection2, ";
				ValueString += (int)data.SortDirection2 + ", ";
            }
			if (data.SortDirection3 != SortDirectionEnum.NullValue)
            {
                FieldString += "sortDirection3, ";
				ValueString += (int)data.SortDirection3 + ", ";
            }
            FieldString += "sortHeader1, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.SortHeader1) + "', ";
            FieldString += "sortHeader2, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.SortHeader2) + "', ";
            FieldString += "sortHeader3, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.SortHeader3) + "', ";
            FieldString += "sortTable1, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.SortTable1) + "', ";
            FieldString += "sortTable2, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.SortTable2) + "', ";
            FieldString += "sortTable3, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.SortTable3) + "', ";
            if (data.BulletinNumber != -1)
            {
                FieldString += "bulletinNumber, ";
                ValueString += data.BulletinNumber + ", ";
            }
            FieldString += "batchName, ";
			ValueString += "'" + MainClass.FixStringForSingleQuote(data.BatchName) + "', ";
            FieldString += "batchUser, ";
            ValueString += "'" + data.BatchUser + "', ";
            if (data.BatchPathId != -1)
            {
                FieldString += "batchPathId, ";
                ValueString += (int)data.BatchPathId + ", ";
            }
            if (data.EmailId != -1)
            {
                FieldString += "emailId, ";
                ValueString += (int)data.EmailId + ", ";
            }
            if (data.OutputType != OutputEnum.NullValue)
            {
                FieldString += "outputType, ";
                ValueString += (int)data.OutputType + ", ";
            }
            if (data.DirPathId != -1)
            {
                FieldString += "dirPathId, ";
                ValueString += data.DirPathId + ", ";
            }
            FieldString += "completedReportFile, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.CompletedReportFile) + "', ";
            if ((int)data.CopyStatus != -1)
            {
                FieldString += "copyStatus, ";
                ValueString += (int)data.CopyStatus + ", ";
            }
            if ((int)data.EmailStatus != -1)
            {
                FieldString += "emailStatus, ";
                ValueString += (int)data.EmailStatus + ", ";
            }
            if ((int)data.RunStatus != -1)
            {
                FieldString += "runStatus, ";
                ValueString += (int)data.RunStatus + ", ";
            }
            FieldString += "eMailType, ";
            ValueString += (int)data.EmailType + "";

            string strSqlStatement = "INSERT INTO REPORT (" + FieldString + ") VALUES (" + ValueString + ")";
            RetVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "Report", "InsertRec", out NewIid);

            return RetVal;
        }

        // update fields of specified record
        public static bool UpdateRecord(TableData data)
        {
            bool Retval = true;
            string sql = "UPDATE " + TableName + " SET ";
            sql += "reportFile=" + "'" + MainClass.FixStringForSingleQuote(data.ReportFile) + "'";
            if ((int)data.ReportType != -1)
            {
                sql += ", reportType=" + "'" + (int)data.ReportType + "'";
            }
            if ((int)data.PrintStatus != -1)
            {
                sql += ", printStatus=" + (int)data.PrintStatus;
            }
            sql += ", PrintPathIid=" + (int)data.PrintPathIid;
            if (data.NumCopy != -1)
            {
                sql += ", numCopy=" + "'" + data.NumCopy + "'";
            }
            sql += ", sendMessage=" + "'" + MainClass.BoolToInt(data.SendMessage) + "'";
            sql += ", graph=" + "'" + MainClass.BoolToInt(data.Graph) + "'";
            if (data.GroupDirection1 != SortDirectionEnum.NullValue)
            {
                sql += ", groupDirection1=" + (int)data.GroupDirection1;
            }
            if (data.GroupDirection2 != SortDirectionEnum.NullValue)
            {
                sql += ", groupDirection2=" + (int)data.GroupDirection2;
            }
            if (data.GroupDirection3 != SortDirectionEnum.NullValue)
            {
                sql += ", groupDirection3=" + (int)data.GroupDirection1;
            }
            sql += ", groupHeader1='" + MainClass.FixStringForSingleQuote(data.GroupHeader1) + "'";
            sql += ", groupHeader2='" + MainClass.FixStringForSingleQuote(data.GroupHeader2) + "'";
            sql += ", groupHeader3='" + MainClass.FixStringForSingleQuote(data.GroupHeader3) + "'";
            sql += ", groupTable1='" + MainClass.FixStringForSingleQuote(data.GroupTable1) + "'";
            sql += ", groupTable2='" + MainClass.FixStringForSingleQuote(data.GroupTable2) + "'";
            sql += ", groupTable3='" + MainClass.FixStringForSingleQuote(data.GroupTable3) + "'";
            if (data.SortDirection1 != SortDirectionEnum.NullValue)
            {
                sql += ", sortDirection1=" + (int)data.SortDirection1;
            }
            if (data.SortDirection2 != SortDirectionEnum.NullValue)
            {
                sql += ", sortDirection2=" + (int)data.SortDirection2;
            }
            if (data.SortDirection3 != SortDirectionEnum.NullValue)
            {
                sql += ", sortDirection3=" + (int)data.SortDirection3;
            }
            sql += ", sortHeader1='" + MainClass.FixStringForSingleQuote(data.SortHeader1) + "'";
            sql += ", sortHeader2='" + MainClass.FixStringForSingleQuote(data.SortHeader2) + "'";
            sql += ", sortHeader3='" + MainClass.FixStringForSingleQuote(data.SortHeader3) + "'";
            sql += ", sortTable1='" + MainClass.FixStringForSingleQuote(data.SortTable1) + "'";
            sql += ", sortTable2='" + MainClass.FixStringForSingleQuote(data.SortTable2) + "'";
            sql += ", sortTable3='" + MainClass.FixStringForSingleQuote(data.SortTable3) + "'";
            if (data.BulletinNumber != -1)
            {
                sql += ", bulletinNumber=" + data.BulletinNumber;
            }
            sql += ", batchName=" + "'" + MainClass.FixStringForSingleQuote(data.BatchName) + "'"; ;
            sql += ", batchUser=" + "'" + data.BatchUser + "'"; ;
            if (data.BatchPathId != -1)
            {
                sql += ", batchPathId=" + data.BatchPathId;
            }
            if (data.EmailId != -1)
            {
                sql += ", emailId=" + data.EmailId;
            }
            if (data.OutputType != OutputEnum.NullValue)
            {
                sql += ", outputType=" + (int)data.OutputType;
            }
            if (data.DirPathId != -1)
            {
                sql += ", dirPathId=" + data.DirPathId;
            }
            sql += ", completedReportFile=" + "'" + MainClass.FixStringForSingleQuote(data.CompletedReportFile) + "'";
            if ((int)data.CopyStatus != -1)
            {
                sql += ", copyStatus=" + (int)data.CopyStatus;
            }
            if ((int)data.EmailStatus != -1)
            {
                sql += ", emailStatus=" + (int)data.EmailStatus;
            }
            if ((int)data.RunStatus != -1)
            {
                sql += ", runStatus=" + (int)data.RunStatus;
            }
            sql += ", eMailType=" + data.EmailType;

            sql += " WHERE reportIid=" + data.ReportIid;

            Retval = MainClass.ExecuteSql(sql, true, TableName, "Report", "UpdateRecord");

            return Retval;
        }

        // update batchPathId
        public static bool UpdateBatchPathId(int reportIid, int newBatchPathId)
        {
            bool retVal = true;
            string strSqlStatement = "UPDATE REPORT SET batchPathId = " + "'" + newBatchPathId + "'" + " WHERE reportIid = " + reportIid;
            retVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "Report", "UpdateBatchPathId");
            return retVal;
        }

        // update PrintPathIid
        public static bool UpdatePrintPathIid(int reportIid, string printerPath)
        {
            bool retVal = true;
            int printerIid = 0;

            printerIid = BBPyxisDB.PrinterData.GetIidFromPrinterName(printerPath);
            string strSqlStatement = "UPDATE REPORT SET PrintPathIid = " + printerIid + " WHERE reportIid = " + reportIid;
            retVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "Report", "UpdatePrintPathIid");
            return retVal;
        }

        // delete record given its primary key
        public static bool DeleteRecord(int reportIid)
        {
            bool Retval = true;
            string SqlStatement = string.Format("DELETE REPORT WHERE reportIid='{0}'", (int)reportIid);
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Report", "DeleteRecord");
            return Retval;
        }

        // Update Print Status
        public static bool UpdatePrintStatus(int reportIid, Report.PrintStatusEnum printStatus)
        {
            bool retVal = true;
            string strSqlStatement = "UPDATE REPORT SET printStatus = " + (int)printStatus + " WHERE reportIid = " + reportIid;
            retVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "Report", "UpdatePrintStatus");
            return retVal;
        }

        // Update Completed Report File
        public static bool UpdateCompletedReportFile(int reportIid, string completedReportFile)
        {
            bool retVal = true;
            string strSqlStatement = "UPDATE REPORT SET completedReportFile = " + "'" + completedReportFile + "'" + " WHERE reportIid = " + reportIid;
            retVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "Report", "UpdateCompletedReportFile");
            return retVal;
        }

        // Update Copy Status
        public static bool UpdateCopyStatus(int reportIid, Report.CopyStatusEnum copyStatus)
        {
            bool retVal = true;
            string strSqlStatement = "UPDATE REPORT SET copyStatus = " + (int)copyStatus + " WHERE reportIid = " + reportIid;
            retVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "Report", "UpdateCopyStatus");
            return retVal;
        }

        // Update Email Status
        public static bool UpdateEmailStatus(int reportIid, Report.EmailStatusEnum emailStatus)
        {
            bool retVal = true;
            string strSqlStatement = "UPDATE REPORT SET emailStatus = " + (int)emailStatus + " WHERE reportIid = " + reportIid;
            retVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "Report", "UpdateEmailStatus");
            return retVal;
        }

        // Update Run Status
        public static bool UpdateRunStatus(int reportIid, Report.RunStatusEnum runStatus)
        {
            bool retVal = true;
            string strSqlStatement = "UPDATE REPORT SET runStatus = " + (int)runStatus + " WHERE reportIid = " + reportIid;
            retVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "Report", "UpdateRunStatus");
            return retVal;
        }

        // Update Report Type
        public static bool UpdateReportType(int reportIid, Report.ReportTypeEnum reportType)
        {
            bool retVal = true;
            string strSqlStatement = "UPDATE REPORT SET reportType = " + (int)reportType + " WHERE reportIid = " + reportIid;
            retVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "Report", "UpdateRunStatus");
            return retVal;

        }

        // Update Run Status
        public static bool InsertPrintRecord(string completedFilename,int printPathIid, out int NewIid)
        {
            bool RetVal = false;
            string FieldString = "";
            string ValueString = "";
            NewIid = -1;

            FieldString += "reportFile, reportType, ";
            ValueString += "'Empty', 4, ";

            FieldString += "PrintPathIid, ";
            ValueString += printPathIid + ", ";

            FieldString += "groupDirection1, groupDirection2, groupDirection3, ";
            ValueString += "0,0,0, ";

            FieldString += "groupHeader1, groupHeader2, groupHeader3, ";
            ValueString += "'','','', ";

            FieldString += "groupTable1, groupTable2, groupTable3, ";
            ValueString += "'','','', ";

            FieldString += "sortDirection1, sortDirection2, sortDirection3, ";
            ValueString += "0,0,0, ";

            FieldString += "sortHeader1, sortHeader2, sortHeader3, ";
            ValueString += "'','','', ";

            FieldString += "sortTable1, sortTable2, sortTable3, ";
            ValueString += "'','','', ";

            FieldString += "bulletinNumber, batchName, batchUser, ";
            ValueString += "0, '', 0, ";

            FieldString += "batchPathId, emailId, outputType, dirPathId, ";
            ValueString += "0, 0, 1, 0, ";

            FieldString += "completedReportFile, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(completedFilename) + "', ";

            FieldString += "copyStatus, emailStatus, runStatus, eMailType";
            ValueString += "4, 4, 2, 0";

            string strSqlStatement = "INSERT INTO REPORT (" + FieldString + ") VALUES (" + ValueString + ")";
            RetVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "Report", "InsertRec", out NewIid);

            return RetVal;
        }

        public static bool DeletePrintAndViewReports()
        {
            bool Retval = true;
            TableData data;
            
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            // Get all reports of type PrintAndView only
            string SqlStatement = string.Format("SELECT * FROM REPORT WHERE reportType={0}", (int)ReportTypeEnum.BuildToPrintOrView);
        
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Report", "DeletePrintAndViewReports", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        MakeDataRec(myDataReader, out data);
                        // If Copy failed, delete the record - it will not have a CompletedReportFile
                        if (data.CopyStatus == CopyStatusEnum.Failed || data.RunStatus == RunStatusEnum.Failed || data.RunStatus == RunStatusEnum.Aborted)
                          DeleteRecord(MainClass.ToInt("Report", data.ReportIid));
                        else if(IsToBeDeleted(data.CompletedReportFile))
                        {
                            DeleteRecord(MainClass.ToInt("Report", data.ReportIid));
                        }

                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "Report", "DeletePrintAndViewReports", err);
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

        
        private static bool IsToBeDeleted(string completedFile)
        {
            int endIndex = 0;
            int startIndex = 0;
            string strDate;
            int year, month, day;
            if (completedFile.Length > 0)
            {
                endIndex = completedFile.LastIndexOf('_');
                startIndex = endIndex - 8;
                strDate = completedFile.Substring(startIndex, 8);
                year = Convert.ToInt32(strDate.Substring(0, 4));
                month = Convert.ToInt32(strDate.Substring(4, 2));
                day = Convert.ToInt32(strDate.Substring(6, 2));
                // Construct date from CompletedReportFile field
                DateTime completedDate = new DateTime(year, month, day);

                // Delete record if completedDate is before today
                if (completedDate < DateTime.Today)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }


        // check if a batch report or custom list is using the group list or email
        // return true if record found
        public static bool ReportLinkExists(int nIid)
        {
            bool Retval = false;
            //  data = null;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from REPORT where emailtype=1 and emailId=" + nIid.ToString();
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Report", "ReportLinkExists", out _conn, out myDataReader))
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
                    ServiceMessages.InsertRec(MainClass.AppName, "Report", "ReportLinkExists", err);
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
