using System.Runtime.InteropServices;
using System.Text;

namespace HASystem.StaticClass
{
    static class IniConfigure
    {
        /// <summary>
        /// 写操作
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        /// <summary>
        /// 读操作
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <param name="retVal"></param>
        /// <param name="size"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def,
            StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 保存ini文件的路径
        /// </summary>
        public static string path = "..\\..\\Setting.ini";

        /// <summary>
        /// 写ini文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        public static void IniWritevalue(string Section, string Key, string value)
        => WritePrivateProfileString(Section, Key, value, path);
        /// <summary>
        /// 读ini文件
        /// </summary>
        /// <param name="Seciton"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string IniReadvalue(string Seciton, string Key)
        {
            StringBuilder temp = new StringBuilder(255);

            int i = GetPrivateProfileString(Seciton, Key, "null", temp, 255, path);
            return temp.ToString();
        }
    }
}
