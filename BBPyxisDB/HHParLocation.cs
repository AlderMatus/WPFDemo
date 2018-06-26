using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{
	public class HHParLocation
	{
		const string TableName = "HH_PAR_LOCATION";
		
		public class TableData
		{
			public int ParLocationIid;
			public string ParLocationName;
			public int AreaIid;
			public string AreaName;
			public string CostCenter;
			public string StnCareArea;
			public string Zone;

			public TableData(int parLocationIid, string parLocationName, int areaIid,
				string areaName, string costCenter, string stnCareArea, string zone)
			{
				ParLocationIid = parLocationIid;
				ParLocationName = parLocationName;
				AreaIid = areaIid;
				AreaName = areaName;
				CostCenter = costCenter;
				StnCareArea = stnCareArea;
				Zone = zone;
			}
		}

		// get data from HH_PAR_LOCATION table that has specified Iid
		// integer vals are returned as -1 if they're null
        // return false if error or no record found
		public static bool GetRecord(int ParLocationIid, out TableData data)
        {
            bool Retval = false;
			data = null;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = "SELECT * from HH_PAR_LOCATION JOIN AREAS ON HH_PAR_LOCATION.areaIid=AREAS.areaIid WHERE parLocationIid=" + ParLocationIid;
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "HHParLocation", "GetRecord", out _conn, out myDataReader))
            {
                try
                {
                    if (myDataReader.Read())
                    {
                        Retval = true;
                        data = new TableData(
                            MainClass.ToInt(TableName, myDataReader["parLocationIid"]),
                            myDataReader["parLocationName"].ToString(),
                            MainClass.ToInt(TableName, myDataReader["areaIid"]),
                            myDataReader["areaName"].ToString(),
                            myDataReader["costCenter"].ToString(),
                            myDataReader["stnCareArea"].ToString(),
                            myDataReader["zone"].ToString());

                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "HHParLocation", "GetRecord", err);
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

		// get all records from HH_PAR_LOCATION table
		// integer vals are returned as -1 if they're null
		public static bool GetRecs(List<TableData> list)
		{
			bool Retval = true;
			list.Clear();
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            TableData rec;
            string SqlStatement = "SELECT * from HH_PAR_LOCATION JOIN AREAS ON HH_PAR_LOCATION.areaIid=AREAS.areaIid";
            SqlStatement += " ORDER BY areaName, parLocationName";
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "HHParLocation", "GetRecs", out _conn, out myDataReader))
            {
                try
                {
                    while (myDataReader.Read())
                    {
                        rec = new TableData(
                            MainClass.ToInt(TableName, myDataReader["parLocationIid"]),
                            myDataReader["parLocationName"].ToString(),
                            MainClass.ToInt(TableName, myDataReader["areaIid"]),
                            myDataReader["areaName"].ToString(),
                            myDataReader["costCenter"].ToString(),
                            myDataReader["stnCareArea"].ToString(),
                            myDataReader["zone"].ToString());
                        list.Add(rec);
                    }
                }
                catch (Exception ex)
                {
                    Retval = false;
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                        TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, "HHParLocation", "GetRecs", err);
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
