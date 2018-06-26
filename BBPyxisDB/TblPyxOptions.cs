using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Tblpyxoptions
	{
		const string TableName = "TBLPYXOPTIONS";

		// collection of record fields
		public class TableData
		{
			public string ConsoleName;
			public int LogComm;
			public int LogTaskSched;
			public int PtDischInactiveDays;
			public int PtNotDischInactiveDays;
			public int DiscrepKeepDays;
			public int TrxKeepDays;
			public int UserHistKeepDays;
			public int PtHistKeepDays;
			public int SvcMsgKeepDays;
			public int RefillTblKeepHours;
			public int ItemHistKeepDays;
			public string DbServerName;
			public string ConsoleToken0;
			public string SoftwareVersion;
			public int ParsePatientName;
			public int StnMaxTrxKeepDays;
			public int MaxItemAliases;
			public int SummaryTblKeepDays;
			public int TimeBias;
			public string MyDeviceName;
			public int ArchiveKeepDays;
			public int HandheldNonCommHours;
			public int CaseKeepDays;
			public int SavedReportKeepDays;
			public bool LogService;
            public int MaxPvsClients;
            public int TempTransactionKeepDays;
            public int MyCaseListKeepDays;


            public TableData(string ConsoleName, int LogComm, int LogTaskSched, int PtDischInactiveDays, int PtNotDischInactiveDays, int DiscrepKeepDays, int TrxKeepDays, int UserHistKeepDays, int PtHistKeepDays, int SvcMsgKeepDays, int RefillTblKeepHours, int ItemHistKeepDays, string DbServerName, string ConsoleToken0, string SoftwareVersion, int ParsePatientName, int StnMaxTrxKeepDays, int MaxItemAliases, int SummaryTblKeepDays, int TimeBias, string MyDeviceName, int ArchiveKeepDays, int HandheldNonCommHours, int CaseKeepDays, int SavedReportKeepDays, bool LogService, int MaxPvsClients, int TempTransactionKeepDays,int MyCaseListKeepDays)
			{
				this.ConsoleName = ConsoleName;
				this.LogComm = LogComm;
				this.LogTaskSched = LogTaskSched;
				this.PtDischInactiveDays = PtDischInactiveDays;
				this.PtNotDischInactiveDays = PtNotDischInactiveDays;
				this.DiscrepKeepDays = DiscrepKeepDays;
				this.TrxKeepDays = TrxKeepDays;
				this.UserHistKeepDays = UserHistKeepDays;
				this.PtHistKeepDays = PtHistKeepDays;
				this.SvcMsgKeepDays = SvcMsgKeepDays;
				this.RefillTblKeepHours = RefillTblKeepHours;
				this.ItemHistKeepDays = ItemHistKeepDays;
				this.DbServerName = DbServerName;
				this.ConsoleToken0 = ConsoleToken0;
				this.SoftwareVersion = SoftwareVersion;
				this.ParsePatientName = ParsePatientName;
				this.StnMaxTrxKeepDays = StnMaxTrxKeepDays;
				this.MaxItemAliases = MaxItemAliases;
				this.SummaryTblKeepDays = SummaryTblKeepDays;
				this.TimeBias = TimeBias;
				this.MyDeviceName = MyDeviceName;
				this.ArchiveKeepDays = ArchiveKeepDays;
				this.HandheldNonCommHours = HandheldNonCommHours;
				this.CaseKeepDays = CaseKeepDays;
				this.SavedReportKeepDays = SavedReportKeepDays;
				this.LogService = LogService;
                this.MaxPvsClients = MaxPvsClients;
                this.TempTransactionKeepDays = TempTransactionKeepDays;
                this.MyCaseListKeepDays = MyCaseListKeepDays;
            }
		}

        // return 1st record
        public static bool GetRecord(out TableData data)
        {
            bool Retval = true;
            data = null;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from " + TableName;
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Tblpyxoptions", "GetRecord", out _conn, out myDataReader))
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
				myDataReader["ConsoleName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["LogComm"])
				, MainClass.ToInt(TableName, myDataReader["LogTaskSched"])
				, MainClass.ToInt(TableName, myDataReader["PtDischInactiveDays"])
				, MainClass.ToInt(TableName, myDataReader["PtNotDischInactiveDays"])
				, MainClass.ToInt(TableName, myDataReader["DiscrepKeepDays"])
				, MainClass.ToInt(TableName, myDataReader["TrxKeepDays"])
				, MainClass.ToInt(TableName, myDataReader["UserHistKeepDays"])
				, MainClass.ToInt(TableName, myDataReader["PtHistKeepDays"])
				, MainClass.ToInt(TableName, myDataReader["SvcMsgKeepDays"])
				, MainClass.ToInt(TableName, myDataReader["RefillTblKeepHours"])
				, MainClass.ToInt(TableName, myDataReader["itemHistKeepDays"])
				, myDataReader["DbServerName"].ToString()
				, myDataReader["ConsoleToken0"].ToString()
				, myDataReader["SoftwareVersion"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ParsePatientName"])
				, MainClass.ToInt(TableName, myDataReader["StnMaxTrxKeepDays"])
				, MainClass.ToInt(TableName, myDataReader["MaxItemAliases"])
				, MainClass.ToInt(TableName, myDataReader["SummaryTblKeepDays"])
				, MainClass.ToInt(TableName, myDataReader["TimeBias"])
				, myDataReader["MyDeviceName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ArchiveKeepDays"])
				, MainClass.ToInt(TableName, myDataReader["HandheldNonCommHours"])
				, MainClass.ToInt(TableName, myDataReader["CaseKeepDays"])
				, MainClass.ToInt(TableName, myDataReader["SavedReportKeepDays"])
				, MainClass.ToBool(TableName, myDataReader["LogService"])
                , MainClass.ToInt(TableName, myDataReader["MaxPVSClients"])
                , MainClass.ToInt(TableName, myDataReader["TempTransactionKeepDays"])
                , MainClass.ToInt(TableName, myDataReader["MyCaseListKeepDays"]));
}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

            string SqlStatement = "INSERT INTO TBLPYXOPTIONS (ConsoleName, LogComm, LogTaskSched, PtDischInactiveDays, PtNotDischInactiveDays, DiscrepKeepDays, TrxKeepDays, UserHistKeepDays, PtHistKeepDays, SvcMsgKeepDays, RefillTblKeepHours, ItemHistKeepDays, DbServerName, ConsoleToken0, SoftwareVersion, ParsePatientName, StnMaxTrxKeepDays, MaxItemAliases, SummaryTblKeepDays, TimeBias, MyDeviceName, ArchiveKeepDays, HandheldNonCommHours, CaseKeepDays, SavedReportKeepDays, LogService, MaxPVSClients, TempTransactionKeepDays,MyCaseListKeepDays) VALUES ("
                + "'" + MainClass.FixStringForSingleQuote(data.ConsoleName) + "'" + ", " + (int)data.LogComm + ", " + (int)data.LogTaskSched + ", " + (int)data.PtDischInactiveDays + ", " + (int)data.PtNotDischInactiveDays + ", " + (int)data.DiscrepKeepDays + ", " + (int)data.TrxKeepDays + ", " + (int)data.UserHistKeepDays + ", " + (int)data.PtHistKeepDays + ", " + (int)data.SvcMsgKeepDays + ", " + (int)data.RefillTblKeepHours + ", " + (int)data.ItemHistKeepDays + ", " + "'" + MainClass.FixStringForSingleQuote(data.DbServerName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ConsoleToken0) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SoftwareVersion) + "'" + ", " + (int)data.ParsePatientName + ", " + (int)data.StnMaxTrxKeepDays + ", " + (int)data.MaxItemAliases + ", " + (int)data.SummaryTblKeepDays + ", " + (int)data.TimeBias + ", " + "'" + MainClass.FixStringForSingleQuote(data.MyDeviceName) + "'" + ", " + (int)data.ArchiveKeepDays + ", " + (int)data.HandheldNonCommHours + ", " + (int)data.CaseKeepDays + ", " + (int)data.SavedReportKeepDays + ", " + (bool)data.LogService + ", " + (int)data.MaxPvsClients + ", " + (int)data.TempTransactionKeepDays + ", " + (int)data.MyCaseListKeepDays +")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Tblpyxoptions", "InsertRecord");
			return Retval;
		}

        // Get Server name
        public static string GetServerName()
        {
            string consoleName = "";

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from TBLPYXOPTIONS";

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "TBLPYXOPTIONS", "GetServerName", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                        consoleName = myDataReader["DbServerName"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "GetServerName", err);
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
            return consoleName;
            
        }

        public static bool IsClient()
        {
            string serverName = "";
            bool isClient = false;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from TBLPYXOPTIONS";

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "TBLPYXOPTIONS", "IsClient", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                        serverName = myDataReader["DbServerName"].ToString();
                        if (string.Compare(serverName, Environment.MachineName, true) != 0)
                            isClient = true;
                    }
                }
                catch (Exception ex)
                {
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "IsClient", err);
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
            return isClient; 
        }
   }
}
