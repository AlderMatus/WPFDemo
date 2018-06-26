using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class PatientInsurance
	{
		const string TableName = "PATIENT_INSURANCE";

		// collection of record fields
		public class TableData
		{
			public string HospId;
			public string CabOrDeptId;
			public string ServiceDate;
			public string HospPtId;
			public string Title;
			public string PtFirstName;
			public string PtLastName;
			public string PtAddress1;
			public string PtAddress2;
			public string PtHomePhone;
			public string PtCity;
			public string PtState;
			public string PtZipCode;
			public string PtSSN;
			public string PtGender;
			public string PtDOB;
			public string GuarantorRelation;
			public string GuarantorTitle;
			public string GuarantorFirstName;
			public string GuarantorLastName;
			public string GuarantorAddress1;
			public string GuarantorAddress2;
			public string GuarantorCity;
			public string GuarantorState;
			public string GuarantorZipCode;
			public string GuarantorHomePhone;
			public string Diagnosis;
			public string ReferPhysician;
			public string Side;
			public string InjuryCause;
			public string InjuryDate;
			public string PriPaySource;
			public string PriPaySourceAddress;
			public string PolicyNumber;
			public string GroupNumber;
			public string RelationToInsured;
			public string PriEmployerSchool;
			public string PriWorkPhone;
			public string PriSubscriberFirstName;
			public string PriSubscriberLastName;
			public string PriSubscriberGender;
			public string PriSubscriberDOB;
			public string PriSubscriberAddress;
			public string PriSubscriberCity;
			public string PriSubscriberState;
			public string PriSubscriberZip;
			public string SecPaySource;
			public string SecPaySourceAddress;
			public string SecPolicyNumber;
			public string SecGroupNumber;
			public string SecRelationToInsured;
			public string SecEmployerSchool;
			public string SecFirstName;
			public string SecLastName;
			public string SecGender;
			public string SecDOB;
			public string SecAddress1;
			public string SecAddress2;
			public string SecCity;
			public string SecState;
			public string SecZip;
			public string Item;
			public string ItemDesc;
			public int Qty;
			public string PriLanguage;
			public string PatientStatus;
			public string ReqRetroactive;
			public string TARControl1_5;
			public string MedicalGrpIPA;
			public string HospAdmitDate;
			public string HospReleaseDate;

			public TableData(string HospId, string CabOrDeptId, string ServiceDate, string HospPtId, string Title, string PtFirstName, string PtLastName, string PtAddress1, string PtAddress2, string PtHomePhone, string PtCity, string PtState, string PtZipCode, string PtSSN, string PtGender, string PtDOB, string GuarantorRelation, string GuarantorTitle, string GuarantorFirstName, string GuarantorLastName, string GuarantorAddress1, string GuarantorAddress2, string GuarantorCity, string GuarantorState, string GuarantorZipCode, string GuarantorHomePhone, string Diagnosis, string ReferPhysician, string Side, string InjuryCause, string InjuryDate, string PriPaySource, string PriPaySourceAddress, string PolicyNumber, string GroupNumber, string RelationToInsured, string PriEmployerSchool, string PriWorkPhone, string PriSubscriberFirstName, string PriSubscriberLastName, string PriSubscriberGender, string PriSubscriberDOB, string PriSubscriberAddress, string PriSubscriberCity, string PriSubscriberState, string PriSubscriberZip, string SecPaySource, string SecPaySourceAddress, string SecPolicyNumber, string SecGroupNumber, string SecRelationToInsured, string SecEmployerSchool, string SecFirstName, string SecLastName, string SecGender, string SecDOB, string SecAddress1, string SecAddress2, string SecCity, string SecState, string SecZip, string Item, string ItemDesc, int Qty, string PriLanguage, string PatientStatus, string ReqRetroactive, string TARControl1_5, string MedicalGrpIPA, string HospAdmitDate, string HospReleaseDate)
			{
				this.HospId = HospId;
				this.CabOrDeptId = CabOrDeptId;
				this.ServiceDate = ServiceDate;
				this.HospPtId = HospPtId;
				this.Title = Title;
				this.PtFirstName = PtFirstName;
				this.PtLastName = PtLastName;
				this.PtAddress1 = PtAddress1;
				this.PtAddress2 = PtAddress2;
				this.PtHomePhone = PtHomePhone;
				this.PtCity = PtCity;
				this.PtState = PtState;
				this.PtZipCode = PtZipCode;
				this.PtSSN = PtSSN;
				this.PtGender = PtGender;
				this.PtDOB = PtDOB;
				this.GuarantorRelation = GuarantorRelation;
				this.GuarantorTitle = GuarantorTitle;
				this.GuarantorFirstName = GuarantorFirstName;
				this.GuarantorLastName = GuarantorLastName;
				this.GuarantorAddress1 = GuarantorAddress1;
				this.GuarantorAddress2 = GuarantorAddress2;
				this.GuarantorCity = GuarantorCity;
				this.GuarantorState = GuarantorState;
				this.GuarantorZipCode = GuarantorZipCode;
				this.GuarantorHomePhone = GuarantorHomePhone;
				this.Diagnosis = Diagnosis;
				this.ReferPhysician = ReferPhysician;
				this.Side = Side;
				this.InjuryCause = InjuryCause;
				this.InjuryDate = InjuryDate;
				this.PriPaySource = PriPaySource;
				this.PriPaySourceAddress = PriPaySourceAddress;
				this.PolicyNumber = PolicyNumber;
				this.GroupNumber = GroupNumber;
				this.RelationToInsured = RelationToInsured;
				this.PriEmployerSchool = PriEmployerSchool;
				this.PriWorkPhone = PriWorkPhone;
				this.PriSubscriberFirstName = PriSubscriberFirstName;
				this.PriSubscriberLastName = PriSubscriberLastName;
				this.PriSubscriberGender = PriSubscriberGender;
				this.PriSubscriberDOB = PriSubscriberDOB;
				this.PriSubscriberAddress = PriSubscriberAddress;
				this.PriSubscriberCity = PriSubscriberCity;
				this.PriSubscriberState = PriSubscriberState;
				this.PriSubscriberZip = PriSubscriberZip;
				this.SecPaySource = SecPaySource;
				this.SecPaySourceAddress = SecPaySourceAddress;
				this.SecPolicyNumber = SecPolicyNumber;
				this.SecGroupNumber = SecGroupNumber;
				this.SecRelationToInsured = SecRelationToInsured;
				this.SecEmployerSchool = SecEmployerSchool;
				this.SecFirstName = SecFirstName;
				this.SecLastName = SecLastName;
				this.SecGender = SecGender;
				this.SecDOB = SecDOB;
				this.SecAddress1 = SecAddress1;
				this.SecAddress2 = SecAddress2;
				this.SecCity = SecCity;
				this.SecState = SecState;
				this.SecZip = SecZip;
				this.Item = Item;
				this.ItemDesc = ItemDesc;
				this.Qty = Qty;
				this.PriLanguage = PriLanguage;
				this.PatientStatus = PatientStatus;
				this.ReqRetroactive = ReqRetroactive;
				this.TARControl1_5 = TARControl1_5;
				this.MedicalGrpIPA = MedicalGrpIPA;
				this.HospAdmitDate = HospAdmitDate;
				this.HospReleaseDate = HospReleaseDate;
			}
		}

		// return record given its primary key
		public static bool GetRecord(string HospPtId, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
			string SqlStatement = string.Format("SELECT * from PATIENT_INSURANCE WHERE HospPtId='{0}'", 
				MainClass.FixStringForSingleQuote(HospPtId));
			if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "PatientInsurance", "GetRecord", out _conn, out myDataReader))
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
				myDataReader["HospId"].ToString()
				, myDataReader["CabOrDeptId"].ToString()
				, myDataReader["ServiceDate"].ToString()
				, myDataReader["HospPtId"].ToString()
				, myDataReader["Title"].ToString()
				, myDataReader["PtFirstName"].ToString()
				, myDataReader["PtLastName"].ToString()
				, myDataReader["PtAddress1"].ToString()
				, myDataReader["PtAddress2"].ToString()
				, myDataReader["PtHomePhone"].ToString()
				, myDataReader["PtCity"].ToString()
				, myDataReader["PtState"].ToString()
				, myDataReader["PtZipCode"].ToString()
				, myDataReader["PtSSN"].ToString()
				, myDataReader["PtGender"].ToString()
				, myDataReader["PtDOB"].ToString()
				, myDataReader["GuarantorRelation"].ToString()
				, myDataReader["GuarantorTitle"].ToString()
				, myDataReader["GuarantorFirstName"].ToString()
				, myDataReader["GuarantorLastName"].ToString()
				, myDataReader["GuarantorAddress1"].ToString()
				, myDataReader["GuarantorAddress2"].ToString()
				, myDataReader["GuarantorCity"].ToString()
				, myDataReader["GuarantorState"].ToString()
				, myDataReader["GuarantorZipCode"].ToString()
				, myDataReader["GuarantorHomePhone"].ToString()
				, myDataReader["Diagnosis"].ToString()
				, myDataReader["ReferPhysician"].ToString()
				, myDataReader["Side"].ToString()
				, myDataReader["InjuryCause"].ToString()
				, myDataReader["InjuryDate"].ToString()
				, myDataReader["PriPaySource"].ToString()
				, myDataReader["PriPaySourceAddress"].ToString()
				, myDataReader["PolicyNumber"].ToString()
				, myDataReader["GroupNumber"].ToString()
				, myDataReader["RelationToInsured"].ToString()
				, myDataReader["PriEmployerSchool"].ToString()
				, myDataReader["PriWorkPhone"].ToString()
				, myDataReader["PriSubscriberFirstName"].ToString()
				, myDataReader["PriSubscriberLastName"].ToString()
				, myDataReader["PriSubscriberGender"].ToString()
				, myDataReader["PriSubscriberDOB"].ToString()
				, myDataReader["PriSubscriberAddress"].ToString()
				, myDataReader["PriSubscriberCity"].ToString()
				, myDataReader["PriSubscriberState"].ToString()
				, myDataReader["PriSubscriberZip"].ToString()
				, myDataReader["SecPaySource"].ToString()
				, myDataReader["SecPaySourceAddress"].ToString()
				, myDataReader["SecPolicyNumber"].ToString()
				, myDataReader["SecGroupNumber"].ToString()
				, myDataReader["SecRelationToInsured"].ToString()
				, myDataReader["SecEmployerSchool"].ToString()
				, myDataReader["SecFirstName"].ToString()
				, myDataReader["SecLastName"].ToString()
				, myDataReader["SecGender"].ToString()
				, myDataReader["SecDOB"].ToString()
				, myDataReader["SecAddress1"].ToString()
				, myDataReader["SecAddress2"].ToString()
				, myDataReader["SecCity"].ToString()
				, myDataReader["SecState"].ToString()
				, myDataReader["SecZip"].ToString()
				, myDataReader["Item"].ToString()
				, myDataReader["ItemDesc"].ToString()
				, MainClass.ToInt(TableName, myDataReader["Qty"])
				, myDataReader["PriLanguage"].ToString()
				, myDataReader["PatientStatus"].ToString()
				, myDataReader["ReqRetroactive"].ToString()
				, myDataReader["TARControl1_5"].ToString()
				, myDataReader["MedicalGrpIPA"].ToString()
				, myDataReader["HospAdmitDate"].ToString()
				, myDataReader["HospReleaseDate"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

			string SqlStatement = "INSERT INTO PATIENT_INSURANCE (HospId, CabOrDeptId, ServiceDate, HospPtId, Title, PtFirstName, PtLastName, PtAddress1, PtAddress2, PtHomePhone, PtCity, PtState, PtZipCode, PtSSN, PtGender, PtDOB, GuarantorRelation, GuarantorTitle, GuarantorFirstName, GuarantorLastName, GuarantorAddress1, GuarantorAddress2, GuarantorCity, GuarantorState, GuarantorZipCode, GuarantorHomePhone, Diagnosis, ReferPhysician, Side, InjuryCause, InjuryDate, PriPaySource, PriPaySourceAddress, PolicyNumber, GroupNumber, RelationToInsured, PriEmployerSchool, PriWorkPhone, PriSubscriberFirstName, PriSubscriberLastName, PriSubscriberGender, PriSubscriberDOB, PriSubscriberAddress, PriSubscriberCity, PriSubscriberState, PriSubscriberZip, SecPaySource, SecPaySourceAddress, SecPolicyNumber, SecGroupNumber, SecRelationToInsured, SecEmployerSchool, SecFirstName, SecLastName, SecGender, SecDOB, SecAddress1, SecAddress2, SecCity, SecState, SecZip, Item, ItemDesc, Qty, PriLanguage, PatientStatus, ReqRetroactive, TARControl1_5, MedicalGrpIPA, HospAdmitDate, HospReleaseDate) VALUES ("
				+ "'" + MainClass.FixStringForSingleQuote(data.HospId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.CabOrDeptId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ServiceDate) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.HospPtId) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Title) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtFirstName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtLastName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtAddress1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtAddress2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtHomePhone) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtCity) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtState) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtZipCode) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtSSN) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtGender) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PtDOB) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.GuarantorRelation) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.GuarantorTitle) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.GuarantorFirstName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.GuarantorLastName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.GuarantorAddress1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.GuarantorAddress2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.GuarantorCity) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.GuarantorState) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.GuarantorZipCode) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.GuarantorHomePhone) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Diagnosis) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ReferPhysician) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Side) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.InjuryCause) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.InjuryDate) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PriPaySource) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PriPaySourceAddress) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PolicyNumber) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.GroupNumber) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.RelationToInsured) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PriEmployerSchool) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PriWorkPhone) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PriSubscriberFirstName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PriSubscriberLastName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PriSubscriberGender) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PriSubscriberDOB) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PriSubscriberAddress) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PriSubscriberCity) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PriSubscriberState) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PriSubscriberZip) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecPaySource) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecPaySourceAddress) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecPolicyNumber) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecGroupNumber) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecRelationToInsured) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecEmployerSchool) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecFirstName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecLastName) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecGender) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecDOB) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecAddress1) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecAddress2) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecCity) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecState) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SecZip) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.Item) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ItemDesc) + "'" + ", " + (int)data.Qty + ", " + "'" + MainClass.FixStringForSingleQuote(data.PriLanguage) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.PatientStatus) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.ReqRetroactive) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.TARControl1_5) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.MedicalGrpIPA) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.HospAdmitDate) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.HospReleaseDate) + "'" + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PatientInsurance", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
		public static bool DeleteRecord(string HospPtId)
		{
			bool Retval = true;
			string SqlStatement = string.Format("DELETE PATIENT_INSURANCE WHERE HospPtId='{0}'", 
				MainClass.FixStringForSingleQuote(HospPtId));
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "PatientInsurance", "DeleteRecord");
			return Retval;
		}


	}
}
