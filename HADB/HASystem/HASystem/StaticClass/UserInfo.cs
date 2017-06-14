using System;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;

namespace HASystem.StaticClass
{
    public static class UserInfo
    {
        static SQLiteConnection conn = ModelInfo.conn;
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public static DataSet getAllUserInfo()
        {
          
            string strComm;
            string role = LoginWindow.strRole;
            string id = LoginWindow.strUserId;
            conn.Open();
            if (role == "普通用户")
                strComm = $"select User.user_id,user_name,telephone,role_name from User,Role,UR where User.user_id=UR.user_id and Role.role_id=UR.role_id and User.user_id='{id}' and UR.role_id='002'";
            else
                strComm = $"select User.user_id,user_name,telephone,role_name from User,Role,UR where User.user_id=UR.user_id and Role.role_id=UR.role_id";
            SQLiteDataAdapter Da = new SQLiteDataAdapter(strComm, conn);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            conn.Close();
            return Ds;           
        }

        /// <summary>
        /// 按用户账号查找用户
        /// </summary>
        /// <param name="strUserId">用户账号</param>
        /// <returns></returns>
        public static DataSet getUserInfoByUserId(string strUserId)
        {
            conn.Open();
            string strComm = "select User.user_id,user_name,telephone,role_name from User,Role,UR where User.user_id=UR.user_id and Role.role_id=UR.role_id and User.user_id='"+strUserId+"'";
            SQLiteDataAdapter Da = new SQLiteDataAdapter(strComm, conn);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            conn.Close();
            return Ds;
        }

        /// <summary>
        /// 添加新的用户
        /// </summary>
        /// <param name="user_id">用户ID</param>
        /// <param name="user_name">用户名</param>
        /// <param name="Pwd">用户密码</param>
        /// <param name="telephone">用户电话</param>
        /// <param name="role_name">角色名</param>
        /// <returns></returns>
        public static bool AddNewUserInfo(string user_id, string Pwd, string user_name, string telephone)
        {
            bool b = false;
            conn.Open();
            string strCmd = string.Format("insert into User values('{0}','{1}','{2}','{3}')", user_id, Pwd, user_name, telephone);
            SQLiteCommand comm = new SQLiteCommand(strCmd, conn);
            comm.ExecuteNonQuery();
            b = true;
            conn.Close();
            return b;
        }

        /// <summary>
        /// 查询用户是否存在
        /// </summary>
        /// <param name="user_id">用户ID</param>
        /// <returns></returns>
        public static bool FindUserInfo(string user_id)
        {
            bool b = false;
            conn.Open();
            string strCmd = string.Format("select * from User where user_id='{0}'", user_id);
            SQLiteCommand comm = new SQLiteCommand(strCmd, conn);
            SQLiteDataReader Dr = comm.ExecuteReader();
            if (Dr.Read())
            {
                b = true;
            }
            Dr.Close();
            conn.Close();
            return b;
        }

        /// <summary>
        /// 判断用户密码是否正确
        /// </summary>
        /// <param name="user_id">用户账户</param>
        /// <param name="strPwd">用户密码</param>
        /// <returns></returns>
        public static bool FindUserPassword(string user_id, string strPwd)
        {
            bool b = false;
            conn.Open();
            string strCmd = string.Format("select * from User where user_id='{0}' and password='{1}'", user_id, strPwd);
            SQLiteCommand comm = new SQLiteCommand(strCmd, conn);
            SQLiteDataReader Dr = comm.ExecuteReader();
            if (Dr.Read())
            {
                b = true;
            }
            Dr.Close();
            conn.Close();
            return b;
        }

        /// <summary>
        /// 判断用户是否具有某个权限
        /// </summary>
        /// <param name="user_id">用户账号</param>
        /// <param name="role_id">角色编码</param>
        /// <returns></returns>
        public static bool FindUserPower(string user_id, string role_id)
        {
            bool b = false;
            conn.Open();
            string strCmd = string.Format("select * from UR where user_id='{0}' and role_id='{1}'", user_id, role_id);
            SQLiteCommand comm = new SQLiteCommand(strCmd, conn);
            SQLiteDataReader Dr = comm.ExecuteReader();
            if (Dr.Read())
            {
                b = true;
            }
            Dr.Close();
            conn.Close();
            return b;
        }

        /// <summary>
        ///  添加新的用户角色关系
        /// </summary>
        /// <param name="user_id">用户ID</param>
        /// <param name="role_id">角色ID</param>
        /// <returns></returns>
        public static bool AddNewURInfo(string user_id, string role_id)
        {
            bool b = false;
            conn.Open();
            string strCmd = string.Format("insert into UR values('{0}','{1}')", user_id, role_id);
            SQLiteCommand comm = new SQLiteCommand(strCmd, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            b = true;
            return b;
        }

        /// <summary>
        /// 修改用户基本信息
        /// </summary>
        /// <param name="strUserId">用户的Id</param>
        /// <param name="strUserName">用户姓名</param>
        /// <param name="strTelephone">用户电话</param>
        /// <returns></returns>
        public static bool AlterUserInfo(string strUserId, string strUserName, string strTelephone)
        {
            bool b = false;
            conn.Open();
            string strCmd = string.Format("update User set user_name='{0}',telephone='{1}' where user_id='{2}'", strUserName, strTelephone, strUserId);
            SQLiteCommand comm = new SQLiteCommand(strCmd, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            b = true;
            return b;
        }

        /// <summary>
        /// 删除用户UR关系
        /// </summary>
        /// <param name="strUserId">用户Id</param>
        /// <param name="strRoleId">用户角色编码</param>
        /// <returns></returns>
        public static bool DelURInfo(string strUserId)
        {
            bool b = false;
            conn.Open();
            string strCmd = string.Format("delete from UR where user_id='{0}'", strUserId);
            SQLiteCommand comm = new SQLiteCommand(strCmd, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            b = true;
            return b;
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="strUserId">用户账户</param>
        /// <returns></returns>
        public static bool DelUserInfo(string strUserId)
        {
            bool b = false;
            conn.Open();
            string strCmd = string.Format("delete from User where user_id='{0}'", strUserId);
            SQLiteCommand comm = new SQLiteCommand(strCmd, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            b = true;
            return b;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="strUserId">用户账号</param>
        /// <param name="strPwd">新密码</param>
        /// <returns></returns>
        public static bool AlterPassword(string strUserId, string strPwd)
        {
            bool b = false;
            conn.Open();
            string strCmd = string.Format("update User set password='{0}' where user_id='{1}'", strPwd, strUserId);
            SQLiteCommand comm = new SQLiteCommand(strCmd, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            b = true;
            return b;
        }
    }
}
