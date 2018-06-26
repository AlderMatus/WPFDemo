using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Discrepancies
	{
		const string TableName = "DISCREPANCIES";

		// collection of record fields
		public class TableData
		{
			public string DeviceName;
			public int BtnBoardNbr;
			public int SubDrawer;
			public string PktDescriptor;
			public DateTime TxTime;
			public int TrxSeq;
			public DateTime PriorPktAccessTime;
			public int PriorTrxSeq;
			public string UserId;
			public string UserName;
			public string WitnId;
			public string WitnName;
			public string ResolutionReason;
			public DateTime ResolveTime;
			public int IgnoreDiscrep;
			public int HasBeenArchived;

			public TableData(string DeviceName, int BtnBoardNbr, int SubDrawer, string PktDescriptor, DateTime TxTime, int TrxSeq, DateTime PriorPktAccessTime, int PriorTrxSeq, string UserId, string UserName, string WitnId, string WitnName, string ResolutionReason, DateTime ResolveTime, int IgnoreDiscrep, int HasBeenArchived)
			{
				this.DeviceName = DeviceName;
				this.BtnBoardNbr = BtnBoardNbr;
				this.SubDrawer = SubDrawer;
				this.PktDescriptor = PktDescriptor;
				this.TxTime = TxTime;
				this.TrxSeq = TrxSeq;
				this.PriorPktAccessTime = PriorPktAccessTime;
				this.PriorTrxSeq = PriorTrxSeq;
				this.UserId = UserId;
				this.UserName = UserName;
				this.WitnId = WitnId;
				this.WitnName = WitnName;
				this.ResolutionReason = ResolutionReason;
				this.ResolveTime = ResolveTime;
				this.IgnoreDiscrep = IgnoreDiscrep;
				this.HasBeenArchived = HasBeenArchived;
			}
		}

		// return record given its primary keys
		public static bool GetRecord(string DeviceName, int BtnBoardNbr, int SubDrawer, string PktDescriptor, DateTime TxTime, int TrxSeq, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from DISCREPANCIES WHERE DeviceName='{0}' AND BtnBoardNbr='{1}' AND SubDrawer='{2}' AND PktDescriptor='{3}' AND TxTime='{4}' AND TrxSeq='{5}'", 
				MainClass.FixStringForSingleQuote(DeviceName), (int)BtnBoardNbr, (int)SubDrawer, MainClass.FixStringForSingleQuote(PktDescriptor), MainClass.DateTimeToTimestamp(TxTime), (int)TrxSeq);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Discrepancies", "GetRecord", out _conn, out myDataReader))
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
				myDataReader["DeviceName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["BtnBoardNbr"])
				, MainClass.ToInt(TableName, myDataReader["SubDrawer"])
				, myDataReader["PktDescriptor"].ToString()
				, MainClass.ToDate(TableName, myDataReader["TxTime"])
				, MainClass.ToInt(TableName, myDataReader["TrxSeq"])
				, MainClass.ToDate(TableName, myDataReader["PriorPktAccessTime"])
				, MainClass.ToInt(TableName, myDataReader["PriorTrxSeq"])
				, myDataReader["UserId"].ToString()
				, myDataReader["UserName"].ToString()
				, myDataReader["WitnId"].ToString()
				, myDataReader["WitnName"].ToString()
				, myDataReader["ResolutionReason"].ToString()
				, MainClass.ToDate(TableName, myDataReader["ResolveTime"])
				, MainClass.ToInt(TableName, myDataReader["IgnoreDiscrep"])
				, MainClass.ToInt(TableName, myDataReader["HasBeenArchived"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO DISCREPANCIES (DeviceName, BtnBoardNbr, SubDrawer, PktDescriptor, TxTime, TrxSeq, PriorPktAccessTime, PriorTrxSeq, UserId, UserName, WitnId, WitnName, ResolutionReason, ResolveTime, IgnoreDiscrep, HasBeenArchived) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.DeviceName) + "'" + ", " + (int)data.BtnBoardNbr + ", " + (int)data.SubDrawer + ", " + "'" + MainClass.FixStringForSingleQuote(data.PktDescriptor) + "'" + ", " + MainClass.DateTimeToTimestamp(data.TxTime) + ", " + (int)data.TrxSeq + ", " + MainClass.DateTimeToTimestamp(data.PriorPktAccessTime) + ", " + (int)data.PriorTrxSeq + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.UserName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.WitnId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.WitnName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ResolutionReason) + "'" + ", " + MainClass.DateTimeToTimestamp(data.ResolveTime) + ", " + (int)data.IgnoreDiscrep + ", " + (int)data.HasBeenArchived + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Discrepancies", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(string DeviceName, int BtnBoardNbr, int SubDrawer, string PktDescriptor, DateTime TxTime, int TrxSeq)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE DISCREPANCIES WHERE DeviceName='{0}' AND BtnBoardNbr='{1}' AND SubDrawer='{2}' AND PktDescriptor='{3}' AND TxTime='{4}' AND TrxSeq='{5}'", 
				MainClass.FixStringForSingleQuote(DeviceName), (int)BtnBoardNbr, (int)SubDrawer, MainClass.FixStringForSingleQuote(PktDescriptor), MainClass.DateTimeToTimestamp(TxTime), (int)TrxSeq);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Discrepancies", "DeleteRecord");
			return Retval;
		}


	}
}
