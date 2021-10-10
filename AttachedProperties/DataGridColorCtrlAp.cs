using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Windows . Media;
using System . Windows;
using WPFPages . ViewModels;

namespace WPFPages . Views
{
        /// <summary>
        /// Set of Attached properties for use with Listbox
        /// </summary>
        public class DataGridColorCtrlAp : DependencyObject
        {
                #region Attached Properties

                #region ItemHeight AP
                public static readonly DependencyProperty ItemHeightProperty
                        = DependencyProperty . RegisterAttached (
                        "ItemHeight",
                        typeof ( double ),
                        typeof ( DataGridColorCtrlAp ),
                        new PropertyMetadata ( ( double ) 20 ), OnItemheightChanged );

                public static double GetItemHeight ( DependencyObject d )
                {
                        return ( double ) d . GetValue ( ItemHeightProperty );
                }
                public static void SetItemHeight ( DependencyObject d, double value )
                {
                        d . SetValue ( ItemHeightProperty, value );
                }
                private static bool OnItemheightChanged ( object value )
                {
                        Console . WriteLine ( $"AP : ONItemHeightchanged = {value}" );

                        return true;
                }
                #endregion ItemHeight

                #region Background1 AP
                public static readonly DependencyProperty Background1Property
                  = DependencyProperty . RegisterAttached (
                  "Background1",
                  typeof ( Brush ),
                  typeof ( DataGridColorCtrlAp ),
                  new PropertyMetadata ( Brushes . Aquamarine ), OnBackground1Changed );
                public static Brush GetBackground1 ( DependencyObject d )
                {
                        return ( Brush ) d . GetValue ( Background1Property );
                }
                public static void SetBackground1 ( DependencyObject d, Brush value )
                {
                        Console . WriteLine ( $"AP : setting Background1 to {value}" );
                        d . SetValue ( Background1Property, value );
                }
                private static bool OnBackground1Changed ( object value )
                {
                        Console . WriteLine ( $"AP : OnBackground1changed = {value}" );
                        return true;
                }
                #endregion Background1

                #region Background2 AP
                public static readonly DependencyProperty Background2Property
                  = DependencyProperty . RegisterAttached (
                  "Background2",
                  typeof ( Brush ),
                  typeof ( DataGridColorCtrlAp ),
                  new PropertyMetadata ( Brushes . Aquamarine ), OnBackground2Changed );
                public static Brush GetBackground2 ( DependencyObject d )
                {
                        return ( Brush ) d . GetValue ( Background2Property );
                }
                public static void SetBackground2 ( DependencyObject d, Brush value )
                {
                        Console . WriteLine ( $"AP : setting Background2 to {value}" );
                        d . SetValue ( Background2Property, value );
                }
                private static bool OnBackground2Changed ( object value )
                {
                        Console . WriteLine ( $"AP : OnBackground2changed = {value}" );
                        return true;
                }
                #endregion Background2

                #region Background3 AP
                public static readonly DependencyProperty Background3Property
                  = DependencyProperty . RegisterAttached (
                  "Background3",
                  typeof ( Brush ),
                  typeof ( DataGridColorCtrlAp ),
                  new PropertyMetadata ( Brushes . Aquamarine ), OnBackground3Changed );
                public static Brush GetBackground3 ( DependencyObject d )
                {
                        return ( Brush ) d . GetValue ( Background3Property );
                }
                public static void SetBackground3 ( DependencyObject d, Brush value )
                {
                        Console . WriteLine ( $"AP : setting Background3 to {value}" );
                        d . SetValue ( Background3Property, value );
                }
                private static bool OnBackground3Changed ( object value )
                {
                        Console . WriteLine ( $"AP : OnBackground3changed = {value}" );
                        return true;
                }
                #endregion Background3

                #region Background4 AP
                public static readonly DependencyProperty Background4Property
                  = DependencyProperty . RegisterAttached (
                  "Background4",
                  typeof ( Brush ),
                  typeof ( DataGridColorCtrlAp ),
                  new PropertyMetadata ( Brushes . Aquamarine ), OnBackground4Changed );
                public static Brush GetBackground4 ( DependencyObject d )
                {
                        return ( Brush ) d . GetValue ( Background4Property );
                }
                public static void SetBackground4 ( DependencyObject d, Brush value )
                {
                        Console . WriteLine ( $"AP : setting Background4 to {value}" );
                        d . SetValue ( Background4Property, value );
                }
                private static bool OnBackground4Changed ( object value )
                {
                        Console . WriteLine ( $"AP : OnBackground4changed = {value}" );
                        return true;
                }
		#endregion Background2

