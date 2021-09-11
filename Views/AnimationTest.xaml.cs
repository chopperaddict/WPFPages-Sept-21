//#define TESTING

using System;
using System . Collections . ObjectModel;
using System . Data;
using System . Data . SqlClient;
using System . Diagnostics;
using System . Threading;
using System . Windows;
using System . Windows . Controls;
using System . Windows . Documents;
using System . Windows . Input;
using System . Windows . Media;
using System . Windows . Media . Animation;
using System . Windows . Media . Imaging;
using System . Windows . Threading;

namespace WPFPages . Views
{
        /// <summary>
        /// Interaction logic for AnimationTe.xaml
        /// </summary>
        public partial class AnimationTest : Window
        {
                private static int counter = 0;
                private Point MousePosition = new Point ( 0, 0 );
                private bool MouseDn = false;
                private int defaultMenuheight = 0;
                private int CurrentIndex = 0;
                private bool ComboChangeinProcess = false;
                public static ObservableCollection<nwcustomer> nwc = new ObservableCollection<nwcustomer> ( );
                public static nwcustomer NwCust = new nwcustomer ( );

                private static Point currentwinpos;
                private static Point currentCtrlpos;
                private static Point menupos;

                #region std properties

                private bool IsMenuOpen
                {
                        get; set;
                }

                private bool Menu1Open
                {
                        get; set;
                }

                private bool Menu2Open
                {
                        get; set;
                }

                private bool Menu3Open
                {
                        get; set;
                }

                private bool Menu4Open
                {
                        get; set;
                }

                private bool Menu5Open
                {
                        get; set;
                }

                private bool Menu6Open
                {
                        get; set;
                }

                private bool Menu7Open
                {
                        get; set;
                }

                #endregion std properties

                private DispatcherTimer timer = new ( );

                private long _timercount;

                public long timercount
                {
                        get
                        {
                                return ( long ) _timercount;
                        }

                        set
                        {
                                _timercount = value;
                        }
                }

                #region Full Properties

                private string menuOpenText = "Hide Menu Options";

                public string MenuOpenText
                {
                        get
                        {
                                return menuOpenText;
                        }

                        set
                        {
                                menuOpenText = value;
                        }
                }

                private string menu0Text = "Open 3D Buttons Panel";

                public string Menu0Text
                {
                        get
                        {
                                return menu0Text;
                        }

                        set
                        {
                                menu0Text = value;
                        }
                }

                private string menu1Text = "Panel 1";

                public string Menu1Text
                {
                        get
                        {
                                return menu1Text;
                        }

                        set
                        {
                                menu1Text = value;
                        }
                }

                private string menu2Text = "Expander Demo";

                public string Menu2Text
                {
                        get
                        {
                                return menu2Text;
                        }

                        set
                        {
                                menu2Text = value;
                        }
                }

                private string menu3Text = "Panel 3";

                public string Menu3Text
                {
                        get
                        {
                                return menu3Text;
                        }

                        set
                        {
                                menu3Text = value;
                        }
                }

                private string menu4Text = "Tab Control Panel";

                public string Menu4Text
                {
                        get
                        {
                                return menu4Text;
                        }

                        set
                        {
                                menu4Text = value;
                        }
                }

                private string menu5Text = "3D Button Panel";
                public string Menu5Text
                {
                        get
                        {
                                return menu5Text;
                        }

                        set
                        {
                                menu5Text = value;
                        }
                }

                private string menu6Text = "Experiments";

                public string Menu6Text
                {
                        get
                        {
                                return menu6Text;
                        }

                        set
                        {
                                menu6Text = value;
                        }
                }

                private string menu7Text = "Unused 1";

                public string Menu7Text
                {
                        get
                        {
                                return menu7Text;
                        }

                        set
                        {
                                menu7Text = value;
                        }
                }

                #endregion Full Properties

                #region Dummy strings

                private string dummymenutext1 = "\n3D Buttons ";
                private string dummymenutext2 = "\njust uses the Width property of the image but the values of the From [the Width property of the image but the values of the From] ... ";
                private string dummymenutext3 = "\n\nthe Completed event handler to restart the animation... ";
                private string dummymenutext4 = "\n\nI have created an application which displays... ";
                private string dummymenutext5 = "\n\nhe above code initially creates a rectangle... ";
                private string dummymenutext6 = "\n\nFollowing is the output of the above code:... ";
                private string dummymenutext7 = "\n\nThe Very End....";

                #endregion Dummy strings

                public AnimationTest ( )
                {
                        this . DataContext = this;
                        InitializeComponent ( );
                        Utils . SetupWindowDrag ( this );
                        Storyboard s = ( Storyboard ) TryFindResource ( "HideLeftMenu" );
                        s . Begin ( );
                        this . LeftMenuTogglePanel . Opacity = 0.0;
                        timercount = 0L;
                        timer . Interval = TimeSpan . FromMilliseconds ( 1 );
                        timer . Tick += Timer_Tick;
                        timer . Start ( );
                        this . DataContext = nwc;
                        nwc = Loadcustomers ( );
                        dg1 . ItemsSource = nwc;
                        lv1 . Items . Clear ( );
                        lv1 . ItemsSource = nwc;
                        lv1 . SelectedIndex = 0;
                        lv2 . Items . Clear ( );
                        lv2 . ItemsSource = nwc;
                        lv2 . SelectedIndex = 0;
                        cb1 . ItemsSource = nwc;
                        cb2 . ItemsSource = nwc;
                        cb3 . ItemsSource = nwc;
                        cb4 . ItemsSource = nwc;
                        lv1 . ItemContainerGenerator . StatusChanged += ItemContainerGenerator_StatusChanged;
                        expander . IsExpanded = false;
                        expander2 . IsExpanded = false;
                        ImageTest1.Refresh();
                }

                #region Dependency properties

                #region ShowBorder

                public Visibility ShowBorder
                {
                        get
                        {
                                return ( Visibility ) GetValue ( ShowBorderProperty );
                        }

                        set
                        {
                                SetValue ( ShowBorderProperty, value );
                                //                                ButtonText . Refresh ( );
                        }
                }

                public static readonly DependencyProperty ShowBorderProperty =
                        DependencyProperty . Register ( "ShowBorder",
                        typeof ( Visibility ),
                        typeof ( AnimationTest ),
                        new PropertyMetadata ( Visibility . Visible ) );

                #endregion ShowBorder

                #region TextHeight

                /// <summary>
                /// Size of the button text we store for use elsewhere
                /// </summary>
                public int TextHeight
                {
                        get
                        {
                                return ( int ) GetValue ( TextHeightProperty );
                        }

                        set
                        {
                                SetValue ( TextHeightProperty, value );
                                this . Refresh ( );
                        }
                        //set{}
                }

                public static readonly DependencyProperty TextHeightProperty =
                        DependencyProperty . Register ( "TextHeightProperty",
                        typeof ( int ),
                        typeof ( AnimationTest ),
                        new PropertyMetadata ( 40 ) );

                #endregion TextHeight

                #region TextHeightScale

                /// <summary>
                /// Set to a value of  -x to +x to shrink or stretch text on a button
                /// normally range is between 0 & 1
                /// </summary>
                public double TextHeightScale
                {
                        get
                        {
                                return ( double ) GetValue ( TextHeightScaleProperty );
                        }

                        //set { }
                        set
                        {
                                SetValue ( TextHeightScaleProperty, value );
                                this . Refresh ( );
                        }
                }

                public static readonly DependencyProperty TextHeightScaleProperty =
                        DependencyProperty . Register ( "TextHeightScale",
                        typeof ( double ),
                        typeof ( AnimationTest ),
                        new PropertyMetadata ( ( double ) 1 ), OnTextHeightScalePropertyPropertyChanged );

