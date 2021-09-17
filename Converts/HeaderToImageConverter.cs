using System;
using System . Globalization;
using System . Windows . Data;
using System . Windows . Media . Imaging;

namespace WPFPages . Converts
{
        //	[ValueConversion ( typeof ( string ), typeof ( BitmapImage ) )]
        public class HeaderToImageConverter : IValueConverter
	{
		public static HeaderToImageConverter Instance =
			 new HeaderToImageConverter ( );

		public object Convert ( object value, Type targetType,
		    object parameter, CultureInfo culture )
		{
			BitmapImage source = null;
			Uri uri;
			string arg = parameter as string;
			if ( value == null )
				return null;
			if ( parameter != null )
			{
				if ( ( string ) parameter != "" && arg . Contains ( "." ) )
				{
					//NB the parameter MUST MUST contain "/folderxxx/xxx/filename.suffix" with partially qualifed folder/path before the file name itself
					uri = new Uri
					( $"pack://application:,,,{arg}" );
					source = new BitmapImage ( uri );
					return source;
				}
			}
			if ( ( value as string ) . Contains ( @"Smaller Font" ) )
			{
				//Drive
				uri = new Uri
				( "pack://application:,,,/Views/magnify minus.png" );
				source = new BitmapImage ( uri );
				return source;
			}
			else if ( ( value as string ) . Contains ( @"Larger Font" ) )
			{
				//Drive
				uri = new Uri
				( "pack://application:,,,/Views/magnify minus.png" );
				source = new BitmapImage ( uri );
				return source;
			}
			else if ( ( value as string ) . Contains ( @"\" ) )
			{
				//Drive
				uri = new Uri
				( "pack://application:,,,/Icons/folder1.png" );
				source = new BitmapImage ( uri );
				return source;
			}
			else if ( ( value as string ) . Contains ( "." ) )
			{
				// Sub folder
				uri = new Uri
				( $"pack://application:,,{value as string}" );
				source = new BitmapImage ( uri );
				return source;
			}
			//                      File alone

			uri = new Uri ( $"pack://applicaiton:,,,/icons/cloud.png" );
			source = new BitmapImage ( uri );
			return source;

			//string s = value as string;
			//if ( s . Contains ( ".." ))
			//        s = s . Substring ( 2 );
			//uri = new Uri ( $"pack://application:,,,{s}" );
			//source = new BitmapImage ( uri );
			//return source;
		}

		public object ConvertBack ( object value, Type targetType,
		    object parameter, CultureInfo culture )
		{
			throw new NotSupportedException ( "Cannot convert back" );
		}
	}
}
