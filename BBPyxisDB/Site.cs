using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
using System.Runtime.InteropServices;
#endif

namespace BBPyxisDB
{

	public class Site
	{
		const string TableName = "SITE";

		// collection of record fields
		public class TableData : IDisposable
		{
			public int SIid;
			public string SiteId;
			public string SiteName;
			public string Address1;
			public string Address2;
			public string City;
			public string State;
			public string ZipCode;
			public string Country;
			public string Contact;
			public string Phone;
			public string DiagLine;
			public string Fax;
			public string Description;
			public string Notes;
			public string FacilityCode;
			public int PreExpPw;
			public string ArchiveId;
			public int PwExpDelta;
			public int UserIdExpireDelta;
			public int TempUserExpDays;
			public int PwLen;
			public string PtIdFormat;
			public int ConLogOffTimeOut;
			public int RptSortByItemId;
			public int Voice;
			public string Version;
			public int ArchiveYearFormat;
			public string DbMachineName;
			public int PrintStockOut;
			public int PrintCriticalLow;
			public int PrintADTconflict;
			public int PrintIncomingDiscrep;
			public int PrintCommDown;
			public int PermitGlobalPatientList;
			public string HomeFax;
			public string HomeEmail;
			public int ClientServerSystem;
			public int ORFeatures;
			public int DodWarning;
            public bool CopyReports;
			public bool EmailReports;
			public int UserDoorAccessPerItem;
			public int TechDoorAccessPerItem;
			public int Ticci;
			public int PwEnableComplex;
			public string PwComplexRegex;
			public string PwComplexDesc;
			public int PwMaxLen;
			public int PwHistory;
			public int PwHistoryClamp;
			public int ORISInterface;
            public string SmtpHost;
            public int SmtpPort;
            public bool SmtpUseSSL;
            public string SmtpUsername;
            //Use SecureString class for password storage
            //public string SmtpPassword;
            public SecureString SmtpPassword;

            public bool pwEnableExtended;
            public bool upgrade;
            public string siteGUID;
            public int allowGLCodeTransactions;

            public int AllowORISTempToTempCaseTransfer;
            public int allowAssociator;
            public int allowAutoCaseToPatientXfer;
            public int HoldRFIDTxForCase;
            public int ActiveDirectoryOnly;
            public int logSeverity;
            public string logMsgFormat;
            public string logServerIP;
            public string logServerPort;
            public int logEnableRemote;
			public int PwMinAge;
            public int PwMinChg;


