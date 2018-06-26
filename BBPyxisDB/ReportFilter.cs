using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{
	public class ReportFilter
	{
        const string TableName = "REPORT_FILTER";
        const int FilterItemsLength = 2046;  // zero based length of string as defined in database (minus 2)

        public class TableData
        {
            public int ReportIid;
            public string FilterField;  // table.field
            public FilterMethodEnum FilterMethod;
            public RelativeDatesEnum RelativeDates;
            public DateTime StartDate;
            public DateTime EndDate;
            public string FilterItems;  // comma separated list defining selected DB values

            public TableData(int ReportIid, string FilterField, FilterMethodEnum FilterMethod,
                RelativeDatesEnum RelativeDates, DateTime StartDate, DateTime EndDate,
                string FilterItems)
            {
                this.ReportIid = ReportIid;
                this.FilterField = FilterField;
                this.FilterMethod = FilterMethod;
                this.RelativeDates = RelativeDates;
                this.StartDate = StartDate;
                this.EndDate = EndDate;
                this.FilterItems = FilterItems;
            }
        }

        // filterMethod values. Defines which fields are used and which are ignored
        public enum FilterMethodEnum
        {
            Unused = -1,
            Include,    // FilterItems contains included values
            Exclude,    // FilterItems contains excluded values
            StringRange,// FilterItems contains 2 values. Means between 'a' and 'b' 
            DateRange,  // between StartDate & EndDate
            Before,     // < StartDate
            After,      // > StartDate
            RelativeDates // use RelativeDates field
        }

        // RelativeDates values
        // values the same as the 8.1 values. This facilitates conversion of batch reports from 8.1 to 9.0
        public enum RelativeDatesEnum
        {
            // The spelling of these enums needs to match the EXACT spelling of crystal date ranges
            // do not change them without making sure the strings work in the reportSelectionFilter
            Unused = -1,
            AllDatesFromToday = 0,   //         field formula = 0 in .rpt file (datatype=a)
            AllDatesToToday,         // = 1     field formula = 0 in .rpt file (datatype=b)
            Last24Hours,             // = 2     field formula = 1 in .rpt file
            LastFullDay,             // = 3
            Last7Days,               // = 4     field formula = 7 in .rpt file
            Aged0To30Days,           // = 5     field formula = 30 in .rpt file
            WeekToDateFromSun,       // = 6
            LastFullWeek,            // = 7
            LastFullMonth,           // = 8
            MonthToDate,             // = 9
            Next30Days,              // = 10
            Aged31To60Days,          // = 11
            Aged61To90Days,          // = 12
            LastYear,                // = 13     field formula = 365 in .rpt file
            LastYearYTD,             // = 14
            LastYearMTD,             // = 15
            Last4WeeksToSun,         // = 16
            YearToDate,              // = 17
            AllDatesToYesterday,     // = 18
            AllDatesFromTomorrow,    // = 19
            Over90Days,              // = 20
            Next31To60Days,          // = 21
            Next61To90Days,          // = 22
            Next91To365Days,         // = 23
            Calendar1stQtr,          // = 24
            Calendar2ndQtr,          // = 25
            Calendar3rdQtr,          // = 26
            Calendar4thQtr,          // = 27
            Calendar1stHalf,         // = 28
            Calendar2ndHalf          // = 29
        }

        // get a record
        // integer vals are returned as -1 if they're null
        public static bool GetRecord(int ReportIid, string FilterField, out TableData data)
        {
            bool Retval = true;
            data = new TableData(-1, "", FilterMethodEnum.Unused,
                RelativeDatesEnum.Unused, DateTime.MinValue, DateTime.MinValue, "");
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            StringBuilder tempStr = new StringBuilder();
            string SqlStatement = "SELECT * from REPORT_FILTER WHERE reportIid=" + ReportIid + " AND FilterField='" + FilterField + "'";
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ReportFilter", "GetRecord", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        MakeDataRec(myDataReader, out data);
                        if (tempStr.Length > 0)
                            tempStr.Append(",");

                        tempStr.Append(data.FilterItems);
                    }

                    data.FilterItems = tempStr.ToString();
                    
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
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

#if !NO_ASA
		// make a TableData object from a SADataReader object
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
			data = new TableData(
				MainClass.ToInt(TableName, myDataReader["ReportIid"])
				, myDataReader["FilterField"].ToString()
				, (FilterMethodEnum)MainClass.ToInt(TableName, myDataReader["FilterMethod"])
				, (RelativeDatesEnum)MainClass.ToInt(TableName, myDataReader["RelativeDates"])
				, MainClass.ToDate(TableName, myDataReader["StartDate"])
				, MainClass.ToDate(TableName, myDataReader["EndDate"])
				, myDataReader["FilterItems"].ToString());
		}
#endif

		// get all records with specified ReportIid
        // integer vals are returned as -1 if they're null
        // to use:
        // List<TableData> data = new List<TableData>(); GetRecords(iid, data);
        public static bool GetRecords(int ReportIid, List<TableData> list)
        {
            bool Retval = true;
            StringBuilder filter = new StringBuilder();
            StringBuilder items = new StringBuilder();
            list.Clear();
            TableData data = null;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from REPORT_FILTER WHERE reportIid=" + ReportIid + " ORDER BY filterField, filterItems";
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ReportFilter", "GetRecord", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        if (myDataReader["FilterField"].ToString() == filter.ToString())
                        {  // multiple records, append items
                            items.Append("," + myDataReader["filteritems"]);
                            continue;
                        }
                        else
                        {  // new filter field
                            if(data != null)
                            {  // save previous record
                                data.FilterItems = items.ToString();
                                list.Add(data);
                            }
                            // create new record
                            MakeDataRec(myDataReader, out data);
                            filter.Clear();
                            items.Clear();
                            filter.Append(myDataReader["FilterField"]);
                            items.Append(myDataReader["FilterItems"]);
                        }
                    }

                    // add last record to list
                    if (data != null)
                    {
                        data.FilterItems = items.ToString();
                        list.Add(data);
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "GetRecords", err);
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
        // OUTPUT: NewIid - IID of inserted record
        public static bool InsertRecord(TableData data, out int NewIid)
        {
            bool RetVal = false;
            int startVal = 0;
            int length = data.FilterItems.Length;
            StringBuilder tempStr;
            NewIid = -1;

            if (length > FilterItemsLength)
            {
                length = data.FilterItems.Substring(startVal, FilterItemsLength).LastIndexOf(',');
            }

            do
            {
                tempStr = new StringBuilder(data.FilterItems.Substring(startVal, length));

                string strSqlStatement = "INSERT INTO REPORT_FILTER (reportIid, filterField, filterMethod, relativeDates, startDate, endDate, filterItems) "
                    + "VALUES ('" + data.ReportIid + "', '" 
                    + MainClass.FixStringForSingleQuote(data.FilterField)
                    + "', '" + (int)data.FilterMethod
                    + "', '" + (int)data.RelativeDates
                    + "', " + MainClass.DateTimeToTimestamp(data.StartDate)
                    + ", " + MainClass.DateTimeToTimestamp(data.EndDate)
                    + ", '" + MainClass.FixStringForSingleQuote(tempStr.ToString()) + "')";
                RetVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "ReportFilter", "InsertRec", out NewIid);

                if (length == data.FilterItems.Length)
                { tempStr.Clear(); }  // filter was short, exit loop on first pass
                else
                {  // filter was long, split string into pieces and save multiple records
                    startVal += length + 1;
                    length = data.FilterItems.Length - startVal;

                    if (length > FilterItemsLength)  // still too long, split again
                    {
                        length = data.FilterItems.Substring(startVal, FilterItemsLength).LastIndexOf(',');
                    }
                    else if(length <= 0)  // ran out of string, time to stop
                    { tempStr.Clear(); }
                }

            } while (tempStr.Length > 0);

            return RetVal;
        }

        // delete records that have specified ReportIid
        public static bool DeleteRecords(int ReportIid)
        {
            bool Retval = true;
            string SqlStatement = string.Format("DELETE " + TableName + " WHERE ReportIid='{0}'",
                ReportIid);
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ReportFilter", "DeleteRecord");
            return Retval;
        }

        // update fields of specified record
        //public static bool UpdateRecord(TableData data)
        //{
        //    bool Retval = true;
        //    string sql = "UPDATE REPORT_FILTER SET " 
        //    + "filterMethod='" + data.filterMethod 
        //    + "', filterItems='" + MainClass.FixStringForSingleQuote(data.FilterItems) 
        //    + "' WHERE reportIid=" + data.ReportIid + " AND filterField='" + MainClass.FixStringForSingleQuote(data.FilterField) + "'";
        //    Retval = MainClass.ExecuteSql(sql, true, TableName, "ReportFilter", "UpdateRecord");

        //    return Retval;
        //}

        // parse string out to list
        public static bool CSVtoList(string csvInput, out bool IsInteger, out List<string> listOutput)
        {
            // convert CSV to a list of strings or integers
            // if the field is a number, there are no quotes, which distinguishes it from a string field
            // if the field is a string, then all of the single quotes have been doubled
            // and the strings are enclosed in quotes, so it must be parsed and the quotes restored

            bool fRet = false;
            bool isInt = false;
            Regex rRegexHolder = null;

            listOutput = null;
            IsInteger = false;

            // beginning error checking
            if (csvInput == null || csvInput.Length.Equals(0))
            {
                return fRet;
            }

            // field is integer if no quotes present
            isInt = IsInteger = (csvInput.IndexOf("'") == -1);

            listOutput = new List<string>();

            // different regex for strings and integers
            if (isInt == false)
            {
                // create regular expression to parse the CSV without messing up on quotation marks
                rRegexHolder = new Regex(@" \G (?:^|,) (?: \' ( (?: [^\'] | \'\' )* ) \' | ( [^\',]* ) )", RegexOptions.IgnorePatternWhitespace);
            }
            else
            {
                // parsing integers is easier
                rRegexHolder = new Regex(@" \G (?:^|,) ( -? [0-9]* )", RegexOptions.IgnorePatternWhitespace);
            }
            // get list of parameters
            MatchCollection MColl = rRegexHolder.Matches(csvInput);

            // fill in list, and remove double quotes
            foreach (Match M in MColl)
            {
                listOutput.Add(M.Groups[1].ToString().Replace("\'\'", "\'"));
                fRet = true;
            }

            return fRet;
        }

        // convert a list to a csv string
        public static bool ListToCSV(List<string> listInput, bool IsInteger, out string csvOutput)
        {
            // create CSV to be stored in the database for filtering on strings or integers
            // if the field is a string, then all of the single quotes are doubled
            // and the strings are enclosed in quotes, like would be inserted into a SQL query
            // if the field is a number, there are no quotes, which distinguishes it from a string field

            bool fRet = true;
            StringBuilder cat = new StringBuilder();
            string temp = null;
            string format = "\'{0}\'";

            // no quotes if not a string
            if (IsInteger)
            {
                format = "{0}";
            }

            try
            {
                // iterate through until you hit the end
                foreach (string s in listInput)
                {
                    temp = s.Replace("\'", "\'\'"); // double up quotes

                    if (!cat.Length.Equals(0))
                    {
                        cat.Append(","); // add separating comma
                    }
                    cat.AppendFormat(format, temp);  // add to tail of string
                }
            }
            catch
            {
                fRet = false;
            }

            // assign output
            csvOutput = cat.ToString();

            return fRet;
        }

        public static bool ListToDisplay(List<string> listInput, int length, out string dispOutput)
        {
            bool fRet = true;
            StringBuilder tempStr = new StringBuilder();

            // default to null
            dispOutput = null;
            try
            {
                foreach (string s in listInput)
                {
                    if ((tempStr.Length + s.Length + 6) > length)
                    {
                        tempStr.Append("...");
                        break;
                    }
                    else
                    {
                        // add another string
                        if (tempStr.Length > 0)
                        {
                            tempStr.Append(", ");
                        }
                        tempStr.AppendFormat("'{0}'", s);
                    }
                }
            }
            catch
            {
                fRet = false;
            }

            // double check we didn't go over
            if (tempStr.Length > length)
            {
                fRet = false;
            }

            // only copy if we didn't fail
            if (fRet == true)
            {
                dispOutput = string.Format("'{0}'", tempStr.ToString());
            }
            return fRet;
        }


	}
}
