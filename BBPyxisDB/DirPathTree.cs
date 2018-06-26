using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{
    public class DirPathTree
    {
        const string TableName = "DIR_PATH_TREE";

        public struct TableData
        {
            public int PathId;
            public string Directory;    // dirDescription
            public string Path;         // dirPath
            public bool IsBatch;
            public int ParentId;
            public bool Purge;
            public bool IsNetwork;      // isNetworkDrive

            public TableData(int pathId, string directory, string path,
                bool isBatch, int parentId, bool purge, bool isNetwork)
            {
                PathId = pathId;
                Directory = directory;
                Path = path;
                IsBatch = isBatch;
                ParentId = parentId;
                Purge = purge;
                IsNetwork = isNetwork;
            }
        }

        // contains batch report data from this table
        public class DirTreeSet
        {
            public string DirDescription;   // "folder" name
            public string DirPath;  // Directory Path 
            public List<DirTreeSet> ChildDir;   // child "folders"
            public int PathId, IsNetwork;
            public DirTreeSet(string dirDescription, int pathId, int isNetwork, string dirPath)
            {
                DirDescription = dirDescription;
                DirPath = dirPath;
                PathId = pathId;
                IsNetwork = isNetwork;
                ChildDir = new List<DirTreeSet>();
            }
        }

        // get a record
        // integer vals are returned as -1 if they're null
        public static bool GetRecord(int PathId, out TableData data)
        {
            bool Retval = true;
            data = new TableData();

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from DIR_PATH_TREE WHERE pathId=" + PathId;
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "DirPathTree", "GetRecord", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                        data = new TableData(
                        PathId,
                        myDataReader["dirDescription"].ToString(),
                        myDataReader["dirPath"].ToString(),
                        MainClass.ToBool(TableName, myDataReader["isBatch"]),
                        MainClass.ToInt(TableName, myDataReader["parentId"]),
                        MainClass.ToBool(TableName, myDataReader["purge"]),
                        MainClass.ToBool(TableName, myDataReader["isNetworkDrive"]));
                    }
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

        // get records that are not batch
        public static bool GetNonbatchRecords(out List<TableData> list)
        {
            bool Retval = true;
            list = new List<TableData>();
            TableData data;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from DIR_PATH_TREE WHERE isBatch=0";
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "DirPathTree", "GetRecord", out _conn, out myDataReader))
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

        // make a TableData object from a SADataReader object
#if !NO_ASA
        static void MakeDataRec(SADataReader myDataReader, out TableData data)
        {
            data = new TableData(
                MainClass.ToInt(TableName, myDataReader["pathId"]),
                myDataReader["dirDescription"].ToString(),
                myDataReader["dirPath"].ToString(),
                MainClass.ToBool(TableName, myDataReader["isBatch"]),
                MainClass.ToInt(TableName, myDataReader["parentId"]),
                MainClass.ToBool(TableName, myDataReader["purge"]),
                MainClass.ToBool(TableName, myDataReader["isNetworkDrive"]));
        }
#endif
        // 
        public static bool IsDuplicateDescription(string dirDescription, bool isEdit, int pathId, out bool descExists)
        {
           bool Retval = true;
           descExists = false;
            
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement;
            if(!isEdit)
                SqlStatement= "SELECT * from DIR_PATH_TREE WHERE dirDescription=" + "'" + dirDescription + "'" + "AND isBatch = 0";
            else
                SqlStatement = "SELECT * from DIR_PATH_TREE WHERE dirDescription=" + "'" + dirDescription + "'" + "AND isBatch = 0 AND pathId != " + pathId;

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "DirPathTree", "IsDuplicateDescription", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                        descExists = true;
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "IsDuplicateDescription", err);
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

        public static bool IsDuplicatePath(string dirPath, bool isEdit, int pathId, out bool pathExists)
        {
            bool Retval = true;
            pathExists = false;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement;
            if (!isEdit)
                SqlStatement = "SELECT * from DIR_PATH_TREE WHERE dirpath=" + "'" + dirPath + "'" + "AND isBatch = 0";
            else
                SqlStatement = "SELECT * from DIR_PATH_TREE WHERE dirpath=" + "'" + dirPath + "'" + "AND isBatch = 0 AND pathId != " + pathId;

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "DirPathTree", "IsDuplicatePath", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                        pathExists = true;
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "IsDuplicatePath", err);
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






        // get data from this table
        public static bool GetBatchReportTree(out List<DirTreeSet> TreeSet, int isBatch)
        {
            bool Retval = true;
            TreeSet = new List<DirTreeSet>();
            Dictionary<int, List<DirTreeSet>> dic = new Dictionary<int, List<DirTreeSet>>();
#if !NO_ASA
            string DirDescription, DirPath;
            int PathId, ParentId, IsNetwork;
            List<DirTreeSet> DirTreeList;
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from DIR_PATH_TREE WHERE isBatch= " + isBatch;
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "DirPathTree", "GetBatchReportTree", out _conn, out myDataReader))
            {
                try
                {
                    // build map to group nodes by parentId
                    while (myDataReader.Read())
                    {
                        DirDescription = myDataReader["dirDescription"].ToString();
                        DirPath = myDataReader["dirPath"].ToString();
                        PathId = MainClass.ToInt(TableName, myDataReader["pathId"]);
                        ParentId = MainClass.ToInt(TableName, myDataReader["parentId"]);
                        IsNetwork = MainClass.ToInt(TableName, myDataReader["isNetworkDrive"]);
                        if (ParentId == 0)
                            TreeSet.Add(new DirTreeSet(DirDescription, PathId, IsNetwork, DirPath));
                        else if (dic.TryGetValue(ParentId, out DirTreeList))
                            DirTreeList.Add(new DirTreeSet(DirDescription, PathId, IsNetwork, DirPath));
                        else
                        {
                            DirTreeList = new List<DirTreeSet>();
                            DirTreeList.Add(new DirTreeSet(DirDescription, PathId, IsNetwork, DirPath));
                            dic.Add(ParentId, DirTreeList);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "DirPathTree", "GetBatchReportTree", err);
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

                // build tree
                foreach (DirTreeSet dts in TreeSet)
                {
                    AddNode(dic, dts);
                }
            }
#endif
			return Retval;
        }

        // recursively add nodes to the tree
        static void AddNode(Dictionary<int, List<DirTreeSet>> dic, DirTreeSet ParentDts)
        {
            List<DirTreeSet> DirTreeList;
            if (dic.TryGetValue(ParentDts.PathId, out DirTreeList))
            {
                foreach (DirTreeSet dts in DirTreeList)
                {
                    ParentDts.ChildDir.Add(dts);
                    AddNode(dic, dts);
                }
            }
        }

        // Update directory description
        public static bool UpdateDirDesc(int pathId, string newDesc)
        {
            bool retVal = false;
            newDesc = MainClass.FixStringForSingleQuote(newDesc);

            string strSqlStatement = "UPDATE DIR_PATH_TREE SET dirDescription = " + "'" + newDesc + "'" + " WHERE pathId = " + pathId;
            retVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "DirPathTree", "UpdateDirDesc");

            return retVal;
        }



        // insert record
        // -1 for an int or enum arg means that DB field should be NULL
        // OUTPUT: NewPathId - pathId of inserted record
        public static bool InsertRecord(TableData data, out int NewPathId)
        {
            bool RetVal = false;
            string FieldString = "";
            string ValueString = "";
            NewPathId = -1;

            if (data.PathId != -1)
            {
                FieldString += "pathId, ";
                ValueString += "'" + data.PathId + "', ";
            }
            
            FieldString += "dirDescription, ";
            ValueString += "'" + data.Directory+ "', ";
            FieldString += "dirPath, ";
            ValueString += "'" + data.Path + "', ";
            FieldString += "isBatch, ";
            ValueString += "'" + MainClass.BoolToInt(data.IsBatch) + "', ";
            
            if (data.ParentId != -1)
            {
                FieldString += "parentId, ";
                ValueString += "'" + data.ParentId + "', ";
            }
            FieldString += "purge, ";
            ValueString += "'" + MainClass.BoolToInt(data.Purge) + "', ";
            FieldString += "isNetworkDrive";
            ValueString += "'" + MainClass.BoolToInt(data.IsNetwork) + "'";
 


            string strSqlStatement = "INSERT INTO DIR_PATH_TREE (" + FieldString + ") VALUES (" + ValueString + ")";
            RetVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "DirPathTree", "InsertRec", out NewPathId);

            return RetVal;
        }

        // Delete record
        public static bool DeleteRecord(int pathId)
        {
            bool Retval = true;
            string sql = "DELETE DIR_PATH_TREE WHERE pathId = " + pathId;
            Retval = MainClass.ExecuteSql(sql, true, TableName, "DirPathTree", "DeleteRecord");

            return Retval;
        }

        // Update parent
        public static bool UpdateParentId(int oldParentId, int newParentId)
        {
            bool retVal = true;
            string sql = "UPDATE DIR_PATH_TREE SET parentId = " + newParentId + " WHERE parentId = " + oldParentId;
            retVal = MainClass.ExecuteSql(sql, true, TableName, "DirPathTree", "UpdateParentId");
            return retVal;
        }

        // get records that are not batch
        public static bool GetRecordsToPurge(out List<TableData> list)
        {
            bool Retval = true;
            list = new List<TableData>();
            TableData data;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from DIR_PATH_TREE WHERE isBatch=0 AND purge=1";
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "DirPathTree", "GetRecord", out _conn, out myDataReader))
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
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "GetRecordsToPurge", err);
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