                private static bool OnTextHeightScalePropertyPropertyChanged ( object value )
                {
                        //			Console . WriteLine ( $"TextHeightScaleProperty = {value}" );

                        return true;
                }

                #endregion TextHeightScale

                #region TextTopZero

                /// <summary>
                /// Size of the button text we store for use elsewhere
                /// </summary>
                public Thickness TextTopZero
                {
                        get
                        {
                                return ( Thickness ) GetValue ( TextTopZeroProperty );
                        }

                        set
                        {
                                SetValue ( TextTopZeroProperty, value );
                                this . Refresh ( );
                        }
                        //set{}
                }

                public static readonly DependencyProperty TextTopZeroProperty =
                        DependencyProperty . Register ( "TextTopZero",
                        typeof ( Thickness ),
                        typeof ( AnimationTest ),
                        new PropertyMetadata ( new Thickness ( 0, 0, 0, 0 ) ) );

                #endregion TextTopZero

                #region TextWidthScale

                /// <summary>
                /// Set to a value of  -x to +x to shrink or stretch text on a button
                /// normally range is between 0 & 1
                /// </summary>
                public double TextWidthScale
                {
                        get
                        {
                                return ( double ) GetValue ( TextWidthScaleProperty );
                        }

                        set
                        {
                                SetValue ( TextWidthScaleProperty, value );
                                this . Refresh ( );
                        }
                }

                public static readonly DependencyProperty TextWidthScaleProperty =
                        DependencyProperty . Register ( "TextWidthScale",
                        typeof ( double ),
                        typeof ( AnimationTest ),
                        new PropertyMetadata ( ( double ) 1.0 ), OnTextWidthScalePropertyChanged );

                private static bool OnTextWidthScalePropertyChanged ( object value )
                {
                        //			Console . WriteLine ( $"TextWidthScaleProperty   = {value}" );
                        return true;
                }

                #endregion TextWidthScale

                #region TextShadowColor (UNUSED)

                //public Color TextShadowColor
                //{
                //        get
                //        {
                //                return ( Color ) GetValue ( TextShadowColorProperty );
                //        }

                //        set
                //        {
                //                SetValue ( TextShadowColorProperty, value );
                //                this . Refresh ( );
                //        }
                //        //set{}
                //}

                //public static readonly DependencyProperty TextShadowColorProperty =
                //        DependencyProperty . Register ( "TextShadowColor",
                //        typeof ( Color ),
                //        typeof ( AnimationTest ),
                //        new PropertyMetadata ( Colors . DarkGray ) );

                #endregion TextShadowColor

                #region TextShadowOpacity (UNUSED)

                //public double TextShadowOpacity
                //{
                //        get
                //        {
                //                return ( double ) GetValue ( TextShadowOpacitProperty );
                //        }

                //        set
                //        {
                //                SetValue ( TextShadowOpacitProperty, value );
                //                this . Refresh ( );
                //        }
                //}

                //public static readonly DependencyProperty TextShadowOpacitProperty =
                //        DependencyProperty . Register ( "TextShadowOpacity",
                //        typeof ( double ),
                //        typeof ( AnimationTest ),
                //        new PropertyMetadata ( ( double ) 0.5 ), OnTextShadowOpacityProperty );

                //private static bool OnTextShadowOpacityProperty ( object value )
                //{
                //        return true;
                //}

                #endregion TextShadowOpacity

                #region TextShadowRadius (UNUSED)

                //public double TextShadowRadius
                //{
                //        get
                //        {
                //                return ( double ) GetValue ( TextShadowRadiusProperty );
                //        }

                //        //set { }
                //        set
                //        {
                //                SetValue ( TextShadowRadiusProperty, value );
                //                this . Refresh ( );
                //        }
                //}

                //public static readonly DependencyProperty TextShadowRadiusProperty =
                //        DependencyProperty . Register ( "TextShadowRadius",
                //        typeof ( double ),
                //        typeof ( AnimationTest ),
                //        new PropertyMetadata ( ( double ) 1 ), OnTextShadowRadiusProperty );

                //private static bool OnTextShadowRadiusProperty ( object value )
                //{
                //        return true;
                //}

                #endregion TextShadowRadius

                #region TextShadowSize (UNUSED)
                //public double TextShadowSize
                //{
                //        get
                //        {
                //                return ( double ) GetValue ( TextShadowSizeProperty );
                //        }

                //        //set { }
                //        set
                //        {
                //                SetValue ( TextShadowSizeProperty, value );
                //                this . Refresh ( );
                //        }
                //}

                //public static readonly DependencyProperty TextShadowSizeProperty =
                //        DependencyProperty . Register ( "TextShadowSize",
                //        typeof ( double ),
                //        typeof ( AnimationTest ),
                //        new PropertyMetadata ( ( double ) 2 ), OnTextShadowSizePropertyProperty );

                //private static bool OnTextShadowSizePropertyProperty ( object value )
                //{
                //        //			Console . WriteLine ( $"ShadowBlurSizeProperty = {value}" );

                //        return true;
                //}

                #endregion TextShadowSize

                #region TextShadowDirection (UNUSED)

                //public double TextShadowDirection
                //{
                //        get
                //        {
                //                return ( double ) GetValue ( TextShadowDirectionProperty );
                //        }

                //        //set { }
                //        set
                //        {
                //                SetValue ( TextShadowDirectionProperty, value );
                //                this . Refresh ( );
                //        }
                //}

                //public static readonly DependencyProperty TextShadowDirectionProperty =
                //        DependencyProperty . Register ( "TextShadowDirection",
                //        typeof ( double ),
                //        typeof ( AnimationTest ),
                //        new PropertyMetadata ( ( double ) 330 ), OnTextShadowDirectionPropertyProperty );

                //private static bool OnTextShadowDirectionPropertyProperty ( object value )
                //{
                //        return true;
                //}

                #endregion TextShadowDirection

                #endregion Dependency properties


                #region Load NW Data from SQl server

                public ObservableCollection<nwcustomer> Loadcustomers ( )
                {
                        DataTable dt = new DataTable ( "Customers" );
                        string ConString = ( string ) Properties . Settings . Default [ "NorthwindConnectionString" ];

                        string CmdString = string . Empty;
                        try
                        {
                                using ( SqlConnection con = new SqlConnection ( ConString ) )
                                {
                                        CmdString = $"SELECT *  FROM Customers ";
                                        SqlCommand cmd = new SqlCommand ( CmdString, con );
                                        SqlDataAdapter sda = new SqlDataAdapter ( cmd );
                                        sda . Fill ( dt );
                                }
                        }
                        catch ( Exception ex )
                        {
                                Debug . WriteLine ( $"Data={ex . Data}, {ex . Message}\n[{CmdString}]" );
                        }
                        CreateCustCollection ( dt );
                        return nwc;
                }

