using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

    public class DiscardReason
    {
        const string TableName = "DISCARD_REASONS";

        // collection of record fields
        public class TableData
        {
            public int ReasonIid;
            public string ReasonID;
            public string ReasonText;

            public TableData(int _ReasonIid, string _ReasonID, string _ReasonText)
            {
                this.ReasonIid = _ReasonIid;
                this.ReasonID = _ReasonID;
                this.ReasonText = _ReasonText;
            }
            public override string ToString()
            {
                if (this.ReasonIid == 1)
                {
                    return "[" + this.ReasonText + "]";
                }
                else
                {
                    return this.ReasonText;
                }
            }
        }

        // return record given its primary key
        public static bool GetRecord(int _ReasonIid, out TableData data)
        {
            bool Retval = true;
            data = null;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * FROM DISCARD_REASONS WHERE _ReasonIid='{0}'", _ReasonIid);
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "DiscardReason", "GetRecord", out _conn, out myDataReader))
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
                MainClass.ToInt(TableName, myDataReader["ReasonIid"])
                , myDataReader["ReasonId"].ToString()
                , myDataReader["ReasonText"].ToString());
        }
#endif

        // insert record and return its primary key
        public static bool InsertRecord(TableData data, out int NewIid)
        {
            bool Retval = false;
            NewIid = -1;

            string SqlStatement = "INSERT INTO DISCARD_REASONS (ReasonId, ReasonText) VALUES ("
                + "'" + MainClass.FixStringForSingleQuote(data.ReasonID) + "'"
                + ", '" + MainClass.FixStringForSingleQuote(data.ReasonText) + "'" + ")";
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "DiscardReason", "InsertRecord", out NewIid);
            return Retval;
        }

        // update discard reason record
        public static bool UpdateRecord(TableData data)
        {
            bool Retval = false;
            string SqlStatement = "UPDATE DISCARD_REASONS set ReasonId='"
                + MainClass.FixStringForSingleQuote(data.ReasonID) + "', ReasonText='"
                + MainClass.FixStringForSingleQuote(data.ReasonText) + "' WHERE ReasonIid = " + data.ReasonIid;
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "DiscardReason", "UpdateRecord");
            return Retval;
        }

        // delete record given its primary key
        public static bool DeleteRecord(int _ReasonIid)
        {
            bool Retval = true;
            string SqlStatement = string.Format("DELETE DISCARD_REASONS WHERE ReasonIid={0}", _ReasonIid);
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "DiscardReason", "DeleteRecord");
            return Retval;
        }

        public static bool GetSortedAllDiscardReasons(List<TableData> list)
        {
            bool Retval = true;
            list.Clear();
#if !NO_ASA
            TableData data;
            SAConnection _conn;
            SADataReader myDataReader;

            string SqlStatement = "SELECT ReasonIid, ReasonId, ReasonText FROM DISCARD_REASONS ORDER BY ReasonText";

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "DiscardReason", "GetAllDiscardReasons", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        MakeDataRec(myDataReader, out data);
                        if (data.ReasonIid == 1)
                        {
                            list.Insert(0, data);
                        }
                        else
                        {
                            list.Add(data);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "DiscardReason", "GetSortedAllDiscardReasons", err);
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
