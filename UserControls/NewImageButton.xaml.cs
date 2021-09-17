using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Diagnostics . Eventing . Reader;
using System . Linq;
using System . Security . Cryptography;
using System . Text;
using System . Threading . Tasks;
using System . Windows;
using System . Windows . Controls;
using System . Windows . Data;
using System . Windows . Documents;
using System . Windows . Input;
using System . Windows . Media;
using System . Windows . Media . Effects;
using System . Windows . Media . Imaging;
using System . Windows . Navigation;
using System . Windows . Shapes;
using System . Xml . Linq;

using WPFPages . Converts;
using WPFPages . UserControls;
using WPFPages . Views;

namespace WPFPages . UserControls
{
        /// <summary>
        /// Interaction logic for testbutton.xaml
        /// </summary>
        public partial class NewImageButton : UserControl
        {
                public NewImageButton ( )
                {
                        InitializeComponent ( );
                }
                private void Testbutton_Loaded ( object sender, RoutedEventArgs e )
                {
                        // These demonstrate how to acess DP's' in various ways

                        GetParentSize ( _border, out double dpHeight, out double dpWidth, out string parentname );
                        Console . WriteLine ( $"GETPARENTSIZE: Parent of \"_border\" is '{parentname}', Size of {parentname} is H/W {dpHeight} / {dpWidth}" );
                        GetParentObjects ( txtblock, out List<string> name, 4 );
                        Console . WriteLine ( $"GETPARENTOBJECTS : Tree of parents upwards from {name [ 0 ]} is :" );
                        for ( int x = 1 ; x < name . Count - 1 ; x++ )
                                Console . WriteLine ( $"{name [ x ]}" );
                        Console . WriteLine ( "\n" );
  
                        GetParentObjects ( BtnImage, out List<string> name2, 6 );
                        Console . WriteLine ( $"GETPARENTOBJECTS : Tree of BtnImage's parents upwards :" );
                        for ( int x = 1 ; x < name2 . Count - 1 ; x++ )
                                Console . WriteLine ( $"{name2 [ x ]}" );


                        GetObjectSize ( Testbutton, out double pdpHeight, out double pdpWidth );
                        Console . WriteLine ( $"GETOBJECTSIZE :  NORMAL Height/Width of 'Testbutton' is H/W {dpHeight} / {dpWidth}" );
                        GetObjectActualSize ( Testbutton, out double pdpHeight2, out double pdpWidth2 );
                        Console . WriteLine ( $"GETOBJECTSIZE :  ActualHeight/ActualWidth of 'Testbutton' is H/W {pdpHeight2} / {pdpWidth2}" );
                }

                #region Dependency Properties

                #region Background
                new public Brush Background
                {
                        get
                        {
                                return ( Brush ) GetValue ( BackgroundProperty );
                        }
                        set
                        {
                                SetValue ( BackgroundProperty, value );
                        }
                }

                // Using a DependencyProperty as the backing store for Background.  This enables animation, styling, binding, etc...
                new public static readonly DependencyProperty BackgroundProperty =
                        DependencyProperty . Register ( "Background", typeof ( Brush ), typeof ( NewImageButton ), new PropertyMetadata ( Brushes . DarkGray ) );
                #endregion

                #region BorderThickness
                new public Thickness BorderThickness
                {
                        get
                        {
                                return ( Thickness ) GetValue ( BorderThicknessProperty );
                        }
                        set
                        {
                                SetValue ( BorderThicknessProperty, value );
                        }
                }

                // Using a DependencyProperty as the backing store for Thickness BorderThickness.  This enables animation, styling, binding, etc...
                new public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty . Register ( "BorderThickness", typeof ( Thickness ), typeof ( NewImageButton ), new PropertyMetadata ( default ) );

                #endregion

                #region  BorderBrush
                new public Brush BorderBrush
                {
                        get
                        {
                                return ( Brush ) GetValue ( BorderBrushProperty );
                        }
                        set
                        {
                                SetValue ( BorderBrushProperty, value );
                        }
                }

                // Using a DependencyProperty as the backing store for BorderBrush.  This enables animation, styling, binding, etc...
                new public static readonly DependencyProperty BorderBrushProperty =
                    DependencyProperty . Register ( "BorderBrush", typeof ( Brush ), typeof ( NewImageButton ), new PropertyMetadata ( default ) );

