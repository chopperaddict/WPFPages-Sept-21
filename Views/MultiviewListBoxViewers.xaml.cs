using System;
using System . Collections;
using System . Collections . Generic;
using System . Collections . ObjectModel;
using System . ComponentModel;
using System . Data;
using System . Data . SqlClient;
using System . Diagnostics;
using System . Linq;
using System . Reflection;
using System . Runtime . CompilerServices;
using System . Runtime . Remoting . Channels;
using System . Text;
using System . Threading . Tasks;
using System . Windows;
using System . Windows . Controls;
using System . Windows . Controls . Primitives;
using System . Windows . Data;
using System . Windows . Documents;
using System . Windows . Input;
using System . Windows . Media;
using System . Windows . Media . Imaging;
using System . Windows . Shapes;

using WPFPages . ViewModels;
using WPFPages . Views;



namespace WPFPages . Views
{

	/// <summary>
	/// Interaction logic for UserListBoxViewer.xaml
	/// </summary>
	public partial class MultiviewListBoxViewers : Window
	{
		// Declare all 3 of the local Db pointers
		public BankCollection SqlBankcollection = new BankCollection();
		public BankCollection BackupBankcollection = new BankCollection();
		public AllCustomers SqlCustcollection = new AllCustomers();
		public DetCollection SqlDetcollection = new DetCollection();
		public static List<BankAccountViewModel> BankList = new List<BankAccountViewModel>();
		public List<CustomerViewModel> CustList = new List<CustomerViewModel>();
		public List<DetailsViewModel> DetList = new List<DetailsViewModel>();

		private static readonly DataGridColumn dataGridColumn   ;
		private DataGridColumn[] DGBankColumnsCollection = {dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,
			dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn };
		private DataGridColumn[] DGCustColumnsCollection
			= {dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,
			dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn ,dataGridColumn };
		private DataGridColumn[] DGDetailsColumnsCollection= {dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,
			dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn };


