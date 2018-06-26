//#define TestCases

using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

// interface to task_schedule table

namespace BBPyxisDB
{
       
    public class TaskSchedule
	{
        const string TableName = "TASK_SCHEDULE";

        // timeUnit field
        public enum TimeUnitEnum
        {
            Once = 0,
            Day,
            Week,
            Month,
            None    // not periodic - just run on demand
        }

        // dayOfWeekMask field
        public enum DayMask
        {
			Unused = -1,
			Sunday = 1,
            Monday = 2,
            Tuesday = 4,
            Wednesday = 8,
            Thursday = 16,
            Friday = 32,
            Saturday = 64,
            WeekDays = DayMask.Monday | DayMask.Tuesday | DayMask.Wednesday | DayMask.Thursday | DayMask.Friday,
            WeekEndDays = DayMask.Saturday | DayMask.Sunday,
			Day = DayMask.Monday | DayMask.Tuesday | DayMask.Wednesday | DayMask.Thursday | DayMask.Friday | DayMask.Saturday | DayMask.Sunday
        }

        // firstLast field
        public enum FirstLastEnum
        {
            Unused = -1,
            First,
            Second,
            Third,
            Fourth,
            Last
        }

        // monthOfYearMask field
        public enum MonthMask
        {
            January = 1,
            February = 2,
            March = 4,
            April = 8,
            May = 16,
            June = 32,
            July = 64,
            August = 128,
            September = 256,
            October = 512,
            November = 1024,
            December = 2048,	// 2048
    		AllMonths = 4095    // all above or'ed together
        }

		// contains values from a record in task_schedule table
		public class TableData
		{
			public int TaskScheduleIid;
			public int TaskId;
			public string TaskName;
			public int ArgId;
			public string UserName;
			public bool Enabled;
			public TaskSchedule.TimeUnitEnum TimeUnit;
			public int IntervalNum;
			public DateTime StartTime;
			public DateTime EndTime;
			public int DayOfWeekMask;
			public int MonthOfYearMask;
			public int DayOfMonth;
            public FirstLastEnum FirstLast;
			public bool DeleteWhenDone;
			public string TaskComment;
			public bool ExecMissedTasks;
			public string SchedString;
			public DateTime LastSuccess;
			public DateTime NextExecTime;

			public TableData(int taskScheduleIid, int taskId, string taskName,
				int argId, string userName,
				bool enabled, TaskSchedule.TimeUnitEnum timeUnit, int intervalNum,
				DateTime startTime, DateTime endTime, int dayOfWeekMask, int monthOfYearMask,
                int dayOfMonth, FirstLastEnum firstLast, bool deleteWhenDone,
				string taskComment, bool execMissedTasks,
				string schedString, DateTime lastSuccess, DateTime nextExecTime)
			{
				TaskScheduleIid = taskScheduleIid;
				TaskId = taskId;
				TaskName = taskName;
				ArgId = argId;
				UserName = userName;
				Enabled = enabled;
				TimeUnit = timeUnit;
				IntervalNum = intervalNum;
				StartTime = startTime;
				EndTime = endTime;
				DayOfWeekMask = dayOfWeekMask;
				MonthOfYearMask = monthOfYearMask;
				DayOfMonth = dayOfMonth;
                FirstLast = firstLast;
				DeleteWhenDone = deleteWhenDone;
				TaskComment = taskComment;
				ExecMissedTasks = execMissedTasks;
				SchedString = schedString;
				LastSuccess = lastSuccess;
				NextExecTime = nextExecTime;
			}

			// provided so that the listbox in RefillToMaxGui.HHAutoRefill can easily
			// display tasks
			public override string ToString()
			{
				return TaskName;
			}
		}

		// get all tasks from TASK_SCHEDULE table
		// integer vals are returned as -1 if they're null
		// DateTime vals are returned as MinValue if they're null
		// EnabledOnly - if true, only return enabled tasks
		// TaskId - only return records with this taskId. If 0 return all tasks
		public static bool GetRecs(bool EnabledOnly, int TaskId, List<TableData> list)
		{
			bool Retval = true;
			list.Clear();
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            TableData tsd;
			string where="";
            string SqlStatement = "SELECT * from TASK_SCHEDULE";
			if (EnabledOnly)
				where = " WHERE enabled=1";
            if (TaskId > 0)
            {
                if (where == "")
                    where = " WHERE taskId=" + TaskId;
                else
                    where += " AND taskId=" + TaskId;
            }
			if (where != "")
				SqlStatement += " " + where;
            SqlStatement += " ORDER BY nextExecTime, taskScheduleIid";
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "TaskSchedule", "GetRecs", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        DateTime dt = MainClass.ToDate(TableName, myDataReader["endTime"]);
                        MakeDataRec(myDataReader, out tsd);
                        list.Add(tsd);
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "TaskSchedule", "GetRecs", err);
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