                #endregion

                #region CornerRadius
                public double CornerRadius
                {
                        get
                        {
                                return ( double ) GetValue ( CornerRadiusProperty );
                        }
                        set
                        {
                                SetValue ( CornerRadiusProperty, value );

                        }
                }

                // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty CornerRadiusProperty =
                        DependencyProperty . Register ( "CornerRadius", typeof ( double ), typeof ( NewImageButton ), new PropertyMetadata ( ( double ) 0 ) );

                #endregion

                #region  FontSize
                new public int FontSize
                {
                        get
                        {
                                return ( int ) GetValue ( FontSizeProperty );
                        }
                        set
                        {
                                SetValue ( FontSizeProperty, value );
                        }
                }

                // Using a DependencyProperty as the backing store for FontSize.  This enables animation, styling, binding, etc...
                new public static readonly DependencyProperty FontSizeProperty =
                    DependencyProperty . Register ( "FontSize", typeof ( int ), typeof ( NewImageButton ), new PropertyMetadata ( ( int ) 18 ) );

                #endregion

                //#region Height  
                //new public double Height
                //{
                //        get
                //        {
                //                return ( double ) GetValue ( HeightProperty );
                //        }
                //        set
                //        {
                //                SetValue ( HeightProperty, value );
                //        }
                //}

                //// Using a DependencyProperty as the backing store for Height.  This enables animation, styling, binding, etc...
                //new public static readonly DependencyProperty HeightProperty =
                //    DependencyProperty . Register ( "Height", typeof ( double ), typeof ( NewImageButton ), new PropertyMetadata ( ( double ) 50 ) );

                //#endregion

                #region ImgWidth        
                public double ImgWidth
                {
                        get
                        {
                                return ( double ) GetValue ( ImgWidthProperty );
                        }
                        set
                        {
                                SetValue ( ImgWidthProperty, value );
                        }
                }

                // Using a DependencyProperty as the backing store for ImgWidth.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty ImgWidthProperty =
                    DependencyProperty . Register ( "ImgWidth", typeof ( double ), typeof ( NewImageButton ), new PropertyMetadata ( default ) );

                #endregion

                #region PushDistance
                public double PushDistance
                {
                        get
                        {
                                return ( double ) GetValue ( PushDistanceProperty );
                        }
                        set
                        {
                                SetValue ( PushDistanceProperty, value );
                        }
                }

                // Using a DependencyProperty as the backing store for PushDistance.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty PushDistanceProperty =
                    DependencyProperty . Register ( "PushDistance", typeof ( double ), typeof ( NewImageButton ), new PropertyMetadata ( ( double ) 2 ) );

                #endregion

                #region RotateAngle
                public double RotateAngle
                {
                        get
                        {
                                return ( double ) GetValue ( RotateAngleProperty );
                        }
                        set
                        {
                                SetValue ( RotateAngleProperty, value );
                        }
                }

                // Using a DependencyProperty as the backing store for RotateAngle.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty RotateAngleProperty =
                    DependencyProperty . Register ( "RotateAngle", typeof ( double ), typeof ( NewImageButton ), new PropertyMetadata ( ( double ) 0 ) );


                #endregion

                #region ShadowDirection
                public double ShadowDirection
                {
                        get
                        {
                                return ( double ) GetValue ( ShadowDirectionProperty );
                        }
                        set
                        {
                                SetValue ( ShadowDirectionProperty, value );
                        }
                }

                public static readonly DependencyProperty ShadowDirectionProperty =
                        DependencyProperty . Register ( "ShadowDirection",
                        typeof ( double ),
                        typeof ( NewImageButton ),
                        new PropertyMetadata ( ( double ) 30 ), OnShadowDirectionPropertyProperty );

                private static bool OnShadowDirectionPropertyProperty ( object value )
                {
                        return true;
                }

                #endregion TextShadowDirection

                #region ShadowOpacity
                public double ShadowOpacity
                {
                        get
                        {
                                return ( double ) GetValue ( ShadowOpacityProperty );
                        }
                        set
                        {
                                SetValue ( ShadowOpacityProperty, value );
                        }
                }

