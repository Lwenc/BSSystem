using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;
using ini = HASystem.StaticClass.IniConfigure;

namespace HASystem.StaticClass
{
    static class StringExtension
    {
        public static bool IsVoltRight = true;
        public static bool IsResisRight = true;
        static Dictionary<char, string> asciiMap = new Dictionary<char, string>()
        {
            ['#'] = "23",
            ['0'] = " 30",
            ['1'] = " 31",
            ['2'] = " 32",
            ['3'] = " 33",
            ['4'] = " 34",
            ['5'] = " 35",
            ['6'] = " 36",
            ['7'] = " 37",
            ['8'] = " 38",
            ['9'] = " 39",
            ['A'] = " 41",
            ['B'] = " 42",
            ['C'] = " 43",
            ['D'] = " 44",
            ['E'] = " 45",
            ['F'] = " 46",
        };

        public static string Convert(this string str)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var c in str)
                sb.Append($"{asciiMap[c]}");

            return sb.ToString();
        }
        //判断电压并计算
        public static string strVoltRemove(this string str,string parameter)
        {
            //string[] result = new string[4];
            IsVoltRight = true;
            var strs = str.Split('.');
            string volt = parameter;
            //电压为整数时
            if (strs.Length == 1)
            {
                str = str.Replace(".", "");
                if (str.Length == 1)
                    if (volt == "2")
                        str = "0" + str + "00";
                    else
                        str = str + "000";
                if (str.Length == 2)
                    if (volt == "2")
                        str = str + "00";
                    else
                        IsVoltRight = false;

                if (str.Length == 3)
                    if (volt == "2")
                        str = str + "0";
                    else
                        IsVoltRight = false;
            }
            //电压为小数时
            else
            {
                str = str.Replace(".", "");
                if (strs[0].Length == 1 && strs[1].Length == 1)
                    if (volt == "2")
                        str = "0" + str + "0";
                    else
                        str = str + "00";
                if (strs[0].Length == 2 && strs[1].Length == 1)
                    if (volt == "2")
                        str = str + "0";
                    else
                        IsVoltRight = false;
                if (strs[0].Length >= 3 && strs[1].Length == 1)
                    IsVoltRight = false;

                if (strs[0].Length == 1 && strs[1].Length == 2)
                    if (volt == "2")
                        str = "0" + str;
                    else
                        str = str + "0";
                if (strs[0].Length == 2 && strs[1].Length == 2)
                    if (volt == "2")
                        return str;
                    else
                        IsVoltRight = false;
                if (strs[0].Length >= 3 && strs[1].Length == 2)
                    IsVoltRight = false;

                if (strs[0].Length == 1 && strs[1].Length == 3)
                    if (volt == "2")
                        str = "0" + str;
                    else
                        return str;
                if (strs[0].Length >= 2 && strs[1].Length == 3)
                    IsVoltRight = false;
            }

            return str;
        }
        //判断电阻并计算
        public static string strResisRemove(this string str, string parameter)
        {
            IsResisRight = true;
            var strs = str.Split('.');
            string resis = parameter;

            //电阻为整数时
            if (strs.Length == 1)
            {
                str = str.Replace(".", "");
                if (str.Length == 1)
                    if (resis == "0")
                        str = "000" + str;
                    else
                        str = "00" + str + "0";
                if (str.Length == 2)
                    if (resis == "0")
                        str = "00" + str;
                    else
                        str = "0" + str + "0";
                if (str.Length == 3)
                    if (resis == "0")
                        str = "0" + str;
                    else
                        str = str + "0";
                if (str.Length == 4)
                    if (resis == "0")
                        return str;
                    else
                        IsResisRight = false;
            }
            //电阻为小数时
            else
            {
                //挡位为0时报错
                if (resis == "0")
                {
                    IsResisRight = false;
                    return str;
                }
                if (strs[1].Length > 1)
                {
                    IsResisRight = false;
                    return str;
                }
                //
                str = str.Replace(".", "");
                if (strs[0].Length == 1)
                    str = "00" + str;
                if (strs[0].Length == 2)
                    str = "0" + str;
                if (strs[0].Length == 3)
                    return str;

            }
            return str;
        }
    }
}
