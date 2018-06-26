using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class Vendor
	{
		const string TableName = "VENDOR";

		// collection of record fields
		public class TableData
		{
			public int VendorIid;
			public string VendorId;
			public string VendorName;
			public string Address1;
			public string Address2;
			public string City;
			public string State;
			public string ZipCode;
			public string AcctNetId;
			public string AcctNetPassW;
			public string Phone1;
			public string Phone2;
			public string VendorContact;
			public string Fax;

			public TableData(int VendorIid, string VendorId, string VendorName, string Address1, string Address2, string City, string State, string ZipCode, string AcctNetId, string AcctNetPassW, string Phone1, string Phone2, string VendorContact, string Fax)
			{
				this.VendorIid = VendorIid;
				this.VendorId = VendorId;
				this.VendorName = VendorName;
				this.Address1 = Address1;
				this.Address2 = Address2;
				this.City = City;
				this.State = State;
				this.ZipCode = ZipCode;
				this.AcctNetId = AcctNetId;
				this.AcctNetPassW = AcctNetPassW;
				this.Phone1 = Phone1;
				this.Phone2 = Phone2;
				this.VendorContact = VendorContact;
				this.Fax = Fax;
			}
		}

		// return record given its primary key
		public static bool GetRecord(int VendorIid, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from VENDOR WHERE VendorIid='{0}'", 
				(int)VendorIid);
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Vendor", "GetRecord", out _conn, out myDataReader))
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
				MainClass.ToInt(TableName, myDataReader["VendorIid"])
				, myDataReader["VendorId"].ToString()
				, myDataReader["VendorName"].ToString()
				, myDataReader["Address1"].ToString()
				, myDataReader["Address2"].ToString()
				, myDataReader["City"].ToString()
				, myDataReader["State"].ToString()
				, myDataReader["ZipCode"].ToString()
				, myDataReader["AcctNetId"].ToString()
				, myDataReader["AcctNetPassW"].ToString()
				, myDataReader["Phone1"].ToString()
				, myDataReader["Phone2"].ToString()
				, myDataReader["VendorContact"].ToString()
				, myDataReader["Fax"].ToString());
		}
#endif

		// insert record and return its primary key
		public static bool InsertRecord(TableData data, out int NewIid)
		{
			bool Retval = false;
			NewIid = -1;

			string SqlStatement = "INSERT INTO VENDOR (VendorId, VendorName, Address1, Address2, City, State, ZipCode, AcctNetId, AcctNetPassW, Phone1, Phone2, VendorContact, Fax) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.VendorId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.VendorName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Address1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Address2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.City) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.State) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ZipCode) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.AcctNetId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.AcctNetPassW) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Phone1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Phone2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.VendorContact) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Fax) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Vendor", "InsertRecord", out NewIid);
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(int VendorIid)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE VENDOR WHERE VendorIid='{0}'", 
				(int)VendorIid);
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Vendor", "DeleteRecord");
			return Retval;
		}


	}
}