                public bool CreateCustCollection ( DataTable dt )
                {
                        int count = 0;
                        try
                        {
                                for ( int i = 0 ; i < dt . Rows . Count ; i++ )
                                {
                                        nwc . Add ( new nwcustomer
                                        {
                                                CustomerId = dt . Rows [ i ] [ 0 ] . ToString ( ),
                                                CompanyName = dt . Rows [ i ] [ 1 ] . ToString ( ),
                                                ContactName = dt . Rows [ i ] [ 2 ] . ToString ( ),
                                                ContactTitle = dt . Rows [ i ] [ 3 ] . ToString ( ),
                                                Address = dt . Rows [ i ] [ 4 ] . ToString ( ),
                                                Fax = dt . Rows [ i ] [ 10 ] . ToString ( ),
                                                Region = dt . Rows [ i ] [ 6 ] . ToString ( ),
                                                Country = dt . Rows [ i ] [ 8 ] . ToString ( ),
                                                PostalCode = dt . Rows [ i ] [ 7 ] . ToString ( ),
                                                Phone = dt . Rows [ i ] [ 9 ] . ToString ( ),
                                                City = dt . Rows [ i ] [ 5 ] . ToString ( ),
                                        } );
                                        count = i;
                                }
                        }
                        catch ( Exception ex )
                        {
                                Debug . WriteLine ( $"DETAILS : ERROR in  LoadDetCollection() : loading Details into ObservableCollection \"DetCollection\" : [{ex . Message}] : {ex . Data} ...." );
                                MessageBox . Show ( $"DETAILS : ERROR in  LoadDetCollection() : loading Details into ObservableCollection \"DetCollection\" : [{ex . Message}] : {ex . Data} ...." );
                                return false;
                        }
                        finally
                        {
                        }
                        return true;
                }

                #endregion Load NW Data from SQl server

                private void Timer_Tick ( object sender, EventArgs e )
                {
                        // this is called every millisecond by dispatchTimer
                        if ( timercount >= long . MaxValue )
                                timercount = 0;
                        timercount++;
                        //			Console . WriteLine ( $"{timercount}" );
                }


                private void TestRectangle_Copy3_MouseLeftButtonDown ( object sender, MouseButtonEventArgs e )
                {
                        this . Close ( );
                }

                #region menu Bar Control

                /// <summary>
                /// Makes the Button menu disappear/Reappear to/from bottom right
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                private void Image_MouseLeftButtonDown ( object sender, MouseButtonEventArgs e )
                {
                        Storyboard s = null;
                        if ( counter == 0 )
                        {
                                //Hide menu
                                s = ( Storyboard ) TryFindResource ( "SlideStackPanelRight" );
                                var uri = new Uri ( "/Icons/left-arrow.png", UriKind . Relative );
                                var bitmap = new BitmapImage ( uri );
                                this . OpenCloseButton . Source = bitmap;
                                this . myimage . Visibility = Visibility . Visible;
                                this . OpenCloseButton . Visibility = Visibility . Hidden;
                                this . myimage2 . Visibility = Visibility . Visible;
                                this . OpenButton . Visibility = Visibility . Visible;
                                counter++;
                        }
                        else
                        {
                                //Show menu
                                s = ( Storyboard ) TryFindResource ( "SlideStackPanelLeft" );
                                var uri = new Uri ( "/Icons/right-arrow.png", UriKind . Relative );
                                var bitmap = new BitmapImage ( uri );
                                this . OpenCloseButton . Source = bitmap;
                                this . myimage . Visibility = Visibility . Hidden;
                                this . OpenCloseButton . Visibility = Visibility . Visible;
                                this . myimage2 . Visibility = Visibility . Hidden;
                                this . OpenButton . Visibility = Visibility . Hidden;
                                //				//				RectbuttonStackPanel . RenderTransform = mySliderLeft;
                                //				mySliderLeft . Begin ( this, true );
                                //				s = ( Storyboard ) TryFindResource ( "mySliderLeft" );
                                counter = 0;
                        }
                        try
                        {
                                if ( s != null )
                                        s . Begin ( );
                        }
                        catch
                        {
                        }
                        return;
                }

                #endregion menu Bar Control

                #region TestButton functionality

                private void Testbutton_MouseEnter ( object sender, MouseEventArgs e )
                {
                        //Button b = sender as Button;
                        //if ( b != null )
                        //	b . Background = ( SolidColorBrush ) FindResource ( "Magenta2" );
                        //else
                        //{
                        //	Rectangle r = sender as Rectangle;
                        //	if ( r != null )
                        //		r . Fill = ( SolidColorBrush ) FindResource ( "Magenta2" );
                        //}
                }

                private void Testbutton_MouseLeave ( object sender, MouseEventArgs e )
                {
                        //Button b = sender as Button;
                        //if ( b != null )
                        //	b . Background = ( SolidColorBrush ) FindResource ( "Red3" );
                        //else
                        //{
                        //	Rectangle r = sender as Rectangle;
                        //	if ( r != null )
                        //		r . Fill = ( SolidColorBrush ) FindResource ( "Red3" );
                        //}
                }

                private void Xscale_Click ( object sender, RoutedEventArgs e )
                {
                        //Testbuton functionality
                }

                #endregion TestButton functionality

                private void AnimWin_Loaded ( object sender, RoutedEventArgs e )
                {
                        this . LeftMenuTogglePanel . Opacity = 0.0;
                }

                
                private void TestEllipse_Click ( object sender, RoutedEventArgs e )
                {
                        System . Diagnostics . Debugger . Break ( );
                }

                private void Btn2_Click ( object sender, RoutedEventArgs e )
                {
                        this . Close ( );
                }

                #region scale changing

                //		private void TestRectangle_MouseLeftButtonUp ( object sender, MouseButtonEventArgs e )
                //		{
                ////			Btn1 . TextWidthScale += 0.1;
                //			this . TestEllipse . TextWidthScale += 0.1;
                //			this . TestEllipse_Copy . TextWidthScale += 0.1;
                //			this . Btn2_Copy . TextWidthScale += 0.1;
                ////			this . ScaleDimensions . Text = $"{this . Btn1 . TextHeightScale } : {this . Btn1 . TextWidthScale }";
                //		}

                private void TextScaleDn ( object sender, MouseButtonEventArgs e )
                {
                        this . ToolBtn1 . TextWidthScale -= 0.1;
                        this . TestEllipse . TextWidthScale -= 0.1;
                        this . TestEllipse_Copy . TextWidthScale -= 0.1;
                        this . Btn2_Copy . TextWidthScale -= 0.1;
                        this . ScaleDimensions . Text = $"{this . ToolBtn1 . TextHeightScale } : {this . ToolBtn1 . TextWidthScale }";
                }

                private void TextScaleUp ( object sender, MouseButtonEventArgs e )
                {
                        this . ToolBtn1 . TextWidthScale += 0.1;
                        this . TestEllipse . TextWidthScale += 0.1;
                        this . TestEllipse_Copy . TextWidthScale += 0.1;
                        this . Btn2_Copy . TextWidthScale += 0.1;
                        this . ScaleDimensions . Text = $"{this . ToolBtn1 . TextHeightScale } : {this . ToolBtn1 . TextWidthScale }";
                }

                private void TextHeightScaleUp ( object sender, MouseButtonEventArgs e )
                {
                        this . ToolBtn1 . TextHeightScale += 0.1;
                        this . TestEllipse . TextHeightScale += 0.1;
                        this . TestEllipse_Copy . TextHeightScale += 0.1;
                        this . Btn2_Copy . TextHeightScale += 0.1;
                        this . ScaleDimensions . Text = $"{this . ToolBtn1 . TextHeightScale } : {this . ToolBtn1 . TextWidthScale }";
                }

                private void TextHeightScaleDn ( object sender, MouseButtonEventArgs e )
                {
                        this . ToolBtn1 . TextHeightScale -= 0.1;
                        this . TestEllipse . TextHeightScale -= 0.1;
                        this . TestEllipse_Copy . TextHeightScale -= 0.1;
                        this . Btn2_Copy . TextHeightScale -= 0.1;
                        this . ScaleDimensions . Text = $"{this . ToolBtn1 . TextHeightScale } : {this . ToolBtn1 . TextWidthScale }";
                }

                #endregion scale changing

                #region Drag functionality

