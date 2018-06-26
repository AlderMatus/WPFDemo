using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

    public class Reports
    {
        const string TableName = "Reports";
        public enum DateRangeEnum
        {
			NullValue = -1,
            Last24Hours = 1,
            LastFullDay,
            Last7Days,
            LastFullWeek,
            LastSundayToToday,
            MonthToDate,
            Last30Days,
            LastFullMonth,
            Prior31To60Days,
            Prior61To90Days,
            BeforeCurrentDate
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
            Email = 3,
            File = 4
		}

        public class TableData
        {
            public int ReportIid;
            public string ReportFile;
            public string RecordFilter;
            public int NumCopy;
            public bool SendMessage;
            public string DateFrom;
            public string DateTo;
            public string DateRange;
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
            public string FilterText1;
            public string FilterText2;
            public string FilterText3;
            public string FilterText4;
            public string FilterText5;
            public string FilterText6;
            public string FilterText7;
            public string FilterText8;
            public string FilterText9;
            public string FilterText10;
            public string FilterText11;
            public string FilterText12;
            public string FilterText13;
            public string FilterText14;
            public string FilterText15;
            public string BatchName;
            public bool ReportDelete;

            public TableData()
            {
                ReportIid = -1;

                NumCopy = 1;
                SendMessage = false;

                GroupDirection1 = SortDirectionEnum.Ascending;
                GroupDirection2 = SortDirectionEnum.Ascending;
                GroupDirection3 = SortDirectionEnum.Ascending;

                SortDirection1 = SortDirectionEnum.Ascending;
                SortDirection2 = SortDirectionEnum.Ascending;
                SortDirection3 = SortDirectionEnum.Ascending;

                ReportDelete = false;
            }

            public TableData(
                int reportIid, string reportFile,
                string recordFilter, int numCopy, bool sendMessage,
                string dateFrom, string dateTo, string dateRange,
                SortDirectionEnum groupDirection1, SortDirectionEnum groupDirection2, SortDirectionEnum groupDirection3,
                string groupHeader1, string groupHeader2, string groupHeader3,
                string groupTable1, string groupTable2, string groupTable3,
                SortDirectionEnum sortDirection1, SortDirectionEnum sortDirection2, SortDirectionEnum sortDirection3,
                string sortHeader1, string sortHeader2, string sortHeader3,
                string sortTable1, string sortTable2, string sortTable3,
                string filterText1, string filterText2, string filterText3, string filterText4,
                string filterText5, string filterText6, string filterText7, string filterText8,
                string filterText9, string filterText10, string filterText11, string filterText12,
                string filterText13, string filterText14, string filterText15,
                string batchName, bool reportDelete)
            {
                ReportIid = reportIid;
                ReportFile = reportFile;
                RecordFilter = recordFilter;
                NumCopy = numCopy;
                SendMessage = sendMessage;
                DateFrom = dateFrom;
                DateTo = dateTo;
                DateRange = dateRange;
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
                FilterText1 = filterText1;
                FilterText2 = filterText2;
                FilterText3 = filterText3;
                FilterText4 = filterText4;
                FilterText5 = filterText5;
                FilterText6 = filterText6;
                FilterText7 = filterText7;
                FilterText8 = filterText8;
                FilterText9 = filterText9;
                FilterText10 = filterText10;
                FilterText11 = filterText11;
                FilterText12 = filterText12;
                FilterText13 = filterText13;
                FilterText14 = filterText14;
                FilterText15 = filterText15;
                BatchName = batchName;
                ReportDelete = reportDelete;
            }
        }

        // return data for given IID
        // -1 for an int or enum arg means that DB field was NULL
        // DateTime.MinTime for a DateTime arg means that DB field was NULL
        public static bool GetRecord(int ReportIid, out TableData data)
        {
            bool Retval = true;
            data = null;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from Reports where ReportIid=" + ReportIid;
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Reports", "GetData", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                        data = new TableData(
                            ReportIid,
                            myDataReader["reportFile"].ToString(),
                            myDataReader["recordFilter"].ToString(),
                            MainClass.ToInt(TableName, myDataReader["numCopy"]),
                            MainClass.ToBool(TableName, myDataReader["sendMessage"]),
                            myDataReader["dateFrom"].ToString(),
                            myDataReader["dateTo"].ToString(),
                            myDataReader["dateRange"].ToString(),
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
                            myDataReader["filterText1"].ToString(),
                            myDataReader["filterText2"].ToString(),
                            myDataReader["filterText3"].ToString(),
                            myDataReader["filterText4"].ToString(),
                            myDataReader["filterText5"].ToString(),
                            myDataReader["filterText6"].ToString(),
                            myDataReader["filterText7"].ToString(),
                            myDataReader["filterText8"].ToString(),
                            myDataReader["filterText9"].ToString(),
                            myDataReader["filterText10"].ToString(),
                            myDataReader["filterText11"].ToString(),
                            myDataReader["filterText12"].ToString(),
                            myDataReader["filterText13"].ToString(),
                            myDataReader["filterText14"].ToString(),
                            myDataReader["filterText15"].ToString(),
                            myDataReader["batchName"].ToString(),
                            MainClass.ToBool(TableName, myDataReader["reportDelete"])
                            );
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        "Reports", ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "Reports", "GetRecord", err);
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
            string SqlStatement = "SELECT * from Reports";
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Reports", "GetData", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        data = new TableData(
                            MainClass.ToInt(TableName, myDataReader["ReportIid"]),
                            myDataReader["reportFile"].ToString(),
                            myDataReader["recordFilter"].ToString(),
                            MainClass.ToInt(TableName, myDataReader["numCopy"]),
                            MainClass.ToBool(TableName, myDataReader["sendMessage"]),
                            myDataReader["dateFrom"].ToString(),
                            myDataReader["dateTo"].ToString(),
                            myDataReader["dateRange"].ToString(),
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
                            myDataReader["filterText1"].ToString(),
                            myDataReader["filterText2"].ToString(),
                            myDataReader["filterText3"].ToString(),
                            myDataReader["filterText4"].ToString(),
                            myDataReader["filterText5"].ToString(),
                            myDataReader["filterText6"].ToString(),
                            myDataReader["filterText7"].ToString(),
                            myDataReader["filterText8"].ToString(),
                            myDataReader["filterText9"].ToString(),
                            myDataReader["filterText10"].ToString(),
                            myDataReader["filterText11"].ToString(),
                            myDataReader["filterText12"].ToString(),
                            myDataReader["filterText13"].ToString(),
                            myDataReader["filterText14"].ToString(),
                            myDataReader["filterText15"].ToString(),
                            myDataReader["batchName"].ToString(),
                            MainClass.ToBool(TableName, myDataReader["reportDelete"])
                        );
                        list.Add(data);
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "Reports", "GetRecs", err);
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

        // insert record
        // -1 for an int or enum arg means that DB field should be NULL
        // DateTime.MinTime for a DateTime arg means that DB field should be NULL
        // OUTPUT: NewIid - IID of inserted record
        public static bool InsertRec(TableData data, out int NewIid)
        {
            bool RetVal = false;
            string FieldString = "";
            string ValueString = "";
            string tempString = "";
            NewIid = -1;

            if (data.ReportIid != -1)
            {
                FieldString += "ReportIid, ";
				ValueString += "'" + data.ReportIid + "', ";
            }
            FieldString += "reportFile, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.ReportFile) + "', ";
            FieldString += "recordFilter, ";
            tempString = MainClass.FixStringForSingleQuote(data.RecordFilter);
            ValueString += "'" + tempString + "', ";
			if (data.NumCopy != -1)
            {
                FieldString += "numCopy, ";
				ValueString += "'" + data.NumCopy + "', ";
            }
            FieldString += "sendMessage, ";
            ValueString += "'" + MainClass.BoolToInt(data.SendMessage) + "', ";
            FieldString += "dateFrom, ";
            ValueString += "'" + data.DateFrom + "', ";
            FieldString += "dateTo, ";
            ValueString += "'" + data.DateTo + "', ";
            FieldString += "dateRange, ";
            ValueString += "'" + data.DateRange + "', ";

            if (data.GroupDirection1 != SortDirectionEnum.NullValue)
            {
                FieldString += "groupDirection1, ";
                ValueString += "'" + (int)data.GroupDirection1 + "', ";
            }
            if (data.GroupDirection2 != SortDirectionEnum.NullValue)
            {
                FieldString += "groupDirection2, ";
                ValueString += "'" + (int)data.GroupDirection2 + "', ";
            }
            if (data.GroupDirection3 != SortDirectionEnum.NullValue)
            {
                FieldString += "groupDirection3, ";
                ValueString += "'" + (int)data.GroupDirection3 + "', ";
            }
            FieldString += "groupHeader1, ";
			ValueString += "'" + data.GroupHeader1 + "', ";
            FieldString += "groupHeader2, ";
			ValueString += "'" + data.GroupHeader2 + "', ";
            FieldString += "groupHeader3, ";
			ValueString += "'" + data.GroupHeader3 + "', ";
            FieldString += "groupTable1, ";
			ValueString += "'" + data.GroupTable1 + "', ";
            FieldString += "groupTable2, ";
			ValueString += "'" + data.GroupTable2 + "', ";
            FieldString += "groupTable3, ";
			ValueString += "'" + data.GroupTable3 + "', ";
			if (data.SortDirection1 != SortDirectionEnum.NullValue)
            {
                FieldString += "sortDirection1, ";
				ValueString += "'" + (int)data.SortDirection1 + "', ";
            }
			if (data.SortDirection2 != SortDirectionEnum.NullValue)
            {
                FieldString += "sortDirection2, ";
				ValueString += "'" + (int)data.SortDirection2 + "', ";
            }
			if (data.SortDirection3 != SortDirectionEnum.NullValue)
            {
                FieldString += "sortDirection3, ";
				ValueString += "'" + (int)data.SortDirection3 + "', ";
            }
            FieldString += "sortHeader1, ";
			ValueString += "'" + data.SortHeader1 + "', ";
            FieldString += "sortHeader2, ";
			ValueString += "'" + data.SortHeader2 + "', ";
            FieldString += "sortHeader3, ";
			ValueString += "'" + data.SortHeader3 + "', ";
            FieldString += "sortTable1, ";
			ValueString += "'" + data.SortTable1 + "', ";
            FieldString += "sortTable2, ";
			ValueString += "'" + data.SortTable2 + "', ";
            FieldString += "sortTable3, ";
			ValueString += "'" + data.SortTable3 + "', ";
            FieldString += "filterText1, ";
			ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText1) + "', ";
            FieldString += "filterText2, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText2) + "', ";
            FieldString += "filterText3, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText3) + "', ";
            FieldString += "filterText4, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText4) + "', ";
            FieldString += "filterText5, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText5) + "', ";
            FieldString += "filterText6, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText6) + "', ";
            FieldString += "filterText7, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText7) + "', ";
            FieldString += "filterText8, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText8) + "', ";
            FieldString += "filterText9, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText9) + "', ";
            FieldString += "filterText10, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText10) + "', ";
            FieldString += "filterText11, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText11) + "', ";
            FieldString += "filterText12, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText12) + "', ";
            FieldString += "filterText13, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText13) + "', ";
            FieldString += "filterText14, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText14) + "', ";
            FieldString += "filterText15, ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.FilterText15) + "', ";
            FieldString += "batchName, ";
			ValueString += "'" + MainClass.FixStringForSingleQuote(data.BatchName) + "', ";
            FieldString += "reportDelete";
			ValueString += "'" + MainClass.BoolToInt(data.ReportDelete) + "'";

            string strSqlStatement = "INSERT INTO REPORTS (" + FieldString + ") VALUES (" + ValueString + ")";
            RetVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "Reports", "InsertRec", out NewIid);

            return RetVal;
        }

        public static bool UpdateCopyStatus(int reportIid, string copyStatus)
        {
            bool retVal = false;
            copyStatus = MainClass.FixStringForSingleQuote(copyStatus);

            string strSqlStatement = "UPDATE REPORTS SET copyStatus = " + "'" + copyStatus + "'" + " WHERE reportIid = " + reportIid;
            retVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "Reports", "UpdateCopyStatus");
           
            return retVal;
        }

        public static bool UpdateBatchPathId(int reportIid, int newBatchPathId)
        {
            bool retVal = true;
            string strSqlStatement = "UPDATE REPORTS SET batchPathId = " + "'" + newBatchPathId + "'" + " WHERE reportIid = " + reportIid;
            retVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "Reports", "UpdateBatchPathId");
            return retVal;
        }

        // delete record given its primary key
        public static bool DeleteRecord(int reportIid)
        {
            bool Retval = true;
            string SqlStatement = string.Format("DELETE REPORTS WHERE reportIid='{0}'", (int)reportIid);
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Reports", "DeleteRecord");
            return Retval;
        }
    }
}
