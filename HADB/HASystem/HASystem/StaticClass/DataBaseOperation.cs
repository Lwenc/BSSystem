using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace HASystem.StaticClass
{
    public class DataBaseOperation
    {
        public static string Path = "DB\\BS.db";
        public static SQLiteConnection conn=new SQLiteConnection();

        /// <summary>
        /// 判断数据库文件是否存在
        /// </summary>
        /// <returns></returns>
        public static bool FindDataBase()
        {
            bool b = false;
            if (File.Exists(Path) == false)
            {
                b = false;
            }
            else
            {
                b = true;
                conn = new SQLiteConnection("Data Source="+Path);
            }
            return b;
        }

        //创建数据库并创建表格,创建默认的管理员与用户
        public static void CreatDataBase()
        {
            try
            {
                SQLiteConnection.CreateFile(Path);
                conn = new SQLiteConnection("Data Source=" + Path);
                conn.Open();

                List<string> listLauguage = new List<string>();
                //创建Log日志表语句
                string strCmdLog = "CREATE TABLE [Log] ([log_id] VARCHAR(20) NOT NULL,[form_user] VARCHAR(20) NOT NULL,[name_user] VARCHAR(15) NOT NULL,[name_role] VARCHAR(15), [name_operation] VARCHAR(30), [time] DATETIME, [remark] VARCHAR(200), CONSTRAINT[] PRIMARY KEY([log_id]));";
                //创建ModelInfo表语句
                string strCmdModelInfo = "CREATE TABLE [ModelInfo] ([model] VARCHAR(20) NOT NULL,[type_1] VARCHAR(5), [voltMax_1] DECIMAL(8, 5), [voltMin_1] DECIMAL(8, 5), [resistanceMax_1] DECIMAL(8, 4), [resistanceMin_1] DECIMAL(8, 4), [type_2] VARCHAR(5), [voltMax_2] DECIMAL(8, 5), [voltMin_2] DECIMAL(8, 5), [resistanceMax_2] DECIMAL(8, 4), [resistanceMin_2] DECIMAL(8, 4), [k_valueMax_2] DECIMAL(6, 3), [k_valueMin_2] DECIMAL(6, 3), [volt_compensate] DECIMAL(8, 5), [resis_compensate] DECIMAL(8, 4), [from_user] VARCHAR(20) NOT NULL,[update_time] DATETIME, CONSTRAINT[sqlite_autoindex_ModelInfo_1] PRIMARY KEY([model]));";
                //创建Role表语句
                string strCmdRole = "CREATE TABLE [Role] ([role_id] VARCHAR(3) NOT NULL,[role_name] VARCHAR(20) NOT NULL,CONSTRAINT[sqlite_autoindex_Role_1] PRIMARY KEY ([role_id])); ";
                //创建TestInfo表语句
                string strCmdTestInfo = "CREATE TABLE [TestInfo] ([barcod] VARCHAR(25) NOT NULL,[model] VARCHAR(20) NOT NULL,[from_user1] VARCHAR(20), [testtype_1] VARCHAR(20), [passageway_1] VARCHAR(5), [time_1] DATETIME, [volt_1] DECIMAL(8, 5), [resistance_1] DECIMAL(8, 4), [ispass_1] VARCHAR(6), [remark_1] VARCHAR(200), [from_user2] VARCHAR(20), [testtype_2] VARCHAR(20), [passageway_2] VARCHAR(5), [time_2] DATETIME, [volt_2] DECIMAL(8, 5), [resistance_2] DECIMAL(8, 4), [k_value_2] DECIMAL(6, 3), [ispass_2] VARCHAR(6), [remark_2] VARCHAR(200), CONSTRAINT[sqlite_autoindex_TestInfo_1] PRIMARY KEY([barcod]));";
                //创建User表语句
                string strCmdUser = "CREATE TABLE [User] ([user_id] VARCHAR(20) NOT NULL,[password] VARCHAR(50) NOT NULL,[user_name] VARCHAR(30) NOT NULL,[telephone] VARCHAR(18), CONSTRAINT[sqlite_autoindex_User_1] PRIMARY KEY ([user_id])); ";
                //创建UR表语句
                string strCmdUR = "CREATE TABLE [UR] ([user_id] VARCHAR(20) NOT NULL CONSTRAINT[f1] REFERENCES[User]([user_id]) ON DELETE CASCADE ON UPDATE CASCADE,[role_id] VARCHAR(3) NOT NULL CONSTRAINT[f2] REFERENCES[Role]([role_id]) ON DELETE CASCADE ON UPDATE CASCADE,CONSTRAINT[sqlite_autoindex_UR_1] PRIMARY KEY ([user_id], [role_id])); ";

                //下面是创建默认的管理员与用户,相应的UR表也要创建角色权限
                string strCmdRole1 = "insert into Role values('001','管理员')";
                string strCmdRole2 = "insert into Role values('002','普通用户')";
                string strCmdAdmin = "Insert into User values('Admin','Admin','易伯特','13538592448')";
                string strCmdAdminUR = "Insert into UR values('Admin','001')";
                string strCmduser = "insert into User values('User','User','易伯特','13538592448')";
                string strCmduserUR= "Insert into UR values('User','002')";

                //开始执行查询语句
                listLauguage.Add(strCmdLog);
                listLauguage.Add(strCmdModelInfo);
                listLauguage.Add(strCmdRole);
                listLauguage.Add(strCmdTestInfo);
                listLauguage.Add(strCmdUser);
                listLauguage.Add(strCmdUR);
                listLauguage.Add(strCmdRole1);
                listLauguage.Add(strCmdRole2);
                listLauguage.Add(strCmdAdmin);
                listLauguage.Add(strCmdAdminUR);
                listLauguage.Add(strCmduser);
                listLauguage.Add(strCmduserUR);
                foreach (string s in listLauguage)
                {
                    SQLiteCommand comm = new SQLiteCommand(s, conn);
                    comm.ExecuteNonQuery();
                }
            }
            catch { }
        }
    }
}