                private void DragEllipse ( object sender, MouseButtonEventArgs e )
                {
                        MousePosition = Mouse . GetPosition ( LeftPanel );
                        MouseDn = true;
                }

                private void Btn1_MouseMove ( object sender, MouseEventArgs e )
                {
                        if ( MouseDn )
                        {
                                ThreeDeeBtnControl tdbc = sender as ThreeDeeBtnControl;
                                if ( tdbc == null )
                                        return;
                        }
                }

                private void DrqagEllipseStop ( object sender, MouseButtonEventArgs e )
                {
                        MouseDn = false;
                }

                #endregion Drag functionality

                private void TextHeightChange ( object sender, MouseButtonEventArgs e )
                {
                        //Rectangle r = sender as Rectangle;
                        //TestRectangle_Copy4 . Visibility = Visibility . Hidden;
                        //TextHeightSettings . Visibility = Visibility . Visible;
                        //TextHeightSettings . Text = Btn1 . Height . ToString ( );
                        ////TextHeightSettings . Text.
                        //TextHeightSettings . Focus ( );
                }

                private void TextHeightSettings_KeyDown ( object sender, KeyEventArgs e )
                {
                        if ( e . Key == Key . Enter )
                        {
                                //int val = Convert.ToInt32(TextHeightSettings.Text);
                                ////				Btn1 . TextHeight = val;
                                ////				Btn1 . FontSize = val;
                                //if ( val < 30 )
                                //	val = 30;
                                //Btn1 . Height = val;
                                //Btn1 . Refresh ( );
                                //TextHeightSettings . Visibility = Visibility . Hidden;
                                //TestRectangle_Copy4 . Visibility = Visibility . Visible;
                        }
                }

                private void Togglepanel ( object sender, MouseButtonEventArgs e )
                {
                        if ( LeftPanel . Visibility == Visibility . Visible )
                        {
                                this . LeftPanel . Visibility = Visibility . Hidden;
                                this . TabControlPanel . Visibility = Visibility . Visible;
                                //You can use this format almost anywhere to changfe a Dependency Poperty
                        }
                        else
                        {
                                this . LeftPanel . Visibility = Visibility . Visible;
                                this . TabControlPanel . Visibility = Visibility . Hidden;
                        }
                }

                private void Toggle_Click ( object sender, RoutedEventArgs e )
                {
                        if ( this . LeftPanel . Visibility == Visibility . Visible )
                        {
                                this . LeftPanel . Visibility = Visibility . Hidden;
                                this . TabControlPanel . Visibility = Visibility . Visible;
                        }
                        else
                        {
                                this . LeftPanel . Visibility = Visibility . Visible;
                                this . TabControlPanel . Visibility = Visibility . Hidden;
                        }
                }

                private void Image_Click ( object sender, MouseButtonEventArgs e )
                {
                        this . Close ( );
                }

                private void Tb1_MouseLeftButtonDown ( object sender, MouseButtonEventArgs e )
                {
                        if ( this . SlideOutMenu . Width > 0 )
                        {
                                Storyboard s = ( Storyboard ) TryFindResource ( "HideLeftMenu" );
                                s . Begin ( );
                                //				SlideOutMenu . Visibility = Visibility . Visible;
                                this . Tb1 . Visibility = Visibility . Visible;
                              //  this . SlideOutMenu . Visibility = Visibility . Hidden;
                        }
                        else
                        {
                                this . SlideOutMenu . Visibility = Visibility . Visible;
                                Storyboard s = ( Storyboard ) TryFindResource ( "ShowLeftMenu" );
                                s . Begin ( );
                                //				SlideOutMenu . Visibility = Visibility . Collapsed;
                                this . Tb1 . Visibility = Visibility . Hidden;
                        }
                }

                private void Menu0_Click ( object sender, MouseButtonEventArgs e )
                {
                }

                private void Menu1_Click ( object sender, MouseButtonEventArgs e )
                {
                        this . Panel2 . Visibility = Visibility . Hidden;
                        //			this . Panel3 . Visibility = Visibility . Hidden;
                        this . Panel4 . Visibility = Visibility . Hidden;
                        this . TabControlPanel . Visibility = Visibility . Hidden;
                        this . ThreeDButtonsPanel . Visibility = Visibility . Hidden;

                        this . Panel1 . Visibility = Visibility . Visible;
                        this . Title = "Panel1 visible";
                }

                private void Menu2_Click ( object sender, MouseButtonEventArgs e )
                {
                        this . Panel1 . Visibility = Visibility . Hidden;
                        //			this . Panel3 . Visibility = Visibility . Hidden;
                        this . Panel4 . Visibility = Visibility . Hidden;
                        this . TabControlPanel . Visibility = Visibility . Hidden;
                        this . ThreeDButtonsPanel . Visibility = Visibility . Hidden;

                        this . Panel2 . Visibility = Visibility . Visible;
                        this . Title = "Panel2 visible";
                }

                private void Menu3_Click ( object sender, MouseButtonEventArgs e )
                {
                        this . Panel1 . Visibility = Visibility . Hidden;
                        this . Panel2 . Visibility = Visibility . Hidden;
                        this . Panel4 . Visibility = Visibility . Hidden;
                        this . TabControlPanel . Visibility = Visibility . Hidden;
                        this . ThreeDButtonsPanel . Visibility = Visibility . Hidden;

                        //			this . Panel3 . Visibility = Visibility . Visible;
                        Title = "Panel3Canvas visible";
                }

                private void Menu4_Click ( object sender, MouseButtonEventArgs e )
                {
                        this . Panel1 . Visibility = Visibility . Hidden;
                        this . Panel2 . Visibility = Visibility . Hidden;
                        //			this . Panel3 . Visibility = Visibility . Hidden;
                        this . Panel4 . Visibility = Visibility . Hidden;
                        this . ThreeDButtonsPanel . Visibility = Visibility . Hidden;

                        this . TabControlPanel . Visibility = Visibility . Visible;
                        this . LeftslidingMenuMarker . Visibility = Visibility . Visible;
                        this . Title = "TabControlPanel visible";
                }

                private void Menu5_Click ( object sender, MouseButtonEventArgs e )
                {
                        this . Panel1 . Visibility = Visibility . Hidden;
                        this . Panel2 . Visibility = Visibility . Hidden;
                        //			this . Panel3 . Visibility = Visibility . Hidden;
                        this . Panel4 . Visibility = Visibility . Hidden;
                        this . TabControlPanel . Visibility = Visibility . Hidden;

                        this . ThreeDButtonsPanel . Visibility = Visibility . Visible;

                        this . Title = "ThreeDButtonsPanel visible";
                }

                private void Menu6_Click ( object sender, MouseButtonEventArgs e )
                {
                        this . Panel1 . Visibility = Visibility . Hidden;
                        this . Panel2 . Visibility = Visibility . Hidden;
                        //			this . Panel3 . Visibility = Visibility . Hidden;
                        this . TabControlPanel . Visibility = Visibility . Hidden;
                        this . ThreeDButtonsPanel . Visibility = Visibility . Hidden;

                        this . Panel4 . Visibility = Visibility . Visible;
                        this . Title = "Panel 4 visible";
                }

                private void Menu7_Click ( object sender, MouseButtonEventArgs e )
                {
                        this . Panel1 . Visibility = Visibility . Hidden;
                        this . Panel2 . Visibility = Visibility . Hidden;
                        //			this . Panel3 . Visibility = Visibility . Hidden;
                        this . Panel4 . Visibility = Visibility . Hidden;
                        this . TabControlPanel . Visibility = Visibility . Hidden;
                        this . ThreeDButtonsPanel . Visibility = Visibility . Hidden;
                        this . Title = "Unused1 visible";
                }

