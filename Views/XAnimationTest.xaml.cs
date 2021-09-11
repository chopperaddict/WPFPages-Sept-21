//#define TESTING

using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Windows;
using System . Windows . Controls;
using System . Windows . Data;
using System . Windows . Documents;
using System . Windows . Input;
using System . Windows . Media;
using System . Windows . Media . Animation;
using System . Windows . Media . Imaging;
using System . Windows . Shapes;

using static System . Net . Mime . MediaTypeNames;


namespace WPFPages . Views
{
	/// <summary>
	/// Interaction logic for AnimationTe.xaml
	/// </summary>
	public partial class AnimationTest : Window
	{
		static int counter = 0;
		private Point MousePosition = new Point ( 0, 0 );
		private bool MouseDn = false;
//		private string Name = "Ian Turner";
//		private int Age = 77;
		public AnimationTest ( )
		{
			InitializeComponent ( );
			Utils . SetupWindowDrag ( this );

			//DoErrorBeep ( 280, 100, 3 );
		}

		public class PointAnimationExample : Page
		{
			//public PointAnimationExample ( )
			//{
			//      // Create a NameScope for this page so that
			//      // Storyboards can be used.
			//      NameScope . SetNameScope ( this , new NameScope ( ) );

			//      EllipseGeometry myEllipseGeometry = new EllipseGeometry();
			//      myEllipseGeometry . Center = new Point ( 200 , 100 );
			//      myEllipseGeometry . RadiusX = 15;
			//      myEllipseGeometry . RadiusY = 15;

			//      // Assign the EllipseGeometry a name so that
			//      // it can be targeted by a Storyboard.
			//      this . RegisterName (
			//          "MyAnimatedEllipseGeometry" , myEllipseGeometry );

			//      Path myPath = new Path();
			//      myPath . Fill = Brushes . Blue;
			//      myPath . Margin = new Thickness ( 15 );
			//      myPath . Data = myEllipseGeometry;

			//      PointAnimation myPointAnimation = new PointAnimation();
			//      myPointAnimation . Duration = TimeSpan . FromSeconds ( 2 );

			//      // Set the animation to repeat forever.
			//      myPointAnimation . RepeatBehavior = RepeatBehavior . Forever;

			//      // Set the From and To properties of the animation.
			//      myPointAnimation . From = new Point ( 200 , 100 );
			//      myPointAnimation . To = new Point ( 450 , 250 );

			//      // Set the animation to target the Center property
			//      // of the object named "MyAnimatedEllipseGeometry."
			//      Storyboard . SetTargetName ( myPointAnimation , "MyAnimatedEllipseGeometry" );
			//      Storyboard . SetTargetProperty (
			//          myPointAnimation , new PropertyPath ( EllipseGeometry . CenterProperty ) );

			//      // Create a storyboard to apply the animation.
			//      Storyboard ellipseStoryboard = new Storyboard();
			//      ellipseStoryboard . Children . Add ( myPointAnimation );

			//      // Start the storyboard when the Path loads.
			//      myPath . Loaded += delegate ( object sender , RoutedEventArgs e )
			//      {
			//            ellipseStoryboard . Begin ( this );
			//      };

			//      Canvas containerCanvas = new Canvas();
			//      containerCanvas . Children . Add ( myPath );

			//      Content = containerCanvas;
			//}
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
				 s = ( Storyboard ) TryFindResource ( "SlideStackPanelRight" );
				var uri = new Uri ( "/Icons/left-arrow.png", UriKind . Relative );
				var bitmap = new BitmapImage ( uri );
				OpenCloseButton . Source = bitmap;
				myimage . Visibility = Visibility . Visible;
				OpenCloseButton . Visibility = Visibility . Hidden;
				myimage2 . Visibility = Visibility . Visible;

