using System;
using System .Collections .Generic;
using System .ComponentModel;
using System .Diagnostics;
using System .Linq;
using System .Text;
using System .Threading;
using System .Threading .Tasks;
using System .Windows;
using System .Windows .Controls;
using System .Windows .Data;
using System .Windows .Documents;
using System .Windows .Input;
using System .Windows .Media;
using System .Windows .Media .Imaging;
using System .Windows .Shapes;

using WPFPages .ViewModels;
namespace WPFPages .Views
{
	/// <summary>
	/// Interaction logic for Bankaccount.xaml
	/// </summary>
	public partial class Bankaccount : Window
	{
		public BankCollection BankViewcollection = null;
		private static int TemplateType = 0;
		//		public WpfCommand LoadNwCommand {get; set;}

		// Get our personal Collection view of the Db
		public ICollectionView BankviewerView
		{
			get; set;
		}
		public Bankaccount ( )
		{
			InitializeComponent ( );
			this .DataContext = BankviewerView;
		}

		private void bankgrid_Loaded ( object sender , RoutedEventArgs e )
		{
			EventControl .BankDataLoaded += EventControl_BankDataLoaded;
			BankCollection .LoadBank ( BankViewcollection , "BankAccount" , 99 , true );

		}

		private void EventControl_BankDataLoaded ( object sender , LoadedEventArgs e )
		{
			// Event handler for BankDataLoaded
			if ( e .DataSource == null )
				return;
			 Debug .WriteLine ( $"\n*** Loading Bank data in BankDbView after BankDataLoaded trigger\n" );
			this .BankGrid .ItemsSource = null;
			this .BankGrid .Items .Clear ( );
			BankViewcollection = e .DataSource as BankCollection;
			BankviewerView = CollectionViewSource .GetDefaultView ( BankViewcollection );
			BankviewerView .Refresh ( );
			// Set our grids items source
			this .BankGrid .ItemsSource = BankviewerView;
			this .ListGrid .ItemsSource = BankviewerView;
			this .BankGrid .SelectedIndex = 0;
			this .BankGrid .SelectedItem = 0;
			this .BankGrid .UpdateLayout ( );
			this .BankGrid .Refresh ( );
			Mouse .OverrideCursor = Cursors .Arrow;
			Thread .Sleep ( 250 );
			Debug .WriteLine ( "BANKDBVIEW : Bank Data fully loaded" );
		}

		private void Button_Click ( object sender , RoutedEventArgs e )
		{
			// Change the listbox DataTemplate in use
			var v = ListGrid .ItemTemplate;
			// get info on current DT
			DataTemplate dt = ListGrid .ItemTemplate;
			dt = FindResource ( "BankDataTemplate1" ) as DataTemplate;
			if ( TemplateType == 0 )
			{
				ListGrid .ItemTemplate = FindResource ( "BankDataTemplate1" ) as DataTemplate;
				TemplateType = 1;
			}
			else
			{
				ListGrid .ItemTemplate = FindResource ( "BankDataTemplate2" ) as DataTemplate;
				TemplateType = 0;
			}
		}
	}
}
