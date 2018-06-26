using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
using Carefusion.Supply.CSharpResource;
using System.Resources;
#endif

namespace BBPyxisDB
{
    public enum PickTicketSearchCategory
    {
        ItemName,
        ItemID
    }


	public class PickTicket
	{
		const string TableName = "PICK_TICKET";

        private static readonly ResourceManager StringTable = SupplyResource.GetResource();

		// collection of record fields
		public class TableData
		{
			public int PickTicketIid;
			public string OrderNum;
			public string PatientId;
			public string CaseId;
			public string ProcedureCode;
			public string ItemId;
			public string ItemName;
			public int TrxQty;
			public string ItemUnitOfIssue;
			public decimal CostPerIssue;
			public DateTime TrxTime;
            public int FilledQty;

			public TableData(int PickTicketIid, string OrderNum, string PatientId, string CaseId, string ProcedureCode, string ItemId, string ItemName, int TrxQty, string ItemUnitOfIssue, decimal CostPerIssue, DateTime TrxTime, int FilledQty)
			{
				this.PickTicketIid = PickTicketIid;
				this.OrderNum = OrderNum;
				this.PatientId = PatientId;
				this.CaseId = CaseId;
				this.ProcedureCode = ProcedureCode;
				this.ItemId = ItemId;
				this.ItemName = ItemName;
				this.TrxQty = TrxQty;
				this.ItemUnitOfIssue = ItemUnitOfIssue;
				this.CostPerIssue = CostPerIssue;
				this.TrxTime = TrxTime;
                this.FilledQty = FilledQty;
			}
		}


		// return record given its primary key
		public static bool GetRecord(int PickTicketIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PICK_TICKET WHERE PickTicketIid='{0}'", 
				(int)PickTicketIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PickTicket", "GetRecord", out _conn, out myDataReader))
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

        public static bool GetPickTicketList(int caseIid, out List<TableData> list, string procedureCode, bool wordSearch, string searchText, PickTicketSearchCategory category)
        {
            bool Retval = true;
            list = new List<TableData>();
            TableData data;
            string appendStmt= string.Empty;
#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string baseSqlStmt = string.Format("select * from PICK_TICKET t  inner join proc_case c on t.caseid = c.caseid where  c.caseiid ={0}", caseIid);
            
            if (!string.IsNullOrEmpty(procedureCode))
                appendStmt += string.Format(" and ProcedureCode = '{0}' ", procedureCode);
            
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (category == PickTicketSearchCategory.ItemID)
                    appendStmt += string.Format(" and ItemID like '{1}{0}%' ", searchText, wordSearch?"%":string.Empty);
                else if (category == PickTicketSearchCategory.ItemName)
                    appendStmt += string.Format(" and ItemName like '{1}{0}%' ", searchText, wordSearch?"%":string.Empty);
            }

            string sqlStatement = baseSqlStmt + appendStmt;

           // string SqlStatement = string.Format("select pickticketiid, c.caseid, procedurecode, orderNum, itemid, itemname, trxQty, patientid, P.ptlastname, P.ptFirstName from PICK_TICKET t  inner join proc_case c on t.caseid = c.caseid full outer join patients P on t.patientId = P.ptId where  c.caseiid ={0}", caseIid);


            //string SqlStatement = string.Format("select pickticketiid, caseid, procedurecode, itemid, itemname, orderNum, patientid, ptlastname, ptFirstName from PICK_TICKET t full outer join patients P on t.patientId = P.ptId where CaseID='{0}'",
            //    caseId);
            if (MainClass.ExecuteSelect(sqlStatement, true, TableName, "PickTicket", "GetPickTicketList", out _conn, out myDataReader))
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
                    string err = String.Format(MainClass.StringTable.GetString("DatabaseError"), TableName, ex.Message.ToString() + "(" + sqlStatement + ")");
                    ServiceMessages.InsertRec(MainClass.AppName, TableName, "GetPickTicketList", err);
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
				MainClass.ToInt(TableName, myDataReader["PickTicketIid"])
				, myDataReader["OrderNum"].ToString()
				, myDataReader["PatientId"].ToString()
				, myDataReader["CaseId"].ToString()
				, myDataReader["ProcedureCode"].ToString()
				, myDataReader["itemId"].ToString()
				, myDataReader["itemName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["trxQty"])
				, myDataReader["itemUnitOfIssue"].ToString()
				, MainClass.ToDecimal(TableName, myDataReader["costPerIssue"])
				, MainClass.ToDate(TableName, myDataReader["trxTime"])
                , MainClass.ToInt(TableName, myDataReader["filledQty"]));
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO PICK_TICKET (OrderNum, PatientId, CaseId, ProcedureCode, ItemId, ItemName, TrxQty, ItemUnitOfIssue, CostPerIssue, TrxTime, FilledQty) VALUES ("
                + "'" + MainClass.FixStringForSingleQuote(data.OrderNum) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PatientId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CaseId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ProcedureCode) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemName) + "'" + ", " + (int)data.TrxQty + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemUnitOfIssue) + "'" + ", " + data.CostPerIssue + ", " + MainClass.DateTimeToTimestamp(data.TrxTime) + ", " + (int)data.FilledQty + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PickTicket", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int PickTicketIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PICK_TICKET WHERE PickTicketIid='{0}'", 
				(int)PickTicketIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PickTicket", "DeleteRecord");
			return Retval;
		}


	}
}
