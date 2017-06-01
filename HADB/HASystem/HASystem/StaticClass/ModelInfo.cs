using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;

namespace HASystem.StaticClass
{
    static class ModelInfo
    {
        public static ObservableCollection<ModelResult> list;
        public static SQLiteConnection conn = new SQLiteConnection("Data Source=DB\\BS.db");

        public struct ModelResult
        {
            public string model { get; set; }
            public string type1 { get; set; }
            public string voltMax1 { get; set; }
            public string voltMin1 { get; set; }
            public string resistanceMax1 { get; set; }
            public string resistanceMin1 { get; set; }
            public string type2 { get; set; }
            public string voltMax2 { get; set; }
            public string voltMin2 { get; set; }
            public string resistanceMax2 { get; set; }
            public string resistanceMin2 { get; set; }
            public string k_valueMax2 { get; set; }
            public string k_valueMin2 { get; set; }
            public string volt_compensate { get; set; }
            public string from_user { get; set; }
            public string update_time { get; set; }
        }

        /// <summary>
        /// 获得型号信息
        /// </summary>
        public static ObservableCollection<ModelResult> GetModelData()
        {
            list = new ObservableCollection<ModelResult>();
            try
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"select model,type_1,voltMax_1,voltMin_1,resistanceMax_1,resistanceMin_1,type_2,voltMax_2,"
                  + $"voltMin_2,resistanceMax_2,resistanceMin_2,k_valueMax_2,k_valueMin_2,volt_compensate,from_user,update_time from ModelInfo";
                SQLiteDataReader reader = cmd.ExecuteReader();
                ModelResult result = default(ModelResult);
                foreach(var item in reader)
                {
                    result.model = reader["model"].ToString();
                    result.type1 = reader["type_1"].ToString();
                    result.voltMax1 = reader["voltMax_1"].ToString();
                    result.voltMin1 = reader["voltMin_1"].ToString();
                    result.resistanceMax1 = reader["resistanceMax_1"].ToString();
                    result.resistanceMin1 = reader["resistanceMin_1"].ToString();
                    result.type2 = reader["type_2"].ToString();
                    result.voltMax2 = reader["voltMax_2"].ToString();
                    result.voltMin2 = reader["voltMin_2"].ToString();
                    result.resistanceMax2 = reader["resistanceMax_2"].ToString();
                    result.resistanceMin2 = reader["resistanceMin_2"].ToString();
                    result.k_valueMax2 = reader["k_valueMax_2"].ToString();
                    result.k_valueMin2 = reader["k_valueMin_2"].ToString();
                    result.volt_compensate=reader["volt_compensate"].ToString();
                    result.from_user = reader["from_user"].ToString();
                    result.update_time = reader["update_time"].ToString();
                    list.Add(result);
                    
                }
                conn.Close();
                return list;               
            }
            catch(Exception ex)
            {
                conn.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 查找型号信息
        /// </summary>
        public static ObservableCollection<ModelResult> SearchModelData(string search)
        {
            list = new ObservableCollection<ModelResult>();
            try
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"select model,type_1,voltMax_1,voltMin_1,resistanceMax_1,resistanceMin_1,type_2,voltMax_2,"
                + $"voltMin_2,resistanceMax_2,resistanceMin_2,k_valueMax_2,k_valueMin_2,volt_compensate,from_user,update_time from ModelInfo "
                + $"where model like '%{search}%' or type_2 like '%{search}%' or type_1 like '%{search}%' or update_time like '%{search}%'";
                SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                ModelResult result = default(ModelResult);
                foreach (var item in reader)
                {
                    result.model = reader["model"].ToString();
                    result.type1 = reader["type_1"].ToString();
                    result.voltMax1 = reader["voltMax_1"].ToString();
                    result.voltMin1 = reader["voltMin_1"].ToString();
                    result.resistanceMax1 = reader["resistanceMax_1"].ToString();
                    result.resistanceMin1 = reader["resistanceMin_1"].ToString();
                    result.type2 = reader["type_2"].ToString();
                    result.voltMax2 = reader["voltMax_2"].ToString();
                    result.voltMin2 = reader["voltMin_2"].ToString();
                    result.resistanceMax2 = reader["resistanceMax_2"].ToString();
                    result.resistanceMin2 = reader["resistanceMin_2"].ToString();
                    result.k_valueMax2 = reader["k_valueMax_2"].ToString();
                    result.k_valueMin2 = reader["k_valueMin_2"].ToString();
                    result.volt_compensate = reader["volt_compensate"].ToString();
                    result.from_user = reader["from_user"].ToString();
                    result.update_time = reader["update_time"].ToString();
                    list.Add(result);
                }
                conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
           }
        }
        /// <summary>
        /// 更新型号信息
        /// </summary>
        /// <param name="strModel"></param>
        /// <param name="strType_1"></param>
        /// <param name="dVoltMax_1"></param>
        /// <param name="dVoltMin_1"></param>
        /// <param name="dResistanceMax_1"></param>
        /// <param name="dResistanceMin_1"></param>
        /// <param name="strType_2"></param>
        /// <param name="dVoltMax_2"></param>
        /// <param name="dVoltMin_2"></param>
        /// <param name="dResistanceMax_2"></param>
        /// <param name="dResistanceMin_2"></param>
        /// <param name="dK_valueMax_2"></param>
        /// <param name="dK_valueMin_2"></param>
        /// <param name="dVolt_compensate"></param>
        /// <param name="dResis_compensate"></param>
        /// <param name="strFrom_user"></param>
        /// <param name="dtUpdate_time"></param>
        /// <returns></returns>
        public static bool UpdateModeInfo(string strModel, string strType_1, double dVoltMax_1, double dVoltMin_1, double dResistanceMax_1, double dResistanceMin_1, string strType_2, double dVoltMax_2, double dVoltMin_2, double dResistanceMax_2, double dResistanceMin_2, double dK_valueMax_2, double dK_valueMin_2, double dVolt_compensate, double dResis_compensate, string strFrom_user, DateTime dtUpdate_time)
        {
            bool b = false;
            conn.Open();
            string strCmd = string.Format("Update ModelInfo set type_1='{0}',voltMax_1={1},voltMin_1={2},resistanceMax_1={3},resistanceMin_1={4},type_2='{5}',voltMax_2={6},voltMin_2={7},resistanceMax_2={8},resistanceMin_2={9},k_valueMax_2={10},k_valueMin_2={11},volt_compensate={12},resis_compensate={13},from_user='{14}',update_time='{15}' where model='{16}'", strType_1, dVoltMax_1, dVoltMin_1, dResistanceMax_1, dResistanceMin_1, strType_2, dVoltMax_2, dVoltMin_2, dResistanceMax_2, dResistanceMin_2, dK_valueMax_2, dK_valueMin_2, dVolt_compensate, dResis_compensate, strFrom_user, dtUpdate_time.ToString("yyyy-MM-dd HH:mm:ss.fff"), strModel);
            SQLiteCommand comm = new SQLiteCommand(strCmd, conn);
            comm.ExecuteNonQuery();
            b = true;
            conn.Close();
            return b;
        }

        public static bool AddNewModeInfo(string strModel, string strType_1, double dVoltMax_1, double dVoltMin_1, double dResistanceMax_1, double dResistanceMin_1, string strType_2, double dVoltMax_2, double dVoltMin_2, double dResistanceMax_2, double dResistanceMin_2, double dK_valueMax_2, double dK_valueMin_2, double dVolt_compensate, double dResis_compensate, string strFrom_user, DateTime dtUpdate_time)
        {
            bool b = false;
            conn.Open();
            string strCmd = string.Format("insert into  ModelInfo values('{0}','{1}',{2},{3},{4},{5},'{6}',{7},{8},{9},{10},{11},{12},{13},{14},'{15}','{16}')", strModel, strType_1, dVoltMax_1, dVoltMin_1, dResistanceMax_1, dResistanceMin_1, strType_2, dVoltMax_2, dVoltMin_2, dResistanceMax_2, dResistanceMin_2, dK_valueMax_2, dK_valueMin_2, dVolt_compensate, dResis_compensate, strFrom_user, dtUpdate_time.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            SQLiteCommand comm = new SQLiteCommand(strCmd, conn);
            comm.ExecuteNonQuery();
            b = true;
            conn.Close();
            return b;
        }
    } 
}
