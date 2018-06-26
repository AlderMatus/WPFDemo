using System;
using System.Collections.Generic;
using System.Text;

namespace BBPyxisDB
{
    public class ServiceLog
    {
        const string TableName = "service_log";

        // write debug message to service_log table
        public static bool InsertRecord(string application, string classname, string function, string LogMsg)
        {
            //We do not wnat any exceptions to bog us down here, there is really nothing we can do other than retrurn false
            try
            {
                bool RetVal = false;
                string strMessageTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                string strSqlStatement = string.Format("INSERT INTO service_log (message_time, processBox, application, class, function, zmessage) VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                    strMessageTime,
                    MainClass.FixStringForSingleQuote(System.Environment.MachineName),
                    application,
                    classname,
                    function,
                    MainClass.FixStringForSingleQuote(LogMsg));
                RetVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "ServiceLog", "InsertRec");

                return RetVal;
            }
            catch (Exception)
            {
            }

            return false;
        }


    }
}
