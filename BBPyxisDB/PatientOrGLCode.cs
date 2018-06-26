using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

    public class Patient_Or_GL_Code
	{
        const string TableName = "Patient_Or_GL_Code";

		// collection of record fields
		public class TableData
		{
            public int patientOrGLCode;
            public string patientOrGLCodeText;
			/*public int ColorRef;
			public int Speed;
			public int Font;
			public string Msg;*/

            public TableData(int patientOrGLCode, string patientOrGLCodeText)
			{
                this.patientOrGLCode = patientOrGLCode;
                this.patientOrGLCodeText = patientOrGLCodeText;
				/*this.ColorRef = ColorRef;
				this.Speed = Speed;
				this.Font = Font;
				this.Msg = Msg;*/
			}
		}

		// return record given its primary key
		public static bool GetRecord(int PatientOrGLCode, out TableData data)
		{
			bool Retval = true;
			data = null;

#if !NO_ASA
			SAConnection _conn;
			SADataReader myDataReader;
            string SqlStatement = string.Format("SELECT * from Patient_Or_GL_Code WHERE patientOrGLCode='{0}'",
                (int)PatientOrGLCode);
            if (MainClass.ExecuteSelect(SqlStatement, true, TableName, "Patient_Or_GL_Code", "GetRecord", out _conn, out myDataReader))
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
                MainClass.ToInt(TableName, myDataReader["patientOrGLCode"])
                , myDataReader["patientOrGLCodeText"].ToString());
		}
#endif

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;

            string SqlStatement = "INSERT INTO Patient_Or_GL_Code (patientOrGLCode, patientOrGLCodeText) VALUES ("
                + (int)data.patientOrGLCode + ", " + "'" + MainClass.FixStringForSingleQuote(data.patientOrGLCodeText) + "'" + ")";
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Patient_Or_GL_Code", "InsertRecord");
			return Retval;
		}

		// delete record given its primary key
        public static bool DeleteRecord(int patientOrGLCode)
		{
			bool Retval = true;
            string SqlStatement = string.Format("DELETE Patient_Or_GL_Code WHERE patientOrGLCode='{0}'",
                (int)patientOrGLCode);
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "Patient_Or_GL_Code", "DeleteRecord");
			return Retval;
		}


	}
}
