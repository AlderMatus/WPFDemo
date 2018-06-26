using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{
	public class HHPocket
	{
        const string TableName = "hh_pocket";

        public class TableData
        {
            public int PktIid;
            public int ParLocationIid;
            public string StorageUnitName;
            public string SubUnitName; 
            public int SubUnitIid; 
            public int BinNumber; 
            public int ItemIid; 
            public int PktPhysMaxQty;
			public int PktParQty; 
            public int PktRefillPoint;
            public int PktCurQty;
            public int PktCritLow;
            public string PocketUnitOfIssue;
			public int PocketUOIRatio; 
            public int IncludeInDOP;
            public DateTime LastRefill;
            public DateTime LastStockOut;
            public DateTime LastInventory;

            public TableData(int pktIid, int parLocationIid, string storageUnitName,
                string subUnitName, int subUnitIid, int binNumber, int itemIid, int pktPhysMaxQty,
                int pktParQty, int pktRefillPoint, int pktCurQty, int pktCritLow,
                string pocketUnitOfIssue,
                int pocketUOIRatio, int includeInDOP, DateTime lastRefill, DateTime lastStockOut,
                DateTime lastInventory)
            {
				PktIid = pktIid;
				ParLocationIid = parLocationIid;
				StorageUnitName = storageUnitName;
				SubUnitName = subUnitName;
				SubUnitIid = subUnitIid;
				BinNumber = binNumber;
				ItemIid = itemIid;
				PktPhysMaxQty = pktPhysMaxQty;
				PktParQty = pktParQty;
				PktRefillPoint = pktRefillPoint;
				PktCurQty = pktCurQty;
                PktCritLow = pktCritLow;
				PocketUnitOfIssue = pocketUnitOfIssue;
				PocketUOIRatio = pocketUOIRatio;
				IncludeInDOP = includeInDOP;
				LastRefill = lastRefill;
				LastStockOut = lastStockOut;
				LastInventory = lastInventory;
            }
        }

		// insert a record
		public static bool InsertRecord(TableData data)
		{
			bool RetVal = false;
            string strSqlStatement = string.Format("INSERT INTO hh_pocket (parLocationIid, storageUnitName, subUnitName,  subUnitIid,  binNumber,  itemIid,  pktPhysMaxQty, pktParQty, pktRefillPoint, pktCurQty, pktCritLow, pocketUnitOfIssue, pocketUOIRatio, includeInDOP, lastRefill, lastStockOut, lastInventory) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'), '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}'), '{14}', '{15}', '{16}')",
				data.ParLocationIid, 
                MainClass.FixStringForSingleQuote(data.StorageUnitName),
                MainClass.FixStringForSingleQuote(data.SubUnitName), 
                data.SubUnitIid, 
                data.BinNumber, 
                data.ItemIid, 
                data.PktPhysMaxQty, 
                data.PktParQty, 
                data.PktRefillPoint, 
                data.PktCurQty, 
                data.PktCritLow, 
                MainClass.FixStringForSingleQuote(data.PocketUnitOfIssue), 
                data.PocketUOIRatio, 
                data.IncludeInDOP, 
                data.LastRefill, 
                data.LastStockOut, 
                data.LastInventory);
            RetVal = MainClass.ExecuteSql(strSqlStatement, true, TableName, "HHPocket", "InsertRec");

			return RetVal;
		}

		// get records to be auto refilled
        // return true if no error, even if no data returned
        public static bool GetLowBins(int ParLocationIid,
            HHAutoRefill.WhenRefillEnum WhenRefill,
            HHAutoRefill.ItemsToRefillEnum ItemsToRefill, List<TableData> list)
        {
            bool Retval = true;
            list.Clear();
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string where;
            string SqlStatement = "SELECT * from HH_POCKET as pocket JOIN ITEMS as item ON pocket.itemIid=item.itemIid";
            where = " WHERE parLocationIid = " + ParLocationIid;
            if (ItemsToRefill == HHAutoRefill.ItemsToRefillEnum.StockItemsOnly)
                where += " AND itemStock = 1";
            else if (ItemsToRefill == HHAutoRefill.ItemsToRefillEnum.NonstockItemsOnly)
                where += " AND itemStock = 0";
            if (WhenRefill == HHAutoRefill.WhenRefillEnum.CurLessPar)
                where += " AND pktCurQty < pktParQty";
            else if (WhenRefill == HHAutoRefill.WhenRefillEnum.CurLessRefillPt)
                where += " AND pktCurQty < pktRefillPoint";
            else if (WhenRefill == HHAutoRefill.WhenRefillEnum.CurLessCritlow)
                where += " AND pktCurQty < pktCritLow";
            else
                Retval = false;
            if (Retval)
            {
                SqlStatement += where;
                if (MainClass.ExecuteSelect(SqlStatement, true, "hh_pocket join items", "HHPocket", "GetLowBins", out _conn, out myDataReader))
                {
                    try
                    {
                        while (myDataReader.Read())
                        {
                            list.Add(new TableData(
                                MainClass.ToInt(TableName, myDataReader["pktIid"]),
                                ParLocationIid,
                                myDataReader["storageUnitName"].ToString(),
                                myDataReader["subUnitName"].ToString(),
                                MainClass.ToInt(TableName, myDataReader["subUnitIid"]),
                                MainClass.ToInt(TableName, myDataReader["binNumber"]),
                                MainClass.ToInt(TableName, myDataReader["itemIid"]),
                                MainClass.ToInt(TableName, myDataReader["pktPhysMaxQty"]),
                                MainClass.ToInt(TableName, myDataReader["pktParQty"]),
                                MainClass.ToInt(TableName, myDataReader["pktRefillPoint"]),
                                MainClass.ToInt(TableName, myDataReader["pktCurQty"]),
                                MainClass.ToInt(TableName, myDataReader["pktCritLow"]),
                                myDataReader["pocketUnitOfIssue"].ToString(),
                                MainClass.ToInt(TableName, myDataReader["pocketUOIRatio"]),
                                MainClass.ToInt(TableName, myDataReader["includeInDOP"]),
                                MainClass.ToDate(TableName, myDataReader["lastRefill"]),
                                MainClass.ToDate(TableName, myDataReader["lastStockOut"]),
                                MainClass.ToDate(TableName, myDataReader["lastInventory"])));
                        }
                    }
                    catch (Exception ex)
                    {
                        Retval = false;
                        string err = String.Format(MainClass.StringTable.GetString("DatabaseError"),
                            TableName, ex.Message.ToString() + "(" + SqlStatement + ")");
                        ServiceMessages.InsertRec(MainClass.AppName, "HHPocket", "GetLowBins", err);
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
            }
#endif

            return Retval;
        }

        // update cur field of specified record
		public static bool UpdateCur(int pktIid, int cur)
		{
			bool Retval = true;
			string sql = "UPDATE HH_POCKET SET pktCurQty = " + cur + " WHERE pktIid = " + pktIid;
            Retval = MainClass.ExecuteSql(sql, true, TableName, "HHPocket", "SetCurToPar");

			return Retval;
		}



	}
}