                private void Expand_MenuItemBorder ( object sender, MouseEventArgs e )
                {
                        //Thickness t = new Thickness ( );
                        //Border b = sender as Border;
                        //b . Height = 120;
                        //if ( b . Name == "Menu6" )
                        //{
                        //	t = tb6 . Margin;
                        //	t . Top = 0;
                        //	//				tb6 . Margin = t;
                        //	TextTopZero = t;
                        //}
                }

                private void Expand_MenuItemTextblock ( object sender, MouseEventArgs e )
                {
                        TextBlock b = sender as TextBlock;
                        b . Height = 120;
                }

                private void Contract_Textblock1 ( object sender, MouseEventArgs e )
                {
                        TextBlock b = sender as TextBlock;
                        b . Height = 40;
                }

                private void Contract_Textblock2 ( object sender, MouseEventArgs e )
                {
                        TextBlock b = sender as TextBlock;
                        b . Height = 40;
                }

                private void Contract_Textblock3 ( object sender, MouseEventArgs e )
                {
                        TextBlock b = sender as TextBlock;
                        b . Height = 40;
                }

                private void Contract_Textblock4 ( object sender, MouseEventArgs e )
                {
                        TextBlock b = sender as TextBlock;
                        b . Height = 40;
                }

                private void Contract_Textblock5 ( object sender, MouseEventArgs e )
                {
                        TextBlock b = sender as TextBlock;
                        b . Height = 40;
                }

                private void Contract_Textblock7 ( object sender, MouseEventArgs e )
                {
                        TextBlock b = sender as TextBlock;
                        b . Height = 40;
                }

                private static void GetPointerPosition ( AnimationTest AnimWin, Canvas LeftPanel, Border Menu1 )
                {
                        currentwinpos = Mouse . GetPosition ( AnimWin );
                        currentCtrlpos = Mouse . GetPosition ( LeftPanel );
                        menupos = Mouse . GetPosition ( Menu1 );
                        if ( menupos . Y < 10 )
                        {
                                menupos . Y += Menu1 . ActualHeight / 2;
                                currentCtrlpos . Y = Menu1 . ActualHeight / 2;
                        }
                }

                private void SetPointerPosition ( AnimationTest AnimWin, Canvas LeftPanel, Border Menu1 )
                {
                        currentCtrlpos . Y += menupos . Y;
                        currentwinpos = PointToScreen ( currentCtrlpos );
                        Point newpos = new Point ( 0, 0 );
                        newpos . X = ( int ) currentwinpos . X;
                        newpos . Y = ( int ) currentwinpos . Y + 20;//
                        Console . WriteLine ( $"win= {currentwinpos . X}:{currentwinpos . Y}\nMenu1 = {menupos . X} : {menupos . Y}\nnewpos = {newpos . X} : {newpos . Y}\n" );
                        //			NativeMethods . SetCursorPos ( ( int ) newpos . X, ( int ) newpos . Y );
                }

                private void Expand_MenuItemBorder2 ( object sender, MouseEventArgs e )
                {
                        //defaultMenuheight = ( int ) Menu2 . ActualHeight;
                        ////			GetPointerPosition ( AnimWin, LeftPanel, Menu2 );
                        //Border b = sender as Border;
                        //TextBlock t = new TextBlock ( );
                        //if ( b != null )
                        //{
                        //	defaultMenuheight = ( int ) b . Height;
                        //	Expanded ( b, tb2, Menu2Text, dummymenutext2 );
                        //}
                        //else
                        //{
                        //	t = sender as TextBlock;
                        //	defaultMenuheight = ( int ) t . Height;
                        //	Expanded ( Menu2, t, Menu2Text, dummymenutext2 );
                        //}
                        //			ShowMenuOptions ( null, false );
                }

                private void Contract_Border2 ( object sender, MouseEventArgs e )
                {
                        //Border b = sender as Border;
                        //TextBlock t = new TextBlock ( );
                        //if ( b != null )
                        //{
                        //	if ( CheckCallerOpen ( Menu2 ) == false )
                        //		return;
                        //	Contract ( b, tb2, Menu2Text );
                        //	//				Menu2 . Height = defaultMenuheight;
                        //	Menu2 . Refresh ( );
                        //}
                        //else
                        //{
                        //	if ( CheckCallerOpen ( Menu2 ) == false )
                        //		return;
                        //	t = sender as TextBlock;
                        //	Contract ( Menu2, t, Menu2Text );
                        //	//				Menu2 . Height = defaultMenuheight;
                        //	Menu2 . Refresh ( );
                        //}
                        ////			ShowMenuOptions ( Menu2, false );
                }

                private void Expand_MenuItemBorder3 ( object sender, MouseEventArgs e )
                {
                        //defaultMenuheight = ( int ) Menu3 . ActualHeight;
                        ////			GetPointerPosition ( AnimWin, LeftPanel, Menu3 );
                        //Border b = sender as Border;
                        //TextBlock t = new TextBlock ( );
                        //if ( b != null )
                        //{
                        //	defaultMenuheight = ( int ) b . Height;
                        //	Expanded ( b, tb3, Menu3Text, dummymenutext3 );
                        //}
                        //else
                        //{
                        //	t = sender as TextBlock;
                        //	defaultMenuheight = ( int ) t . Height;
                        //	Expanded ( Menu3, t, Menu3Text, dummymenutext3 );
                        //}
                        ////			ShowMenuOptions ( Menu3, true );
                }

                private void Contract_Border3 ( object sender, MouseEventArgs e )
                {
                        //Border b = sender as Border;
                        //TextBlock t = new TextBlock ( );
                        //if ( b != null )
                        //{
                        //	if ( CheckCallerOpen ( Menu3 ) == false )
                        //		return;
                        //	Contract ( b, tb3, Menu3Text );
                        //	//				Menu3 . Height = defaultMenuheight;
                        //	Menu3 . Refresh ( );
                        //}
                        //else
                        //{
                        //	if ( CheckCallerOpen ( Menu3 ) == false )
                        //		return;
                        //	t = sender as TextBlock;
                        //	Contract ( Menu3, t, Menu3Text );
                        //	//				Menu3 . Height = defaultMenuheight;
                        //	Menu3 . Refresh ( );
                        //}
                        ////			ShowMenuOptions ( Menu3, false );
                }

                private void Expand_MenuItemBorder4 ( object sender, MouseEventArgs e )
                {
                        //defaultMenuheight = ( int ) Menu4 . ActualHeight;
                        ////			GetPointerPosition ( AnimWin, LeftPanel, Menu4 );
                        //Border b = sender as Border;
                        //TextBlock t = new TextBlock ( );
                        //if ( b != null )
                        //{
                        //	defaultMenuheight = ( int ) b . Height;
                        //	Expanded ( b, tb4, Menu4Text, dummymenutext4 );
                        //}
                        //else
                        //{
                        //	t = sender as TextBlock;
                        //	defaultMenuheight = ( int ) t . Height;
                        //	Expanded ( Menu4, t, Menu4Text, dummymenutext4 );
                        //}
                        //			ShowMenuOptions ( Menu4, true );
                }

                private void Contract_Border4 ( object sender, MouseEventArgs e )
                {
                        //Border b = sender as Border;
                        //TextBlock t = new TextBlock ( );
                        //if ( b != null )
                        //{
                        //	if ( CheckCallerOpen ( Menu4 ) == false )
                        //		return;
                        //	Contract ( b, tb4, Menu4Text );
                        //	//				Menu4 . Height = defaultMenuheight;
                        //	Menu4 . Refresh ( );
                        //}
                        //else
                        //{
                        //	if ( CheckCallerOpen ( Menu4 ) == false )
                        //		return;
                        //	t = sender as TextBlock;
                        //	Contract ( Menu4, t, Menu4Text );
                        //	//				Menu4 . Height = defaultMenuheight;
                        //	Menu4 . Refresh ( );
                        //}
                        //			ShowMenuOptions ( Menu4, false );
                }

