using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class ScanCode
	{
		const string TableName = "SCANCODE";

		// collection of record fields
		public class TableData
		{
			public string MedID;
			public string Scancode;
			public int Qty;
			public string OwnerID;
			public string FacilityID;
			public string Extra;
			public DateTime Last_modified;

			public TableData(string MedID, string Scancode, int Qty, string OwnerID, string FacilityID, string Extra, DateTime Last_modified)
			{
				this.MedID = MedID;
				this.Scancode = Scancode;
				this.Qty = Qty;
				this.OwnerID = OwnerID;
				this.FacilityID = FacilityID;
				this.Extra = Extra;
				this.Last_modified = Last_modified;
			}
		}

		// make a TableData object from a SADataReader record
#if !NO_ASA
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
			data = new TableData(
				myDataReader["MedID"].ToString()
				, myDataReader["Scancode"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Qty"])
				, myDataReader["OwnerID"].ToString()
				, myDataReader["FacilityID"].ToString()
				, myDataReader["Extra"].ToString()
				, MainClass.ToDate(TableName, myDataReader["Last_modified"]));
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO SCANCODE (MedID, Scancode, Qty, OwnerID, FacilityID, Extra) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.MedID) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Scancode) + "'" + ", " + (int)data.Qty + ", " + "'" + MainClass.FixStringForSingleQuote(data.OwnerID) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.FacilityID) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Extra) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "ScanCode", "InsertRecord");
			return Retval;
		}

        public static bool GetRecord(string scanCode, out TableData data)
        {
            bool Retval = true;
            data = null;

#if !NO_ASA
            SAConnection _conn;
            SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from SCANCODE WHERE Scancode='{0}'", scanCode);

            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Scancode", "GetRecord", out _conn, out myDataReader))
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
	}
}
