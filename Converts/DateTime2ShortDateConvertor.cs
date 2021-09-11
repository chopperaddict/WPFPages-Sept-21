using System;
using System . Globalization;
using System . Windows;
using System . Windows . Data;

namespace WPFPages . Converts
{
	public class DateTime2ShortDateConvertor : IValueConverter
	{
		public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
		{
			if ( value == DependencyProperty . UnsetValue )
				return DependencyProperty . UnsetValue;
			// Receives full date/Time string and returns just the date part of it as a string
			return value . ToString ( ) . Substring ( 0, 10 );
		}

		public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture )
		{
			return ( object ) null;
		}

	}

}