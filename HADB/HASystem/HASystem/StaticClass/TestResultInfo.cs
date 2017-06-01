using System.Collections.ObjectModel;

namespace HASystem.StaticClass
{
    static class TestResultInfo
    {
        public static string barcode;
        public static string type;
        public static double volt;
        public static double resistance;
        public static double kvalue = 0;
        public static string ispass = "";
        private static ObservableCollection<Result> list = new ObservableCollection<Result>();

        public struct Result
        {
            public string Barcode { get; set; }
            public string Type { get; set; }
            public string Volt { get; set; }
            public string Resistance { get; set; }
            public string KValue { get; set; }
            public string IsPass { get; set; }
        }

        public static ObservableCollection<Result> SetTestInfo()
        {         
            Result result = default(Result);
            result.Barcode = barcode;
            result.Type = type;
            result.Volt = volt.ToString();
            result.Resistance = resistance.ToString();
            result.KValue = kvalue.ToString();
            result.IsPass = ispass;

            list.Add(result);
            return list;
        }
    }
}
