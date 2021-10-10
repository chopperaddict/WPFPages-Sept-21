using System;
using System . Collections . ObjectModel;
using System . ComponentModel;
using System . Diagnostics;
using System . Runtime . CompilerServices;
using System . Threading;
using System . Windows;
using System . Windows . Controls;
using System . Windows . Data;
using System . Windows . Media;

using WPFPages . UserControls;
using WPFPages . ViewModels;
using WPFPages . Views;

namespace WPFPages . ViewModels
{
        /// <summary>
        /// Interaction logic for ListBoxWindow.xaml
        /// </summary>
        public partial class ListBoxWindow : Window
        {
                object _state;
                public static Window win = new Window ( );
                private Stopwatch sw = new Stopwatch ( );

                public ListBoxWindow ( )
                {
                        this . DataContext = this;
                        win = this;
                        InitializeComponent ( );
                        listboxclass . ItemsSource = null;
                        listboxclass . Items . Clear ( );
                        ObservableCollection<nwcustomer> NwCust = new ObservableCollection<nwcustomer> ( );
                        NwCust = LoadData . loadData ( );
                        listboxclass . ItemsSource = NwCust;
                        listviewclass . ItemsSource = null;
                        listviewclass . Items . Clear ( );
                        listviewclass . ItemsSource = NwCust;
                        listboxclass . SelectedIndex = 0;
                        listviewclass . SelectedIndex = 0;
                        sw . Start ( );
                        //stdproperty = "abc567 lkser 879";
                        //stdproperty = "abc567 lkser 879";
                        //stdproperty = "abc567 lkser 879";
                }

                private void listboxwindow_Loaded ( object sender, RoutedEventArgs e )
                {
//
//ListBoxWindow lb = new ListBoxWindow ( );
                        ListboxColorCtrlAP ap = new ListboxColorCtrlAP ( );
                        //ap.GetValue(t)
                        ////backGround . GetBackground ( Background );
                }


                private string _stdproperty;
                
                [DefaultValue("ian is the best")]
                public string stdproperty
                {
                        get
                        {
                                return _stdproperty;
                        }
                        set
                        {
                                _stdproperty = value;
                                Console . WriteLine ( $"ListWindow : stdproperty set to  {value}" );
                        }
                }

           
                private string _dummy2 = "dad gf hhy6868 54";
                public string dummy2
                {
                        get
                        {
                                return _dummy2;
                        }
                        set
                        {
                                _dummy2 = value;
                        }
                }

                #region Dependency Properties
                public class lbwaps
                {
                        #region ItemHeight AP
                        public static readonly DependencyProperty ItemHeightProperty
                                = DependencyProperty . RegisterAttached (
                                "ItemHeight",
                                typeof ( double ),
                                typeof (ListBoxWindow),
                                new PropertyMetadata ( ( double ) 25 ), OnItemheightChanged );

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

                        #region Background AP
                        public static readonly DependencyProperty BackgroundProperty
                         = DependencyProperty . RegisterAttached (
                         "Background",
                         typeof ( Brush ),
                         typeof (ListBoxWindow),
                         new PropertyMetadata ( default ), OnBackgroundChanged );

                        public static Brush GetBackground ( DependencyObject d )
                        {
                                return ( Brush ) d . GetValue ( BackgroundProperty );
                        }
                        public static void SetBackground ( DependencyObject d, Brush value )
                        {
                                Console . WriteLine ( $"AP : setting Background \"{d}\" to {value}" );
                                d . SetValue ( BackgroundProperty, value );
                        }
                        private static bool OnBackgroundChanged ( object value )
                        {
                                Console . WriteLine ( $"AP : OnBackgroundchanged = {value}" );
                                return true;
                        }
                        #endregion Background

                        #region Foreground Attached Property
                        public static readonly DependencyProperty ForegroundProperty
                         = DependencyProperty . RegisterAttached (
                         "Foreground",
                         typeof ( Brush ),
                         typeof (ListBoxWindow),
                         new PropertyMetadata ( default ), OnForegroundChanged );

                        public static Brush GetForeground ( DependencyObject d )
                        {
                                return ( Brush ) d . GetValue ( ForegroundProperty );
                        }
                        public static void SetForeground ( DependencyObject d, Brush value )
                        {
                                d . SetValue ( ForegroundProperty, value );
                        }
                        private static bool OnForegroundChanged ( object value )
                        {
                                Console . WriteLine ( $"AP : OnForegroundchanged = {value}" );
                                return true;
                        }
                        #endregion Foreground