		private bool ExpandAll = false;
		private bool MouseLeftBtnDown = false;
		//CURRENT Foreground color
		private Brush CurrentForeColor = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x00, 0x00));  // current foreground =black
		private Brush CurrentBackColor = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));  // current foreground =White
		private string CurrentCellName = "";
		private bool areItemsExpanded;
		public object FilterBankData { get; private set; }

		public double uclistboxheight = 0;
		public double uclistbox2height = 0;

		#region full properties

		private double dg1Width;

		public double Dg1Width
		{
			get { return dg1Width; }
			set { dg1Width = value; }
		}

		public bool AreItemsExpanded
		{
			get
			{
				return areItemsExpanded;
			}
			set
			{
				areItemsExpanded = value;
			}
		}

		private double rowheight = 25;
		public double Rowheight
		{
			get
			{
				return rowheight;
			}
			set
			{
				rowheight = value;
			}
		}
		private string tbBalance = "";
		public string TbBalance
		{
			get
			{
				return tbBalance;
			}
			set
			{
				tbBalance = value;
			}
		}
		// CURRENT Background color
		private Brush tbCurrentBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
		public Brush TbCurrentBrush
		{
			get
			{
				return tbCurrentBrush;
			}
			set
			{
				tbCurrentBrush = value;
			}
		}

		private int currentindex;
		public int CurrentIndex
		{
			get
			{
				return currentindex;
			}
			set
			{
				currentindex = value;
			}
		}

		private bool isdirty;
		public bool IsDirty
		{
			get
			{
				return isdirty;
			}
			set
			{
				isdirty = value;
			}
		}
		private int gridSelection;
		public int GridSelection
		{
			get
			{
				return gridSelection;
			}
			set
			{
				gridSelection = value;
			}
		}
		private int listSelection;
		public int ListSelection
		{
			get
			{
				return listSelection;
			}
			set
			{
				listSelection = value;
			}
		}

		private int gridSelection2;
		public int GridSelection2
		{
			get
			{
				return gridSelection2;
			}
			set
			{
				gridSelection2 = value;
			}
		}
		private int listSelection2;
		public int ListSelection2
		{
			get
			{
				return listSelection2;
			}
			set
			{
				listSelection2 = value;
			}
		}

		private bool itemExpanded = true;
		public bool ItemExpanded
		{
			get
			{
				return itemExpanded;
			}
			set
			{
				itemExpanded = value;
			}
		}
		private bool isSelected;
		public bool IsSelected
		{
			get
			{
				return isSelected;
			}
			set
			{
				isSelected = value;
			}
		}
		#endregion full properties

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged ( string PropertyName )
		{
			if ( null != PropertyChanged )
			{
				PropertyChanged ( this ,
					new PropertyChangedEventArgs ( PropertyName ) );
			}
		}
		#endregion INotifyPropertyChanged Members



		// Drag & Drop stuff

		private bool IsLeftButtonDown = false;
		private static Point _startPoint
		{
			get; set;
		}
		private static bool ScrollBarMouseMove
		{
			get; set;
		}


		public MultiviewListBoxViewers ( )
		{
			InitializeComponent ( );
		}
		private async void Window_Loaded ( object sender , RoutedEventArgs e )
		{
			int counter = 0;
			EventControl . BankDataLoaded += EventControl_BankDataLoaded;
			EventControl . CustDataLoaded += EventControl_CustDataLoaded;
			this . Show ( );
			Utils . SetupWindowDrag ( this );
			Flags . SqlBankActive = true;

			ColumnSelection . Items . Add ( 0 );
			ColumnSelection . Items . Add ( 1 );
			ColumnSelection . Items . Add ( 2 );


			// Load Columns layout BEFORE loading data
			if ( datagrid. Columns . Count == 0 )
			{
				DataGridUtilities . LoadDataGridColumns ( datagrid , "DGMultiBankColumns" );
				DataGridUtilities . LoadDataGridTextColumns ( datagrid , "DGMultiBankTextColumns" );
			}
			//Saved default Columns layout
			foreach ( var item in datagrid . Columns )
			{
				DGBankColumnsCollection [ counter++ ] = item;
			}
			DataGridSupport . SortBankColumns ( datagrid , DGBankColumnsCollection );

			if ( datagrid2 . Columns . Count == 0 )
			{
				DataGridUtilities . LoadDataGridColumns ( datagrid2 , "DGMultiCustomerColumns" );
				DataGridUtilities . LoadDataGridTextColumns ( datagrid2 , "DGMultiCustomerTextColumns" );
			}
			//Saved default Columns layout
			counter = 0;
			foreach ( var item in datagrid2. Columns )
			{
				DGCustColumnsCollection [ counter++ ] = item;
			}
			DataGridSupport . SortCustomerColumns ( datagrid2 , DGCustColumnsCollection );
			// Columns are all loaded ...

			await BankCollection . LoadBank ( SqlBankcollection , "SQLDBVIEWER" , 1 , true );
			AllCustomers . LoadCust ( SqlCustcollection , "" , 1 , true , 0 , 0 , 0 );

			uclistboxheight = UCListbox . ActualHeight;
			uclistbox2height = UCListbox2 . ActualHeight;
			datagrid . BringIntoView ( );
			datagrid2 . BringIntoView ( );
			dg1Width = datagrid . ActualWidth;
		}

		private void EventControl_BankDataLoaded ( object sender , LoadedEventArgs e )
		{
			bool privateMethod = false;
			Debug . WriteLine ( $"\n*** Loading Bank data in UserListboxViewer after BankDataLoaded trigger\n" );
			SqlBankcollection = e . DataSource as BankCollection;
			UCListbox . ItemsSource = SqlBankcollection;
			//Get a View  of the Bank Data so we can sort and filter
			var  BankCollectionView = CollectionViewSource.GetDefaultView(SqlBankcollection);
			if ( privateMethod )
			{
				// Using my own filter class
				var  filter = new ViewFilter ( BankCollectionView);
				// Set the filter to data entry field on the window
				filter . FilterExpression = "CustNo >= int.Parse(ViewFilterCondition.Text) AND CustNo < 1057000";
				//filter .FilterExpression = ViewFilterCondition .Text;
				datagrid . ItemsSource = BankCollectionView;
				UCListbox . ItemsSource = BankCollectionView;
			}
			else
			{
				// Std method  to filter
				datagrid . ItemsSource = BankCollectionView;
				datagrid . SelectedIndex = 0;
				UCListbox . ItemsSource = BankCollectionView;
			}

			datagrid . SelectedIndex = 0;
			UCListbox . SelectedIndex = 0;

			Mouse . OverrideCursor = System . Windows . Input . Cursors . Arrow;
			//datagrid . RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode . Visible;
		}

		/// <summary>
		/// Filter condition for BankAccount Daa
		/// </summary>
		/// <param name="bankaccount"></param>
		/// <returns></returns>
		private bool FiterBankData ( int custno )
		{
			return true;
			//return  custno >1055000 && custno < 1055080;
			//.IndexOf ( Search , StringComparison .OrdinalIgnoreCase ) != -1;
			//if(Search != null) Console .WriteLine ($"{Search}");
			////if ( Search == null ) return false;
			//return Search.Contains(bankaccount .CustNo);

		}

		private void EventControl_CustDataLoaded ( object sender , LoadedEventArgs e )
		{
			SqlCustcollection = e . DataSource as AllCustomers;
			datagrid2 . ItemsSource = SqlCustcollection;
			Console . WriteLine ( $"in CustomerdataLoaded  handler: datagrid2 count = { datagrid2 . Items . Count}" );
			datagrid2 . SelectedIndex = 0;
			UCListbox2 . ItemsSource = SqlCustcollection;
			UCListbox2 . SelectedIndex = 0;
			//			UCListbox . Visibility = Visibility . Collapsed;
			//			datagrid2 . Visibility = Visibility . Visible;
			Console . WriteLine ( $"Customer data just Loaded : datagrid2 count = { datagrid2 . Items . Count}" );
			Mouse . OverrideCursor = System . Windows . Input . Cursors . Arrow;
			datagrid2 . Visibility = Visibility . Visible;
			datagrid2 . BringIntoView ( );
		}

		private void Select ( int low , int high , int total )
		{
			// DataTable dtBank = new DataTable ( );
			//DbListbox . UCListbox . ItemsSource = null;
			//DbListbox . UCListbox . Items . Clear ( );
			//DbListbox . UCListbox . Refresh ( );
			//DbListbox . UCListbox . UpdateLayout ( );
			//Mouse . OverrideCursor = System . Windows . Input . Cursors . Wait;
			//SqlBankcollection . Clear ( );
			//dtBank = DbListWindowControl . LoadBankData ( low, high, total );
			//SqlBankcollection = DbListWindowControl . LoadBankTest ( SqlBankcollection, dtBank );
			//DbListbox . UCListbox . ItemsSource = SqlBankcollection;
			//DbListbox . UCListbox . DataContext = SqlBankcollection;
			//DbListbox . ActiveType = "BANKACCOUNT";
			//DbListbox . UCListbox . SelectedIndex = 63;
			//DbListbox . BankRecord = DbListbox . UCListbox . SelectedItem as BankAccountViewModel;
			//Mouse . OverrideCursor = System . Windows . Input . Cursors . Arrow;
		}
		private void Window_Closed ( object sender , EventArgs e )
		{
			EventControl . BankDataLoaded -= EventControl_BankDataLoaded;
			EventControl . CustDataLoaded -= EventControl_CustDataLoaded;
			UnloadDataGridColumns ( );
		}
		/// <summary>
		/// Have to clear clumns collection otherwise it barfs on reloading after closure f the window
		/// </summary>
		private void UnloadDataGridColumns ( )
		{
			ObservableCollection<DataGridColumn> dgc = datagrid.Columns;
			dgc . Clear ( );
			ObservableCollection<DataGridColumn> dgc2 = datagrid2.Columns;
			dgc2 . Clear ( );
		}
		#region DATA LOAD FUNCTIONS
		public static BankCollection LoadBankTest ( BankCollection temp , DataTable dtBank )
		{
			//example of using an Action

			int x = dtBank.Rows.Count;
			//			int i = 0;
			Func<int, int, bool> action = (i, x) =>
	     {
		     while (i++ < x)
		     {
			     temp.Add(new BankAccountViewModel
			     {
				     Id = Convert.ToInt32(dtBank.Rows[i][0]),
				     BankNo = dtBank.Rows[i][1].ToString(),
				     CustNo = dtBank.Rows[i][2].ToString(),
				     AcType = Convert.ToInt32(dtBank.Rows[i][3]),
				     Balance = Convert.ToDecimal(dtBank.Rows[i][4]),
				     IntRate = Convert.ToDecimal(dtBank.Rows[i][5]),
				     ODate = Convert.ToDateTime(dtBank.Rows[i][6]),
				     CDate = Convert.ToDateTime(dtBank.Rows[i][7]),
			     });
		     }
		     return true;
	     };
			action ( 0 , x );

			return null;
		}
		#endregion DATA LOAD FUNCTIONS

		private async void ClearBtn_PreviewMouseLeftButtonDown ( object sender , MouseButtonEventArgs e )
		{
			// Clear Grid
			//datagrid . ItemsSource = null;
			//datagrid . Items . Clear ( );
			//datagrid2 . ItemsSource = null;
			//datagrid2 . Items . Clear ( );
			//// clear listbox.
			//UCListbox . ItemsSource = null;
			//UCListbox . Items . Clear ( );
			// loads the Bank Db and calls load of Customer as well
			DbList_LoadBtnPressed ( null , null );
			//UCListbox2 . ItemsSource = null;
			//UCListbox2 . Items . Clear ( );
			////Reload data for both types
			//await BankCollection . LoadBank ( SqlBankcollection , "SQLDBVIEWER" , 1 , true );
			//AllCustomers . LoadCust ( SqlCustcollection , "" , 1 , true , 0 , 0 , 0 );
		}

		private void LbiExpander_Expanded ( object sender , RoutedEventArgs e )
		{
			// triggered whenever an item is expanded
			Expander ex = new Expander();
			AreItemsExpanded = ex . IsExpanded;

		}

		private void TextBox_PreviewKeyDown ( object sender , KeyEventArgs e )
		{
			BankAccountViewModel bvm = new BankAccountViewModel();
			// we are in a TextBlock or TextBox of the ListView
			if ( e . Key == Key . Enter || e . Key == Key . Tab )
			{
				string t = sender.GetType().ToString();
				if ( t . Contains ( "TextBox" ) || t . Contains ( "TextBlock" ) )
				{
					// in a listview !!
					if ( listSelection > -1 )
						bvm = UCListbox . SelectedItem as BankAccountViewModel;
				}
				if ( bvm != null )
					BankCollection . UpdateBankDb ( bvm , "BANKACCOUNT" );

				EventControl . TriggerViewerDataUpdated ( this , new LoadedEventArgs
				{
					CallerType = "USERLISTBOXVIEWER" ,
					CallerDb = "BANKACCOUNT" ,
					DataSource = SqlBankcollection ,
					RowCount = UCListbox . SelectedIndex ,
					Bankno = bvm . BankNo ,
					Custno = bvm . CustNo
				} );
			}

		}

		private void ToggleBtn_PreviewMouseLeftButtonDown ( object sender , MouseButtonEventArgs e )
		{
			// switch betwee Datagrid and ListView
			if ( UCListbox . Visibility == Visibility . Visible )
			{
				//Switch  to DataGrid view
				Utils . SetUpGridSelection ( datagrid , ListSelection );
				// Toggle viz of the grid -v- view
				UCListbox . Visibility = Visibility . Collapsed;
				UCListbox2 . Visibility = Visibility . Collapsed;
				datagrid . Visibility = Visibility . Visible;
				datagrid2 . Visibility = Visibility . Visible;
				datagrid . SelectedIndex = ListSelection;
				datagrid . SelectedItem = GridSelection;
				if ( datagrid . SelectedItem != null )
					datagrid . ScrollIntoView ( datagrid . SelectedItem );
				this . Refresh ( );
				datagrid . Width = dg1Width;
				datagrid . Focus ( );
				datagrid . Refresh ( );
				datagrid . SelectedIndex = GridSelection;
				datagrid . SelectedItem = GridSelection;
				ColumnSelection . Visibility = Visibility . Visible;
			}
			else
			{
				//Switch  to ListView view
				dg1Width = datagrid . ActualWidth;
				datagrid . Visibility = Visibility . Collapsed;
				datagrid2 . Visibility = Visibility . Collapsed;
				UCListbox . Visibility = Visibility . Visible;
				UCListbox2 . Visibility = Visibility . Visible;
				this . Refresh ( );
				UCListbox . SelectedIndex = ListSelection;
				UCListbox . SelectedItem = GridSelection;
				UCListbox . Refresh ( );
				UCListbox2 . Refresh ( );
				if ( UCListbox . SelectedItem != null )
					UCListbox . ScrollIntoView ( UCListbox . SelectedItem );
				UCListbox . Focus ( );
				ColumnSelection . Visibility = Visibility . Hidden;
			}
			//if ( UCListbox2 .Visibility == Visibility .Visible )
			//{
			//      Utils .SetUpGridSelection ( datagrid2 , ListSelection2 );
			//      UCListbox2 .Visibility = Visibility .Hidden;
			//      datagrid2 .Visibility = Visibility .Visible;
			//      datagrid2 .SelectedIndex = ListSelection2;
			//      datagrid2 .SelectedItem = GridSelection2;
			//      datagrid2 .ScrollIntoView ( datagrid2 .SelectedItem );

			//      datagrid2 .Focus ( );
			//      datagrid2 .Refresh ( );
			//      datagrid2 .SelectedIndex = GridSelection2;
			//      datagrid2 .SelectedItem = GridSelection2;
			//}
			//else
			//{
			//      Utils .SetUpGListboxSelection ( UCListbox2 , GridSelection );
			//      datagrid2 .Visibility = Visibility .Hidden;
			//      UCListbox2 .Visibility = Visibility .Visible;
			//      UCListbox2 .SelectedIndex = ListSelection;
			//      UCListbox2 .SelectedItem = GridSelection;
			//      UCListbox2 .Refresh ( );
			//      UCListbox2 .ScrollIntoView ( UCListbox2 .SelectedItem );
			//      UCListbox2 .Focus ( );
			//}
		}
		private void DbListbox_PreviewMouseLeftButtonDown ( object sender , MouseButtonEventArgs e )
		{
			try
			{
				ListSelection = UCListbox . SelectedIndex;
				CurrentIndex = UCListbox . SelectedIndex;
				ListSelection = CurrentIndex;
				UCListbox . SelectedItem = CurrentIndex;
			}
			catch
			{

			}
		}
		private void DbListbox_PreviewMouseLeftButtonDown2 ( object sender , MouseButtonEventArgs e )
		{
			try
			{
				ListSelection = UCListbox2 . SelectedIndex;
				CurrentIndex = UCListbox2 . SelectedIndex;
				ListSelection2 = CurrentIndex;
				this . IsSelected = true;
			}
			catch
			{

			}
		}

		private void Datagrid_PreviewMouseLeftButtonDown ( object sender , MouseButtonEventArgs e )
		{
			string t = sender.GetType().ToString();
			if ( t . Contains ( "DataGrid" ) )
			{
				GridSelection = datagrid . SelectedIndex;
				ListSelection = GridSelection;
				UCListbox . SelectedIndex = ListSelection;
				MouseLeftBtnDown = true;
			}
		}
		//private void Datagrid_PreviewMouseLeftButtonDown2 ( object sender, MouseButtonEventArgs e )
		//{
		//        string t = sender . GetType ( ) . ToString ( );
		//        if ( t . Contains ( "DataGrid" ) )
		//        {
		//                GridSelection2 = datagrid2 . SelectedIndex;
		//                ListSelection2 = GridSelection2;
		//                UCListbox2 . SelectedIndex = ListSelection2;
		//        }
		//        else
		//        {
		//        }
		//        MouseLeftBtnDown = true;
		//}

		private void LbItem_PreviewMouseLeftButtonDown ( object sender , MouseButtonEventArgs e )
		{
			ListSelection = UCListbox . SelectedIndex;
			//UCListbox . Items [ ListSelection ] = UCListbox . SelectedItem;
			GridSelection = ListSelection;
			//this . IsSelected = true;
			MouseLeftBtnDown = true;
		}
		private void LbItem_PreviewMouseLeftButtonDown2 ( object sender , MouseButtonEventArgs e )
		{
			ListSelection = UCListbox2 . SelectedIndex;
			//UCListbox2 . Items [ ListSelection ] = UCListbox2 . SelectedItem;
			GridSelection = ListSelection;
			this . IsSelected = true;
		}

		//private void Expander_Expanded ( object sender, RoutedEventArgs e )
		//{
		//        Expander exp = sender as Expander;
		//        exp . IsExpanded = true;
		//}

		//private void Expander_Collapsed ( object sender, RoutedEventArgs e )
		//{
		//        Expander exp = sender as Expander;
		//        exp . IsExpanded = false;
		//}

		private void UCListbox_MouseDoubleClick ( object sender , MouseButtonEventArgs e )
		{
			ListBoxItem lbi = new ListBoxItem();
			ListBox lv = sender as ListBox;
			int sel = lv.SelectedIndex;
			lbi = ( ListBoxItem ) UCListbox . ItemContainerGenerator . ContainerFromIndex ( UCListbox . SelectedIndex );
			ListSelection = UCListbox . SelectedIndex;
			GridSelection = ListSelection;
			//			int count = 0;

		}

		private void UCListbox_SelectionChanged ( object sender , SelectionChangedEventArgs e )
		{
			// Store in a class variable
			CurrentIndex = UCListbox . SelectedIndex;
			ListSelection = CurrentIndex;
			Debug . WriteLine ( $"Index is set to {CurrentIndex}" );
		}
		private void UCListbox_SelectionChanged2 ( object sender , SelectionChangedEventArgs e )
		{
			CurrentIndex = UCListbox2 . SelectedIndex;
			ListSelection2 = CurrentIndex;
			Debug . WriteLine ( $"Index of  Customer ListView is set to {CurrentIndex}" );
		}


		private FrameworkElement FindByName ( string name , FrameworkElement root )
		{
			Stack<FrameworkElement> tree = new Stack<FrameworkElement>();
			tree . Push ( root );

			while ( tree . Count > 0 )
			{
				FrameworkElement current = tree.Pop();
				if ( current . Name == name )
					return current;

				int count = VisualTreeHelper.GetChildrenCount(current);
				for ( int i = 0 ; i < count ; ++i )
				{
					DependencyObject child = VisualTreeHelper.GetChild(current, i);
					if ( child is FrameworkElement )
						tree . Push ( ( FrameworkElement ) child );
				}
			}

			return null;
		}
		private void datagrid_SelectionChanged ( object sender , SelectionChangedEventArgs e )
		{
			DataGrid dg = new DataGrid();
			dg = e . Source as DataGrid;
			var dgr = dg.Items.CurrentItem;
			GridSelection = dg . SelectedIndex;
			ListSelection = GridSelection;
			var template = datagrid;
			TextBlock tb = (TextBlock)this.datagrid.FindName("custno2");
			Brush newbrush = new SolidColorBrush(Color.FromArgb(255, (byte)255, (byte)255, (byte)255));
		}

		private void CheckTypes ( )
		{

			//Type [ ] types = { typeof(Example), typeof(NestedClass),
			// typeof(INested), typeof(S) };

			Type[] types = { typeof ( MultiviewListBoxViewers),
				typeof(DataGrid),
				typeof(TextBlock)};

			foreach ( var t in types )
			{
				Console . WriteLine ( "Attributes for type {0}:" , t . Name );

				TypeAttributes attr = t.Attributes;

				// To test for visibility attributes, you must use the visibility mask.
				TypeAttributes visibility = attr & TypeAttributes.VisibilityMask;
				switch ( visibility )
				{
					case TypeAttributes . NotPublic:
						Console . WriteLine ( "   ...is not public" );
						break;
					case TypeAttributes . Public:
						Console . WriteLine ( "   ...is public" );
						break;
					case TypeAttributes . NestedPublic:
						Console . WriteLine ( "   ...is nested and public" );
						break;
					case TypeAttributes . NestedPrivate:
						Console . WriteLine ( "   ...is nested and private" );
						break;
					case TypeAttributes . NestedFamANDAssem:
						Console . WriteLine ( "   ...is nested, and inheritable only within the assembly" +
						   "\n         (cannot be declared in C#)" );
						break;
					case TypeAttributes . NestedAssembly:
						Console . WriteLine ( "   ...is nested and internal" );
						break;
					case TypeAttributes . NestedFamily:
						Console . WriteLine ( "   ...is nested and protected" );
						break;
					case TypeAttributes . NestedFamORAssem:
						Console . WriteLine ( "   ...is nested and protected internal" );
						break;
				}

				// Use the layout mask to test for layout attributes.
				TypeAttributes layout = attr & TypeAttributes.LayoutMask;
				switch ( layout )
				{
					case TypeAttributes . AutoLayout:
						Console . WriteLine ( "   ...is AutoLayout" );
						break;
					case TypeAttributes . SequentialLayout:
						Console . WriteLine ( "   ...is SequentialLayout" );
						break;
					case TypeAttributes . ExplicitLayout:
						Console . WriteLine ( "   ...is ExplicitLayout" );
						break;
				}

				// Use the class semantics mask to test for class semantics attributes.
				TypeAttributes classSemantics = attr & TypeAttributes.ClassSemanticsMask;
				switch ( classSemantics )
				{
					case TypeAttributes . Class:
						if ( t . IsValueType )
						{
							Console . WriteLine ( "   ...is a value type" );
						}
						else
						{
							Console . WriteLine ( "   ...is a class" );
						}
						break;
					case TypeAttributes . Interface:
						Console . WriteLine ( "   ...is an interface" );
						break;
				}

				if ( ( attr & TypeAttributes . Abstract ) != 0 )
				{
					Console . WriteLine ( "   ...is abstract" );
				}

				if ( ( attr & TypeAttributes . Sealed ) != 0 )
				{
					Console . WriteLine ( "   ...is sealed" );
				}

				Console . WriteLine ( );

			}
		}

		//private void Balancefield_PreviewMouseEnter ( object sender, MouseEventArgs e )
		//{
		//}

		//private void Balancefield_PreviewMouseLeave ( object sender, MouseEventArgs e )
		//{
		//}

		//private void Balancefield_MouseMove ( object sender, MouseEventArgs e )
		//{
		//}

		private void Datagrid_PreviewMouseMove ( object sender , MouseEventArgs e )
		{
			string dataFormat = "";
			string str = "";
			TextBlock tb = new TextBlock();
			tb = sender as TextBlock;

			if ( tb != null )
			{
				CurrentBackColor = tb . Background;
				CurrentForeColor = tb . Foreground;
				CurrentCellName = tb . Name;
				//				Debug . WriteLine ( $"Name={CurrentCellName }, B = {CurrentBackColor}, F = {CurrentForeColor}" );
			}
			if ( MouseLeftBtnDown == true )
			{
				bool isbank = false;
				ObservableCollection<DataGridColumn> dgc = datagrid.Columns;
				foreach ( var item in dgc )
				{
					string a = item.Header as String;

					if ( a . Contains ( "Balance" ) )
					{
						isbank = true;
						break;
					}
				}
				if ( isbank )
				{
					// must be a Bank grid
					BankAccountViewModel bvm = new BankAccountViewModel();
					string t1 = sender.GetType().ToString();
					if ( t1 . Contains ( "ListView" ) )
						bvm = UCListbox . SelectedItem as BankAccountViewModel;
					else
						bvm = datagrid . SelectedItem as BankAccountViewModel;
					str = GetExportRecords . CreateTextFromRecord ( bvm , null , null , true , false );
					dataFormat = DataFormats . Text;
				}
				else
				{
					// must be a Customer grid
					// must be a Customer grid
					CustomerViewModel cvm = new CustomerViewModel ();
					string t1 = sender.GetType().ToString();
					if ( t1 . Contains ( "ListView" ) )
						cvm = UCListbox . SelectedItem as CustomerViewModel;
					else
						cvm = datagrid . SelectedItem as CustomerViewModel;
					str = GetExportRecords . CreateTextFromRecord ( null , null , cvm , true , false );
					dataFormat = DataFormats . Text;
				}

				DataObject dataObject = new DataObject(dataFormat, str);
				// DragDrop . DoDragDrop ( );

			}
			MouseLeftBtnDown = false;
		}
		private void datagrid_PreviewMouseMove2 ( object sender , MouseEventArgs e )
		{
			string dataFormat = "";
			string str = "";
			TextBlock tb = new TextBlock();
			tb = sender as TextBlock;

			if ( tb != null )
			{
				CurrentBackColor = tb . Background;
				CurrentForeColor = tb . Foreground;
				CurrentCellName = tb . Name;
				//				Debug . WriteLine ( $"Name={CurrentCellName }, B = {CurrentBackColor}, F = {CurrentForeColor}" );
			}
			if ( MouseLeftBtnDown == true )
			{
				bool isbank = false;
				ObservableCollection<DataGridColumn> dgc = datagrid.Columns;
				foreach ( var item in dgc )
				{
					string a = item.Header as String;

					if ( a . Contains ( "Balance" ) )
					{
						isbank = true;
						break;
					}
				}
				if ( isbank )
				{
					// must be a Bank grid
					BankAccountViewModel bvm = new BankAccountViewModel();
					string t1 = sender.GetType().ToString();
					if ( t1 . Contains ( "ListView" ) )
						bvm = UCListbox . SelectedItem as BankAccountViewModel;
					else
						bvm = datagrid . SelectedItem as BankAccountViewModel;
					str = GetExportRecords . CreateTextFromRecord ( bvm , null , null , true , false );
					dataFormat = DataFormats . Text;
				}
				else
				{
					// must be a Customer grid
					CustomerViewModel cvm = new CustomerViewModel ();
					string t1 = sender.GetType().ToString();
					if ( t1 . Contains ( "ListView" ) )
						cvm = UCListbox . SelectedItem as CustomerViewModel;
					else
						cvm = datagrid . SelectedItem as CustomerViewModel;
					str = GetExportRecords . CreateTextFromRecord ( null , null , cvm , true , false );
					dataFormat = DataFormats . Text;
				}

				DataObject dataObject = new DataObject(dataFormat, str);
				// DragDrop . DoDragDrop ( );

			}
			MouseLeftBtnDown = false;
		}

		private void ListView_PreviewMouseRightButtonDown ( object sender , MouseButtonEventArgs e )
		{
			ContextMenu cm = this.FindResource("ContextMenu2") as ContextMenu;
			cm . PlacementTarget = sender as ListView;
			cm . IsOpen = true;
		}

		public void FindChildren<T> ( List<T> results , DependencyObject startNode )
		  where T : DependencyObject
		{
			int count = VisualTreeHelper.GetChildrenCount(startNode);
			for ( int i = 0 ; i < count ; i++ )
			{
				DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
				if ( ( current . GetType ( ) ) . Equals ( typeof ( T ) ) || ( current . GetType ( ) . GetTypeInfo ( ) . IsSubclassOf ( typeof ( T ) ) ) )
				{
					T asType = (T)current;
					results . Add ( asType );
				}
				FindChildren<T> ( results , current );
			}
		}

		private void _Border_PreviewMouseDown ( object sender , MouseButtonEventArgs e )
		{
			//Click inside ListView Item
			Border brdr = sender as Border;

		}
		private ListView GetParent ( Visual v )
		{
			while ( v != null )
			{
				v = VisualTreeHelper . GetParent ( v ) as Visual;
				if ( v is ListView )
					break;
			}
			return v as ListView;
		}

		private async void DbList_LoadBtnPressed ( object sender , MouseButtonEventArgs e )
		{
			int min = 0, max = 0, tot = 0;
			//			return;
			// Reset the background of the Load Data button
			//			Border b = sender as Border;
			//			if ( b == null )
			//				return;
			//			b . Background = FindResource ( "Gray3" ) as SolidColorBrush;
			//			b . BorderBrush = FindResource ( "Red3" ) as SolidColorBrush;
			Mouse . OverrideCursor = System . Windows . Input . Cursors . Wait;
			datagrid . ItemsSource = null;
			datagrid . Items . Clear ( );
			// clear listbox.
			UCListbox . ItemsSource = null;
			UCListbox . Items . Clear ( );

			UCListbox . Refresh ( );
			SqlBankcollection . Clear ( );
			min = Convert . ToInt32 ( MinValue . Text );
			max = Convert . ToInt32 ( MaxValue . Text );
			tot = Convert . ToInt32 ( MaxRecords . Text );

			// Load data  from both dbs'
			DataTable dtBank = new DataTable();
			dtBank = BankCollection . LoadSelectedBankData ( Min: min , Max: max , Tot: tot );
			BankCollection . LoadSelectedCollection ( SqlBankcollection , max: max , dtBank, true );
			//await BankCollection . LoadBank ( SqlBankcollection , "SQLDBVIEWER" , 1 , true );

			// This loads the Customers Db
			 DbList_LoadBtnPressed2 ( sender , e );
			Mouse . OverrideCursor = System . Windows . Input . Cursors . Arrow;

		}

		private void DbList_LoadBtnPressed2 ( object sender , MouseButtonEventArgs e )
		{
			int min = 0, max = 0, tot = 0;
			datagrid2 . ItemsSource = null;
			datagrid2 . Items . Clear ( );
			UCListbox2 . ItemsSource = null;
			UCListbox2 . Items . Clear ( );
			UCListbox2 . Refresh ( );
			datagrid2 . Refresh ( );
			UCListbox2 . UpdateLayout ( );
			Mouse . OverrideCursor = System . Windows . Input . Cursors . Wait;
			SqlCustcollection . Clear ( );
			min = Convert . ToInt32 ( MinValue . Text );
			max = Convert . ToInt32 ( MaxValue . Text );
			tot = Convert . ToInt32 ( MaxRecords . Text );
			AllCustomers . LoadCust ( SqlCustcollection , "MULTIVIEWLISTBOXVIEWERS" , start: min , end: max , max: tot , NotifyAll: true );
			Mouse . OverrideCursor = System . Windows . Input . Cursors . Arrow;

		}
		private void Columns_SelectionChanged ( object sender , SelectionChangedEventArgs e )
		{
			ListBox lb = sender as ListBox;
			var  Content = lb . SelectedItem;
			//ListBoxItem lbi = Content as  ListBoxItem;
			var selection = int.Parse(Content.ToString());
			if ( selection >= 0 && selection <= 2 )
			{
				//					int[] sortorder = { 2,3,1,5,4,7,6,0};
				DataGridSupport . SortBankColumns ( datagrid , DGBankColumnsCollection , selection );
				datagrid . Refresh ( );
				DataGridSupport . SortCustomerColumns ( datagrid2 , DGCustColumnsCollection , selection );
				datagrid2 . Refresh ( );
			}

		}

		#region LINQ methods
		private void Linq1_Click ( object sender , RoutedEventArgs e )
		{
			//			BackupBankcollection = SqlBankcollection;
			LinqResults lq = new LinqResults();
			var accounts = from items in SqlBankcollection
					   where (items.AcType == 1)
					   orderby items.CustNo
					   select items;
			BankCollection vm = new BankCollection();
			foreach ( var item in accounts )
			{
				vm . Add ( item );
			}
			UCListbox . ItemsSource = null;
			UCListbox . Items . Clear ( );
			UCListbox . ItemsSource = vm;
		}

		private void Linq2_Click ( object sender , RoutedEventArgs e )
		{
			//select items;
			//			BackupBankcollection = SqlBankcollection;
			var accounts = from items in SqlBankcollection
					   where (items.AcType == 2)
					   orderby items.CustNo
					   select items;
			BankCollection vm = new BankCollection();
			foreach ( var item in accounts )
			{
				vm . Add ( item );
			}
			UCListbox . ItemsSource = null;
			UCListbox . Items . Clear ( );
			UCListbox . ItemsSource = vm;
		}

		private void Linq3_Click ( object sender , RoutedEventArgs e )
		{
			//select items;
			//			BackupBankcollection = SqlBankcollection;
			var accounts = from items in SqlBankcollection
					   where (items.AcType == 3)
					   orderby items.CustNo
					   select items;
			BankCollection vm = new BankCollection();
			foreach ( var item in accounts )
			{
				vm . Add ( item );
			}
			UCListbox . ItemsSource = null;
			UCListbox . Items . Clear ( );
			UCListbox . ItemsSource = vm;
		}
		private void Linq4_Click ( object sender , RoutedEventArgs e )
		{
			//select items;
			//			BackupBankcollection = SqlBankcollection;
			var accounts = from items in SqlBankcollection
					   where (items.AcType == 4)
					   orderby items.CustNo
					   select items;
			BankCollection vm = new BankCollection();
			foreach ( var item in accounts )
			{
				vm . Add ( item );
			}
			UCListbox . ItemsSource = null;
			UCListbox . Items . Clear ( );
			UCListbox . ItemsSource = vm;
		}

		/// <summary>
		/// Create a subset that only includes those cust acs with >1 bankaccounts
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Linq5_Click ( object sender , RoutedEventArgs e )
		{
			//select All the items first;
			//			BackupBankcollection = SqlBankcollection;
			var accounts = from items in SqlBankcollection orderby items.CustNo, items.BankNo select items;
			//Next Group collection on CustNo
			var grouped = accounts.GroupBy(b => b.CustNo);

			//Now filter content down to only those a/c's with multiple Bank A/c's
			var sel = from g in grouped
				    where g.Count() > 1
				    select g;

			// Finally, iterate thru the list of grouped CustNo's matching to CustNo in the full BankAccounts data
			// giving us ONLY the full records for any records that have > 1 Bank accounts
			List<BankAccountViewModel> output = new List<BankAccountViewModel>();

			foreach ( var item1 in sel )
			{
				foreach ( var item2 in accounts )
				{
					if ( item2 . CustNo . ToString ( ) == item1 . Key )
					{
						output . Add ( item2 );
					}
				}
			}
			BankCollection vm = new BankCollection();
			foreach ( var item in output )
			{
				vm . Add ( item );
			}
			UCListbox . ItemsSource = null;
			UCListbox . Items . Clear ( );
			UCListbox . ItemsSource = vm;
		}
		//*************************************************************************************************************//
		// Turn filter OFF
		/// <summary>
		/// Reset our viewer to FULL record display by reloading  the Db from disk - JIC
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Linq6_Click ( object sender , RoutedEventArgs e )
		{
			//			BackupBankcollection = null;
			SqlBankcollection = null;
			UCListbox . ItemsSource = null;
			Flags . SqlCustActive = true;
			BankCollection . LoadBank ( SqlBankcollection , "SQLDBVIEWER" , 1 , true );
			UCListbox . Refresh ( );
		}



		#endregion LINQ methods
		private void ViewJsonRecord_Click ( object sender , RoutedEventArgs e )
		{
			//============================================//
			//MENU ITEM 'Read and display JSON File'
			//============================================//

			//causing debugger to break 5/10/21

			//string Output = "";
			//Mouse . OverrideCursor = Cursors . Wait;
			//BankAccountViewModel bvm = this . UCListbox . SelectedItem as BankAccountViewModel;
			//Output = JsonSupport . CreateShowJsonText ( true, "BANKACCOUNT", bvm, "BankAccountViewModel" );
			//MessageBox . Show ( Output, "Currently selected record in JSON format", MessageBoxButton . OK, MessageBoxImage . Information, MessageBoxResult . OK );
		}

		private void linq1_IsMouseDirectlyOverChanged ( object sender , DependencyPropertyChangedEventArgs e )
		{
			//			int x = 0;
		}


		private void datagrid_PreviewDragEnter ( object sender , DragEventArgs e )
		{
			e . Effects = ( DragDropEffects ) DragDropEffects . Move;
			//Debug . WriteLine ( $"Setting drag cursor...." );
		}

		//================================================================================================
		#region DRAG CODE
		private void datagrid_PreviewMouseLeftButtonup ( object sender , MouseButtonEventArgs e )
		{
			ScrollBarMouseMove = false;
			this . IsSelected = false;

		}

		private void datagrid_PreviewMouseLeftButtondown ( object sender , MouseButtonEventArgs e )
		{

			// Gotta make sure it is not anywhere in the Scrollbar we clicked on 
			if ( Utils . HitTestScrollBar ( sender , e ) )
			{
				ScrollBarMouseMove = true;
				return;
			}
			if ( Utils . HitTestHeaderBar ( sender , e ) )
				return;

			_startPoint = e . GetPosition ( null );
			// Make sure the left mouse button is pressed down so we are really moving a record
			if ( e . LeftButton == MouseButtonState . Pressed )
			{
				IsLeftButtonDown = true;
			}
			string t = sender.GetType().ToString();
			object b = e.OriginalSource;
			if ( t . Contains ( "DataGrid" ) )// && b.Name == "DGR_Border")
			{
				DataGrid dg = sender as DataGrid;
				if ( dg . Name == "datagrid" )
				{
					GridSelection = datagrid . SelectedIndex;
					ListSelection = GridSelection;
					UCListbox . SelectedIndex = ListSelection;
				}
				else
				{
					GridSelection2 = datagrid2 . SelectedIndex;
					ListSelection2 = GridSelection2;
					UCListbox2 . SelectedIndex = ListSelection;
				}
			}
			else
			{
				ListSelection = UCListbox . SelectedIndex;
				GridSelection = ListSelection;
				datagrid . SelectedIndex = ListSelection;
			}

		}
		private void datagrid_PreviewMouseLeftButtondown2 ( object sender , MouseButtonEventArgs e )
		{
			// Gotta make sure it is not anywhere in the Scrollbar we clicked on 
			if ( Utils . HitTestScrollBar ( sender , e ) )
			{
				ScrollBarMouseMove = true;
				return;
			}
			if ( Utils . HitTestHeaderBar ( sender , e ) )
				return;

			_startPoint = e . GetPosition ( null );
			// Make sure the left mouse button is pressed down so we are really moving a record
			if ( e . LeftButton == MouseButtonState . Pressed )
			{
				IsLeftButtonDown = true;
			}
			string t = sender.GetType().ToString();
			object b = e.OriginalSource;
			if ( t . Contains ( "DataGrid" ) )// && b.Name == "DGR_Border")
			{
				DataGrid dg = sender as DataGrid;
				if ( dg . Name == "datagrid" )
				{
					GridSelection = datagrid . SelectedIndex;
					ListSelection = GridSelection;
					UCListbox . SelectedIndex = ListSelection;
				}
				else
				{
					GridSelection2 = datagrid2 . SelectedIndex;
					ListSelection2 = GridSelection2;
					UCListbox2 . SelectedIndex = ListSelection2;
				}
			}
			else
			{
				ListSelection2 = UCListbox2 . SelectedIndex;
				GridSelection2 = ListSelection2;
				datagrid2 . SelectedIndex = ListSelection2;
			}

		}

		private void datagrid_PreviewMouseMove ( object sender , MouseEventArgs e )
		{
			bool IsCust = false;
			Point mousePos = e.GetPosition(null);
			Vector diff = _startPoint - mousePos;
			string t1 = sender.GetType().ToString();
			if ( e . LeftButton == MouseButtonState . Pressed &&
			    Math . Abs ( diff . X ) > SystemParameters . MinimumHorizontalDragDistance ||
			    Math . Abs ( diff . Y ) > SystemParameters . MinimumVerticalDragDistance )
			{
				if ( IsLeftButtonDown && e . LeftButton == MouseButtonState . Pressed )
				{
					bool isvalid = false;
					if ( t1 . Contains ( "ListView" ) )
					{
						isvalid = true;
						ListView dg = sender as ListView ;
						if ( dg . Name == "UCListbox2" )
							IsCust = true;
					}
					else if ( t1 . Contains ( "DataGrid" ) )
					{
						isvalid = true;
						DataGrid dg = sender as DataGrid;
						if ( dg . Name == "datagrid2" )
							IsCust = true;
					}
					if ( isvalid )
					{
						if ( ScrollBarMouseMove )
							return;
						// We are dragging from the DETAILS grid
						//Working string version
						if ( IsCust == false )
						{
							BankAccountViewModel bvm = new BankAccountViewModel();
							if ( t1 . Contains ( "ListView" ) )
								bvm = UCListbox . SelectedItem as BankAccountViewModel;
							else
								bvm = datagrid . SelectedItem as BankAccountViewModel;
							string str = GetExportRecords.CreateTextFromRecord(bvm, null, null, true, false);
							string dataFormat = DataFormats.Text;
							DataObject dataObject = new DataObject(dataFormat, str);
							DragDrop . DoDragDrop (
							datagrid ,
							dataObject ,
							DragDropEffects . Copy );
						}
						else
						{
							CustomerViewModel cvm = new CustomerViewModel();
							if ( t1 . Contains ( "ListView" ) )
								cvm = UCListbox2 . SelectedItem as CustomerViewModel;
							else
								cvm = datagrid2 . SelectedItem as CustomerViewModel;
							string str = GetExportRecords.CreateTextFromRecord(null, null, cvm, true, false);
							string dataFormat = DataFormats.Text;
							DataObject dataObject = new DataObject(dataFormat, str);
							DragDrop . DoDragDrop (
							datagrid ,
							dataObject ,
							DragDropEffects . Copy );
						}
						IsLeftButtonDown = false;
					}
				}
			}
		}

		private void DbList_ShowDropGrid ( object sender , MouseButtonEventArgs e )
		{
			DropDataGridData ddg = new DropDataGridData();
			ddg . Show ( );
		}
		private void CloseBtn_PreviewMouseLeftButtonDown ( object sender , MouseButtonEventArgs e )
		{
			this . Close ( );
		}

		private void ResizeUp ( object sender , MouseButtonEventArgs e )
		{
			datagrid . RowHeight += 1;
			datagrid2 . RowHeight += 1;

		}

		private void ResizeDn ( object sender , MouseButtonEventArgs e )
		{
			datagrid . RowHeight -= 1;
			datagrid2 . RowHeight -= 1;
		}

		private void LbItem_PreviewMouseLeftButtonUp2 ( object sender , MouseButtonEventArgs e )
		{
			this . IsSelected = false;
		}

		private void LbItem_PreviewMouseLeftButtonUp ( object sender , MouseButtonEventArgs e )
		{
			this . IsSelected = false;
		}

		private void DbListbox_PreviewMouseLeftButtonUp2 ( object sender , MouseButtonEventArgs e )
		{
			this . IsSelected = false;
		}

		private void DbListbox_PreviewMouseLeftButtonUp ( object sender , MouseButtonEventArgs e )
		{
			this . IsSelected = false;
		}
	}
	#endregion DRAG CODE

	#region VisualTree
	public class FocusVisualTreeChanger
	{
		public static bool GetIsChanged ( DependencyObject obj )
		{
			return ( bool ) obj . GetValue ( IsChangedProperty );
		}

		public static void SetIsChanged ( DependencyObject obj , bool value )
		{
			obj . SetValue ( IsChangedProperty , value );
		}

		public static readonly DependencyProperty IsChangedProperty =
		DependencyProperty.RegisterAttached("IsChanged", typeof(bool), typeof(FocusVisualTreeChanger), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, IsChangedCallback));

		private static void IsChangedCallback ( DependencyObject d , DependencyPropertyChangedEventArgs e )
		{
			if ( true . Equals ( e . NewValue ) )
			{
				FrameworkContentElement contentElement = d as FrameworkContentElement;
				if ( contentElement != null )
				{
					contentElement . FocusVisualStyle = null;
					return;
				}

				FrameworkElement element = d as FrameworkElement;
				if ( element != null )
				{
					element . FocusVisualStyle = null;
				}
			}
		}
		#endregion VisualTree

	}

}
