using Sap.Data.SQLAnywhere;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBPyxisDB
{
    public class UserDomains
    {
        const string TableName = "USER_DOMAINS";
        const string ModuleName = "UserDomains";

        public class TableData
        {
            public int domainIid;
            public string domainName;
            public string systemAccountName;
            public string encryptedBlock;
            public string creatorId;
            public bool isDefault;

            public TableData(string domainName, int domainIid, string systemAccountName, string encryptedBlock, string creatorID, bool isDefault)
            {
                this.domainName = domainName;
                this.domainIid = domainIid;
                this.systemAccountName = systemAccountName;
                this.encryptedBlock = encryptedBlock;
                this.creatorId = creatorID;
                this.isDefault = isDefault;
            }
        }
        public static bool GetRecord(int DomainIid, out TableData data)
        {
            bool Retval = true;
            data = null;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
                
            string SqlStatement = string.Format("SELECT * from {0} WHERE domainIid ='{1}'", TableName, (int)DomainIid);

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, ModuleName, "GetRecord", out _conn, out myDataReader))
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

        public static bool GetAllDomains (List<TableData> list, string search = "")
        {
            bool Retval = true;
            list.Clear();
#if !NO_ASA
            TableData data;
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement;
            if (string.IsNullOrEmpty(search))
                SqlStatement = "SELECT DomainName, DomainIid, SystemAccountName, DataBlock, CreatorID, IsDefault FROM USER_DOMAINS ORDER BY DomainName";
            else
                SqlStatement = string.Format("SELECT DomainName, DomainIid, SystemAccountName, DataBlock, CreatorID, IsDefault FROM USER_DOMAINS WHERE DomainName = '{0}'", search);

                if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "ModuleName", "GetAllDomains", out _conn, out myDataReader))
                {
                    try
                    {
                        while (myDataReader.Read())
                        {
                            MakeDataRec(myDataReader, out data);
                            list.Add(data);
                        }
                    }
                    catch (Exception ex)
                    {
                        Retval = false;
                        string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                            TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                        ServiceMessages.InsertRec(MainClass.AppName, "ModuleName", "GetAllDomains", err);
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

        static void MakeDataRec(SADataReader reader,out TableData data)
        {
            data = new TableData(
                reader["DomainName"].ToString(),
                MainClass.ToInt(TableName, reader["DomainIid"]), 
                reader["SystemAccountName"].ToString(),
                reader["DataBlock"].ToString(),
                reader["CreatorID"].ToString(),
                MainClass.ToBool(TableName, reader["IsDefault"]));
        }

        public static bool InsertRecord(TableData data, out int newId)
        {
            bool retVal = false;

            string SqlStatement = "INSERT INTO USER_DOMAINS (DomainName, SystemAccountName, DataBlock, CreatorID, IsDefault) VALUES ("
            + "'" + MainClass.FixStringForSingleQuote(data.domainName) + "', " + "'" + MainClass.FixStringForSingleQuote(data.systemAccountName)  + "', " 
            + "'" + MainClass.FixStringForSingleQuote(data.encryptedBlock) + "', " + "'" + MainClass.FixStringForSingleQuote(data.creatorId) + "', "
            + MainClass.BoolToInt(data.isDefault) + ")";
                
            retVal = MainClass.ExecuteSql(SqlStatement, true, TableName, ModuleName, "InsertRecord", out newId);
            return retVal;
        }

        public static bool UpdateDomain(TableData data, int domainIid)
        {
            bool retVal = false;

            string SqlStatement = string.Format("Update USER_DOMAINS Set DomainName = ?, SystemAccountName = ?, DataBlock = ?, CreatorID = ?, IsDefault = ? where DomainIid = {0}", domainIid);
            retVal = MainClass.ExecuteSql(SqlStatement, false,
                        new SAParameter("", MainClass.FixStringForSingleQuote(data.domainName)),
                        new SAParameter("", MainClass.FixStringForSingleQuote(data.systemAccountName)),
                        new SAParameter("", MainClass.FixStringForSingleQuote(data.encryptedBlock)),
                        new SAParameter("", MainClass.FixStringForSingleQuote(data.creatorId)),
                        new SAParameter("", MainClass.BoolToInt(data.isDefault)));
                
            return retVal;
        }

        public static bool UpdateDefaultDomain(int domainIid, bool defaultFlag)
        {
            bool retVal = false;

            string SqlStatement = "Update USER_DOMAINS Set IsDefault = " + MainClass.BoolToInt(defaultFlag) + "where DomainIid = " + domainIid;

            retVal = MainClass.ExecuteSql(SqlStatement, true, TableName, ModuleName, "UpdateDefault");
            return retVal;
        }

        public static bool DeleteDomain(int domainIid)
        {
            bool retVal = false;

            string SqlStatement = "delete USER_DOMAINS where DomainIid = " + domainIid;

            retVal = MainClass.ExecuteSql(SqlStatement, true, TableName, ModuleName, "DeleteDomain");
            return retVal;
        
        }

        public static bool GetRecordByName(String domainName, out TableData data)
        {
            {
                bool Retval = true;
                data = null;
#if !NO_ASA
                SAConnection _conn;
                SADataReader myDataReader;

                string SqlStatement = string.Format("SELECT * from {0} WHERE domainName ='{1}'", TableName, domainName);

                if (MainClass.ExecuteSelect(SqlStatement, true, TableName, ModuleName, "GetRecord", out _conn, out myDataReader))
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
        }
    }
}