            public TableData(int SIid, string SiteId, string SiteName, string Address1, string Address2, string City, string State, string ZipCode, string Country, string Contact, string Phone, string DiagLine, string Fax, string Description, string Notes, string FacilityCode, int PreExpPw, string ArchiveId, int PwExpDelta, int UserIdExpireDelta, int TempUserExpDays, int PwLen, string PtIdFormat, int ConLogOffTimeOut, int RptSortByItemId, int Voice, string Version, int ArchiveYearFormat, string DbMachineName, int PrintStockOut, int PrintCriticalLow, int PrintADTconflict, int PrintIncomingDiscrep, int PrintCommDown, int PermitGlobalPatientList, string HomeFax, string HomeEmail, int ClientServerSystem, int ORFeatures, int DodWarning, bool CopyReports, bool EmailReports, int UserDoorAccessPerItem, int TechDoorAccessPerItem, int Ticci, int PwEnableComplex, string PwComplexRegex, string PwComplexDesc, int PwMaxLen, int PwHistory, int PwHistoryClamp, int ORISInterface, string SmtpHost, int SmtpPort, bool SmtpUseSSL, string SmtpUsername, SecureString SmtpPassword, bool pwEnableExtended, bool upgrade, string siteGUID, int allowGLCodeTransactions,
                int allowORISTempToTempCaseTransfer, int allowAssociator, int allowAutoCaseToPatientXfer, int holdRFIDTxForCase, int activeDirectoryOnly, int logSeverity, string logMsgFormat, string logServerIP, string logServerPort, int logEnableRemote, int PwMinAge, int PwMinChg)
			{
				this.SIid = SIid;
				this.SiteId = SiteId;
				this.SiteName = SiteName;
				this.Address1 = Address1;
				this.Address2 = Address2;
				this.City = City;
				this.State = State;
				this.ZipCode = ZipCode;
				this.Country = Country;
				this.Contact = Contact;
				this.Phone = Phone;
				this.DiagLine = DiagLine;
				this.Fax = Fax;
				this.Description = Description;
				this.Notes = Notes;
				this.FacilityCode = FacilityCode;
				this.PreExpPw = PreExpPw;
				this.ArchiveId = ArchiveId;
				this.PwExpDelta = PwExpDelta;
				this.UserIdExpireDelta = UserIdExpireDelta;
				this.TempUserExpDays = TempUserExpDays;
				this.PwLen = PwLen;
				this.PtIdFormat = PtIdFormat;
				this.ConLogOffTimeOut = ConLogOffTimeOut;
				this.RptSortByItemId = RptSortByItemId;
				this.Voice = Voice;
				this.Version = Version;
				this.ArchiveYearFormat = ArchiveYearFormat;
				this.DbMachineName = DbMachineName;
				this.PrintStockOut = PrintStockOut;
				this.PrintCriticalLow = PrintCriticalLow;
				this.PrintADTconflict = PrintADTconflict;
				this.PrintIncomingDiscrep = PrintIncomingDiscrep;
				this.PrintCommDown = PrintCommDown;
				this.PermitGlobalPatientList = PermitGlobalPatientList;
				this.HomeFax = HomeFax;
				this.HomeEmail = HomeEmail;
				this.ClientServerSystem = ClientServerSystem;
				this.ORFeatures = ORFeatures;
				this.DodWarning = DodWarning;
				this.CopyReports = CopyReports;
				this.EmailReports = EmailReports;
				this.UserDoorAccessPerItem = UserDoorAccessPerItem;
				this.TechDoorAccessPerItem = TechDoorAccessPerItem;
				this.Ticci = Ticci;
				this.PwEnableComplex = PwEnableComplex;
				this.PwComplexRegex = PwComplexRegex;
				this.PwComplexDesc = PwComplexDesc;
				this.PwMaxLen = PwMaxLen;
				this.PwHistory = PwHistory;
				this.PwHistoryClamp = PwHistoryClamp;
				this.ORISInterface = ORISInterface;
                this.SmtpHost = SmtpHost;
                this.SmtpPort = SmtpPort;
                this.SmtpUseSSL = SmtpUseSSL;
                this.SmtpUsername = SmtpUsername;
                this.SmtpPassword = SmtpPassword;
                this.pwEnableExtended = pwEnableExtended;
                this.upgrade = upgrade;
                this.siteGUID = siteGUID;
                this.allowGLCodeTransactions = allowGLCodeTransactions;

                this.AllowORISTempToTempCaseTransfer = allowORISTempToTempCaseTransfer;
                this.allowAssociator = allowAssociator;
                this.allowAutoCaseToPatientXfer = allowAutoCaseToPatientXfer;
                this.HoldRFIDTxForCase = holdRFIDTxForCase;
                this.ActiveDirectoryOnly = activeDirectoryOnly;

                this.logSeverity = logSeverity;
                this.logMsgFormat = logMsgFormat;
                this.logServerIP = logServerIP;
                this.logServerPort = logServerPort;
                this.logEnableRemote = logEnableRemote;
                this.PwMinAge = PwMinAge;
                this.PwMinChg = PwMinChg;
            }

            public void Dispose()
            {
                SmtpPassword.Dispose();
            }
        }

        // return 1st (only) record
        public static bool GetFirstRecord(out TableData data)
        {
            bool Retval = true;
            data = null;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from SITE");
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Site", "GetFirstRecord", out _conn, out myDataReader))
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
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "GetFirstRecord", err);
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


        // return record given its primary key
        public static bool GetRecord(int SIid, out TableData data)
        {
            bool Retval = true;
            data = null;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from SITE WHERE SIid='{0}'",
                (int)SIid);
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Site", "GetRecord", out _conn, out myDataReader))
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

            SecureString secureSmtpPass = new SecureString();
            char[] smtpPass = myDataReader["SmtpPassword"].ToString().ToCharArray();
            foreach (char c in smtpPass)
            {
                secureSmtpPass.AppendChar(c);
            }
            Array.Clear(smtpPass, 0, smtpPass.Length);

