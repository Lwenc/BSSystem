using System;
using System.Data;
using System.Data.SQLite;

namespace HASystem.StaticClass
{
    public class SaveTestData
    {
        static SQLiteConnection conn = ModelInfo.conn;//统一调用ModelInfo类的静态conn
        string strBarCod;
        string strModel;
        string strFrom_user1;
        string strTesttype_1;
        string strPassageway_1;
        DateTime dtTime_1;
        Decimal dVolt_1;
        Decimal dResistance_1;
        string strIspass_1;

        public static bool AddNewTsetData_O1(string strBarCod, string strModel, string strFrom_user1, string strTesttype_1, string strPassageway_1, DateTime dtTime_1, Decimal dVolt_1, Decimal dResistance_1, string strIspass_1,string strRemark_1)//O1测试数据保存，创建新的测试数据
        {
            conn.Open();
            bool b = false;
            string strcmd = string.Format("insert into TestInfo(barcod,model,from_user1,testtype_1,passageway_1,time_1,volt_1,resistance_1,ispass_1,remark_1) values('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},'{8}','{9}')", strBarCod, strModel, strFrom_user1, strTesttype_1, strPassageway_1, dtTime_1.ToString("yyyy-MM-dd HH:mm:ss.fff"), dVolt_1, dResistance_1, strIspass_1, strRemark_1);
            SQLiteCommand comm = new SQLiteCommand(strcmd, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            b = true;
            return b;
        }

        public static bool UpdateTsetData_O1(string strBarCod,string strFrom_user1, string strTesttype_1, string strPassageway_1, DateTime dtTime_1, Decimal dVolt_1, Decimal dResistance_1, string strIspass_1,string strRemark_1)//O1测试数据覆盖操作
        {
            conn.Open();
            bool b = false;
            string strcmd = string.Format("update TestInfo set from_user1='{0}',testtype_1='{1}',passageway_1='{2}',time_1='{3}',volt_1={4},resistance_1={5},ispass_1='{6}',remark_1='{7}' where barcod='{8}'",strFrom_user1,strTesttype_1,strPassageway_1,dtTime_1.ToString("yyyy-MM-dd HH:mm:ss.fff"),dVolt_1,dResistance_1,strIspass_1,strRemark_1,strBarCod);
            SQLiteCommand comm = new SQLiteCommand(strcmd, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            b = true;
            return b;
        }

        public static bool AddNewTsetData_OB(string strBarCod,string strFrom_user2, string strTesttype_2, string strPassageway_2, DateTime dtTime_2, Decimal dVolt_2, Decimal dResistance_2,Decimal dK_value_2, string strIspass_2,string strRemark_2)//O2测试数据保存,修改代码
        {
            conn.Open();
            bool b = false;
            string strcmd = string.Format("update TestInfo set from_user2='{0}',testtype_2='{1}',passageway_2='{2}',time_2='{3}',volt_2={4},resistance_2={5},k_value_2={6},ispass_2='{7}', remark_2='{8}' where barcod='{9}'", strFrom_user2, strTesttype_2, strPassageway_2, dtTime_2.ToString("yyyy-MM-dd HH:mm:ss.fff"), dVolt_2, dResistance_2, dK_value_2, strIspass_2, strRemark_2, strBarCod);
            SQLiteCommand comm = new SQLiteCommand(strcmd, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            b = true;
            return b;
        }

        public static bool CheckBarCode(string strBarCod,string strType)//检测电池是否已经进行O1或者OB测试
        {
            conn.Open();
            bool b=false;
            string strCmd;
            if (strType.Equals("O1"))
                strCmd = string.Format("select * from TestInfo where barcod='{0}' and testtype_1='O1'",strBarCod);
            else
                strCmd = string.Format("select * from TestInfo where barcod='{0}' and testtype_2='OB'", strBarCod);
            SQLiteCommand comm = new SQLiteCommand(strCmd, conn);
            SQLiteDataReader Dr = comm.ExecuteReader();
            if (Dr.Read())
                b = true;
            else
                b = false;
            Dr.Close();
            conn.Close();
            return b;
        }

        public static bool CheckBarCodePsaa1(string strBarCod)//检测电池O1测试是否合格
        {
            conn.Open();
            string strIsPass;
            bool b = false;
            string strCmd;
            strCmd = string.Format("select ispass_1 from TestInfo where barcod='{0}'", strBarCod);
            SQLiteCommand comm = new SQLiteCommand(strCmd, conn);
            SQLiteDataReader Dr = comm.ExecuteReader();
            Dr.Read();
            strIsPass = Dr[0].ToString();
            if (strIsPass.Equals("FAIL"))
                b = false;
            else if (strIsPass.Equals("PASS"))
                b = true;
            Dr.Close();
            conn.Close();
            return b;
        }

        //通过输入电池编码，获取O1测试电压，O1时间，通过输入OB测试电压，OB测试时间，计算出K值
        public static double getK(string strBarcod, double dVolt_2,DateTime dtTime_2)
        {
            double dVolt_1=0;
            DateTime dtTime_1 = DateTime.Now;
            conn.Open();
            string strCmd = string.Format("select volt_1,time_1 from TestInfo where barcod='{0}'", strBarcod);
            SQLiteCommand comm = new SQLiteCommand(strCmd, conn);
            SQLiteDataReader Dr = comm.ExecuteReader();
            while (Dr.Read())
            {
                dVolt_1 =double.Parse(Dr[0].ToString());
                dtTime_1 = DateTime.Parse(Dr[1].ToString());
            }
            Dr.Close();
            conn.Close();
            //计算时间差
            TimeSpan ts = dtTime_2 - dtTime_1;
            double d = ts.Days * 24 + ts.Hours + ts.Minutes / 60.0;
            if (d <= 0)
                throw new Exception("O1与OB测试时间间隔太短");
            double K = (dVolt_1 - dVolt_2) / d;
            return K;
        }

        public static DataSet getTestDataByDate(DateTime dtTime, string strTestType)
        {
            DateTime dtTime2 = dtTime.AddDays(1);
            conn.Open();
            string strCmd = "";
            if (strTestType.Equals("O1"))
            {
                strCmd = string.Format("select * from TestInfo where time_1>='{0}' and time_1<'{1}' ", dtTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), dtTime2.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }
            else if (strTestType.Equals("OB"))
            {
                strCmd = string.Format("select * from TestInfo where time_2>='{0}' and time_2<'{1}' ", dtTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), dtTime2.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }
            SQLiteDataAdapter Da = new SQLiteDataAdapter(strCmd, conn);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            conn.Close();
            return Ds;

        }

    }
}