                private void Expand_MenuItemBorder5 ( object sender, MouseEventArgs e )
                {
                        //defaultMenuheight = ( int ) Menu5 . ActualHeight;
                        ////			GetPointerPosition ( AnimWin, LeftPanel, Menu5 );
                        //Border b = sender as Border;
                        //TextBlock t = new TextBlock ( );
                        //if ( b != null )
                        //{
                        //	defaultMenuheight = ( int ) b . Height;
                        //	Expanded ( b, tb5, Menu5Text, dummymenutext5 );
                        //}
                        //else
                        //{
                        //	t = sender as TextBlock;
                        //	defaultMenuheight = ( int ) t . Height;
                        //	Expanded ( Menu5, t, Menu5Text, dummymenutext5 );
                        //}
                        ////			ShowMenuOptions ( Menu5, true );
                }

                private void Contract_Border5 ( object sender, MouseEventArgs e )
                {
                        //Border b = sender as Border;
                        //TextBlock t = new TextBlock ( );
                        //if ( b != null )
                        //{
                        //	if ( CheckCallerOpen ( Menu5 ) == false )
                        //		return;
                        //	Contract ( b, tb5, Menu5Text );
                        //	//				Menu5 . Height = defaultMenuheight;
                        //	Menu5 . Refresh ( );
                        //}
                        //else
                        //{
                        //	if ( CheckCallerOpen ( Menu5 ) == false )
                        //		return;
                        //	t = sender as TextBlock;
                        //	Contract ( Menu5, t, Menu5Text );
                        //	//				Menu5 . Height = defaultMenuheight;
                        //	Menu5 . Refresh ( );
                        //}
                        ////			ShowMenuOptions ( Menu5, false );
                }

                private void Expand_MenuItemBorder6 ( object sender, MouseEventArgs e )
                {
                        //defaultMenuheight = ( int ) Menu6 . ActualHeight;
                        ////			GetPointerPosition ( AnimWin, LeftPanel, Menu6 );
                        //Border b = sender as Border;
                        //TextBlock t = new TextBlock ( );
                        //if ( b != null )
                        //{
                        //	defaultMenuheight = ( int ) b . Height;
                        //	Expanded ( b, tb6, Menu6Text, dummymenutext6 );
                        //}
                        //else
                        //{
                        //	t = sender as TextBlock;
                        //	defaultMenuheight = ( int ) t . Height;
                        //	Expanded ( Menu6, t, Menu6Text, dummymenutext6 );
                        //}
                        ////			ShowMenuOptions ( Menu6, true );
                }

                private void Contract_Border6 ( object sender, MouseEventArgs e )
                {
                        //Border b = sender as Border;
                        //TextBlock t = new TextBlock ( );
                        //if ( b != null )
                        //{
                        //	if ( CheckCallerOpen ( Menu6 ) == false )
                        //		return;
                        //	Contract ( b, tb6, Menu6Text );
                        //	Menu6 . Refresh ( );
                        //}
                        //else
                        //{
                        //	if ( CheckCallerOpen ( Menu6 ) == false )
                        //		return;
                        //	t = sender as TextBlock;
                        //	Contract ( Menu6, t, Menu6Text );
                        //	Menu6 . Refresh ( );
                        //}
                        //Thread . Sleep ( 100 );
                        //			ShowMenuOptions ( Menu6, false );
                }

                private void Expand_MenuItemBorder7 ( object sender, MouseEventArgs e )
                {
                        //defaultMenuheight = ( int ) Menu7 . ActualHeight;
                        ////			GetPointerPosition ( AnimWin, LeftPanel, Menu7 );
                        //Border b = sender as Border;
                        //TextBlock t = new TextBlock ( );
                        //if ( b != null )
                        //{
                        //	defaultMenuheight = ( int ) b . Height;
                        //	Expanded ( b, tb7, Menu7Text, dummymenutext7 );
                        //}
                        //else
                        //{
                        //	t = sender as TextBlock;
                        //	defaultMenuheight = ( int ) t . Height;
                        //	Expanded ( Menu7, t, Menu7Text, dummymenutext7 );
                        //}
                        //			ShowMenuOptions ( Menu7, true );
                }

                private void Contract_Border7 ( object sender, MouseEventArgs e )
                {
                        //Border b = sender as Border;
                        //TextBlock t = new TextBlock ( );
                        //if ( b != null )
                        //{
                        //	if ( CheckCallerOpen ( Menu7 ) == false )
                        //		return;
                        //	Contract ( b, tb7, Menu7Text );
                        //	Menu7 . Refresh ( );
                        //}
                        //else
                        //{
                        //	if ( CheckCallerOpen ( Menu7 ) == false )
                        //		return;
                        //	t = sender as TextBlock;
                        //	Contract ( Menu7, t, Menu7Text );
                        //	Menu7 . Refresh ( );
                        //}
                        //Thread . Sleep ( 100 );
                        //			ShowMenuOptions ( Menu7, false );
                }

                private void Tb1_Mouseover ( object sender, MouseEventArgs e )
                {
                        if ( SlideOutMenu . Width > 0 )
                        {
                                Storyboard s = ( Storyboard ) TryFindResource ( "HideLeftMenu" );
                                s . Begin ( );
                                Tb1 . Visibility = Visibility . Visible;
                        }
                        else
                        {
                                Storyboard s = ( Storyboard ) TryFindResource ( "ShowLeftMenu" );
                                s . Begin ( );
                                Tb1 . Visibility = Visibility . Hidden;
                        }
                }

                private void MenuTimerCallback ( object state )
                {
                        TimeSpan ts = timer . Interval;
                        if ( timer . IsEnabled && ts . Ticks < 500 )
                        {
                                Thread . Sleep ( 200 );
                        }
                }

                private void Expand_MenuItemBorder0 ( object sender, MouseEventArgs e )
                {
                }

                private void Contract_Border0 ( object sender, MouseEventArgs e )
                {
                        //ShowMenuOptions ( null, false);
                }

                private void MenuOpen_Click ( object sender, MouseButtonEventArgs e )
                {
                        if ( Menu2 . Visibility == Visibility . Visible )
                        {
                                ShowMenuOptions ( null, false );
                                //				this.Text = "Show Menu Options";
                                this . SlideOutMenu . Background = new SolidColorBrush ( Colors . Transparent );
                                //				SlideOutMenu. Height = 25;
                        }
                        else
                        {
                                ShowMenuOptions ( null, true );
                                //				Text= "Show Menu Options";
                                //				MenuOpen . Text = "Hide Menu Options";
                                this . SlideOutMenu . Background = ( Brush ) FindResource ( "Black5" );
                                //				SlideOutMenu . Height = 500;
                        }
                }

                private void Expand_MenuItemBorderOpen ( object sender, MouseEventArgs e )
                {
                        //if ( Menu1 . Visibility == Visibility . Visible )
                        //	ShowMenuOptions ( null, false );
                        //else
                        ShowMenuOptions ( null, true );
                }

                private void Contract_BorderOpen ( object sender, MouseEventArgs e )
                {
                        ShowMenuOptions ( null, true );
                }