        // get task from TASK_SCHEDULE table that has specified TaskScheduleIid
        // integer vals are returned as -1 if they're null
        // DateTime vals are returned as MinValue if they're null
        // return false if error or no record found
        public static bool GetRecord(bool EnabledOnly, int TaskScheduleIid,
            out TableData tsd)
        {
            tsd = null;
            string SqlStatement = "SELECT * from TASK_SCHEDULE WHERE taskScheduleIid=" + TaskScheduleIid;
            if (EnabledOnly)
                SqlStatement += " AND enabled=1";
            return GetData(SqlStatement, out tsd);
        }

        // get task from TASK_SCHEDULE table that has specified TaskName
        // integer vals are returned as -1 if they're null
        // DateTime vals are returned as MinValue if they're null
        // return false if error or no record found
        public static bool GetRecord(string TaskName, out TableData tsd)
        {
            tsd = null;
            string SqlStatement = "SELECT * from TASK_SCHEDULE WHERE taskName='" + TaskName + "'";
            return GetData(SqlStatement, out tsd);
        }

        // get task from TASK_SCHEDULE table that has specified ArgId
        // integer vals are returned as -1 if they're null
        // DateTime vals are returned as MinValue if they're null
        // return false if error or no record found
        public static bool GetRecordForArg(int Arg, out TableData tsd)
        {
            tsd = null;
            string SqlStatement = "SELECT * from TASK_SCHEDULE WHERE ArgId=" + Arg;
            return GetData(SqlStatement, out tsd);
        }

        // get task from TASK_SCHEDULE table given a select statement
        // integer vals are returned as -1 if they're null
        // DateTime vals are returned as MinValue if they're null
        // return false if error or no record found
        public static bool GetData(string SqlStatement, out TableData tsd)
        {
            bool Retval = false;
            tsd = null;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "TaskSchedule", "GetRecord", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                        MakeDataRec(myDataReader, out tsd);
                        Retval = true;
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "TaskSchedule", "GetRecord", err);
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
                 MainClass.ToInt(TableName, myDataReader["taskScheduleIid"]),
                 MainClass.ToInt(TableName, myDataReader["taskId"]),
                 myDataReader["taskName"].ToString(),
                 MainClass.ToInt(TableName, myDataReader["argId"]),
                 myDataReader["userName"].ToString(),
                 MainClass.ToBool(TableName, myDataReader["enabled"]),
                 (TimeUnitEnum)(Int16)myDataReader["timeUnit"],
                 MainClass.ToInt(TableName, myDataReader["intervalNum"]),
                 MainClass.ToDate(TableName, myDataReader["startTime"]),
                 MainClass.ToDate(TableName, myDataReader["endTime"]),
                 MainClass.ToInt(TableName, myDataReader["dayOfWeekMask"]),
                 MainClass.ToInt(TableName, myDataReader["monthOfYearMask"]),
                 MainClass.ToInt(TableName, myDataReader["dayOfMonth"]),
                 (FirstLastEnum)MainClass.ToInt(TableName, myDataReader["firstLast"]),
                 MainClass.ToBool(TableName, myDataReader["deleteWhenDone"]),
                 myDataReader["taskComment"].ToString(),
                 MainClass.ToBool(TableName, myDataReader["execMissedTasks"]),
                 myDataReader["schedString"].ToString(),
                 MainClass.ToDate(TableName, myDataReader["lastSuccess"]),
                 MainClass.ToDate(TableName, myDataReader["nextExecTime"]));
        }
