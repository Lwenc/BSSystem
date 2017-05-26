using ini = HASystem.StaticClass.IniConfigure;

namespace HASystem.StaticClass
{
    public struct StructSerialInfo
    {

        public static string portName { get; set; }
        public static string baudRate { get; set; }
        public static string stopBite { get; set; }
        public static string parity { get; set; }
        public static string dataBits { get; set; }
    }
    static class SerialInfo
    {
         public static void GetSerialInfo()
        {
            ini.path = @"..\\..\\IniConfigures\\SerialSetting.ini";
            StructSerialInfo.portName= ini.IniReadvalue("section1", "key1");
            StructSerialInfo.baudRate = ini.IniReadvalue("section1", "key2");
            StructSerialInfo.stopBite = ini.IniReadvalue("section1", "key3");
            StructSerialInfo.parity = ini.IniReadvalue("section1", "key4");
            StructSerialInfo.dataBits = ini.IniReadvalue("section1", "key5");
        }
    }
}