		            #region BackgroundColor AP
		public static readonly DependencyProperty BackgroundColorProperty
		     = DependencyProperty . RegisterAttached (
		     "BackgroundColor",
		     typeof ( Brush ),
		     typeof ( DataGridColorCtrlAp ),
		     new PropertyMetadata ( Brushes . Aquamarine ), OnBackgroundColorChanged );
		public static Brush GetBackgroundColor ( DependencyObject d )
		{
			return ( Brush ) d .GetValue ( BackgroundColorProperty );
		}
		public static void SetBackgroundColor ( DependencyObject d , Brush value )
		{
			Console .WriteLine ( $"AP : setting Background to {value}" );
			d .SetValue ( BackgroundColorProperty , value );
		}
		private static bool OnBackgroundColorChanged ( object value )
		{
			Console .WriteLine ( $"AP : OnBackgroundColorchanged = {value}" );
			return true;
		}
		#endregion BackgroundColor

		#region BorderBrush1
		public static Brush GetBorderBrush1 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( BorderBrush1Property );
                }

                public static void SetBorderBrush1 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( BorderBrush1Property, value );
                }

                // Using a DependencyProperty as the backing store for BorderBrush1.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty BorderBrush1Property =
                    DependencyProperty . RegisterAttached ( "BorderBrush1", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Transparent ) );
                #endregion BorderBrush1

                #region BorderBrush2
                public static Brush GetBorderBrush2 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( BorderBrush2Property );
                }

                public static void SetBorderBrush2 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( BorderBrush2Property, value );
                }

                // Using a DependencyProperty as the backing store for BorderBrush21.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty BorderBrush2Property =
                    DependencyProperty . RegisterAttached ( "BorderBrush2", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Transparent ) );
                #endregion BorderBrush2

                #region BorderBrush3
                public static Brush GetBorderBrush3 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( BorderBrush3Property );
                }

                public static void SetBorderBrush3 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( BorderBrush3Property, value );
                }

                // Using a DependencyProperty as the backing store for BorderBrush3.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty BorderBrush3Property =
                    DependencyProperty . RegisterAttached ( "BorderBrush3", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Transparent ) );
                #endregion BorderBrush3

                #region BorderBrush4
                public static Brush GetBorderBrush4 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( BorderBrush4Property );
                }

                public static void SetBorderBrush4 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( BorderBrush4Property, value );
                }

                // Using a DependencyProperty as the backing store for BorderBrush4.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty BorderBrush4Property =
                    DependencyProperty . RegisterAttached ( "BorderBrush4", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Transparent ) );
                #endregion BorderBrush4

                #region BorderThickness1
                public static Thickness GetBorderThickness1 ( DependencyObject obj )
                {
                        return ( Thickness ) obj . GetValue ( BorderThickness1Property );
                }

                public static void SetBorderThickness1 ( DependencyObject obj, Thickness value )
                {
                        obj . SetValue ( BorderThickness1Property, value );
                }

                // Using a DependencyProperty as the backing store for BorderThickness1.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty BorderThickness1Property =
                    DependencyProperty . RegisterAttached ( "BorderThickness1", typeof ( Thickness ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( default ) );
                #endregion BorderThickness1

                #region BorderThickness2
                public static Thickness GetBorderThickness2 ( DependencyObject obj )
                {
                        return ( Thickness ) obj . GetValue ( BorderThickness2Property );
                }

                public static void SetBorderThickness2 ( DependencyObject obj, Thickness value )
                {
                        obj . SetValue ( BorderThickness2Property, value );
                }

                // Using a DependencyProperty as the backing store for BorderThickness1.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty BorderThickness2Property =
                    DependencyProperty . RegisterAttached ( "BorderThickness2", typeof ( Thickness ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( default ) );
                #endregion BorderThickness2

                #region BorderThickness3
                public static Thickness GetBorderThickness3 ( DependencyObject obj )
                {
                        return ( Thickness ) obj . GetValue ( BorderThickness3Property );
                }

                public static void SetBorderThickness3 ( DependencyObject obj, Thickness value )
                {
                        obj . SetValue ( BorderThickness3Property, value );
                }

                // Using a DependencyProperty as the backing store for BorderThickness3.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty BorderThickness3Property =
                    DependencyProperty . RegisterAttached ( "BorderThickness3", typeof ( Thickness ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( default ) );
                #endregion BorderThickness3
  
                #region BorderThickness4
                public static Thickness GetBorderThickness4 ( DependencyObject obj )
                {
                        return ( Thickness ) obj . GetValue ( BorderThickness4Property );
                }

                public static void SetBorderThickness4 ( DependencyObject obj, Thickness value )
                {
                        obj . SetValue ( BorderThickness4Property, value );
                }
                
                   // Using a DependencyProperty as the backing store for BorderThickness1.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty BorderThickness4Property =
                    DependencyProperty . RegisterAttached ( "BorderThickness4", typeof ( Thickness ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( default ) );
                #endregion BorderThickness1

                #region Foreground1 Attached Property
                public static readonly DependencyProperty Foreground1Property
                 = DependencyProperty . RegisterAttached (
                 "Foreground1",
                 typeof ( Brush ),
                 typeof ( DataGridColorCtrlAp ),
                 new PropertyMetadata ( Brushes . Black ), OnForeground1Changed );

                public static Brush GetForeground1 ( DependencyObject d )
                {
                        return ( Brush ) d . GetValue ( Foreground1Property );
                }
                public static void SetForeground1 ( DependencyObject d, Brush value )
                {
                        d . SetValue ( Foreground1Property, value );
                }
                private static bool OnForeground1Changed ( object value )
                {
                        Console . WriteLine ( $"AP : OnForeground1changed = {value}" );
                        return true;
                }
                #endregion Foreground1

                #region Foreground2 Attached Property
                public static readonly DependencyProperty Foreground2Property
                 = DependencyProperty . RegisterAttached (
                 "Foreground2",
                 typeof ( Brush ),
                 typeof ( DataGridColorCtrlAp ),
                 new PropertyMetadata ( Brushes . Black ), OnForeground2Changed );

                public static Brush GetForeground2 ( DependencyObject d )
                {
                        return ( Brush ) d . GetValue ( Foreground2Property );
                }
                public static void SetForeground2 ( DependencyObject d, Brush value )
                {
                        d . SetValue ( Foreground2Property, value );
                }
                private static bool OnForeground2Changed ( object value )
                {
                        Console . WriteLine ( $"AP : OnForeground2changed = {value}" );
                        return true;
                }
                #endregion Foreground2

                #region Foreground3 Attached Property
                public static readonly DependencyProperty Foreground3Property
                 = DependencyProperty . RegisterAttached (
                 "Foreground3",
                 typeof ( Brush ),
                 typeof ( DataGridColorCtrlAp ),
                 new PropertyMetadata ( Brushes . Black ), OnForeground3Changed );

                public static Brush GetForeground3 ( DependencyObject d )
                {
                        return ( Brush ) d . GetValue ( Foreground3Property );
                }
                public static void SetForeground3 ( DependencyObject d, Brush value )
                {
                        d . SetValue ( Foreground3Property, value );
                }
                private static bool OnForeground3Changed ( object value )
                {
                        Console . WriteLine ( $"AP : OnForeground3changed = {value}" );
                        return true;
                }
                #endregion Foreground3

                #region Foreground4 Attached Property
                public static readonly DependencyProperty Foreground4Property
                 = DependencyProperty . RegisterAttached (
                 "Foreground4",
                 typeof ( Brush ),
                 typeof ( DataGridColorCtrlAp ),
                 new PropertyMetadata ( Brushes . Black ), OnForeground4Changed );

                public static Brush GetForeground4 ( DependencyObject d )
                {
                        return ( Brush ) d . GetValue ( Foreground4Property );
                }
                public static void SetForeground4 ( DependencyObject d, Brush value )
                {
                        d . SetValue ( Foreground4Property, value );
                }
                private static bool OnForeground4Changed ( object value )
                {
                        Console . WriteLine ( $"AP : OnForeground4changed = {value}" );
                        return true;
                }
                #endregion Foreground4

                #region SelectionBackground1 AP
                public static Brush GetSelectionBackground1 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( SelectionBackground1Property );
                }

                public static void SetSelectionBackground1 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( SelectionBackground1Property, value );
                }

                // Using a DependencyProperty as the backing store for SelectionBackground1.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty SelectionBackground1Property =
                    DependencyProperty . RegisterAttached ( "SelectionBackground1", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Blue ) );

                #endregion SelectionBackground1 AP

                #region SelectionBackground2 AP
                public static Brush GetSelectionBackground2 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( SelectionBackground2Property );
                }

                public static void SetSelectionBackground2 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( SelectionBackground2Property, value );
                }

                // Using a DependencyProperty as the backing store for SelectionBackground2.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty SelectionBackground2Property =
                    DependencyProperty . RegisterAttached ( "SelectionBackground2", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Blue ) );

                #endregion SelectionBackground2 AP

                #region SelectionBackground3 AP
                public static Brush GetSelectionBackground3 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( SelectionBackground3Property );
                }

                public static void SetSelectionBackground3 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( SelectionBackground3Property, value );
                }

                // Using a DependencyProperty as the backing store for SelectionBackground3.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty SelectionBackground3Property =
                    DependencyProperty . RegisterAttached ( "SelectionBackground3", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Blue ) );

                #endregion SelectionBackground3 AP

                #region SelectionBackground4 AP
                public static Brush GetSelectionBackground4 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( SelectionBackground4Property );
                }

                public static void SetSelectionBackground4 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( SelectionBackground4Property, value );
                }

                // Using a DependencyProperty as the backing store for SelectionBackground4.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty SelectionBackground4Property =
                    DependencyProperty . RegisterAttached ( "SelectionBackground4", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Blue ) );

                #endregion SelectionBackground4 AP

                #region SelectionForeground1 AP
                public static Brush GetSelectionForeground1 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( SelectionForeground1Property );
                }
                public static void SetSelectionForeground1 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( SelectionForeground1Property, value );
                }
                // Using a DependencyProperty as the backing store for SelectionForeground1.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty SelectionForeground1Property =
                    DependencyProperty . RegisterAttached ( "SelectionForeground1", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . White ) );

                #endregion SelectionForeground1 AP              

                #region SelectionForeground2 AP
                public static Brush GetSelectionForeground2 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( SelectionForeground2Property );
                }
                public static void SetSelectionForeground2 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( SelectionForeground2Property, value );
                }
                // Using a DependencyProperty as the backing store for SelectionForeground2.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty SelectionForeground2Property =
                    DependencyProperty . RegisterAttached ( "SelectionForeground2", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . White ) );

                #endregion SelectionForeground2 AP              

                #region SelectionForeground3 AP
                public static Brush GetSelectionForeground3 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( SelectionForeground3Property );
                }
                public static void SetSelectionForeground3 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( SelectionForeground3Property, value );
                }
                // Using a DependencyProperty as the backing store for SelectionForeground3.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty SelectionForeground3Property =
                    DependencyProperty . RegisterAttached ( "SelectionForeground3", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . White ) );

                #endregion SelectionForeground3 AP              

                #region SelectionForeground4 AP
                public static Brush GetSelectionForeground4 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( SelectionForeground4Property );
                }
                public static void SetSelectionForeground4 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( SelectionForeground4Property, value );
                }
                // Using a DependencyProperty as the backing store for SelectionForeground4.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty SelectionForeground4Property =
                    DependencyProperty . RegisterAttached ( "SelectionForeground4", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . White ) );

                #endregion SelectionForeground4 AP              

                #region MouseoverForeground1 AP
                public static Brush GetMouseoverForeground1 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverForeground1Property );
                }

                public static void SetMouseoverForeground1 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverForeground1Property, value );
                }

                // Using a DependencyProperty as the backing store for MouseoverForeground1.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty MouseoverForeground1Property =
                    DependencyProperty . RegisterAttached ( "MouseoverForeground1", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Black ) );
                #endregion MouseoverForeground1 AP

                #region MouseoverForeground2 AP
                public static Brush GetMouseoverForeground2 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverForeground2Property );
                }

                public static void SetMouseoverForeground2 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverForeground2Property, value );
                }

                // Using a DependencyProperty as the backing store for MouseoverForeground2.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty MouseoverForeground2Property =
                    DependencyProperty . RegisterAttached ( "MouseoverForeground2", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Black ) );
                #endregion MouseoverForeground2 AP

                #region MouseoverForeground3 AP
                public static Brush GetMouseoverForeground3 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverForeground3Property );
                }

                public static void SetMouseoverForeground3 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverForeground3Property, value );
                }

                // Using a DependencyProperty as the backing store for MouseoverForeground3.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty MouseoverForeground3Property =
                    DependencyProperty . RegisterAttached ( "MouseoverForeground3", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Black ) );
                #endregion MouseoverForeground3 AP

                #region MouseoverForeground4 AP
                public static Brush GetMouseoverForeground4 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverForeground4Property );
                }

                public static void SetMouseoverForeground4 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverForeground4Property, value );
                }

                // Using a DependencyProperty as the backing store for MouseoverForeground4.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty MouseoverForeground4Property =
                    DependencyProperty . RegisterAttached ( "MouseoverForeground4", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Black ) );
                #endregion MouseoverForeground4 AP

                #region MouseoverBackground1 AP
                public static Brush GetMouseoverBackground1 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverBackground1Property );
                }

                public static void SetMouseoverBackground1 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverBackground1Property, value );
                }

                // Using a DependencyProperty as the backing store for MouseoverBackground1.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty MouseoverBackground1Property =
                    DependencyProperty . RegisterAttached ( "MouseoverBackground1", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . LightGray ) );
                #endregion MouseoverBackground1 AP

                #region MouseoverBackground2 AP
                public static Brush GetMouseoverBackground2 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverBackground2Property );
                }

                public static void SetMouseoverBackground2 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverBackground2Property, value );
                }

                // Using a DependencyProperty as the backing store for MouseoverBackground2.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty MouseoverBackground2Property =
                    DependencyProperty . RegisterAttached ( "MouseoverBackground2", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . LightGray ) );
                #endregion MouseoverBackground2 AP

                #region MouseoverBackground3 AP
                public static Brush GetMouseoverBackground3 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverBackground3Property );
                }

                public static void SetMouseoverBackground3 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverBackground3Property, value );
                }

                // Using a DependencyProperty as the backing store for MouseoverBackground3.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty MouseoverBackground3Property =
                    DependencyProperty . RegisterAttached ( "MouseoverBackground3", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . LightGray ) );
                #endregion MouseoverBackground3 AP

                #region MouseoverBackground4 AP
                public static Brush GetMouseoverBackground4 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverBackground4Property );
                }

                public static void SetMouseoverBackground4 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverBackground4Property, value );
                }

                // Using a DependencyProperty as the backing store for MouseoverBackground4.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty MouseoverBackground4Property =
                    DependencyProperty . RegisterAttached ( "MouseoverBackground4", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . LightGray ) );
                #endregion MouseoverBackground4 AP

                #region MouseoverSelectedForeground1 AP
                public static Brush GetMouseoverSelectedForeground1 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverSelectedForeground1Property );
                }
                public static void SetMouseoverSelectedForeground1 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverSelectedForeground1Property, value );
                }
                public static readonly DependencyProperty MouseoverSelectedForeground1Property =
                    DependencyProperty . RegisterAttached ( "MouseoverSelectedForeground1", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . White ) );
                #endregion MouseoverSelectedForeground1 AP

                #region MouseoverSelectedForeground2 AP
                public static Brush GetMouseoverSelectedForeground2 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverSelectedForeground2Property );
                }
                public static void SetMouseoverSelectedForeground2 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverSelectedForeground2Property, value );
                }
                public static readonly DependencyProperty MouseoverSelectedForeground2Property =
                    DependencyProperty . RegisterAttached ( "MouseoverSelectedForeground2", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . White ) );
                #endregion MouseoverSelectedForeground2 AP
 
                #region MouseoverSelectedForeground3 AP
                public static Brush GetMouseoverSelectedForeground3 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverSelectedForeground3Property );
                }
                public static void SetMouseoverSelectedForeground3 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverSelectedForeground3Property, value );
                }
                public static readonly DependencyProperty MouseoverSelectedForeground3Property =
                    DependencyProperty . RegisterAttached ( "MouseoverSelectedForeground3", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . White ) );
                #endregion MouseoverSelectedForeground3 AP
  
                #region MouseoverSelectedForeground4 AP
                public static Brush GetMouseoverSelectedForeground4 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverSelectedForeground4Property );
                }
                public static void SetMouseoverSelectedForeground4 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverSelectedForeground4Property, value );
                }
                public static readonly DependencyProperty MouseoverSelectedForeground4Property =
                    DependencyProperty . RegisterAttached ( "MouseoverSelectedForeground4", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . White ) );
                #endregion MouseoverSelectedForeground4 AP

                #region MouseoverSelectedBackground1 AP
                public static Brush GetMouseoverSelectedBackground1 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverSelectedBackground1Property );
                }
                public static void SetMouseoverSelectedBackground1 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverSelectedBackground1Property, value );
                }
                public static readonly DependencyProperty MouseoverSelectedBackground1Property =
                    DependencyProperty . RegisterAttached ( "MouseoverSelectedBackground1", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Red ) );
                #endregion MouseoverSelectedBackground1 AP

                #region MouseoverSelectedBackground2 AP
                public static Brush GetMouseoverSelectedBackground2 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverSelectedBackground2Property );
                }
                public static void SetMouseoverSelectedBackground2 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverSelectedBackground2Property, value );
                }
                public static readonly DependencyProperty MouseoverSelectedBackground2Property =
                    DependencyProperty . RegisterAttached ( "MouseoverSelectedBackground2", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Red ) );
                #endregion MouseoverSelectedBackground2 AP

                #region MouseoverSelectedBackground3 AP
                public static Brush GetMouseoverSelectedBackground3 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverSelectedBackground3Property );
                }
                public static void SetMouseoverSelectedBackground3 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverSelectedBackground3Property, value );
                }
                public static readonly DependencyProperty MouseoverSelectedBackground3Property =
                    DependencyProperty . RegisterAttached ( "MouseoverSelectedBackground3", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Red ) );
                #endregion MouseoverSelectedBackground3 AP

                #region MouseoverSelectedBackground4 AP
                public static Brush GetMouseoverSelectedBackground4 ( DependencyObject obj )
                {
                        return ( Brush ) obj . GetValue ( MouseoverSelectedBackground4Property );
                }
                public static void SetMouseoverSelectedBackground4 ( DependencyObject obj, Brush value )
                {
                        obj . SetValue ( MouseoverSelectedBackground4Property, value );
                }
                public static readonly DependencyProperty MouseoverSelectedBackground4Property =
                    DependencyProperty . RegisterAttached ( "MouseoverSelectedBackground4", typeof ( Brush ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( Brushes . Red ) );
                #endregion MouseoverSelectedBackground4 AP

                #region FontSize1 AP
                public static double GetFontSize1 ( DependencyObject obj )
                {
                        return ( double ) obj . GetValue ( FontSize1Property );
                }

                public static void SetFontSize1 ( DependencyObject obj, double value )
                {
                        obj . SetValue ( FontSize1Property, value );
                }

                // Using a DependencyProperty as the backing store for FontSize1.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty FontSize1Property =
                    DependencyProperty . RegisterAttached ( "FontSize1", typeof ( double ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( ( double ) 16 ) );
                #endregion FontSize1 AP

                #region FontSize2 AP
                public static double GetFontSize2 ( DependencyObject obj )
                {
                        return ( double ) obj . GetValue ( FontSize2Property );
                }

                public static void SetFontSize2 ( DependencyObject obj, double value )
                {
                        obj . SetValue ( FontSize2Property, value );
                }

                // Using a DependencyProperty as the backing store for FontSize2.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty FontSize2Property =
                    DependencyProperty . RegisterAttached ( "FontSize2", typeof ( double ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( ( double ) 14 ) );
                #endregion FontSize2 AP

                #region FontSize3 AP
                public static double GetFontSize3 ( DependencyObject obj )
                {
                        return ( double ) obj . GetValue ( FontSize3Property );
                }

                public static void SetFontSize3 ( DependencyObject obj, double value )
                {
                        obj . SetValue ( FontSize3Property, value );
                }

                // Using a DependencyProperty as the backing store for FontSize3.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty FontSize3Property =
                    DependencyProperty . RegisterAttached ( "FontSize3", typeof ( double ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( ( double ) 14 ) );
                #endregion FontSize3 AP

                #region FontSize4 AP
                public static double GetFontSize4 ( DependencyObject obj )
                {
                        return ( double ) obj . GetValue ( FontSize4Property );
                }

                public static void SetFontSize4 ( DependencyObject obj, double value )
                {
                        obj . SetValue ( FontSize4Property, value );
                }

                // Using a DependencyProperty as the backing store for FontSize4.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty FontSize4Property =
                    DependencyProperty . RegisterAttached ( "FontSize4", typeof ( double ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( ( double ) 14 ) );
                #endregion FontSize4 AP

                #region FontWeight1 AP
                public static FontWeight GetFontWeight1 ( DependencyObject obj )
                {
                        return ( FontWeight ) obj . GetValue ( FontWeight1Property );
                }

                public static void SetFontWeight1 ( DependencyObject obj, FontWeight value )
                {
                        obj . SetValue ( FontWeight1Property, value );
                }

                // Using a DependencyProperty as the backing store for FontWeight1.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty FontWeight1Property =
                    DependencyProperty . RegisterAttached ( "FontWeight1", typeof ( FontWeight ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( default ) );
                #endregion FontWeight1 AP

                #region FontWeight2 AP
                public static FontWeight GetFontWeight2 ( DependencyObject obj )
                {
                        return ( FontWeight ) obj . GetValue ( FontWeight2Property );
                }

                public static void SetFontWeight2 ( DependencyObject obj, FontWeight value )
                {
                        obj . SetValue ( FontWeight2Property, value );
                }

                // Using a DependencyProperty as the backing store for FontWeight2.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty FontWeight2Property =
                    DependencyProperty . RegisterAttached ( "FontWeight2", typeof ( FontWeight ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( default ) );
                #endregion FontWeight2 AP

                #region FontWeight3 AP
                public static FontWeight GetFontWeight3 ( DependencyObject obj )
                {
                        return ( FontWeight ) obj . GetValue ( FontWeight3Property );
                }

                public static void SetFontWeight3 ( DependencyObject obj, FontWeight value )
                {
                        obj . SetValue ( FontWeight3Property, value );
                }

                // Using a DependencyProperty as the backing store for FontWeight3.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty FontWeight3Property =
                    DependencyProperty . RegisterAttached ( "FontWeight3", typeof ( FontWeight ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( default ) );
                #endregion FontWeight3 AP

                #region FontWeight4 AP
                public static FontWeight GetFontWeight4 ( DependencyObject obj )
                {
                        return ( FontWeight ) obj . GetValue ( FontWeight4Property );
                }

                public static void SetFontWeight ( DependencyObject obj, FontWeight value )
                {
                        obj . SetValue ( FontWeight4Property, value );
                }

                // Using a DependencyProperty as the backing store for FontWeight4.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty FontWeight4Property =
                    DependencyProperty . RegisterAttached ( "FontWeight4", typeof ( FontWeight ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( default ) );
                #endregion FontWeight4 AP

                #region FontWeightSelected1 AP
                public static FontWeight GetFontWeightSelected1 ( DependencyObject obj )
                {
                        return ( FontWeight ) obj . GetValue ( FontWeightSelected1Property );
                }

                public static void SetFontWeightSelected1 ( DependencyObject obj, FontWeight value )
                {
                        obj . SetValue ( FontWeightSelected1Property, value );
                }

                // Using a DependencyProperty as the backing store for FontWeight.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty FontWeightSelected1Property =
                    DependencyProperty . RegisterAttached ( "FontWeightSelected1", typeof ( FontWeight ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( ( FontWeight ) FontWeight . FromOpenTypeWeight ( 400 ) ), OnFontWeightSelected1Changed );

                private static bool OnFontWeightSelected1Changed ( object value )
                {
                        Console . WriteLine ( $"FontWeightSelected1 has been reset to {value}" );
                        return true;
                }
                #endregion FontWeightSelected1 AP

                #region FontWeightSelected2 AP
                public static FontWeight GetFontWeightSelected2 ( DependencyObject obj )
                {
                        return ( FontWeight ) obj . GetValue ( FontWeightSelected2Property );
                }

                public static void SetFontWeightSelected2 ( DependencyObject obj, FontWeight value )
                {
                        obj . SetValue ( FontWeightSelected2Property, value );
                }

                // Using a DependencyProperty as the backing store for FontWeight.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty FontWeightSelected2Property =
                    DependencyProperty . RegisterAttached ( "FontWeightSelected2", typeof ( FontWeight ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( ( FontWeight ) FontWeight . FromOpenTypeWeight ( 400 ) ), OnFontWeightSelected2Changed );

                private static bool OnFontWeightSelected2Changed ( object value )
                {
                        Console . WriteLine ( $"FontWeightSelected2 has been reset to {value}" );
                        return true;
                }
                #endregion FontWeightSelected2 AP

                #region FontWeightSelected3 AP
                public static FontWeight GetFontWeightSelected3 ( DependencyObject obj )
                {
                        return ( FontWeight ) obj . GetValue ( FontWeightSelected3Property );
                }

                public static void SetFontWeightSelected3 ( DependencyObject obj, FontWeight value )
                {
                        obj . SetValue ( FontWeightSelected3Property, value );
                }

                // Using a DependencyProperty as the backing store for FontWeight.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty FontWeightSelected3Property =
                    DependencyProperty . RegisterAttached ( "FontWeightSelected3", typeof ( FontWeight ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( ( FontWeight ) FontWeight . FromOpenTypeWeight ( 400 ) ), OnFontWeightSelected3Changed );

                private static bool OnFontWeightSelected3Changed ( object value )
                {
                        Console . WriteLine ( $"FontWeightSelected3 has been reset to {value}" );
                        return true;
                }
                #endregion FontWeightSelected3 AP

                #region FontWeightSelected4 AP
                public static FontWeight GetFontWeightSelected4 ( DependencyObject obj )
                {
                        return ( FontWeight ) obj . GetValue ( FontWeightSelected4Property );
                }

                public static void SetFontWeightSelected4 ( DependencyObject obj, FontWeight value )
                {
                        obj . SetValue ( FontWeightSelected4Property, value );
                }

                // Using a DependencyProperty as the backing store for FontWeight.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty FontWeightSelected4Property =
                    DependencyProperty . RegisterAttached ( "FontWeightSelected4", typeof ( FontWeight ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( ( FontWeight ) FontWeight . FromOpenTypeWeight ( 400 ) ), OnFontWeightSelected4Changed );

                private static bool OnFontWeightSelected4Changed ( object value )
                {
                        Console . WriteLine ( $"FontWeightSelected4 has been reset to {value}" );
                        return true;
                }
                #endregion FontWeightSelected4 AP

                #region dumyAPstring
                public static readonly DependencyProperty dummyAPstringProperty =
                    DependencyProperty . RegisterAttached ( "dummyAPstring",
                            typeof ( string ), typeof ( DataGridColorCtrlAp ),
                            new PropertyMetadata ( ( string ) "DummyAPstring from AP ! " ), OnstringSet );
                public static string GetdummyAPstring ( DependencyObject obj )
                {
                        return ( string ) obj . GetValue ( dummyAPstringProperty );
                }
                public static void SetdummyAPstring ( DependencyObject obj, string value )
                {
                        obj . SetValue ( dummyAPstringProperty, value );
                }
                private static bool OnstringSet ( object value )
                {
                        Console . WriteLine ( $"AP.dummyAPstring set to : {value}" );
                        return true;
                }
                #endregion dumyAPstring

                #region test2 AP
                public static readonly DependencyProperty test2Property =
                    DependencyProperty . RegisterAttached ( "test2",
                        typeof ( string ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( ( string ) "99" ) );
                public static string Gettest2 ( DependencyObject obj )
                {
                        return ( string ) obj . GetValue ( test2Property );
                }
                public static void Settest2 ( DependencyObject obj, string value )
                {
                        obj . SetValue ( test2Property, value );
                }
                #endregion test2 AP

                #region test AP
                public static readonly DependencyProperty testProperty =
                    DependencyProperty . RegisterAttached ( "test",
                        typeof ( int ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( 32767 ), Ontestchanged );

                public static int Gettest ( DependencyObject obj )
                {
                        return ( int ) obj . GetValue ( testProperty );
                }
                public static void Settest ( DependencyObject obj, int value )
                {
                        obj . SetValue ( testProperty, value );
                }


                private static bool Ontestchanged ( object value )
                {
                        Console . WriteLine ( $"AP : test value changed to {value}" );
                        return true;
                }
                #endregion test AP

                #region dblvalue
                public static double Getdblvalue ( DependencyObject obj )
                {
                        return ( double ) obj . GetValue ( dblvalueProperty );
                }

                public static void Setdblvalue ( DependencyObject obj, double value )
                {
                        obj . SetValue ( dblvalueProperty, value );
                }

                // Using a DependencyProperty as the backing store for dblvalue.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty dblvalueProperty =
                    DependencyProperty . RegisterAttached ( "dblvalue", typeof ( double ), typeof ( DataGridColorCtrlAp ), new PropertyMetadata ( ( double ) 23.864 ), Ondblvaluechanged );

                private static bool Ondblvaluechanged ( object value )
                {
                        Console . WriteLine ( $"dblvaluechanged = {value}" );
                        return true;
                }
                #endregion dblvalue


                #endregion Attached Properties
        }
}
