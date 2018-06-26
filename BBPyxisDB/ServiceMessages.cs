using System;
using System.Collections.Generic;
using System.Text;
using Sap.Data.SQLAnywhere;

namespace BBPyxisDB
{
    public class ServiceMessages
    {
        const string TableName = "Service_Messages";

        // write error to service_messages table
        public static bool InsertRec(string CallingModule, string CallingClass, string CallingFunction, string LogMsg)
        {
            string sql = "INSERT INTO service_messages (source, message_time, errClass, srvMessage, threadId, lineNum, smSeverity, errId, errNative, ComputerName, moduleName) VALUES ( ?, ?, ?, ?, ?, 0, -1, -1, -1, ?, ?)";
            return MainClass.ExecuteSql(sql, false, 
                new SAParameter("", CallingModule),
                new SAParameter("", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")),
                new SAParameter("", 0x000E),    // same as PyxisDB ServiceMessagesSet.h
                new SAParameter("", LogMsg),
                new SAParameter("", System.Threading.Thread.CurrentThread.GetHashCode()),
                new SAParameter("", System.Environment.MachineName),
                new SAParameter("", CallingClass + "." + CallingFunction)
            );
        }
    }
}
