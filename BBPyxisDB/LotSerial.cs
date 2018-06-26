using System;
using System.Collections.Generic;
using System.Text;
#if !NO_ASA
using Sap.Data.SQLAnywhere;
#endif

namespace BBPyxisDB
{

	public class LotSerial
	{
		const string TableName = "LOT_SERIAL";

		// collection of record fields
		public class TableData
		{
			public int ItemIid;
			public int DeviceIid;
			public int PktIid;
			public string LotNbr;
			public string SerialNbr;
			public DateTime CreateTime;
			public int State;
			public string RefillId;
			public DateTime Expiration;
            public Int64 TagId;
            public string TagIdDisplay;
            public int TagState;
            public int TagLastTrx;
            public DateTime TagLastTrxTime;
            public int TagResync;
            public int ItemResync;
            public string TIMId;
            public string UDI;

			public TableData(int ItemIid, int DeviceIid, int PktIid, string LotNbr, string SerialNbr, DateTime CreateTime, int State, string RefillId, DateTime Expiration, Int64 TagId, string TagIdDisplay, int TagState, int TagLastTrx, DateTime TagLastTrxTime, int TagResync, int ItemResync, string TIMId, string udi)
			{
				this.ItemIid = ItemIid;
				this.DeviceIid = DeviceIid;
				this.PktIid = PktIid;
				this.LotNbr = LotNbr;
				this.SerialNbr = SerialNbr;
				this.CreateTime = CreateTime;
				this.State = State;
				this.RefillId = RefillId;
                this.Expiration = Expiration;
                this.TagId = TagId;
                this.TagIdDisplay = TagIdDisplay;
                this.TagState = TagState;
                this.TagLastTrx = TagLastTrx;
                this.TagLastTrxTime = TagLastTrxTime;
                this.TagResync = TagResync;
                this.ItemResync = ItemResync;
                this.TIMId = TIMId;
                this.UDI = udi;
			}
		}

		// make a TableData object from a SADataReader record
#if !NO_ASA
		static void MakeDataRec(SADataReader myDataReader, out TableData data)
		{
            // Turkish language have two types of 'I'. Changing all 'I' by 'i' to avoid conflicts
			data = new TableData(
				MainClass.ToInt(TableName, myDataReader["itemiid"])
				, MainClass.ToInt(TableName, myDataReader["Deviceiid"])
				, MainClass.ToInt(TableName, myDataReader["Pktiid"])
				, myDataReader["LotNbr"].ToString()
				, myDataReader["SerialNbr"].ToString()
				, MainClass.ToDate(TableName, myDataReader["CreateTime"])
				, MainClass.ToInt(TableName, myDataReader["State"])
				, myDataReader["Refillid"].ToString()
                , MainClass.ToDate(TableName, myDataReader["Expiration"])
                , Convert.ToInt64(myDataReader["tagid"])
                , myDataReader["TagiDDisplay"].ToString()
                , MainClass.ToInt(TableName, myDataReader["tagState"])
                , MainClass.ToInt(TableName, myDataReader["tagLastTrx"])
                , MainClass.ToDate(TableName, myDataReader["tagLastTrxTime"])
                , MainClass.ToInt(TableName, myDataReader["tagResync"])
                , MainClass.ToInt(TableName,myDataReader["itemResync"])
                , myDataReader["TiMid"].ToString()
                , myDataReader["udi"].ToString());
		}
#endif


        // Update a record if it already exists, otherwise insert it...
        public static bool Save(TableData data, out int oldItemIid)
        {
            bool Retval = false;

            TableData tmpData;
            if (GetRecord(data.TagIdDisplay, out tmpData))
            {
                oldItemIid = tmpData.ItemIid;
                Retval = UpdateRFIDRecord(data);
            }
            else
            {
                Retval = InsertRecord(data);
                oldItemIid = -1;            // No old ItemIid!
            }

            return Retval;
        }