                private void ShowMenuOptions ( Border menu, bool open )
                {
                        if ( menu == null && open )
                        {
                                this . Menu1 . Visibility = Visibility . Visible;
                                this . Menu2 . Visibility = Visibility . Visible;
                                this . Menu3 . Visibility = Visibility . Visible;
                                this . Menu4 . Visibility = Visibility . Visible;
                                this . Menu5 . Visibility = Visibility . Visible;
                                this . Menu6 . Visibility = Visibility . Visible;
                                this . Menu7 . Visibility = Visibility . Visible;
                                return;
                        }
                        else if ( menu == null && open == false )
                        {
                                this . Menu1 . Visibility = Visibility . Collapsed;
                                this . Menu2 . Visibility = Visibility . Collapsed;
                                this . Menu3 . Visibility = Visibility . Collapsed;
                                this . Menu4 . Visibility = Visibility . Collapsed;
                                this . Menu5 . Visibility = Visibility . Collapsed;
                                this . Menu6 . Visibility = Visibility . Collapsed;
                                this . Menu7 . Visibility = Visibility . Collapsed;
                                return;
                        }
                        this . Menu1 . Visibility = Visibility . Collapsed;
                        this . Menu2 . Visibility = Visibility . Collapsed;
                        this . Menu3 . Visibility = Visibility . Collapsed;
                        this . Menu4 . Visibility = Visibility . Collapsed;
                        this . Menu5 . Visibility = Visibility . Collapsed;
                        this . Menu6 . Visibility = Visibility . Collapsed;
                        this . Menu7 . Visibility = Visibility . Collapsed;
                        this . AnimWin . Refresh ( );
                        //if ( menu == Menu1 && open )
                        //{
                        //	Menu1 . Visibility = Visibility . Visible;
                        //}
                        //if ( menu == Menu2 && open )
                        //{
                        //	Menu2 . Visibility = Visibility . Visible;
                        //}
                        //else if ( menu == Menu3 && open )
                        //{
                        //	Menu3 . Visibility = Visibility . Visible;
                        //}
                        //else if ( menu == Menu4 && open )
                        //{
                        //	Menu4 . Visibility = Visibility . Visible;
                        //}
                        //else if ( menu == Menu5 && open )

                        //{
                        //	Menu6 . Visibility = Visibility . Visible;
                        //}
                        //else if ( menu == Menu6 && open )
                        //{
                        //	Menu6 . Visibility = Visibility . Visible;
                        //}
                        //else if ( menu == Menu7 && open )
                        //{
                        //	Menu7 . Visibility = Visibility . Visible;
                        //}
                }

                private void Expanded ( Border border, TextBlock textblock, string originaltext, string newtext )
                {
                        //	if ( IsMenuOpen )
                        //		return;
                        //			if ( timer . IsRunning )
                        //				return;
                        //			timer . Start ( );
                        double dtemp = 0;
                        textblock . Text = "";
                        textblock . Height = 0;
                        textblock . Text = originaltext + newtext;
                        textblock . Refresh ( );
                        dtemp = textblock . ActualHeight + 15;
                        border . Height = dtemp;
                        textblock . Height = dtemp;
                        textblock . Refresh ( );
                        border . Height = dtemp + 15;
                        border . Refresh ( );
                        //			Thread . Sleep ( 10 );
                        Thickness t = textblock . Margin;
                        t . Top = 0;
                        t . Bottom = 0;
                        t . Left = 0;
                        t . Right = 0;
                        textblock . Margin = t;
                        textblock . Refresh ( );
                        IsMenuOpen = true;
                        border . Refresh ( );
                        Console . WriteLine ( $"Menu {border . Name} Opened ...." );
                        ReSetOpenFlags ( border, true );
                }

                private void Contract ( Border border, TextBlock textblock, string originaltext )
                {
                        //if ( !IsMenuOpen )
                        //	return;
                        //			timer . Stop ( );
                        if ( CheckCallerOpen ( border ) == false )
                                return;
                        //			if ( timer .< 10000)
                        //				return;
                        textblock . Text = "";
                        textblock . Height = 0;
                        border . Height = 0;
                        textblock . Text = originaltext;
                        textblock . Refresh ( );
                        //textblock . Height = textblock . Height;
                        border . Height = 0;
                        border . Refresh ( );
                        border . Height = textblock . ActualHeight + 15;
                        textblock . Height = border . Height;
                        textblock . Refresh ( );
                        Thickness t = textblock . Margin;
                        t . Top = 0;
                        t . Bottom = 0;
                        t . Left = 0;
                        t . Right = 0;
                        textblock . Margin = t;
                        textblock . Refresh ( );
                        IsMenuOpen = false;
                        Console . WriteLine ( $"Menu {border . Name} Closed ...." );
                        ReSetOpenFlags ( border, false );
                }

                private bool CheckCallerOpen ( Border border )
                {
                        //if ( border == Menu1 )
                        //	return Menu1Open;
                        //if ( border == Menu2 )
                        //	return Menu2Open;
                        //else if ( border == Menu3 )
                        //	return Menu3Open;
                        //else if ( border == Menu4 )
                        //	return Menu4Open;
                        //else if ( border == Menu5 )
                        //	return Menu5Open;
                        //else if ( border == Menu6 )
                        //	return Menu6Open;
                        //else if ( border == Menu7 )
                        //	return Menu7Open;
                        return false;
                }

                private void ReSetOpenFlags ( Border border, bool setting )
                {
                        if ( setting == true )
                        {
                                this . Menu1Open = false;
                                this . Menu2Open = false;
                                this . Menu3Open = false;
                                this . Menu4Open = false;
                                this . Menu5Open = false;
                                this . Menu6Open = false;
                                this . Menu7Open = false;
                        }

                        //if ( border == Menu1 )
                        //	Menu1Open = setting;
                        //if ( border == Menu2 )
                        //	Menu2Open = setting;
                        //else if ( border == Menu3 )
                        //	Menu3Open = setting;
                        //else if ( border == Menu4 )
                        //	Menu4Open = setting;
                        //else if ( border == Menu5 )
                        //	Menu5Open = setting;
                        //else if ( border == Menu6 )
                        //	Menu5Open = setting;
                        //else if ( border == Menu7 )
                        //	Menu7Open = setting;
                }

                private void SlideMenu_mouseEnter ( object sender, MouseEventArgs e )
                {
                        this . LeftMenuTogglePanel . Opacity = 1;
                }

                private void SlideMenu_mouseLeave ( object sender, MouseEventArgs e )
                {
                        this . LeftMenuTogglePanel . Opacity = 0.5;

                        //if ( LeftMenuTogglePanel . Opacity >= 1.0 )
                        //{
                        //	Storyboard s = ( Storyboard ) TryFindResource ( "FadeOutMenu" );
                        //	s . Begin ( );
                        //}
                }

                private void TogglePanes_Loaded ( object sender, RoutedEventArgs e )
                {
                }

                private void ShadowLabelClick ( object sender, MouseButtonEventArgs e )
                {
                        Console . WriteLine ( $"New LabelText Clicked" );
                }

                private void ImgButton_Loaded ( object sender, RoutedEventArgs e )
                {
                }

                private void ImgButton_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
                {
                        Console . WriteLine ( $"click passed through successfully" );
                }

                private void ImgButton_Click ( object sender, RoutedEventArgs e )
                {
                        //this . Close ( );
                } 

 
   
