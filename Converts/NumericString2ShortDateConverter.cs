using System;
using System . Globalization;
using System . Windows;
using System . Windows . Data;

namespace WPFPages . Converts
{
	public class NumericString2ShortDateConverter : IValueConverter
	{
		public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
		{
			if ( value == DependencyProperty . UnsetValue )
				return DependencyProperty . UnsetValue;

			// Assumes receiving a date as "10041932" or similar
			string date = value . ToString ( );
			string Output = "";
			Output = date [ 0 ] + date [ 1 ] + "/";
			Output += date [ 2 ] + date [ 3 ] + "/";
			Output += date [ 4 ] + date [ 5 ];
			Output += date [ 6 ] + date [ 7 ];
			return Output;
		}
		public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture )
		{
			return null as object;
		}
	}

}