                public static readonly DependencyProperty ShadowOpacityProperty =
                        DependencyProperty . Register ( "ShadowOpacity",
                        typeof ( double ),
                        typeof ( NewImageButton ),
                        new PropertyMetadata ( ( double ) 1 ), OnShadowOpacityProperty );

                private static bool OnShadowOpacityProperty ( object value )
                {
                        return true;
                }

                #endregion ShadowOpacity

                #region ShadowDepth
                public double ShadowDepth
                {
                        get
                        {
                                return ( double ) GetValue ( ShadowDepthProperty );
                        }
                        set
                        {
                                SetValue ( ShadowDepthProperty, value );
                        }
                }

                public static readonly DependencyProperty ShadowDepthProperty =
                        DependencyProperty . Register ( "ShadowDepth",
                        typeof ( double ),
                        typeof ( NewImageButton ),
                        new PropertyMetadata ( ( double ) 0 ), OnShadowDepthProperty );

                private static bool OnShadowDepthProperty ( object value )
                {
                        return true;
                }
                #endregion ShadowBlurSize

                #region ShadowBlurRadius
                public double ShadowBlurRadius
                {
                        get
                        {
                                return ( double ) GetValue ( ShadowBlurRadiusProperty );
                        }
                        set
                        {
                                SetValue ( ShadowBlurRadiusProperty, value );
                        }
                }

                public static readonly DependencyProperty ShadowBlurRadiusProperty =
                        DependencyProperty . Register ( "ShadowBlurRadius",
                        typeof ( double ),
                        typeof ( NewImageButton ),
                        new PropertyMetadata ( ( double ) 0 ), OnShadowBlurRadiusProperty );

                private static bool OnShadowBlurRadiusProperty ( object value )
                {
                        return true;
                }
                #endregion ShadowBlurRadius

                #region ShadowColor
                public Color ShadowColor
                {
                        get
                        {
                                return ( Color ) GetValue ( ShadowColorProperty );
                        }
                        set
                        {
                                SetValue ( ShadowColorProperty, value );
                        }
                }

                public static readonly DependencyProperty ShadowColorProperty =
                        DependencyProperty . Register ( "ShadowColor",
                        typeof ( Color ),
                        typeof ( NewImageButton ),
                        new PropertyMetadata ( Colors . Transparent ) );

                #endregion ShadowBlurColor

                #region SkewX
                public double SkewX
                {
                        get
                        {
                                return ( double ) GetValue ( SkewXProperty );
                        }
                        set
                        {
                                SetValue ( SkewXProperty, value );
                        }
                }

                // Using a DependencyProperty as the backing store for SkewTransform.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty SkewXProperty =
            DependencyProperty . Register ( "SkewX", typeof ( double ), typeof ( NewImageButton ), new PropertyMetadata ( ( double ) 0 ) );
                #endregion

                #region SkewY
                public double SkewY
                {
                        get
                        {
                                return ( double ) GetValue ( SkewYProperty );
                        }
                        set
                        {
                                SetValue ( SkewYProperty, value );
                        }
                }

                // Using a DependencyProperty as the backing store for SkewY.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty SkewYProperty =
                    DependencyProperty . Register ( "SkewY", typeof ( double ), typeof ( NewImageButton ), new PropertyMetadata ( ( double ) 0 ) );
                #endregion

                #region SkewPadding
                public double SkewPadding
                {
                        get
                        {
                                return ( double ) GetValue ( SkewPaddingProperty );
                        }
                        set
                        {
                                SetValue ( SkewPaddingProperty, value );
                        }
                }

                // Using a DependencyProperty as the backing store for SkewPadding.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty SkewPaddingProperty =
                    DependencyProperty . Register ( "SkewPadding", typeof ( double ), typeof ( NewImageButton ), new PropertyMetadata ( ( double ) 5 ) );

                #endregion

                #region Source
                new public string Source
                {
                        get
                        {
                                return ( string ) GetValue ( SourceProperty );
                        }
                        set
                        {
                                SetValue ( SourceProperty, value );
                        }
                }

                // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
                new public static readonly DependencyProperty SourceProperty =
                    DependencyProperty . Register ( "Source", typeof ( string ), typeof ( NewImageButton ), new PropertyMetadata ( default ) );


                #endregion

                #region Text
                public string Text
                {
                        get
                        {
                                return ( string ) GetValue ( TextProperty );
                        }
                        set
                        {
                                SetValue ( TextProperty, value );
                        }
                }