                private void lv1_PreviewMouseDown ( object sender, MouseButtonEventArgs e )
                {
                        //Get current index and reset color before it changes
                        //int indx = lv1 . SelectedIndex;
    
                        //if ( indx == -1 )
                        //        return;
                        //ComboChangeinProcess = true;
                        //ListBox lv = sender as ListBox;

                        //ListBoxItem lbi = ( ListBoxItem ) lv1 . ItemContainerGenerator . ContainerFromIndex ( indx) as ListBoxItem;
                        //if ( lbi != null )
                        //{
                        //        //                                lv1.Items.B
                        //        Console . WriteLine ( $"lv1 resetting foreground of previous selection: Index = {indx}" );
                        //        lbi . Background = ( Brush ) FindResource ( "Green2" );
                        //        // lbi . BorderBrush = ( Brush ) FindResource ( "Green2" );
                        //        if ( lbi . IsSelected )
                        //                lbi . Foreground = ( Brush ) FindResource ( "White0" );
                        //        else
                        //                lbi . Foreground = ( Brush ) FindResource ( "Black0" );
                        //        lbi . IsSelected = false;
                        //        lv1 . Refresh ( );
                        //        ComboChangeinProcess = false;
                        //        //Store current index
                        //}
                }

                private void ItemContainerGenerator_StatusChanged ( object sender, EventArgs e )
                {
                        //ListBoxItem lbi = ( ListBoxItem ) lv1 . ItemContainerGenerator . ContainerFromIndex ( lv1 . SelectedIndex ) as ListBoxItem;
                        //if ( lbi != null )
                        //{
                        //        //                                lv1.Items.B
                        //        Console . WriteLine ( $"lv1 Selected : lbi={lbi}, IsSelected = {lbi . IsSelected}, Index = {lv1 . SelectedIndex}" );
                        //        //  lbi . Background = ( Brush ) FindResource ( "Red5" );
                        //        lbi . Foreground = ( Brush ) FindResource ( "Magenta1" );
                        //        lv1 . Refresh ( );
                        //}
                }

       #region Expander handlers
                private void expander_Expanded ( object sender, RoutedEventArgs e )
                {
                        Expander ex = sender as Expander;
                        expander2 . Visibility = Visibility . Collapsed;
                        ex . BringIntoView ( );
                        expander . Height = 250;
                }

                private void expander_Collapsed ( object sender, RoutedEventArgs e )
                {
                        expander . Height = 45;
                        Expander ex = sender as Expander;
                        expander2 . Visibility = Visibility . Visible;
                        ex . BringIntoView ( );
                }
                private void expander2_Expanded ( object sender, RoutedEventArgs e )
                {
                        Expander ex = sender as Expander;
                        ex . BringIntoView ( );
                        expander2 . Height = 350;
                        if(grid1 != null)
                                grid1 . Height = 290;
                        if(lv1 != null)
                                lv1 . Height = 225;
                }

                private void expander2_Collapsed ( object sender, RoutedEventArgs e )
                {
                        expander2 . Height = 45;
                }

                private void cb2_SelectionChanged ( object sender, SelectionChangedEventArgs e )
                {
                        lv1 . SelectedIndex = cb2 . SelectedIndex;
                        cb1 . SelectedIndex = lv1 . SelectedIndex;
                        cb2 . SelectedIndex = lv1 . SelectedIndex;
                }

                private void cb_SelectionChanged ( object sender, SelectionChangedEventArgs e )
                {
                        int select = 0;
 //                       if ( ComboChangeinProcess )
    //                            return;
                        if ( ( ComboBox ) sender == cb1 )
                        {
                                ComboChangeinProcess = true;
                                lv1 . SelectedIndex = cb1 . SelectedIndex;
                                select = cb1 . SelectedIndex;
                                cb2 . SelectedIndex = cb1 . SelectedIndex;
                                lv1 . BringIntoView ( );
                                Utils . ScrollLBRecordIntoView ( lv1, cb1 . SelectedIndex );
                                lv1 . Refresh ( );
                        }
                        else
                        {
                                ComboChangeinProcess = true;
                                lv1 . SelectedIndex = cb2 . SelectedIndex;
                                select = cb2 . SelectedIndex;
                                cb1 . SelectedIndex = cb2 . SelectedIndex;
                                lv1 . BringIntoView ( );
                                Utils . ScrollLBRecordIntoView ( lv1, cb2 . SelectedIndex );
                        }
                        ComboChangeinProcess = false;
                        //lbi = ( ListBoxItem ) lv1. ItemContainerGenerator . ContainerFromIndex ( select ) as ListBoxItem ;
                }
                private void lv1_Selected ( object sender, SelectionChangedEventArgs e )
                {
                        // change colour of newly elected item
                        ListBox lb = sender as ListBox;
                        //ListBoxItem lbi = new ListBoxItem();

                        //// First reset last selected item
                        //lbi = ( ListBoxItem ) ( lb . ItemContainerGenerator . ContainerFromIndex ( CurrentIndex) );
                        //if ( lbi == null )
                        //        return;
                        //SolidColorBrush sb = new SolidColorBrush ( );
                        //sb = ( SolidColorBrush ) FindResource ( "Black0" );
                        //lbi . Foreground = sb;
                        //lbi . FontWeight = FontWeights . Normal;
                        //lbi . FontStretch = FontStretches .Normal;
                        //lbi . FontStyle = FontStyles .Normal;
                        //// Size does NOT work
                        //lbi . FontSize = 18;

                        ////Now update current selection
                        //lbi = ( ListBoxItem )( lb . ItemContainerGenerator . ContainerFromIndex ( lb.SelectedIndex ) );
                        //if ( lbi == null )
                        //        return;
                        //sb = new SolidColorBrush ( );
                        //sb = ( SolidColorBrush ) FindResource ( "Red3" );
                        //lbi . Foreground = ( SolidColorBrush ) FindResource ( "Red5" );
                        //lbi . FontWeight = FontWeights.Bold;
                        //lbi . FontStretch = FontStretches . SemiExpanded;
                        //lbi . FontStyle = FontStyles . Italic;
                        //// Size does NOT work
                        //lbi . FontSize = 24;

                        ComboChangeinProcess = true;
                        cb1 . SelectedIndex = lb.SelectedIndex;
                        cb2 . SelectedIndex = lb . SelectedIndex;
                        CurrentIndex = lb . SelectedIndex;
                //        if ( lbi != null )
                //        {
                //                //                                lv1.Items.B
                //                Console . WriteLine ( $"lv1 Selected : lbi={lbi}, IsSelected = {lbi . IsSelected}, Index = {lv1 . SelectedIndex}" );
                //                lbi . Background = ( Brush ) FindResource ( "Blue0" );
                //                lbi . Foreground = ( Brush ) FindResource ( "White0" );
                //                lbi . IsSelected = true;
                //                lv1 . Refresh ( );
                //        }
               }

                private void cb2_Selected ( object sender, RoutedEventArgs e )
                {
                }
                private void cb1_Loaded ( object sender, RoutedEventArgs e )
                {
                        cb1 . SelectedIndex = 0;
                }

                private void cb2_Loaded ( object sender, RoutedEventArgs e )
                {
                        cb2 . SelectedIndex = 0;
                }

                private void cb3_Loaded ( object sender, RoutedEventArgs e )
                {
                        cb3 . SelectedIndex = 0;
                }

                private void cb4_Loaded ( object sender, RoutedEventArgs e )
                {
                        cb4 . SelectedIndex = 0;
                }

                private ListBoxItem GetLbItem ( ListBox lb )
                {
                        if ( lb . Items . Count == 0 )
                                return null;

                        var index = lb . SelectedIndex;
                        if ( index < 0 )
                                return null;

                        ListBoxItem item = lb . ItemContainerGenerator . ContainerFromIndex ( index ) as ListBoxItem;
                        if ( item == ( ListBoxItem ) null )
                                return null;

                        return item;
                }

                #endregion

                private void ShadowLabelControl_Loaded ( object sender, RoutedEventArgs e )
                {

                }
        }
}
