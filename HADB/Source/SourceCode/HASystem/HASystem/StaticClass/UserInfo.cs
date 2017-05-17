using System;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace HASystem.StaticClass
{
    static class UserInfo
    {
        public static ObservableCollection<UserResult> list;
        static SqlConnection conn = new SqlConnection($"Server=localhost;Database=HADB;User id=sa;PWD=Lwenc");

        public struct UserResult
        {
            public string user { get; set; }
            public string name { get; set; }
            public string telephone { get; set; }
            public string role { get; set; }
        }

        /// <summary>
        /// 获得型号信息
        /// </summary>
        public static ObservableCollection<UserResult> GetUserData()
        {
            list = new ObservableCollection<UserResult>();
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"use HADB exec proc_User";
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                UserResult result = default(UserResult);
                foreach (var item in reader)
                {
                    result.user = reader["userid"].ToString();
                    result.name = reader["name"].ToString();
                    result.telephone = reader["telephone"].ToString();
                    result.role = reader["role"].ToString();
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
    }
}
