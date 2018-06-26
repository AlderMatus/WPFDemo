using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{
    public class ReportTemp
    {
        const string TableName = "REPORT_TEMP";

        public class TableData
        {
            public int TempIid;
            public int ReportIid;
            public string ReportText1;
            public string ReportText2;
            public string ReportText3;
            public string ReportText4;
            public string ReportText5;
            public string ReportText6;
            public string ReportText7;
            public string ReportText8;
            public string ReportText9;

            public TableData(int tempIid, int reportIid, string reportText1, string reportText2, string reportText3, string reportText4, string reportText5, string reportText6, string reportText7, string reportText8, string reportText9)
            {
                TempIid = tempIid;
                ReportIid = reportIid;
                ReportText1 = reportText1;
                ReportText2 = reportText2;
                ReportText3 = reportText3;
                ReportText4 = reportText4;
                ReportText5 = reportText5;
                ReportText6 = reportText6;
                ReportText7 = reportText7;
                ReportText8 = reportText8;
                ReportText9 = reportText9;
            }

            public TableData()
            {
                TempIid = -1;
                ReportIid = -1;
                ReportText1 = string.Empty;
                ReportText2 = string.Empty;
                ReportText3 = string.Empty;
                ReportText4 = string.Empty;
                ReportText5 = string.Empty;
                ReportText6 = string.Empty;
                ReportText7 = string.Empty;
                ReportText8 = string.Empty;
                ReportText9 = string.Empty;
            
            }
        }
        public static bool GetRecord(int tempIid, out TableData data)
        {
            bool Retval = true;
            data = null;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from REPORT_TEMP WHERE TempIid='{0}'", tempIid);
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ReportTemp", "GetRecord", out _conn, out myDataReader))
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

#if !NO_ASA
        static void MakeDataRec(SADataReader myDataReader, out TableData data)
        {
            data = new TableData(
                MainClass.ToInt(TableName, myDataReader["tempIid"])
                , MainClass.ToInt(TableName, myDataReader["reportIid"])
                , myDataReader["reportText1"].ToString()
                , myDataReader["reportText2"].ToString()
                , myDataReader["reportText3"].ToString()
                , myDataReader["reportText4"].ToString()
                , myDataReader["reportText5"].ToString()
                , myDataReader["reportText6"].ToString()
                , myDataReader["reportText7"].ToString()
                , myDataReader["reportText8"].ToString()
                , myDataReader["reportText9"].ToString());
        }
#endif

        public static bool InsertRecord(TableData data)
        {
            bool result;
            int newId;
            string sqlStmt = "INSERT INTO REPORT_TEMP(reportIid, reportText1, reportText2, reportText3, reportText4, reportText5, reportText6, reportText7, reportText8, reportText9) VALUES ("
                                + data.ReportIid + ", '" + data.ReportText1 + "', '" + data.ReportText2 + "', '" + data.ReportText3 + "', '" + data.ReportText4 + "', '" + data.ReportText5 + "', '" + data.ReportText6 + "', '" + data.ReportText7 + "', '" + data.ReportText8 + "', '" + data.ReportText9 + "')";

            result = MainClass.ExecuteSql(sqlStmt, true, TableName, "ReportTemp", "InsertRecord", out newId);
            return result;
        }

        public static bool DeleteReport(int reportIid)
        {
            bool result;
            string sqlStmt = string.Format("Delete from REPORT_TEMP WHERE reportIid = {0}", reportIid);
            result = MainClass.ExecuteSql(sqlStmt, true, TableName, "ReportTemp", "DeleteReport");
            return result;
        }

        public static SADataReader GetReportData(string sqlStmt, out SAConnection _conn)
        {
            SADataReader myDataReader;
            if (MainClass.ExecuteSelect(sqlStmt, true, TableName, "ReportTemp", "GetReportData", out _conn, out myDataReader))
            {
                return myDataReader;
            }
            return null;
        }
    }
}