        private static bool UpdateRFIDRecord(TableData data)
        {
            bool Retval = false;

            string SqlStatement = "UPDATE LOT_SERIAL SET ItemIid = " + (int)data.ItemIid +
                ", DeviceIid = " + (int)data.DeviceIid + 
                ", PktIid = " + (int)data.PktIid +
                ", LotNbr = " + "'" + MainClass.FixStringForSingleQuote(data.LotNbr) + "'" +
                ", SerialNbr = " + "'" + MainClass.FixStringForSingleQuote(data.SerialNbr) + "'" +
                ", CreateTime = " + MainClass.DateTimeToTimestamp(data.CreateTime) + 
                ", State = " + (int)data.State + 
                //", RefillId = "+ MainClass.FixStringForSingleQuote(data.RefillId) + 
                ", Expiration = " + MainClass.DateTimeToTimestamp(data.Expiration) +
                ", tagState = " + (int)data.TagState +
                ", tagLastTrx = " + (int)data.TagLastTrx +
                ", tagLastTrxTime = " + MainClass.DateTimeToTimestamp(data.TagLastTrxTime) +
                ", tagResync = " + (int)data.TagResync +
                ", itemResync = " + (int)data.ItemResync +
                ", udi = '" + MainClass.FixStringForSingleQuote(data.UDI) + "'" + 
                " WHERE tagIDDisplay='" + MainClass.FixStringForSingleQuote(data.TagIdDisplay) + "'";

            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "LotSerial", "UpdateRFIDRecord");
            return Retval;
        }

		// insert record given all TableData fields
		public static bool InsertRecord(TableData data)
		{
			bool Retval = false;
            string SqlStatement = "INSERT INTO LOT_SERIAL (ItemIid, DeviceIid, PktIid, LotNbr, SerialNbr, CreateTime, State, RefillId, Expiration, tagID, tagIDDisplay, tagState, tagLastTrx, tagLastTrxTime, tagResync, itemResync, TIMId, udi) VALUES ("
                + (int)data.ItemIid + ", " + (int)data.DeviceIid + ", " + (int)data.PktIid + ", " + "'"
                + MainClass.FixStringForSingleQuote(data.LotNbr) + "'" + ", " + "'" + MainClass.FixStringForSingleQuote(data.SerialNbr)
                + "'" + ", " + MainClass.DateTimeToTimestamp(data.CreateTime) + ", " + (int)data.State + ", " + "'"
                + MainClass.FixStringForSingleQuote(data.RefillId) + "'" + ", " + MainClass.DateTimeToTimestamp(data.Expiration) + ", "
                + data.TagId + ", " + "'" + MainClass.FixStringForSingleQuote(data.TagIdDisplay) + "'" + ", "
                + data.TagState + ", "
                + data.TagLastTrx + ", "
                + MainClass.DateTimeToTimestamp(data.TagLastTrxTime) + ", "
                + data.TagResync + ", "
                + data.ItemResync + ", "
                + "'" + MainClass.FixStringForSingleQuote(data.TIMId) + "', "
                + "'" + MainClass.FixStringForSingleQuote(data.UDI) + "'"
                + ")";
			Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "LotSerial", "InsertRecord");
			return Retval;
		}

        // delete record given based on the TagID
        public static bool DeleteRecord(string TagIdDisplay)
        {
            bool Retval = true;
            string SqlStatement = string.Format("DELETE LOT_SERIAL WHERE tagIDDisplay='{0}'",
                MainClass.FixStringForSingleQuote(TagIdDisplay));
            Retval = MainClass.ExecuteSql(SqlStatement, true, TableName, "LotSerial", "DeleteRecord");
            return Retval;
        }

        public static bool GetRecord (string tagId, out TableData data)
        {
            bool Retval = false;
            data = null;

#if !NO_ASA
            string SqlStatement = "SELECT * FROM LOT_SERIAL where tagIDDisplay = :tagId";
            try
            {
                using (SAConnection con = MainClass.GetConnection())
                {
                    con.Open();
                    using (SACommand cmd = new SACommand(SqlStatement, con))
                    {
                        cmd.Parameters.Add(new SAParameter("tagId", SADbType.VarChar, 64)).Value = tagId;
                        using (SADataReader sa = cmd.ExecuteReader())
                        {
                            if (sa.Read())
                            {
                                MakeDataRec(sa, out data);
                                Retval = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Retval = false;
                string err = String.Format(MainClass.StringTable.GetString("DatabaseError"), TableName, ex.Message.ToString() + "(" + SqlStatement + "=" + tagId+")");
                ServiceMessages.InsertRec(MainClass.AppName, TableName, "GetRecord", err);
            }
#endif
            return Retval;
        }
	}
}