                        #region SelectionBackground AP
                        public static Brush GetSelectionBackground ( DependencyObject obj )
                        {
                                return ( Brush ) obj . GetValue ( SelectionBackgroundProperty );
                        }

                        public static void SetSelectionBackground ( DependencyObject obj, Brush value )
                        {
                                obj . SetValue ( SelectionBackgroundProperty, value );
                        }

                        // Using a DependencyProperty as the backing store for SelectionBackground.  This enables animation, styling, binding, etc...
                        public static readonly DependencyProperty SelectionBackgroundProperty =
                            DependencyProperty . RegisterAttached ( "SelectionBackground", typeof ( Brush ), typeof (ListBoxWindow), new PropertyMetadata ( default ) );

                        #endregion SelectionBackground AP

                        #region SelectionForeground AP
                        public static Brush GetSelectionForeground ( DependencyObject obj )
                        {
                                return ( Brush ) obj . GetValue ( SelectionForegroundProperty );
                        }
                        public static void SetSelectionForeground ( DependencyObject obj, Brush value )
                        {
                                obj . SetValue ( SelectionForegroundProperty, value );
                        }
                        // Using a DependencyProperty as the backing store for SelectionForeground.  This enables animation, styling, binding, etc...
                        public static readonly DependencyProperty SelectionForegroundProperty =
                            DependencyProperty . RegisterAttached ( "SelectionForeground", typeof ( Brush ), typeof (ListBoxWindow), new PropertyMetadata ( default ) );

                        #endregion SelectionForeground AP              

                        #region MouseoverForeground AP
                        public static Brush GetMouseoverForeground ( DependencyObject obj )
                        {
                                return ( Brush ) obj . GetValue ( MouseoverForegroundProperty );
                        }

                        public static void SetMouseoverForeground ( DependencyObject obj, Brush value )
                        {
                                obj . SetValue ( MouseoverForegroundProperty, value );
                        }

                        // Using a DependencyProperty as the backing store for MouseoverForeground.  This enables animation, styling, binding, etc...
                        public static readonly DependencyProperty MouseoverForegroundProperty =
                            DependencyProperty . RegisterAttached ( "MouseoverForeground", typeof ( Brush ), typeof (ListBoxWindow), new PropertyMetadata ( default ) );
                        #endregion MouseoverForeground AP

                        #region MouseoverBackground AP
                        public static Brush GetMouseoverBackground ( DependencyObject obj )
                        {
                                return ( Brush ) obj . GetValue ( MouseoverBackgroundProperty );
                        }

                        public static void SetMouseoverBackground ( DependencyObject obj, Brush value )
                        {
                                obj . SetValue ( MouseoverBackgroundProperty, value );
                        }

                        // Using a DependencyProperty as the backing store for MouseoverBackground.  This enables animation, styling, binding, etc...
                        public static readonly DependencyProperty MouseoverBackgroundProperty =
                            DependencyProperty . RegisterAttached ( "MouseoverBackground", typeof ( Brush ), typeof (ListBoxWindow), new PropertyMetadata ( default ) );
                        #endregion MouseoverBackground AP

                        #region MouseoverSelectedForeground AP
                        public static Brush GetMouseoverSelectedForeground ( DependencyObject obj )
                        {
                                return ( Brush ) obj . GetValue ( MouseoverSelectedForegroundProperty );
                        }

                        public static void SetMouseoverSelectedForeground ( DependencyObject obj, Brush value )
                        {
                                obj . SetValue ( MouseoverSelectedForegroundProperty, value );
                        }

                        // Using a DependencyProperty as the backing store for MouseoverSelectedForeground.  This enables animation, styling, binding, etc...
                        public static readonly DependencyProperty MouseoverSelectedForegroundProperty =
                            DependencyProperty . RegisterAttached ( "MouseoverSelectedForeground", typeof ( Brush ), typeof (ListBoxWindow), new PropertyMetadata ( default ) );
                        #endregion MouseoverSelectedForeground AP

                        #region MouseoverSelectedBackground AP
                        public static Brush GetMouseoverSelectedBackground ( DependencyObject obj )
                        {
                                return ( Brush ) obj . GetValue ( MouseoverSelectedBackgroundProperty );
                        }

                        public static void SetMouseoverSelectedBackground ( DependencyObject obj, Brush value )
                        {
                                obj . SetValue ( MouseoverSelectedBackgroundProperty, value );
                        }