            data = new TableData(
				MainClass.ToInt(TableName, myDataReader["SIid"])
				, myDataReader["SiteId"].ToString()
				, myDataReader["SiteName"].ToString()
				, myDataReader["Address1"].ToString()
				, myDataReader["Address2"].ToString()
				, myDataReader["City"].ToString()
				, myDataReader["State"].ToString()
				, myDataReader["ZipCode"].ToString()
				, myDataReader["Country"].ToString()
				, myDataReader["Contact"].ToString()
				, myDataReader["Phone"].ToString()
				, myDataReader["DiagLine"].ToString()
				, myDataReader["Fax"].ToString()
				, myDataReader["Description"].ToString()
				, myDataReader["Notes"].ToString()
				, myDataReader["FacilityCode"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PreExpPw"])
				, myDataReader["ArchiveId"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PwExpDelta"])
				, MainClass.ToInt(TableName, myDataReader["UserIdExpireDelta"])
				, MainClass.ToInt(TableName, myDataReader["TempUserExpDays"])
				, MainClass.ToInt(TableName, myDataReader["PwLen"])
				, myDataReader["PtIdFormat"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ConLogOffTimeOut"])
				, MainClass.ToInt(TableName, myDataReader["RptSortByItemId"])
				, MainClass.ToInt(TableName, myDataReader["Voice"])
				, myDataReader["Version"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ArchiveYearFormat"])
				, myDataReader["DbMachineName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PrintStockOut"])
				, MainClass.ToInt(TableName, myDataReader["PrintCriticalLow"])
				, MainClass.ToInt(TableName, myDataReader["PrintADTconflict"])
				, MainClass.ToInt(TableName, myDataReader["PrintIncomingDiscrep"])
				, MainClass.ToInt(TableName, myDataReader["PrintCommDown"])
				, MainClass.ToInt(TableName, myDataReader["PermitGlobalPatientList"])
				, myDataReader["HomeFax"].ToString()
				, myDataReader["HomeEmail"].ToString()
				, MainClass.ToInt(TableName, myDataReader["ClientServerSystem"])
				, MainClass.ToInt(TableName, myDataReader["ORFeatures"])
				, MainClass.ToInt(TableName, myDataReader["DodWarning"])
                , MainClass.ToBool(TableName, myDataReader["CopyReports"])
				, MainClass.ToBool(TableName, myDataReader["EmailReports"])
				, MainClass.ToInt(TableName, myDataReader["UserDoorAccessPerItem"])
				, MainClass.ToInt(TableName, myDataReader["TechDoorAccessPerItem"])
				, MainClass.ToInt(TableName, myDataReader["Ticci"])
				, MainClass.ToInt(TableName, myDataReader["PwEnableComplex"])
				, myDataReader["PwComplexRegex"].ToString()
				, myDataReader["PwComplexDesc"].ToString()
				, MainClass.ToInt(TableName, myDataReader["PwMaxLen"])
                , MainClass.ToInt(TableName, myDataReader["PwHistory"])
				, MainClass.ToInt(TableName, myDataReader["PwHistoryClamp"])
				, MainClass.ToInt(TableName, myDataReader["ORISInterface"])
                , myDataReader["SmtpHost"].ToString()
                , MainClass.ToInt(TableName, myDataReader["SmtpPort"])
                , MainClass.ToBool(TableName, myDataReader["SmtpUseSSL"])
                , myDataReader["SmtpUsername"].ToString()
                , secureSmtpPass
                , MainClass.ToBool(TableName, myDataReader["pwEnableExtended"])
                , MainClass.ToBool(TableName, myDataReader["upgrade"])
                , myDataReader["siteGUID"].ToString()
                , MainClass.ToInt(TableName, myDataReader["allowGLCodeTransactions"])
                , MainClass.ToInt(TableName, myDataReader["AllowORISTempToTempCaseTransfer"])
                , MainClass.ToInt(TableName, myDataReader["allowAssociator"])
                , MainClass.ToInt(TableName, myDataReader["allowAutoCaseToPatientXfer"])
                , MainClass.ToInt(TableName, myDataReader["holdRFIDTxForCase"])
                , MainClass.ToInt(TableName, myDataReader["activeDirectoryOnly"])
                , MainClass.ToInt(TableName, myDataReader["logVerbosity"])
                , myDataReader["logMsgFormat"].ToString()
                , myDataReader["logServerIP"].ToString()
      
                , myDataReader["logServerPort"].ToString()
                , MainClass.ToInt(TableName, myDataReader["logEnableRemote"])
                , MainClass.ToInt(TableName, myDataReader["PwMinAge"])
                , MainClass.ToInt(TableName, myDataReader["PwMinChg"])
                );
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
        {
            bool Retval = false;
            NewIid = -1;
            string SqlStatement = "INSERT INTO SITE (SiteId, SiteName, Address1, Address2, City, State, ZipCode, Country, Contact, Phone, DiagLine, Fax, Description, Notes, FacilityCode, PreExpPw, ArchiveId, PwExpDelta, UserIdExpireDelta, TempUserExpDays, PwLen, PtIdFormat, ConLogOffTimeOut, RptSortByItemId, Voice, Version, ArchiveYearFormat, DbMachineName, PrintStockOut, PrintCriticalLow, PrintADTconflict, PrintIncomingDiscrep, PrintCommDown, PermitGlobalPatientList, HomeFax, HomeEmail, ClientServerSystem, ORFeatures, DodWarning, CopyReports, EmailReports, UserDoorAccessPerItem, TechDoorAccessPerItem, Ticci, PwEnableComplex, PwComplexRegex, PwComplexDesc, PwMaxLen, PwHistory, PwHistoryClamp, ORISInterface, SmtpHost, SmptPort, SmtpUserSSL, SmtpUserName, SmtpPassword, pwEnableExtended, upgrade, siteGUID, allowGLCodeTransactions, allowORISTempToTempCaseTransfer,allowAssociator, allowAutoCaseToPatientXfer, holdRFIDTxForCase, activeDirectoryOnly, logVerbosity, logMsgFormat, logServerIP, logServerPort, logEnableRemote, pwMinAge, pwMinChg) VALUES ("
                + "'" + MainClass.FixStringForSingleQuote(data.SiteId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SiteName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Address1)
                + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Address2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.City) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.State)
                + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ZipCode) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Country) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Contact)
                + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Phone) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.DiagLine) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Fax) 
                + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Description) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Notes) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.FacilityCode) 
                + "'" + ", " + (int)data.PreExpPw + ", " + "'" + MainClass.FixStringForSingleQuote(data.ArchiveId) + "'" + ", " + (int)data.PwExpDelta + ", " + (int)data.UserIdExpireDelta + ", " + (int)data.TempUserExpDays + ", "
                + (int)data.PwLen + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtIdFormat) + "'" + ", " + (int)data.ConLogOffTimeOut + ", " + (int)data.RptSortByItemId + ", " + (int)data.Voice + ", " + "'" + MainClass.FixStringForSingleQuote(data.Version) 
                + "'" + ", " + (int)data.ArchiveYearFormat + ", " + "'" + MainClass.FixStringForSingleQuote(data.DbMachineName) + "'" + ", " + (int)data.PrintStockOut + ", " + (int)data.PrintCriticalLow + ", " + (int)data.PrintADTconflict + ", " 
                + (int)data.PrintIncomingDiscrep + ", " + (int)data.PrintCommDown + ", " + (int)data.PermitGlobalPatientList + ", " + "'" + MainClass.FixStringForSingleQuote(data.HomeFax) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.HomeEmail) 
                + "'" + ", " + (int)data.ClientServerSystem + ", " + (int)data.ORFeatures + ", " + (int)data.DodWarning + ", " + (bool)data.CopyReports + ", " + (bool)data.EmailReports + ", " + (int)data.UserDoorAccessPerItem + ", " 
                + (int)data.TechDoorAccessPerItem + ", " + (int)data.Ticci + ", " + (int)data.PwEnableComplex + ", " + "'" + MainClass.FixStringForSingleQuote(data.PwComplexRegex) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PwComplexDesc) 
                + "'" + ", " + (int)data.PwMaxLen + ", " + (int)data.PwHistory + ", " + (int)data.PwHistoryClamp + ", " + (int)data.ORISInterface + ", " + "'" + MainClass.FixStringForSingleQuote(data.SmtpHost) + "'" + ", " + (int)data.SmtpPort
                + ", " + (bool)data.SmtpUseSSL + ", " + "'" + MainClass.FixStringForSingleQuote(data.SmtpUsername) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(ConvertToUnsecureString(data.SmtpPassword)) + "'"
                + ", " + (bool)data.pwEnableExtended + ", " + (bool)data.upgrade + ", " + "'" + MainClass.FixStringForSingleQuote(data.siteGUID) + "'" + ", " + (int)data.allowGLCodeTransactions
                + ", " + (int)data.AllowORISTempToTempCaseTransfer
                + ", " + (int)data.allowAssociator
                + ", " + (int)data.allowAutoCaseToPatientXfer
                + ", " + (int)data.HoldRFIDTxForCase
                + ", " + (int)data.ActiveDirectoryOnly
                + ", " + (int)data.logSeverity + ", " + MainClass.FixStringForSingleQuote(data.logMsgFormat) + ", " + MainClass.FixStringForSingleQuote(data.logServerIP) + ", " + MainClass.FixStringForSingleQuote(data.logServerPort)
                + ", " + (int)data.logEnableRemote
                + ", " + (int)data.PwMinAge + ", " + (int)data.PwMinChg
                + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Site", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int SIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE SITE WHERE SIid='{0}'", 
				(int)SIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Site", "DeleteRecord");
			return Retval;
		}

        public static string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                return(string.Empty);

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

 


	}
}
