using System;
using System . Globalization;
using System . Windows . Data;

namespace WPFPages . Converts
{
        public class TxtUpperConverter: IValueConverter
        {
                public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
                {
                        string s = value.ToString();
                        return s . ToUpper ( );                       
                }

                public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture )
                {
                        return ( object ) null;
                }

        }
}