                        // Using a DependencyProperty as the backing store for MouseoverSelectedBackground.  This enables animation, styling, binding, etc...
                        public static readonly DependencyProperty MouseoverSelectedBackgroundProperty =
                            DependencyProperty . RegisterAttached ( "MouseoverSelectedBackground", typeof ( Brush ), typeof (ListBoxWindow), new PropertyMetadata ( default ) );
                        #endregion MouseoverSelectedBackground AP

                        #region FontSize AP
                        public static double GetFontSize ( DependencyObject obj )
                        {
                                return ( double ) obj . GetValue ( FontSizeProperty );
                        }

                        public static void SetFontSize ( DependencyObject obj, double value )
                        {
                                obj . SetValue ( FontSizeProperty, value );
                        }

                        // Using a DependencyProperty as the backing store for FontSize.  This enables animation, styling, binding, etc...
                        public static readonly DependencyProperty FontSizeProperty =
                            DependencyProperty . RegisterAttached ( "FontSize", typeof ( double ), typeof (ListBoxWindow), new PropertyMetadata ( ( double ) 14 ) );
                        #endregion FontSize AP

                        #region FontWeight AP
                        public static FontWeight GetFontWeight ( DependencyObject obj )
                        {
                                return ( FontWeight ) obj . GetValue ( FontWeightProperty );
                        }

                        public static void SetFontWeight ( DependencyObject obj, FontWeight value )
                        {
                                obj . SetValue ( FontWeightProperty, value );
                        }

                        // Using a DependencyProperty as the backing store for FontWeight.  This enables animation, styling, binding, etc...
                        public static readonly DependencyProperty FontWeightProperty =
                            DependencyProperty . RegisterAttached ( "FontWeight", typeof ( FontWeight ), typeof (ListBoxWindow), new PropertyMetadata ( default ) );
                        #endregion FontWeight AP


                }
                #endregion


                // Nested class for an Attached Property
                public class Attached :DependencyObject
                {
                        public static int Gettest ( DependencyObject obj )
                        {

                                return ( int ) obj . GetValue ( testProperty );
                        }

                        public static void Settest ( DependencyObject obj, int value )
                        {
                                Console . WriteLine ( $"LBW : Test value set....." );
                                obj . SetValue ( testProperty, value );
                        }

                        // Using a DependencyProperty as the backing store for test.  This enables animation, styling, binding, etc...
                        public static readonly DependencyProperty testProperty =
                            DependencyProperty . RegisterAttached ( "test", typeof ( int ), typeof ( ListBoxWindow ), new PropertyMetadata ( 99 ), Ontestchanged );

                        private static bool Ontestchanged ( object value )
                        {
                                Console . WriteLine ( $"LBW : test value changed to {value}" );
                                //test2 . SetValue ( value );
                                return true;
                        }
                }


                private void APTestingControl_Loaded ( object sender, RoutedEventArgs e )
                {

                }

                private void listboxclass_SelectionChanged ( object sender, SelectionChangedEventArgs e )
                {
    //                    ListboxColorCtrlAP . Setdblvalue (this, sw.ElapsedMilliseconds );
                }
        }

        public static class LoadData
        {
                public static ObservableCollection<nwcustomer> loadData ( )
                {
                       // ObservableCollection<nwcustomer> NwCust = new ObservableCollection<nwcustomer> ( );
                        nwcustomer nwc = new nwcustomer ( );
                        return nwc . GetNwCustomers ( );

                        // assign to our Obs collection
                        //                                listboxclass . Items . Clear ( );
                        //                             listboxclass . ItemsSource = NwCust;
                        //// do  The sortng with CollectionView
                        //CollectionView vw1;
                        //vw1 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwCust );
                        //if ( vw1 != ( CollectionView ) null && view2 . Count > 0 )
                        //        vw1 . SortDescriptions . Clear ( );
                        //vw1 . SortDescriptions . Add ( new SortDescription ( "Country", ListSortDirection . Ascending ) );
                        //vw1 . SortDescriptions . Add ( new SortDescription ( "Region", ListSortDirection . Ascending ) );
                        //vw1 . SortDescriptions . Add ( new SortDescription ( "City", ListSortDirection . Ascending ) );
                        ////NwCustomers6 = view2;
                        //subclassed . ItemsSource = NwCust;
                        //                          nwcustomer nwcust = new nwcustomer ( );
                        // nwc = vw1 . CurrentItem as nwcustomer;

                }
        }
}