                // Using a DependencyProperty as the backing store for BtnText.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty TextProperty =
                        DependencyProperty . Register ( "Text", typeof ( string ), typeof ( NewImageButton ), new PropertyMetadata ( "Text here ..." ) );
                #endregion

                #region TextPadLeft
                public double TextPadLeft
                {
                        get
                        {
                                return ( double ) GetValue ( TextPadLeftProperty );
                        }
                        set
                        {
                                SetValue ( TextPadLeftProperty, value );
                        }
                }

                // Using a DependencyProperty as the backing store for TextPadLeft.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty TextPadLeftProperty =
                        DependencyProperty . Register ( "TextPadLeft", typeof ( double ), typeof ( NewImageButton ), new PropertyMetadata ( ( double ) 0 ) );
                #endregion

                #region TextPadTop
                public double TextPadTop
                {
                        get
                        {
                                return ( double ) GetValue ( TextPadTopProperty );
                        }
                        set
                        {
                                SetValue ( TextPadTopProperty, value );
                        }
                }

                // Using a DependencyProperty as the backing store for TextTop.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty TextPadTopProperty =
                        DependencyProperty . Register ( "TextPadTop", typeof ( double ), typeof ( NewImageButton ), new PropertyMetadata ( ( double ) 0 ) );


                #endregion

                #region TextShadowOpacity
                public double TextShadowOpacity
                {
                        get
                        {
                                return ( double ) GetValue ( TextShadowOpacitProperty );
                        }
                        set
                        {
                                SetValue ( TextShadowOpacitProperty, value );
                        }
                }

                public static readonly DependencyProperty TextShadowOpacitProperty =
                        DependencyProperty . Register ( "TextShadowOpacity",
                        typeof ( double ),
                        typeof ( NewImageButton ),
                        new PropertyMetadata ( ( double ) 1.0 ), OnTextShadowOpacityProperty );

                private static bool OnTextShadowOpacityProperty ( object value )
                {
                        Console . WriteLine ( $"IMGBUTTON : TextShadowOpacity set to  [{value}]." );
                        return true;
                }

                #endregion TextShadowOpacity

                #region TextShadowSize
                public double TextShadowSize
                {
                        get
                        {
                                return ( double ) GetValue ( TextShadowSizeProperty );
                        }
                        set
                        {
                                SetValue ( TextShadowSizeProperty, value );
                        }
                }

                public static readonly DependencyProperty TextShadowSizeProperty =
                        DependencyProperty . Register ( "TextShadowSize",
                        typeof ( double ),
                        typeof ( NewImageButton ),
                        new PropertyMetadata ( ( double ) 1 ), OnTextShadowSizePropertyProperty );

                private static bool OnTextShadowSizePropertyProperty ( object value )
                {
                        Console . WriteLine ( $"IMGBUTTON : TextShadowSize set to  [{value}]." );
                        return true;
                }

                #endregion TextShadowSize

                #region TextShadowRadius
                public double TextShadowRadius
                {
                        get
                        {
                                return ( double ) GetValue ( TextShadowRadiusProperty );
                        }
                        set
                        {
                                SetValue ( TextShadowRadiusProperty, value );
                        }
                }

                public static readonly DependencyProperty TextShadowRadiusProperty =
                        DependencyProperty . Register ( "TextShadowRadius",
                        typeof ( double ),
                        typeof ( NewImageButton ),
                        new PropertyMetadata ( ( double ) 2 ), OnTextShadowRadiusProperty );

                private static bool OnTextShadowRadiusProperty ( object value )
                {
                        Console . WriteLine ( $"IMGBUTTON : TextShadowRadiusProperty = {value}" );
                        return true;
                }

                #endregion TextShadowRadius

                #region TextShadowDirection
                public double TextShadowDirection
                {
                        get
                        {
                                return ( double ) GetValue ( TextShadowDirectionProperty );
                        }
                        set
                        {
                                SetValue ( TextShadowDirectionProperty, value );
                        }
                }

                public static readonly DependencyProperty TextShadowDirectionProperty =
                        DependencyProperty . Register ( "TextShadowDirection",
                        typeof ( double ),
                        typeof ( NewImageButton ),
                        new PropertyMetadata ( ( double ) 30 ), OnTextShadowDirectionPropertyProperty );

