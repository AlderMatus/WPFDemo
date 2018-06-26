using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Catalog
	{
		const string TableName = "CATALOG";

		// collection of record fields
		public class TableData
		{
			public int VendorIid;
			public int ItemIid;
			public string VendorPartNumber;
			public string VendorItemName;
			public int VendorPriority;

			public TableData(int VendorIid, int ItemIid, string VendorPartNumber, string VendorItemName, int VendorPriority)
			{
				this.VendorIid = VendorIid;
				this.ItemIid = ItemIid;
				this.VendorPartNumber = VendorPartNumber;
				this.VendorItemName = VendorItemName;
				this.VendorPriority = VendorPriority;
			}
		}

        public static bool GetRecord(int ItemIid, out TableData data)
        {
            string SqlStatement = string.Format("SELECT * from CATALOG WHERE ItemIid='{0}'", ItemIid);
            return GetRecord(SqlStatement, out data);
		
        }
		public static bool GetRecord(int VendorIid, int ItemIid, out TableData data)
        {
            string SqlStatement = string.Format("SELECT * from CATALOG WHERE VendorIid='{0}' AND ItemIid='{1}'", VendorIid, ItemIid);
        
            return GetRecord(SqlStatement, out data);
        }

		// return record given its primary keys
        private static bool GetRecord(string SqlStatement, out TableData data)
        {
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Catalog", "GetRecord", out _conn, out myDataReader))
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

		// make a TableData object from a SADataReader record
#if !NO_ASA
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
			data = new TableData(
				MainClass.ToInt(TableName, myDataReader["VendorIid"])
				, MainClass.ToInt(TableName, myDataReader["ItemIid"])
				, myDataReader["VendorPartNumber"].ToString()
				, myDataReader["VendorItemName"].ToString()
				, MainClass.ToInt(TableName, myDataReader["VendorPriority"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO CATALOG (VendorIid, ItemIid, VendorPartNumber, VendorItemName, VendorPriority) VALUES ("
				+ (int)data.VendorIid + ", " + (int)data.ItemIid + ", " + "'" + MainClass.FixStringForSingleQuote(data.VendorPartNumber) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.VendorItemName) + "'" + ", " + (int)data.VendorPriority + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Catalog", "InsertRecord");
			return Retval;
		}

		// delete record given its primary keys
		public static bool DeleteRecord(int VendorIid, int ItemIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE CATALOG WHERE VendorIid='{0}' AND ItemIid='{1}'", 
				(int)VendorIid, (int)ItemIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Catalog", "DeleteRecord");
			return Retval;
		}


	}
}