#endif

        // insert record - all fields except lastSuccess and nextExecTime which are updated later.
        // MakeASchedString - if true, generate a string for the SchedString field. If false, use the schedString argument for the field.
        // -1 for an int arg means that DB field should be NULL
        // DateTime.MinTime for a DateTime arg means that DB field should be NULL
        // OUTPUT: NewIid - IID of inserted record
        public static bool InsertRecord(bool MakeASchedString, TableData data,
            out int NewIid)
        {
            bool RetVal = false;
            string FieldString = "taskId, taskName, argId, userName, enabled, timeUnit, intervalNum, startTime, endTime, dayOfWeekMask, monthOfYearMask, dayOfMonth, firstLast, deleteWhenDone, taskComment, execMissedTasks, schedString";
            string ValueString;
            string ScheduleString;
            NewIid = -1;

            ValueString = MainClass.IntToDBString(data.TaskId) + ", ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.TaskName) + "', ";
            ValueString += MainClass.IntToDBString(data.ArgId) + ", ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.UserName) + "', ";
            ValueString += "'" + MainClass.BoolToInt(data.Enabled) + "', ";
            ValueString += MainClass.IntToDBString((int)data.TimeUnit) + ", ";
            ValueString += MainClass.IntToDBString(data.IntervalNum) + ", ";
            ValueString += MainClass.DateTimeToTimestamp(data.StartTime) + ", ";
            ValueString += MainClass.DateTimeToTimestamp(data.EndTime) + ", ";
            ValueString += MainClass.IntToDBString(data.DayOfWeekMask) + ", ";
            ValueString += MainClass.IntToDBString(data.MonthOfYearMask) + ", ";
            ValueString += MainClass.IntToDBString(data.DayOfMonth) + ", ";
            ValueString += MainClass.IntToDBString((int)data.FirstLast) + ", ";
            ValueString += MainClass.BoolToInt(data.DeleteWhenDone) + ", ";
            ValueString += "'" + MainClass.FixStringForSingleQuote(data.TaskComment) + "', ";
            ValueString += MainClass.BoolToInt(data.ExecMissedTasks) + ", ";
            if (MakeASchedString)
                ScheduleString = MakeSchedString(data.TimeUnit, data.IntervalNum, data.StartTime,
                    data.DayOfWeekMask, data.MonthOfYearMask, data.DayOfMonth, data.FirstLast);
            else
                ScheduleString = data.SchedString;
            ValueString += "'" + ScheduleString + "'";

            string strSqlStatement = "INSERT INTO TASK_SCHEDULE (" + FieldString + ") VALUES (" + ValueString + ")";
            RetVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "TaskSchedule", "InsertRec", out NewIid);

            return RetVal;
        }

		// delete specified record
		public static bool DeleteRec(int Iid)
		{
			bool Retval = true;
			string sql = "DELETE TASK_SCHEDULE WHERE taskScheduleIid = " + Iid;
            Retval = MainClass.ExecuteSql(sql, true, TableName, "TaskSchedule", "DeleteRec");

			return Retval;
		}

        // Delete record by arg id
        public static bool DeleteTask(int TaskId, int argId)
        {
            bool retVal = true;
            string sql = "DELETE TASK_SCHEDULE WHERE taskId = " + TaskId + " AND argId = " + argId;
            retVal = MainClass.ExecuteSql(sql, true, TableName, "TaskSchedule", "DeleteTask");

            return retVal;
        }

		// update enabled field of specified record
		public static bool UpdateEnabled(int Iid, bool bEnabled)
		{
			bool Retval = true;
            int nEnabled = MainClass.BoolToInt(bEnabled);
			string sql = "UPDATE TASK_SCHEDULE SET ENABLED = " + nEnabled + " WHERE taskScheduleIid = " + Iid;
            Retval = MainClass.ExecuteSql(sql, true, TableName, "TaskSchedule", "UpdateEnabled");

			return Retval;
		}

        // update nextExecTime field of specified record
        public static bool UpdateNextExecTime(int Iid, DateTime NextExecTime)
        {
            bool Retval = true;
            string sql = "UPDATE TASK_SCHEDULE SET nextExecTime = " + MainClass.DateTimeToTimestamp(NextExecTime) + " WHERE taskScheduleIid = " + Iid;
            Retval = MainClass.ExecuteSql(sql, true, TableName, "TaskSchedule", "UpdateNextExecTime");

            return Retval;
        }

        // update lastSuccess field of specified record
        public static bool UpdateLastSuccessTime(int Iid, DateTime LastSuccess)
        {
            bool Retval = true;
            string sql = "UPDATE TASK_SCHEDULE SET lastSuccess = " + MainClass.DateTimeToTimestamp(LastSuccess) + " WHERE taskScheduleIid = " + Iid;
            Retval = MainClass.ExecuteSql(sql, true, TableName, "TaskSchedule", "UpdateLastSuccessTime");

            return Retval;
        }

        // update if there's a record with given taskId, else insert new record.
		public static bool UpdateOrInsertRecord(TableData data)
		{
			bool Retval = true;
            bool done = false;
            int IID;

            // check for batch report task type
            if (data.TaskId == 3)
            {  // process batch report
                TableData task;
                if (GetRecordForArg(data.ArgId, out task))
                {
                    // update
                    Retval = UpdateRecord(task.TaskScheduleIid, data);
                }
                else
                {
                    // add
                    Retval = InsertRecord(true, data, out IID);
                }
                
            }
            else
            {  // process other task types
                List<TableData> list = new List<TableData>();
                if (GetRecs(false, data.TaskId, list))
                {
                    if (list.Count > 0)
                    {
                        Retval = UpdateRecord(list[0].TaskScheduleIid, data);
                        done = true;
                    }
                }
                if (!done)
                {
                    Retval = InsertRecord(true, data, out IID);
                }
            }

            return Retval;
        }

		// update fields of specified record - all fields except lastSuccess (left alone) and nextExecTime (made null) which are updated later.
        // -1 for an int or DateTime field means make it null
		public static bool UpdateRecord(int TaskScheduleIid, TableData data)
		{
			bool Retval = true;
            string ScheduleString = MakeSchedString(data.TimeUnit, data.IntervalNum, data.StartTime,
                data.DayOfWeekMask, data.MonthOfYearMask, data.DayOfMonth, data.FirstLast);
			string sql = "UPDATE TASK_SCHEDULE SET taskId=" + data.TaskId
                + ", taskName=" + "'" + MainClass.FixStringForSingleQuote(data.TaskName) + "'"
				+ ", argId=" + MainClass.IntToDBString(data.ArgId)
                + ", userName=" + "'" + MainClass.FixStringForSingleQuote(data.UserName) + "'"
				+ ", enabled=" + MainClass.BoolToInt(data.Enabled)
				+ ", timeUnit=" + (int)data.TimeUnit
                + ", intervalNum=" + MainClass.IntToDBString(data.IntervalNum)
				+ ", startTime=" + MainClass.DateTimeToTimestamp(data.StartTime)
				+ ", endTime=" + MainClass.DateTimeToTimestamp(data.EndTime)
                + ", dayOfWeekMask=" + MainClass.IntToDBString(data.DayOfWeekMask)
                + ", monthOfYearMask=" + MainClass.IntToDBString(data.MonthOfYearMask)
                + ", dayOfMonth=" + MainClass.IntToDBString(data.DayOfMonth)
                + ", firstLast=" + MainClass.IntToDBString((int)data.FirstLast)
				+ ", deleteWhenDone=" + MainClass.BoolToInt(data.DeleteWhenDone)
                + ", taskComment=" + "'" + MainClass.FixStringForSingleQuote(data.TaskComment) + "'"
                + ", execMissedTasks=" + MainClass.BoolToInt(data.ExecMissedTasks)
                + ", schedString=" + "'" + ScheduleString + "'"
                + ", nextExecTime=NULL"
                + " WHERE taskScheduleIid=" + TaskScheduleIid;
            Retval = MainClass.ExecuteSql(sql, true, TableName, "TaskSchedule", "UpdateRecord");
			return Retval;
		}

        // make a string that describes the schedule
        static string MakeSchedString(TimeUnitEnum timeUnit, int intervalNum,
                DateTime startTime, int dayOfWeekMask, int monthOfYearMask,
                int dayOfMonth, FirstLastEnum firstLast)
        {
            string strSchedString = "";
            string strFirstLast="";
            string DayString="";
            string[] DayNames = {MainClass.StringTable.GetString("Sunday"), 
                MainClass.StringTable.GetString("Monday"), 
                MainClass.StringTable.GetString("Tuesday"),
		        MainClass.StringTable.GetString("Wednesday"), 
                MainClass.StringTable.GetString("Thursday"), 
                MainClass.StringTable.GetString("Friday"), 
                MainClass.StringTable.GetString("Saturday"), 
                MainClass.StringTable.GetString("day")};

            // one time
            if (timeUnit == TimeUnitEnum.Once)
                //strSchedString = startTime.ToString("yyyy/MM/dd HH:mm:ss");
                strSchedString = startTime.ToShortDateString() + " " + startTime.ToLongTimeString();


            // daily
            else if (timeUnit == TimeUnitEnum.Day)
            {
                if (intervalNum == 1)
                    strSchedString = MainClass.StringTable.GetString("DailyAtSpace") + startTime.ToLongTimeString();
                else
                    strSchedString = MainClass.StringTable.GetString("EverySpace") + intervalNum + MainClass.StringTable.GetString("SpaceDaysAtSpace") + startTime.ToLongTimeString();
            }

            // weekly
            else if (timeUnit == TimeUnitEnum.Week)
            {
                if (intervalNum == 1)
                    strSchedString = MainClass.StringTable.GetString("EverySpace") + MakeDayString(dayOfWeekMask, startTime) + MainClass.StringTable.GetString("SpaceAtSpace") + startTime.ToLongTimeString();
                else
                    strSchedString = MainClass.StringTable.GetString("EverySpace") + intervalNum + MainClass.StringTable.GetString("SpaceWeeksOnSpace") + MakeDayString(dayOfWeekMask, startTime) + MainClass.StringTable.GetString("SpaceAtSpace") + startTime.ToLongTimeString();
            }

            // monthly
            else if (timeUnit == TimeUnitEnum.Month)
            {
                if (dayOfMonth > 0)
                {
                    if (monthOfYearMask == (int)MonthMask.AllMonths)
                        strSchedString = MakeDayNumberString(dayOfMonth) + MainClass.StringTable.GetString("SpaceOfEveryMonthAtSpace") + startTime.ToLongTimeString();
                    else
                        strSchedString = MakeDayNumberString(dayOfMonth) + MainClass.StringTable.GetString("SpaceOfEverySpace") + MakeMonthString(monthOfYearMask) + MainClass.StringTable.GetString("SpaceAtSpace") + startTime.ToLongTimeString();
                }
                else
                {
                    if (firstLast == FirstLastEnum.First)
                    {
                        strFirstLast = MainClass.StringTable.GetString("First");
                        DayString = MakeDayString(dayOfWeekMask, DateTime.MinValue);
                    }
                    else if (firstLast == FirstLastEnum.Second)
                    {
                        strFirstLast = MainClass.StringTable.GetString("Second");
                        DayString = MakeDayString(dayOfWeekMask, DateTime.MinValue);
                    }
                    else if (firstLast == FirstLastEnum.Third)
                    {
                        strFirstLast = MainClass.StringTable.GetString("Third");
                        DayString = MakeDayString(dayOfWeekMask, DateTime.MinValue);
                    }
                    else if (firstLast == FirstLastEnum.Fourth)
                    {
                        strFirstLast = MainClass.StringTable.GetString("Fourth");
                        DayString = MakeDayString(dayOfWeekMask, DateTime.MinValue);
                    }
                    else if (firstLast == FirstLastEnum.Last)
                    {
                        strFirstLast = MainClass.StringTable.GetString("Last");
                        DayString = MakeDayString(dayOfWeekMask, DateTime.MinValue);
                    }
                    else
                    {
                        string err = "bad firstLast value: " + firstLast;
                        ServiceMessages.InsertRec(MainClass.AppName, "TaskSchedule", "MakeSchedString", err);
                    }
                    if (monthOfYearMask == (int)MonthMask.AllMonths)
                    {
                        strSchedString = strFirstLast + " " + DayString + MainClass.StringTable.GetString("SpaceOfEveryMonthAtSpace") + startTime.ToLongTimeString();
                    }
                    else
                    {
                        strSchedString = strFirstLast + " " + DayString + MainClass.StringTable.GetString("SpaceOfSpace") + MakeMonthString(monthOfYearMask) + MainClass.StringTable.GetString("SpaceAtSpace") + startTime.ToLongTimeString();
                    }
                }
            }

            return strSchedString;
        }

        // make a string containing the list of days
        static string MakeDayString(int dayOfWeekMask, DateTime startTime)
        {
            string strDayString = "";
            List<string> DayArray = new List<string>();
            int day;
            string[] DayNames = {MainClass.StringTable.GetString("Sunday"), 
                MainClass.StringTable.GetString("Monday"), 
                MainClass.StringTable.GetString("Tuesday"),
		        MainClass.StringTable.GetString("Wednesday"), 
                MainClass.StringTable.GetString("Thursday"), 
                MainClass.StringTable.GetString("Friday"), 
                MainClass.StringTable.GetString("Saturday")};

            if (dayOfWeekMask == 0 && startTime > DateTime.MinValue)	// 0 means same day as start time
            {
                strDayString = DayNames[(int)startTime.DayOfWeek - 1];
            }
            else if (dayOfWeekMask == 127)	// all days
                strDayString = MainClass.StringTable.GetString("Daylowercase");
            else if (dayOfWeekMask == (int)DayMask.WeekDays)	// WeekDays
                strDayString = MainClass.StringTable.GetString("Weekdaylowercase");
            else
            {
                // build array of selected days
                for (day = 0; day <= 6; day++)
                {
                    if ((IntPow(2, day) & dayOfWeekMask) > 0)
                        DayArray.Add(DayNames[day]);
                }

                // make string
                if (DayArray.Count > 0)
                {
                    strDayString = DayArray[0];
                    for (day = 1; day < DayArray.Count-1; day++)
                    {
                        strDayString += ", " + DayArray[day];
                    }
                    if (DayArray.Count > 1)
                        strDayString += MainClass.StringTable.GetString("andwithspace") + DayArray[DayArray.Count - 1];
                }
            }

            return strDayString;
        }

        // make a string containing the list of months
        static string MakeMonthString(int monthOfYearMask)
        {
            string strMonthString = "";
            List<string> MonthArray = new List<string>();
            int month;
            string[] MonthNames = {MainClass.StringTable.GetString("January"), 
                MainClass.StringTable.GetString("February"), 
                MainClass.StringTable.GetString("March"),
        		MainClass.StringTable.GetString("April"), 
                MainClass.StringTable.GetString("May"), 
                MainClass.StringTable.GetString("June"), 
                MainClass.StringTable.GetString("July"), 
                MainClass.StringTable.GetString("August"), 
		        MainClass.StringTable.GetString("September"), 
                MainClass.StringTable.GetString("October"), 
                MainClass.StringTable.GetString("November"), 
                MainClass.StringTable.GetString("December")};

            // build array of selected months
            for (month = 0; month <= 11; month++)
            {
                if ((IntPow(2, month) & monthOfYearMask) > 0)
                    MonthArray.Add(MonthNames[month]);
            }

            // make string
            if (MonthArray.Count > 0)
            {
                strMonthString = MonthArray[0];
                for (month = 1; month < MonthArray.Count-1; month++)
                {
                    strMonthString += ", " + MonthArray[month];
                }
                if (MonthArray.Count > 1)
                    strMonthString += MainClass.StringTable.GetString("andwithspace") + MonthArray[MonthArray.Count - 1];
            }

            return strMonthString;
        }

        // make a string like 4th
        static string MakeDayNumberString(int nDay)
        {
            string strDayNumberString = "";
            int nLastDigit = nDay;

            if (nLastDigit > 9)
                nLastDigit = nLastDigit - (10 * (nLastDigit / 10));

            string lang;
            lang = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            // 4/14/10 CG: FP 15203 this is a temporary fix to avoid appending the
            // English suffix to convert the date into an ordinal.  The correct way
            // to fix it would be to all all strings first - thirty first to the string
            // table.  For now, I've only added first - seventh to the string table.
            if (lang == "en")
            {
                if (nDay >= 4 && nDay <= 20)
                    strDayNumberString = nDay + "th";	// sorry Frank how the hell do we internationalize this?
                else if (nLastDigit == 1)
                    strDayNumberString = nDay + "st";
                else if (nLastDigit == 2)
                    strDayNumberString = nDay + "nd";
                else if (nLastDigit == 3)
                    strDayNumberString = nDay + "rd";
                else
                    strDayNumberString = nDay + "th";
            }
            else
            {
                strDayNumberString = nDay + MainClass.StringTable.GetString("ordinalchar");
            }

            return strDayNumberString;
        }

