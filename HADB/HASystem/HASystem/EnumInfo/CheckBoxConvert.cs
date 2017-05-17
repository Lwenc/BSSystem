using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;

namespace HASystem.EnumInfo
{
    internal class CheckBoxConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                return false;

            bool isAllSelected = true, isPartSelected = false;

            foreach (var v in values)
            {
                //为空为正方行
                if (v != null && (bool)v)
                    isPartSelected = true;
                else
                    isAllSelected = false;
            }

            if (isAllSelected)
                return true;

            if (isPartSelected)
                return null;

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (targetTypes == null || targetTypes.Length == 0)
                return null;

            object[] values = new object[targetTypes.Length];

            for (int i = 0; i < values.Length; i++)
                values[i] = value != null ? (bool)value : false;

            return values;
        }
    }
}