                private static bool OnTextShadowDirectionPropertyProperty ( object value )
                {
                        Console . WriteLine ( $"IMGBUTTON :TextShadowDirectionProperty = {value}" );
                        return true;
                }

                #endregion TextShadowDirection

                #region TextShadowColor
                public Color TextShadowColor
                {
                        get
                        {
                                return ( Color ) GetValue ( TextShadowColorProperty );
                        }
                        set
                        {
                                SetValue ( TextShadowColorProperty, value );
                        }
                }

                public static readonly DependencyProperty TextShadowColorProperty =
                        DependencyProperty . Register ( "TextShadowColor",
                        typeof ( Color ),
                        typeof ( NewImageButton ),
                        new PropertyMetadata ( Colors . Red ) );

                #endregion TextShadowColor

                //#region Width
                //new public double Width
                //{
                //        get
                //        {
                //                return ( double ) GetValue ( WidthProperty );
                //        }
                //        set
                //        {
                //                SetValue ( WidthProperty, value );
                //        }
                //}

                //// Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
                //new public static readonly DependencyProperty WidthProperty =
                //    DependencyProperty . Register ( "Width", typeof ( double ), typeof ( NewImageButton ), new PropertyMetadata ( ( double ) 250 ) );

                //#endregion

                #endregion

                private void PART_Main_Loaded ( object sender, RoutedEventArgs e )
                {
                        if ( DesignerProperties . GetIsInDesignMode ( this ) == false )
                        {
                                ImgBorder . BorderBrush = Brushes . Transparent;
                                BtnImage . Opacity = 1;
                        }
                        else
                        {
                                ImgBorder . BorderBrush = Brushes . Black;
                                BtnImage . Opacity = 1;
                        }
                        this . Height = Height;
                        this . Width = Width;
                }

                private void Control_MouseEnter ( object sender, MouseEventArgs e )
                {
                        // Move button & Text Right and Down
                        TranslateTransform translateTransform = new TranslateTransform ( );
                        translateTransform . X += PushDistance + 2;
                        translateTransform . Y -= PushDistance + 1;
                        this . RenderTransform = translateTransform;
                        this . SkewX = SkewX;
                        this . SkewY = SkewY;
                        this . SkewPadding = SkewPadding;

                        // Move Shadow to match above
                        DropShadowEffect dropShadowEffect = new DropShadowEffect ( );
                        dropShadowEffect . BlurRadius = ShadowBlurRadius;
                        dropShadowEffect . Color = TextShadowColor;
                        dropShadowEffect . Direction = 235;
                        dropShadowEffect . Opacity = ShadowOpacity;
                        dropShadowEffect . ShadowDepth = ShadowDepth;

                        this . PART_Main . Effect = dropShadowEffect;
                        this . PART_Main . Refresh ( );
                        return;
                }

                private void Control_MouseLeave ( object sender, MouseEventArgs e )
                {
                        TranslateTransform translateTransform = new TranslateTransform ( );
                        translateTransform . X -= PushDistance + 2;
                        translateTransform . Y += PushDistance + 1;
                        this . RenderTransform = translateTransform;

                        DropShadowEffect dropShadowEffect = new DropShadowEffect ( );
                        dropShadowEffect . BlurRadius = ShadowBlurRadius;
                        dropShadowEffect . Color = TextShadowColor;
                        dropShadowEffect . Direction = 49;
                        dropShadowEffect . Opacity = ShadowOpacity;
                        dropShadowEffect . ShadowDepth = ShadowDepth;
                        this . PART_Main . Effect = dropShadowEffect;
                        this . PART_Main . Refresh ( );
                        return;
                }

                private void Testbutton_Loaded_1 ( object sender, RoutedEventArgs e )
                {

                }