#if TestCases
        // used only for debugging
        // if you run this twice in a row without deleting the TASK_SCHEDULE table it will throw an exception since taskName field will not be unique
        public static void TestCrap()
        {
            int IID;
            int alldays = 1 | 2 | 4 | 8 | 16 | 32 | 64;

            // null fields
            InsertRec(true, 1, "1a", 1, "x", true,
                TimeUnitEnum.Month, 1, DateTime.Now, DateTime.Now,
                -1, 4, -1, 2, -1, false, "x", true, "", out IID);
            // once
            InsertRec(true, 1, "1", -1, "", true,
                TimeUnitEnum.Once, -1, DateTime.Now, DateTime.MinValue,
                -1, -1, -1, -1, -1, false, "", true, "", out IID);
            // daily
            InsertRec(true, 1, "2", 1, "x", true,
                TimeUnitEnum.Day, 1, DateTime.Now, DateTime.Now,
                -1, -1, -1, -1, -1, false, "x", true, "", out IID);
            // 3 days
            InsertRec(true, 1, "3", 1, "x", true,
                TimeUnitEnum.Day, 3, DateTime.Now, DateTime.Now,
                -1, -1, -1, -1, -1, false, "x", true, "", out IID);
            // today
            InsertRec(true, 1, "4", 1, "x", true,
                TimeUnitEnum.Week, 1, DateTime.Now, DateTime.Now,
                0, -1, -1, -1, -1, false, "x", true, "", out IID);
            // sun
            InsertRec(true, 1, "5", 1, "x", true,
                TimeUnitEnum.Week, 1, DateTime.Now, DateTime.Now,
                1, -1, -1, -1, -1, false, "x", true, "", out IID);
            // every day
            InsertRec(true, 1, "6", 1, "x", true,
                TimeUnitEnum.Week, 1, DateTime.Now, DateTime.Now,
                alldays, -1, -1, -1, -1, false, "x", true, "", out IID);
            // 3 fri  sat
            InsertRec(true, 1, "7", 1, "x", true,
                TimeUnitEnum.Week, 3, DateTime.Now, DateTime.Now,
                32 | 64, -1, -1, -1, -1, false, "x", true, "", out IID);
            // 31st jan
            InsertRec(true, 1, "8", 1, "x", true,
                TimeUnitEnum.Month, 1, DateTime.Now, DateTime.Now,
                -1, 1, 31, -1, -1, false, "x", true, "", out IID);
            // 1st nov  dec
            InsertRec(true, 1, "9", 1, "x", true,
                TimeUnitEnum.Month, 1, DateTime.Now, DateTime.Now,
                -1, 1024 + 2048, 1, -1, -1, false, "x", true, "", out IID);
            // 2nd every month
            InsertRec(true, 1, "10", 1, "x", true,
                TimeUnitEnum.Month, 1, DateTime.Now, DateTime.Now,
                -1, 4095, 2, -1, -1, false, "x", true, "", out IID);
            // 3rd feb  april
            InsertRec(true, 1, "11", 1, "x", true,
                TimeUnitEnum.Month, 1, DateTime.Now, DateTime.Now,
                -1, 2 + 8, 3, -1, -1, false, "x", true, "", out IID);
            // 4th feb  april
            InsertRec(true, 1, "12", 1, "x", true,
                TimeUnitEnum.Month, 1, DateTime.Now, DateTime.Now,
                -1, 2 + 8, 4, -1, -1, false, "x", true, "", out IID);
            // 11th feb  april
            InsertRec(true, 1, "13", 1, "x", true,
                TimeUnitEnum.Month, 1, DateTime.Now, DateTime.Now,
                -1, 2 + 8, 11, -1, -1, false, "x", true, "", out IID);
            // first tue march
            InsertRec(true, 1, "14", 1, "x", true,
                TimeUnitEnum.Month, 1, DateTime.Now, DateTime.Now,
                -1, 4, -1, 2, -1, false, "x", true, "", out IID);
            // last tue all months
            InsertRec(true, 1, "15", 1, "x", true,
                TimeUnitEnum.Month, 1, DateTime.Now, DateTime.Now,
                -1, 4095, -1, -1, 2, false, "x", true, "", out IID);
            // last day march
            InsertRec(true, 1, "16", 1, "x", true,
                TimeUnitEnum.Month, 1, DateTime.Now, DateTime.Now,
                -1, 4, -1, -1, 7, false, "x", true, "", out IID);
        }
#endif

        // calculate base ** exp
        public static int IntPow(int bas, int exp)
        {
            int RetVal = 1;
            for (int i = 0; i < exp; i++)
                RetVal *= bas;
            return RetVal;
        }

			
	}
}
