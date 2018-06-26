using System;
using System.Collections.Generic;
using System.Text;

namespace BBPyxisDB
{
    public class HHCommRq
    {
        const string TableName = "HH_COMM_RQ";

        public class TableData
        {
            public string Destination;
            public string Source;
            public DateTime MessageTime;
            public string RqMessage;

            public TableData(string destination, string source,
                DateTime messageTime, string rq_message)
            {
                Destination = destination;
                Source = source;
                MessageTime = messageTime;
                RqMessage = rq_message;
            }
        }

        // insert record
        // OUTPUT: NewIid - IID of inserted record
        public static bool InsertRecord(TableData data, out int NewIid)
        {
            bool RetVal = false;
            NewIid = -1;

            string strSqlStatement = "INSERT INTO HH_COMM_RQ (destination, source, message_time, rq_message) VALUES ("
                + "'" + MainClass.FixStringForSingleQuote(data.Destination) + "', '"
                + MainClass.FixStringForSingleQuote(data.Source) + "', "
                + MainClass.DateTimeToTimestamp(data.MessageTime) + ", '"
                + MainClass.FixStringForSingleQuote(data.RqMessage) + "')";
            RetVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "HHCommRq", "InsertRec", out NewIid);

            return RetVal;
        }
    
    
    }
}