                #region Wpf Utility methods
                /// <summary>
                /// Returns the PARENT Dependency Object of whatever object is received
                /// plus it's name as a string in the  'out' parameter
                /// </summary>
                /// <param name="dp"></param>
                /// <param name="name"></param>
                /// <returns></returns>
                public static DependencyObject GetParentObjects ( FrameworkElement dp, out List<string> names, int levels = 1 )
                {
                        // How to get the parent 's Name and the object of any object received
                        // Expects the dp t be the parent DP
                        FrameworkElement dps = null;
                        string clientname = "";
                        names = new List<string> ( );
                        int lvl = 0;
                        FrameworkElement dpo = ( FrameworkElement ) dp . Parent;
                        if ( dpo == null )
                                return null;

                        clientname = ( string ) dp . GetValue ( NameProperty );
                        names . Add ( clientname );
                        for ( int x = 0 ; x < levels ; x++ )
                        {
                                dps = ( FrameworkElement ) dpo . Parent;
                                if ( dps == null )
                                {
                                        dpo = dps;
                                        break;
                                }
                                else
                                {
                                        names . Add ( ( string ) dps . GetValue ( NameProperty ) );
                                        dpo = dps;
                                        if ( names [ lvl ] == "" )
                                        {
                                                names [ lvl ] = "No further Parents";
                                                break;
                                        }
                                        lvl++;
                                }
                        }
                        //if ( lvl < levels )
                        //        Console . WriteLine ( $"GETPARENTSOBJECT : Levels requested up from {clientname} exceed {levels} requested: Top Level Name up {lvl - 1} / {levels} levels & is \"{names [ lvl - 1 ]}\"" );
                        //else
                        //        Console . WriteLine ( $"GETPARENTSOBJECT : Parent Name up {levels} levels from {clientname} is {dp . GetValue ( NameProperty )} is {names [ lvl ]}" );
                        return dpo;
                }
                /// <summary>
                /// Returns the Height/Width of the received objects PARENT Dependency Object 
                /// </summary>
                /// <param name="dp"></param>
                /// <param name="dpHeight"></param>
                /// <param name="dpWidth"></param>
                public static void GetParentSize ( FrameworkElement dp, out double dpHeight, out double dpWidth, out string parentname )
                {
                        // How to get the size of any "Parent" object
                        // Expects the dp t be the parent DP
                        dpHeight = 0;
                        dpWidth = 0;
                        parentname = "";
                        if ( dp == null )
                        {
                                dpHeight = 0;
                                dpWidth = 0;
                                return;
                        }
                        DependencyObject dpo = dp . Parent;
                        if ( dpo == null )
                        {
                                dpHeight = 0;
                                dpWidth = 0;
                                return;
                        }
                        parentname = ( string ) dpo . GetValue ( NameProperty );
                        dpHeight = ( double ) dpo . GetValue ( ActualHeightProperty );
                        dpWidth = ( double ) dpo . GetValue ( ActualWidthProperty );
                        if ( dpHeight == 0 || dpWidth == 0 )
                        {
                                dpHeight = ( double ) dp . GetValue ( HeightProperty );
                                dpWidth = ( double ) dp . GetValue ( WidthProperty );
                        }
                 }
                /// <summary>
                /// Returns the Height/Width of the received object's Dependency Object 
                /// </summary>
                /// <param name="obj"></param>
                /// <param name="dpHeight"></param>
                /// <param name="dpWidth"></param>
                public static void GetObjectSize ( DependencyObject obj, out double dpHeight, out double dpWidth )
                {
                        // How to get the STANDARD Height/Width of any object from its DP settings
                        // Expects the dp to be the object Name 
                        dpHeight = 0;
                        dpWidth = 0;
                        if ( obj == null )
                        {
                                dpHeight = 0;
                                dpWidth = 0;
                                return;
                        }
                        dpHeight = ( double ) obj . GetValue ( HeightProperty );
                        dpWidth = ( double ) obj . GetValue ( WidthProperty );
                 }
                /// <summary>
                /// Returns the Height/Width of the received object's Dependency Object 
                /// </summary>
                /// <param name="obj"></param>
                /// <param name="dpHeight"></param>
                /// <param name="dpWidth"></param>
                public static void GetObjectActualSize ( DependencyObject obj, out double dpHeight, out double dpWidth )
                {
                        // How to get the ACTUAL size of any "Parent" object from its DP settings
                        // Expects the dp to be the object Name
                        dpHeight = 0;
                        dpWidth = 0;
                        if ( obj == null )
                        {
                                dpHeight = 0;
                                dpWidth = 0;
                                return;
                        }
                        dpHeight = ( double ) obj . GetValue ( ActualHeightProperty );
                        dpWidth = ( double ) obj . GetValue ( ActualWidthProperty );
                   }
                #endregion
        }
}
