using System;
using System . Globalization;
using System . Windows . Data;

namespace WPFPages . Converts
{
        public class HexConvert : IValueConverter
        {
                public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
                {
                        int val = System . Convert . ToInt32 ( value );
                        string s = val . ToString ( "X" );
                        return s;
                }

                public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture )
                {
                        double temp = System . Convert . ToDouble ( value );
                        //if ( temp <= 255 )
                        //	return ( string ) temp . ToString ( "X2" );
                        //else if ( temp <= 65535 )
                        //	return ( string ) temp . ToString ( "X4" );
                        //else if ( temp <= 16777215 )
                        //	return ( string ) temp . ToString ( "X6" );

                        return ( double ) temp;
                }
        }
}
