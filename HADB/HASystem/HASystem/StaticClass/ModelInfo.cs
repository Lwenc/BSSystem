using System;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;

namespace HASystem.StaticClass
{
    static class ModelInfo
    {
        public static ObservableCollection<ModelResult> list;
        //static SqlConnection conn = new SqlConnection($"Server=localhost;Database=HADB;User id=sa;PWD=Lwenc");
        private static SQLiteConnection conn = new SQLiteConnection("Data Source=DB\\BS.db");

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
                //SqlCommand cmd = conn.CreateCommand();
                //cmd.CommandText = $"use HADB exec proc_Model";
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"select model,type_1,voltMax_1,voltMin_1,resistanceMax_1,resistanceMin_1,type_2,voltMax_2,"
                  + $"voltMin_2,resistanceMax_2,resistanceMin_2,k_valueMax_2,k_valueMin_2,volt_compensate,from_user,update_time from ModelInfo";
                SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
          //  list = new ObservableCollection<ModelResult>();
            //try
            //{
            //    conn.Open();
            //    SqlCommand cmd = conn.CreateCommand();
            //    cmd.CommandText = $"use HADB exec proc_searchModel '{search}'";
            //    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //    ModelResult result = default(ModelResult);
            //    foreach (var item in reader)
            //    {
            //        result.model = reader["model"].ToString();
            //        result.type1 = reader["type_1"].ToString();
            //        result.voltMax1 = reader["voltMax_1"].ToString();
            //        result.voltMin1 = reader["voltMin_1"].ToString();
            //        result.resistanceMax1 = reader["resistanceMax_1"].ToString();
            //        result.resistanceMin1 = reader["resistanceMin_1"].ToString();
            //        result.type2 = reader["type_2"].ToString();
            //        result.voltMax2 = reader["voltMax_2"].ToString();
            //        result.voltMin2 = reader["voltMin_2"].ToString();
            //        result.resistanceMax2 = reader["resistanceMax_2"].ToString();
            //        result.resistanceMin2 = reader["resistanceMin_2"].ToString();
            //        result.k_valueMax2 = reader["k_valueMax_2"].ToString();
            //        result.k_valueMin2 = reader["k_valueMin_2"].ToString();
            //        list.Add(result);
            //    }
            //    conn.Close();
            //    return list;
            //}
            //catch (Exception ex)
            //{
            //    conn.Close();
            //    System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
           }
       // }
    }

   
}