				counter++;
			}
			else
			{
				 s = ( Storyboard ) TryFindResource ( "SlideStackPanelLeft" );
				var uri = new Uri ( "/Icons/right-arrow.png", UriKind . Relative );
				var bitmap = new BitmapImage ( uri );
				OpenCloseButton . Source = bitmap;
				myimage . Visibility = Visibility . Hidden;
				OpenCloseButton . Visibility = Visibility . Visible;
				myimage2 . Visibility = Visibility . Hidden;
				//				//				RectbuttonStackPanel . RenderTransform = mySliderLeft;
				//				mySliderLeft . Begin ( this, true );
				//				s = ( Storyboard ) TryFindResource ( "mySliderLeft" );
				counter = 0;
			}
			try
			{
			if(s != null)
				s . Begin ( );
			}
			catch
			{

			}
			return;
		}
		#endregion menu Bar Control

		/// <summary>
		/// Set up scale info in window on startup
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RightPanel_Loaded ( object sender, RoutedEventArgs e )
		{
			ScaleDimensions . Text = $"{Btn1 . TextHeightScale } : {Btn1 . TextWidthScale }";
			return;
		}
		private void AnimWin_Loaded ( object sender, RoutedEventArgs e )
		{
		}
		private void TestEllipse_Click ( object sender, RoutedEventArgs e )
		{
			System . Diagnostics . Debugger . Break ( );
		}

		private void Btn2_Click ( object sender, RoutedEventArgs e )
		{
			Close ( );
		}

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

	#region scale changing
		private void TestRectangle_MouseLeftButtonUp ( object sender, MouseButtonEventArgs e )
		{
			Btn1 . TextWidthScale += 0.1;
			TestEllipse . TextWidthScale += 0.1;
			TestEllipse_Copy . TextWidthScale += 0.1;
			Btn2_Copy . TextWidthScale += 0.1;
			ScaleDimensions . Text = $"{Btn1 . TextHeightScale } : {Btn1 . TextWidthScale }";
		}

		private void TextScaleDn ( object sender, MouseButtonEventArgs e )
		{
			Btn1 . TextWidthScale -=  0.1;
			TestEllipse . TextWidthScale -= 0.1;
			TestEllipse_Copy . TextWidthScale -= 0.1;
			Btn2_Copy . TextWidthScale -= 0.1;
			ScaleDimensions . Text = $"{Btn1 . TextHeightScale } : {Btn1 . TextWidthScale }";
		}

		private void TextScaleUp ( object sender, MouseButtonEventArgs e )
		{
			Btn1 . TextWidthScale += 0.1;
			TestEllipse . TextWidthScale += 0.1;
			TestEllipse_Copy . TextWidthScale += 0.1;
			Btn2_Copy . TextWidthScale += 0.1;
			ScaleDimensions . Text = $"{Btn1 . TextHeightScale } : {Btn1 . TextWidthScale }";
		}

		private void TextHeightScaleUp ( object sender, MouseButtonEventArgs e )
		{
			Btn1 . TextHeightScale += 0.1;
			TestEllipse . TextHeightScale += 0.1;
			TestEllipse_Copy . TextHeightScale += 0.1;
			Btn2_Copy . TextHeightScale += 0.1;
			ScaleDimensions . Text = $"{Btn1 . TextHeightScale } : {Btn1 . TextWidthScale }";
		}

		private void TextHeightScaleDn ( object sender, MouseButtonEventArgs e )
		{
			Btn1 . TextHeightScale -= 0.1;
			TestEllipse . TextHeightScale -= 0.1;
			TestEllipse_Copy . TextHeightScale -= 0.1;
			Btn2_Copy . TextHeightScale -= 0.1;
			ScaleDimensions . Text = $"{Btn1 . TextHeightScale } : {Btn1 . TextWidthScale }";
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
				LeftPanel . Visibility = Visibility . Hidden;
				LeftPanel2 . Visibility = Visibility . Visible;
				//You can use this format almost anywhere to changfe a Dependency Poperty
			}
			else
			{
				LeftPanel . Visibility = Visibility . Visible;
				LeftPanel2 . Visibility = Visibility . Hidden;
			}
		}

		private void Toggle_Click ( object sender, RoutedEventArgs e )
		{
			if ( LeftPanel . Visibility == Visibility . Visible )
			{
				LeftPanel . Visibility = Visibility . Hidden;
				LeftPanel2 . Visibility = Visibility . Visible;
			}
			else
			{
				LeftPanel . Visibility = Visibility . Visible;
				LeftPanel2 . Visibility = Visibility . Hidden;
			}
		}
	}  
}
