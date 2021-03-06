using Newtonsoft .Json;

using System;
using System .Collections .Generic;
using System . Collections . ObjectModel;
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
using System .Windows .Threading;

using WPFPages .ViewModels;

namespace WPFPages .Views
{
	/// <summary>
	/// Interaction logic for TestBankDbView.xaml
	/// </summary>
	public partial class TestBankDbView : Window
	{
		private static readonly DataGridColumn dataGridColumn   ;
		private DataGridColumn[] DGBankColumnsCollection = {dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,
			dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn };
		private DataGridColumn[] DGCustColumnsCollection
			= {dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,
			dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn ,dataGridColumn };
		private DataGridColumn[] DGDetailsColumnsCollection= {dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn,
			dataGridColumn,dataGridColumn,dataGridColumn,dataGridColumn };

		public TestBankDbView ( )
		{
			Startup = true;
			InitializeComponent ( );
			this .Show ( );
			this .Refresh ( );
		}
		public TestBankCollection TestBankcollection = null;

		// Get our personal Collection view of the Db
		public CollectionView TestBankviewerView { get; set; }

		public BankAccountViewModel bvm = new BankAccountViewModel();


		private bool IsDirty = false;
		static bool Startup = true;
		private bool LinktoParent = false;
		private bool LinkToAllRecords = false;
		private bool LinktoMultiParent = false;
		//		private bool Triggered = false;
		private bool LoadingDbData = false;
		private bool RowHasBeenEdited { get; set; }
		private bool keyshifted { get; set; }
		private bool IsEditing { get; set; }
		public static int bindex { get; set; }
		public bool IsLeftButtonDown { get; set; }

		private Point _startPoint { get; set; }
		private string _bankno = "";
		private string _custno = "";
		private string _actype = "";
		private string _balance = "";
		private string _odate = "";
		private string _cdate = "";
		private SqlDbViewer SqlParentViewer;
		private MultiViewer MultiParentViewer;
		private Thread t1;

		//		#region INotifyPropertyChanged Members

		//public event PropertyChangedEventHandler PropertyChanged;

		//protected void NotifyPropertyChanged ( string PropertyName )
		//{
		//	if ( null != PropertyChanged )
		//	{
		//		PropertyChanged ( this ,
		//			new PropertyChangedEventArgs ( PropertyName ) );
		//	}
		//}
		//#endregion INotifyPropertyChanged Members


		//private int search;
		//public int Search
		//{
		//	get { return search; }
		//	set
		//	{
		//		search = value;
		//		NotifyPropertyChanged ( "Search" );
		//		//BankGrid,.Refresh ( );
		//	}
		//}	  


		#region DP Setup - GOOD INFO

		//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$//
		// CREATING YOUR OWN DEPENDENCY PROPERTY
		// This one simply increments an Int value the we 
		// bind to a TextBlock, so it simply displays the counter value
		// automatically increasing to max, thren back to zero
		//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$//

		/*
		 *  CAUTION - YOU WILL NEED TO CREATE THIS SOMEWHERE INSIDE THE MODULE YU WABNT TO USE IT IN, 
		 *  AS IT REQUIRES A DATACONTEXT TO BE "BINDED" TO IN THE xaml CODE
		  */
		public int Counter
		{
			get { return ( int ) GetValue ( CounterProperty ); }
			set { SetValue ( CounterProperty , value ); }
		}

		//To use this, your C# code should be something like this : although this is fancy ot create an auto timer
		/*
				DispatcherTimer dispatcherTimer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal,
					delegate
					{
						int newvalue = 0;
						if (Counter == int.MaxValue) newvalue = 0;
						else newvalue = Counter + 1;
						SetValue(CounterProperty, newvalue);
					}, Dispatcher);
		*/

		// Using a DependencyProperty as the backing store for Counter.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CounterProperty =
		    DependencyProperty.Register("Counter", typeof(int), typeof(TestBankDbView), new PropertyMetadata(25));
		// Typeof (int ) needs to match declaration type !!
		// OwnerClass is This class (You cannot use 'this'), PropertyMetadata holds the STARTING Value in this case

		//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$//

		#endregion DP Setup - GOOD INFO

		// Crucial structure for use when a Grid row is being edited
		private RowData bvmCurrent = null;

		#region Mouse support
		private void DoDragMove ( )
		{//Handle the button NOT being the left mouse button
		 // which will crash the DragMove Fn.....
			try
			{ this .DragMove ( ); }
			catch { return; }
		}
		#endregion Mouse support

		#region Startup/ Closedown
		private async void Window_Loaded ( object sender , RoutedEventArgs e )
		{
			// Handle the new Dependency property
			DispatcherTimer dispatcherTimer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal,
				delegate
				{
					int newvalue = 0;
					if (Counter == int.MaxValue)
						newvalue = 0;
					else
						newvalue = Counter + 1;
					SetValue(CounterProperty, newvalue);
				},Dispatcher);


			Mouse .OverrideCursor = System .Windows .Input .Cursors .Wait;
			this .Show ( );
			this .Refresh ( );
			Startup = true;
			//Identify individual windows for update protection
			this .Tag = ( Guid ) Guid .NewGuid ( );
			string ndx = (string)Properties.Settings.Default["BankDbView_bindex"];
			bindex = int .Parse ( ndx );
			this .BankGrid .SelectedIndex = bindex < 0 ? 0 : bindex;

			//			this . MouseDown += delegate { DoDragMove ( ); };
			Utils .SetupWindowDrag ( this );
			// An EditDb has changed the current index 
			EventControl .EditIndexChanged += EventControl_EditIndexChanged;
			// A Multiviewer has changed the current index 
			EventControl .MultiViewerIndexChanged += EventControl_EditIndexChanged;
			// Another viewer has changed the current index 
			EventControl .ViewerIndexChanged += EventControl_EditIndexChanged;      // Callback in THIS FILE
			EventControl .ViewerDataUpdated += EventControl_DataUpdated;
			EventControl .BankDataLoaded += EventControl_BankDataLoaded;

			EventControl .TestDataChanged += EventControl_TestDataChanged;

			EventControl .GlobalDataChanged += EventControl_GlobalDataChanged;

			await TestBankCollection .LoadBank ( TestBankcollection , "BANKDBVIEW" , 3 , true );

			//SaveBttn .IsEnabled = false;
			// Save linkage setting as we need to disable it while we are loading
			bool tmp = Flags.LinkviewerRecords;

			// Set window to TOPMOST
			OntopChkbox .IsChecked = true;
			this .Topmost = true;
			this .Focus ( );
			// Reset linkage setting
			Flags .LinkviewerRecords = tmp;
			if ( Flags .LinkviewerRecords )
			{
				LinkRecords .IsChecked = true;
				LinkToAllRecords = true;
				LinktoParent = true;
			}
			else
			{
				LinkRecords .IsChecked = false;
				LinkToAllRecords = false;
				LinktoParent = false;
			}
			LinktoMultiParent = false;
			// start our linkage monitor
			t1 = new Thread ( checkLinkages );
			t1 .IsBackground = true;
			t1 .Priority = ThreadPriority .Lowest;
			t1 .Start ( );
			Startup = false;
		}
		private async void EventControl_GlobalDataChanged ( object sender , GlobalEventArgs e )
		{
			if ( e .CallerType == "TESTBANKDBVIEW" )
				return;
			//Update our own data tyoe only
			await BankCollection .LoadBank ( null , "BANKACCOUNT" , 1 , true );
		}

		private void EventControl_TestDataChanged ( object sender , LoadedEventArgs e )
		{
			// dont update if we triggered the change
			// Works well 23/6/21
			if ( e .Bankno == this .Tag .ToString ( ) )
				return;
			// Dont update if another Bank viewer did the update
			if ( e .CallerDb == "BANKACCOUNT" )
				return;
			// else update the grid
			TestBankCollection .LoadBank ( TestBankcollection , "BANKDBVIEW" , 1 , true );

			//			int currsel = this.BankGrid.SelectedIndex, SelectedIndex;
			//			this.BankGrid.Refresh();
			//this.BankGrid.ItemsSource = null; 
			//this.BankGrid.ItemsSource = TestBankcollection;
			//			this.BankGrid.SelectedIndex= currsel;
			//			this.BankGrid.UpdateLayout(); ;
		}

		private void EventControl_EditIndexChanged ( object sender , IndexChangedArgs e )
		{
			//			Triggered = true;

			// Handle Selection change in another windowif linkage is ON
			if ( IsEditing || LinkRecords .IsChecked == false )
			{
				//IsEditing = false;
				return;
			}
			this .BankGrid .SelectedIndex = e .Row;
			bindex = e .Row;
			this .BankGrid .Refresh ( );
			//			Triggered = false;
		}
		private async void EventControl_DataUpdated ( object sender , LoadedEventArgs e )
		{
			if ( e .CallerDb == "BANKDBVIEW" || e .CallerDb == "BANKACCOUNT" )
				return;
			int currsel = this.BankGrid.SelectedIndex;
			Debug .WriteLine ( $"BankDbView : Data changed event notification received successfully." );
			Mouse .OverrideCursor = Cursors .Wait;
			await TestBankCollection .LoadBank ( TestBankcollection , "BANKDBVIEW" , 3 , true );
			IsDirty = false;
		}

		//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^//
		private  void EventControl_BankDataLoaded ( object sender , LoadedEventArgs e )
		{
			// Event handler for BankDataLoaded
			if ( e .DataSource == null )
			{ Mouse .OverrideCursor = Cursors .Arrow; return; }
			// ONLY proceeed if we triggered the new data request
			if ( e .CallerDb != "BANKDBVIEW" )
			{ Mouse .OverrideCursor = Cursors .Arrow; return; }

			Stopwatch sw = new Stopwatch();
			sw .Start ( );

			Debug .WriteLine ( "BANKDBVIEW : Loading Bank Data ..." );
			int currsel = this.BankGrid.SelectedIndex;

			this .BankGrid .ItemsSource = null;
			this .BankGrid .Items .Clear ( );

			LoadingDbData = true;
			TestBankcollection = e .DataSource as TestBankCollection;

			#region Filtering setup

			// Get the default Collections View as our default ItemSource
			TestBankviewerView = CollectionViewSource .GetDefaultView ( TestBankcollection ) as CollectionView;
			//Assign Collection to the datagrid
			this .BankGrid .ItemsSource = TestBankcollection;
			//Set the fillter we want up
			TestBankviewerView .Filter = new Predicate<object> ( ( obj ) => FilterBankData ( obj as BankAccountViewModel ) );
			// Clear DataGrid down and assign new (Filtered) data to it
			this .BankGrid .ItemsSource = null;
			this .BankGrid .Items .Clear ( );
			this .BankGrid .ItemsSource = TestBankviewerView;
			// Filtering completed
			#endregion

			Utils .SetUpGridSelection ( BankGrid , bindex );
			Mouse .OverrideCursor = Cursors .Arrow;
			IsDirty = false;
			sw .Stop ( );
			Debug .WriteLine ( $"BANKDBVIEW : Bank Data auto loaded in {( double ) sw .ElapsedMilliseconds / ( double ) 1000} secs\n" );
		}
		#endregion Startup/ Closedown

		#region Filtering Methodology
		//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^//
		// THESE TWO FUNCTIONS LIVE TOGETHER AND HANDLE FILTERING OF A COLLECTION VIEW

		/// <summary>
		/// This is the CRUCIAL method that updates the grid whenever we change its content
		/// which changes the content of the CollectonView we have assigned to our DataGrid's Itemssource
		/// so we just Refresh() the CollectionView to update the grid with JUST the  data matching our filter condition
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void filter_TextChanged ( object sender , TextChangedEventArgs e )
		{
			if ( BankGrid .ItemsSource != null )
				TestBankviewerView .Refresh ( );
		}

		/// <summary>
		/// Filter method to filter BankAccount CustNo into a Datagrid
		/// It checks that the data in  the Collectionview ONLY matches if it Contains the text for the filter.Text field
		/// </summary>
		/// <param name="bvm"></param>
		/// <returns></returns>
		private bool FilterBankData ( BankAccountViewModel bvm )
		{
			if ( bvm == null ) return false;
			string input="";
			bool b = false;
			string  srchtxt = filter .Text;
			if ( ChkboxBank .IsChecked == true )
			{
				input = bvm .BankNo .ToString ( );
				b = input .StartsWith ( srchtxt );
			}
			else if ( ChkboxCustomer .IsChecked == true )
			{
				input = bvm .CustNo .ToString ( );
				b = input .StartsWith ( srchtxt );
			}
			else if ( ChkboxActype .IsChecked == true )
			{
				input = bvm .AcType.ToString();
				if ( srchtxt != null && srchtxt != "" && input != "" )
					b = int .Parse ( input ) == int .Parse ( srchtxt );
			}
			return b;
		}

		private async void ChkboxCustomer_Click ( object sender , RoutedEventArgs e )
		{
			ChkboxBank .IsChecked = false;
			ChkboxActype .IsChecked = false;
			ChkboxCustomer .IsChecked = true;
			TestBankcollection = null;
			await TestBankCollection .LoadBank ( TestBankcollection , "BANKDBVIEW" , 3 , true );
			filter .Text = "105";
			//BankGrid .ItemsSource = null;
			//BankGrid .Items .Clear ( );
			//BankGrid .ItemsSource = TestBankviewerView;
		}

		private async void ChkboxBank_Click ( object sender , RoutedEventArgs e )
		{
			ChkboxCustomer .IsChecked = false;
			ChkboxActype .IsChecked = false;
			ChkboxBank .IsChecked = true;
			TestBankcollection = null;
			await TestBankCollection .LoadBank ( TestBankcollection , "BANKDBVIEW" , 3 , true );
			filter .Text = "41";
			//BankGrid .ItemsSource = null;
			//BankGrid .Items .Clear ( );
			//BankGrid .ItemsSource = TestBankviewerView;
		}
		private async void ChkboxActype_Click ( object sender , RoutedEventArgs e )
		{
			ChkboxCustomer .IsChecked = false;
			ChkboxBank .IsChecked = false;
			ChkboxActype .IsChecked = true;
			TestBankcollection = null;
			await TestBankCollection .LoadBank ( TestBankcollection , "BANKDBVIEW" , 3 , true );
			filter .Text = "1";
		}
		#endregion Filtering

		#region DATA EDIT CONTROL METHODS
		/// <summary>
		///  DATA EDIT CONTROL METHODS
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BankGrid_BeginningEdit ( object sender , DataGridBeginningEditEventArgs e )
		{
			IsEditing = true;
			// Save  the current data for checking later on when we exit editing
			// but first, check to see if we already have one being saved !
			if ( bvmCurrent == null )
			{
				// Nope, so create a new one and get on with the edit process
				BankAccountViewModel tmp = new BankAccountViewModel();
				tmp = e .Row .Item as BankAccountViewModel;
				// This sets up a new bvmControl object if needed, else we  get a null back
				bvmCurrent = CellEditControl .BankGrid_EditStart ( bvmCurrent , e );
			}
			// doesn't work right now - returns NULL
			//string str = CellEditControl.GetSelectedCellValue ( this . BankGrid );
		}

		/// <summary>
		/// does nothing at all because it is called whenver any single cell is exited
		///     and not just when ENTER is hit to save any changes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void BankGrid_CellEditEnding ( object sender , DataGridCellEditEndingEventArgs e )
		{
			if ( bvmCurrent == null )
				return;

			// Has Data been changed in one of our rows. ?
			BankAccountViewModel dvm = this.BankGrid.SelectedItem as BankAccountViewModel;
			dvm = e .Row .Item as BankAccountViewModel;

			// The sequence of these next 2 blocks is critical !!!
			//if we get here, make sure we have been NOT been told to EsCAPE out
			//	this is a DataGridEditAction dgea
			if ( e .EditAction == 0 )
			{
				// ENTER was hit, so data has been saved - go ahead and reload our grid with new data
				// and this will notify any other open viewers as well
				bvmCurrent = null;
				await TestBankCollection .LoadBank ( TestBankcollection , "BANKDBVIEW" , 1 , true );
				return;
			}

			if ( CellEditControl .BankGrid_EditEnding ( bvmCurrent , BankGrid , e ) == false )
			{       // No change made
				return;
			}
		}

		/// <summary>
		/// Compares 2 rows of BANKACCOUNT or DETAILS data to see if there are any changes
		/// </summary>
		/// <param name="ss"></param>
		/// <returns></returns>
		private bool CompareDataContent ( BankAccountViewModel ss )
		{
			if ( ss .CustNo != bvmCurrent ._CustNo .ToString ( ) )
				return false;
			if ( ss .BankNo != bvmCurrent ._BankNo .ToString ( ) )
				return false;
			if ( ss .AcType != bvmCurrent ._AcType )
				return false;
			if ( ss .IntRate != bvmCurrent ._IntRate )
				return false;
			if ( ss .Balance != bvmCurrent ._Balance )
				return false;
			if ( ss .ODate != bvmCurrent ._ODate )
				return false;
			if ( ss .CDate != bvmCurrent ._CDate )
				return false;
			return true;
		}
		/// <summary>
		/// Called when an EDIT ends. This occurs whenever a field is exited, even if ENTER has NOT been pressed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void ViewerGrid_RowEditEnding ( object sender , System .Windows .Controls .DataGridRowEditEndingEventArgs e )
		{
			int currow = 0;
			// if our saved row is null, it has already been checked in Cell_EndDedit processing
			// and found no changes have been made, so we can abort this update
			if ( bvmCurrent == null )
				return;

			currow = this .BankGrid .SelectedIndex;
			// This is now confirmed as being CHANGED DATA in the current row
			// So we proceed and update SQL Db's' and notify all open viewers as well
			BankAccountViewModel ss = new BankAccountViewModel();
			ss = this .BankGrid .SelectedItem as BankAccountViewModel;
			SQLHandlers sqlh = new SQLHandlers();
			await sqlh .UpdateDbRowAsync ( "BANKACCOUNT" , ss , this .BankGrid .SelectedIndex );

			//this.BankGrid.SelectedIndex = currow;
			//this.BankGrid.SelectedItem = currow;
			//Utils.SetUpGridSelection(this.BankGrid, currow);
			IsDirty = false;
			// Notify EditDb to upgrade its grid
			if ( Flags .CurrentEditDbViewer != null )
				Flags .CurrentEditDbViewer .UpdateGrid ( "BANKACCOUNT" );

			// ***********  DEFINITE WIN  **********
			// This DOES trigger a notification to SQLDBVIEWER AND OTHERS for sure !!!   14/5/21
			EventControl .TriggerTestDataChanged ( TestBankcollection ,
				new LoadedEventArgs
				{
					CallerType = "BANKDBVIEW" ,
					CallerDb = "BANKACCOUNT" ,
					DataSource = TestBankcollection ,
					RowCount = this .BankGrid .SelectedIndex ,
					SenderGuid = this .Tag .ToString ( ) ,
					Bankno = this .Tag .ToString ( )
				} );
			this .BankGrid .SelectedIndex = currow;
			this .BankGrid .SelectedItem = currow;
			Utils .SetUpGridSelection ( this .BankGrid , currow );
		}

		#endregion DATA EDIT CONTROL METHODS

		private void Close_Click ( object sender , RoutedEventArgs e )
		{
			Close ( );
		}

		private void Window_Closing ( object sender , System .ComponentModel .CancelEventArgs e )
		{
			//if ( ( Flags .LinkviewerRecords == false && IsDirty )
			//		|| SaveBttn .IsEnabled )
			//{
			//	MessageBoxResult result = MessageBox.Show
			//		("You have unsaved changes.  Do you want them saved now ?", "Possible Data Loss", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
			//	if ( result == MessageBoxResult .Yes )
			//	{
			//		SaveButton ( );
			//	}
			//	// Do not want ot save it, so disable  save button again
			//	SaveBttn .IsEnabled = false;
			//	IsDirty = false;
			//}

			// We must also clear our "loaded" columns, or else it stopsworking
			ObservableCollection<DataGridColumn> dgc = BankGrid.Columns;
			dgc . Clear ( );


			Flags . BankDbEditor = null;
			EventControl .EditIndexChanged -= EventControl_EditIndexChanged;
			// A Multiviewer has changed the current index 
			EventControl .MultiViewerIndexChanged -= EventControl_EditIndexChanged;
			// Another SqlDbviewer has changed the current index 
			EventControl .ViewerIndexChanged -= EventControl_EditIndexChanged;      // Callback in THIS FILE
			EventControl .ViewerDataUpdated -= EventControl_DataUpdated;
			EventControl .BankDataLoaded -= EventControl_BankDataLoaded;
			EventControl .GlobalDataChanged -= EventControl_GlobalDataChanged;

			EventControl .TestDataChanged -= EventControl_TestDataChanged;

			DataFields .DataContext = this .BankGrid .SelectedItem;
			TestBankcollection = null;
			Utils .SaveProperty ( "BankDbView_bindex" , bindex .ToString ( ) );
		}

		private void BankGrid_SelectionChanged ( object sender , System .Windows .Controls .SelectionChangedEventArgs e )
		{
			if ( LoadingDbData )
			{
				LoadingDbData = false;
				return;
			}
			bindex = this .BankGrid .SelectedIndex;
			//			Utils.SetUpGridSelection(this.BankGrid, this.BankGrid.SelectedIndex);
			Startup = true;
			DataFields .DataContext = bindex;

			if ( LinkToAllRecords )// && Triggered == false )
						     //if ( Flags . LinkviewerRecords && Triggered == false )
			{
				//				Debug . WriteLine ( $" 4-1 *** TRACE *** BANKDBVIEW : BankGrid_SelectionChanged  BANKACCOUNT - Sending TriggerEditDbIndexChanged Event trigger" );
				TriggerViewerIndexChanged ( this .BankGrid );
			}

			// check to see if an SqlDbViewer has been opened that we can link to
			if ( Flags .SqlBankViewer != null && LinkToParent .IsEnabled == false )
			{
				LinkToParent .IsEnabled = true;
				SqlParentViewer = Flags .SqlBankViewer;
			}
			//else if ( Flags . SqlBankViewer == null )
			//{
			//	if ( LinkToParent . IsEnabled )
			//	{
			//		LinkToParent . IsEnabled = false;
			//		LinkToParent . IsChecked = false;
			//		LinktoParent = false;
			//		SqlParentViewer = null;
			//	}
			//}

			// Only  do this if global link is OFF
			if ( LinktoParent )// && LinkRecords . IsChecked == false )
			{
				// update parents row selection
				//				string bankno = "";
				//				string custno = "";
				var dvm = this.BankGrid.SelectedItem as BankAccountViewModel;
				if ( dvm == null )
					return;

				if ( SqlParentViewer != null )
				{
					int rec = Utils.FindMatchingRecord(dvm.CustNo, dvm.BankNo, SqlParentViewer.BankGrid, "BANKACCOUNT");
					SqlParentViewer .BankGrid .SelectedIndex = rec;
					Utils .SetUpGridSelection ( SqlParentViewer .BankGrid , rec );
				}
				else if ( MultiParentViewer != null )
				{
					int rec = Utils.FindMatchingRecord(dvm.CustNo, dvm.BankNo, MultiParentViewer.BankGrid, "BANKACCOUNT");
					MultiParentViewer .BankGrid .SelectedIndex = rec;
					Utils .SetUpGridSelection ( MultiParentViewer .BankGrid , rec );
				}
			}
			if ( LinktoMultiParent )
			{
				Flags .SqlMultiViewer .BankGrid .SelectedIndex = this .BankGrid .SelectedIndex;
				Flags .SqlMultiViewer .BankGrid .ScrollIntoView ( this .BankGrid .SelectedIndex );
				Utils .SetUpGridSelection ( Flags .SqlMultiViewer .BankGrid , this .BankGrid .SelectedIndex );
			}

			//			Count.Text = $"{this.BankGrid.SelectedIndex} / { this.BankGrid.Items.Count.ToString()}";
			IsDirty = false;
			Startup = false;
			Debug .WriteLine ( $"Current Bank row  (bindex = {bindex}" );
		}

		//private async Task<bool> SaveButton ( object sender = null , RoutedEventArgs e = null )
		//{
		//	//inprogress = true;
		//	//bindex = this . BankGrid . SelectedIndex;
		//	//cindex = this . CustomerGrid . SelectedIndex;
		//	//dindex = this . DetailsGrid . SelectedIndex;

		//	// Get the current rows data
		//	IsDirty = false;
		//	int CurrentSelection = this.BankGrid.SelectedIndex;
		//	this .BankGrid .SelectedItem = this .BankGrid .SelectedIndex;
		//	BankAccountViewModel bvm = new BankAccountViewModel();
		//	bvm = this .BankGrid .SelectedItem as BankAccountViewModel;
		//	if ( bvm == null )
		//		return false;

		//	SaveFieldData ( );

		//	// update the current rows data content to send  to Update process
		//	//bvm.BankNo = Bankno.Text;
		//	//bvm.CustNo = Custno.Text;
		//	//bvm.AcType = Convert.ToInt32(acType.Text);
		//	//bvm.Balance = Convert.ToDecimal(balance.Text);
		//	//bvm.ODate = Convert.ToDateTime(odate.Text);
		//	//bvm.CDate = Convert.ToDateTime(cdate.Text);
		//	// Call Handler to update ALL Db's via SQL
		//	SQLHandlers sqlh = new SQLHandlers();
		//	await sqlh .UpdateDbRow ( "BANKACCOUNT" , bvm );

		//	TestBankcollection = null;
		//	TestBankCollection bank = new TestBankCollection();
		//	TestBankcollection = await bank .ReLoadBankData ( );

		//	//			Debug . WriteLine ( $" 4-3 *** TRACE *** BANKDBVIEW : SaveButton BANKACCOUNT - Sending TriggerBankDataLoaded Event trigger" );
		//	//			SendDataChanged ( null, this . BankGrid, "BANKACCOUNT" );

		//	//EventControl.TriggerViewerDataUpdated(TestBankcollection,
		//	//	new LoadedEventArgs
		//	//	{
		//	//		CallerType = "BANKDBVIEW",
		//	//		CallerDb = "BANKACCOUNT",
		//	//		DataSource = TestBankcollection,
		//	//		RowCount = this.BankGrid.SelectedIndex
		//	//	});

		//	//Gotta reload our data because the update clears it down totally to null
		//	//this . BankGrid . SelectedIndex = CurrentSelection;
		//	//this . BankGrid . SelectedItem = CurrentSelection;
		//	//this . BankGrid . Refresh ( );

		//	//this . BankGrid . ItemsSource = null;
		//	//this . BankGrid . ItemsSource = TestBankcollection;
		//	//this . BankGrid . Refresh ( );

		//	SaveBttn .IsEnabled = false;
		//	IsDirty = false;
		//	return true;
		//}


		/// <summary>
		/// Called by ALL edit fields
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectionChanged ( object sender , RoutedEventArgs e )
		{
			if ( !Startup )
				SaveFieldData ( );
		}

		/// <summary>
		/// Called by ALL edit fields when text is changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TextChanged ( object sender , TextChangedEventArgs e )
		{
			return;
			//if (!Startup) CompareFieldData();
		}

		private void SaveFieldData ( )
		{
			//_bankno = Bankno.Text;
			//_custno = Custno.Text;
			//_actype = acType.Text;
			//_balance = balance.Text;
			//_odate = odate.Text;
			//_cdate = cdate.Text;
		}

		private void OntopChkbox_Click ( object sender , RoutedEventArgs e )
		{
			if ( OntopChkbox .IsChecked == ( bool? ) true )
				this .Topmost = true;
			else
				this .Topmost = false;
		}

		private void SaveBtn ( object sender , RoutedEventArgs e )
		{
			//SaveButton ( sender , e );
		}

		private void LinkRecords_Click ( object sender , RoutedEventArgs e )
		{
			//			bool reslt = false;
			//if ( IsLinkActive ( reslt ) == false )
			//{
			//	LinkToParent . IsEnabled = false;
			//	LinkToParent . IsChecked = false;
			//	SqlParentViewer = null;
			//	LinkRecords . IsChecked = false;
			//}
			//else
			//{
			//	LinktoParent = !LinktoParent;
			//}
			// force viewers to change records in line with each other
			if ( LinkRecords .IsChecked == true )
			{
				Flags .LinkviewerRecords = true;
				LinkToAllRecords = true;
			}
			else
			{
				//				Flags . LinkviewerRecords = false;
				LinkToAllRecords = false;
			}
			if ( Flags .SqlBankViewer != null )
				Flags .SqlBankViewer .LinkRecords .IsChecked = Flags .LinkviewerRecords;
			if ( Flags .SqlCustViewer != null )
				Flags .SqlCustViewer .LinkRecords .IsChecked = Flags .LinkviewerRecords;
			if ( Flags .SqlDetViewer != null )
				Flags .SqlDetViewer .LinkRecords .IsChecked = Flags .LinkviewerRecords;
			if ( Flags .SqlMultiViewer != null )
				Flags .SqlMultiViewer .LinkRecords .IsChecked = Flags .LinkviewerRecords;
			if ( Flags .CustDbEditor != null )
				Flags .CustDbEditor .LinkRecords .IsChecked = Flags .LinkviewerRecords;
			if ( Flags .DetDbEditor != null )
				Flags .DetDbEditor .LinkRecords .IsChecked = Flags .LinkviewerRecords;
			LinkRecords .Refresh ( );
			if ( LinkToAllRecords == true )
			{
				LinktoParent = false;
				LinkToParent .IsEnabled = false;
				LinkToParent .IsChecked = false;
			}
			else
			{
				if ( SqlParentViewer != null )
					LinkToParent .IsEnabled = true;
				else
					LinkToParent .IsEnabled = false;
			}
		}
		/// <summary>
		/// Generic method to send Index changed Event trigger so that 
		/// other viewers can update thier own grids as relevant
		/// </summary>
		/// <param name="grid"></param>
		//*************************************************************************************************************//
		public void TriggerViewerIndexChanged ( DataGrid grid )
		{
			string SearchCustNo = "";
			string SearchBankNo = "";
			if ( grid .ItemsSource == null )
				return;
			BankAccountViewModel CurrentBankSelectedRecord = grid.SelectedItem as BankAccountViewModel;
			SearchCustNo = CurrentBankSelectedRecord .CustNo;
			SearchBankNo = CurrentBankSelectedRecord .BankNo;
			EventControl .TriggerViewerIndexChanged ( this ,
				new IndexChangedArgs
				{
					Senderviewer = this ,
					Bankno = SearchBankNo ,
					Custno = SearchCustNo ,
					dGrid = grid ,
					Sender = "BANKACCOUNT" ,
					SenderId = "BANKDBVIEW" ,
					Row = grid .SelectedIndex
				} );
		}

		private void BankGrid_PreviewMouseLeftButtondown ( object sender , MouseButtonEventArgs e )
		{
			// Gotta make sure it is not anywhere in the Scrollbar we clicked on 
			if ( Utils .HitTestScrollBar ( sender , e ) )
				return;
			if ( Utils .HitTestHeaderBar ( sender , e ) )
				return;
			_startPoint = e .GetPosition ( null );
			// Make sure the left mouse button is pressed down so we are really moving a record
			if ( e .LeftButton == MouseButtonState .Pressed )
			{
				IsLeftButtonDown = true;
			}
		}

		private void BankGrid_PreviewMouseMove ( object sender , MouseEventArgs e )
		{
			Point mousePos = e.GetPosition(null);
			Vector diff = _startPoint - mousePos;

			if ( e .LeftButton == MouseButtonState .Pressed &&
			    Math .Abs ( diff .X ) > SystemParameters .MinimumHorizontalDragDistance ||
			    Math .Abs ( diff .Y ) > SystemParameters .MinimumVerticalDragDistance )
			{
				if ( IsLeftButtonDown && e .LeftButton == MouseButtonState .Pressed )
				{
					if ( BankGrid .SelectedItem != null )
					{
						// We are dragging from the DETAILS grid
						//Working string version
						BankAccountViewModel bvm = new BankAccountViewModel();
						bvm = BankGrid .SelectedItem as BankAccountViewModel;
						string str = GetExportRecords.CreateTextFromRecord(bvm, null, null, true, false);
						string dataFormat = DataFormats.Text;
						DataObject dataObject = new DataObject(dataFormat, str);
						DragDrop .DoDragDrop (
						BankGrid ,
						dataObject ,
						DragDropEffects .Move );
						IsLeftButtonDown = false;
					}
				}
			}
		}
		#region Menu items

		private void Linq1_Click ( object sender , RoutedEventArgs e )
		{
			//select items;
			var bankaccounts = from items in TestBankcollection
						 where (items.AcType == 1)
						 orderby items.CustNo
						 select items;
			this .BankGrid .ItemsSource = bankaccounts;
		}
		private void Linq2_Click ( object sender , RoutedEventArgs e )
		{
			//select items;
			var bankaccounts = from items in TestBankcollection
						 where (items.AcType == 2)
						 orderby items.CustNo
						 select items;
			this .BankGrid .ItemsSource = bankaccounts;
		}
		private void Linq3_Click ( object sender , RoutedEventArgs e )
		{
			//select items;
			var bankaccounts = from items in TestBankcollection
						 where (items.AcType == 3)
						 orderby items.CustNo
						 select items;
			this .BankGrid .ItemsSource = bankaccounts;
		}
		private void Linq4_Click ( object sender , RoutedEventArgs e )
		{
			//select items;
			var bankaccounts = from items in TestBankcollection
						 where (items.AcType == 4)
						 orderby items.CustNo
						 select items;
			this .BankGrid .ItemsSource = bankaccounts;
		}
		private void Linq5_Click ( object sender , RoutedEventArgs e )
		{
			//select All the items first;			
			var bankaccounts = from items in TestBankcollection orderby items.CustNo, items.AcType select items;
			//Next Group BankAccountViewModel collection on Custno
			var grouped = bankaccounts.GroupBy(
				b => b.CustNo);

			//Now filter content down to only those a/c's with multiple Bank A/c's
			var sel = from g in grouped
				    where g.Count() > 1
				    select g;

			// Finally, iterate thru the list of grouped CustNo's matching to CustNo in the full Bankaccounts data
			// giving us ONLY the full records for any recordss that have > 1 Bank accounts
			List<BankAccountViewModel> output = new List<BankAccountViewModel>();
			foreach ( var item1 in sel )
			{
				foreach ( var item2 in bankaccounts )
				{
					if ( item2 .CustNo .ToString ( ) == item1 .Key )
					{
						output .Add ( item2 );
					}
				}
			}
			this .BankGrid .ItemsSource = output;
		}
		private void Linq6_Click ( object sender , RoutedEventArgs e )
		{
			var accounts = from items in TestBankcollection orderby items.CustNo, items.AcType select items;
			this .BankGrid .ItemsSource = accounts;
		}

		private void Filter_Click ( object sender , RoutedEventArgs e )
		{
			// Show Filter system
			MessageBox .Show ( "Filter dialog will appear here !!" );
		}

		private void Exit_Click ( object sender , RoutedEventArgs e )
		{
			Close ( );
		}

		private void Options_Click ( object sender , RoutedEventArgs e )
		{

		}
		#endregion Menu items



		/// <summary>
		/// Link record selection to parent SQL viewer window only
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkToParent_Click ( object sender , RoutedEventArgs e )
		{
			//			bool reslt = false;
			if ( LinkToParent .IsEnabled == false )
				return;

			if ( LinkToAllRecords == true )
			{
				LinkToParent .IsEnabled = false;
				LinkToParent .IsChecked = false;
				LinktoParent = false;
				return;
			}
			else
			{
				// NOT  linked to All Viewers
				if ( LinkToParent .IsChecked == true )
				{
					LinkRecords .IsEnabled = false;
					LinktoParent = true;
				}
				else
				{
					LinkRecords .IsEnabled = true;
					LinktoParent = false;
				}
			}
		}

		private void BankGrid_PreviewMouseRightButtonDown ( object sender , MouseButtonEventArgs e )
		{
			ContextMenu cm = this.FindResource("ContextMenu1") as ContextMenu;
			cm .PlacementTarget = this .BankGrid as DataGrid;
			cm .IsOpen = true;
		}

		private void Edit_LostFocus ( object sender , RoutedEventArgs e )
		{
			IsDirty = true;
			//SaveBttn .IsEnabled = true;
		}
		#region KEYHANDLER for EDIT fields

		// These let us tab thtorugh the editfields back and forward correctly
		private void Window_PreviewKeyUp ( object sender , KeyEventArgs e )
		{
			Debug .WriteLine ( $"  KEYUP key = {e .Key}, Shift = {keyshifted}" );

			if ( e .Key == Key .RightShift || e .Key == Key .LeftShift )
			{
				keyshifted = false;
				return;
			}

			if ( keyshifted && ( e .Key == Key .RightShift || e .Key == Key .LeftShift ) )
			{
				keyshifted = false;
				e .Handled = true;
				return;
			}

		}

		/// <summary>
		/// Key handling to allow proper tabbing between data Editing fieds
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_PreviewKeyDown ( object sender , KeyEventArgs e )
		{
			if ( e .Key == Key .RightShift || e .Key == Key .LeftShift )
			{
				keyshifted = true;
				e .Handled = true;
				return;
			}

			if ( keyshifted == false )
			{
				//if (e.Key == Key.Tab && e.Source == cdate)
				//{
				//	e.Handled = true;
				//	Custno.Focus();
				//	return;
				//}
				return;
			}
			else
			{
				// SHIFT KEY DOWN - KEY DOWN
				// Handle  the tabs to make them cycle around the data entry fields
				//if (e.Key == Key.Tab && e.Source == cdate)
				//{
				//	e.Handled = true;
				//	odate.Focus();
				//	return;
				//}
				//if (e.Key == Key.Tab && e.Source == Custno)
				//{
				//	e.Handled = true;
				//	cdate.Focus();
				//	//					Debug . WriteLine ( $"KEYDOWN Shift turned OFF" );
				//	return;
				//}
			}
		}

		#endregion KEYHANDLER for EDIT fields


		#region HANDLERS for linkage checkboxes, inluding Thread montior

		static bool IsLinkActive ( bool ParentLinkTo )
		{
			return Flags .SqlBankViewer != null && ParentLinkTo == false;
		}
		static bool IsMultiLinkActive ( bool MultiParentLinkTo )
		{
			if ( Flags .SqlMultiViewer == null )
				return false;
			else
				return true;
		}

		private void LinkToMulti_Click ( object sender , RoutedEventArgs e )
		{
			bool reslt = false;

			if ( IsMultiLinkActive ( reslt ) == false )
			{
				LinkToMulti .IsEnabled = false;
				LinkToMulti .IsChecked = false;
				MultiParentViewer = null;
				LinktoMultiParent = false;
			}
			else
			{
				LinktoMultiParent = !LinktoMultiParent;
				if ( LinktoMultiParent )
				{
					LinkToMulti .IsChecked = true;
					LinktoMultiParent = true;
				}
				else
				{
					LinkToMulti .IsChecked = false;
					LinktoMultiParent = false;
				}
			}
		}
		/// <summary>
		/// Runs as a thread to monitor SqlDbviewer & Multiviewer availabilty
		/// and resets checkboxes as necessary  - thread delay is TWO seconds
		/// </summary>
		private void checkLinkages ( )
		{
			while ( true )
			{
				int AllLinks = 0;
				Thread .Sleep ( 5000 );

				//				if( LinkToParent )
				bool reslt = LinktoParent;
				if ( LinkToAllRecords == false && LinkToAllRecords == false )
				{
					// Link to  ALL is UNCHECKED, so make sure Parent link is ENABLED
					//if( LinktoParent )
					//{
					//AllLinks++;
					Application .Current .Dispatcher .Invoke ( ( ) =>
					{
						ResetLinkages ( "LINKTOPARENT" , true );
					} );
					//}
					//else
					//{
					//	Application . Current . Dispatcher . Invoke ( ( ) =>
					//	{
					//		ResetLinkages ( "LINKTOPARENT", false );
					//	} );
					//}
				}
				else
				{
					// Link to  ALL is CHECKED, so make sure Parent link is DISABLED and Unchecked
					Application .Current .Dispatcher .Invoke ( ( ) =>
					{
						ResetLinkages ( "LINKTOPARENT" , false );
					} );

				}
				if ( IsMultiLinkActive ( reslt ) == false )
				{
					Application .Current .Dispatcher .Invoke ( ( ) =>
					{
						ResetLinkages ( "MULTILINKTOPARENT" , false );
					} );
				}
				else
				{
					AllLinks++;
					Application .Current .Dispatcher .Invoke ( ( ) =>
					{
						ResetLinkages ( "MULTILINKTOPARENT" , true );
					} );
				}
				if ( AllLinks >= 1 )
					Application .Current .Dispatcher .Invoke ( ( ) =>
					{
						ResetLinkages ( "ALLLINKS" , true );
					} );
				else
					Application .Current .Dispatcher .Invoke ( ( ) =>
					{
						ResetLinkages ( "ALLLINKS" , false );
					} );

			}
		}
		private void ResetLinkages ( string linktype , bool value )
		{
			if ( linktype == "LINKTOPARENT" )
			{
				LinkToParent .IsEnabled = value;
				if ( value )
					SqlParentViewer = Flags .SqlBankViewer;
				else
				{
					LinktoParent = false;
					LinkToParent .IsEnabled = false;
					//SqlParentViewer = null;
				}
			}
			if ( linktype == "MULTILINKTOPARENT" )
			{
				if ( value )
				{
					LinkToMulti .IsEnabled = value;
					MultiParentViewer = Flags .SqlMultiViewer;
				}
				else
				{
					LinkToMulti .IsEnabled = false;
					LinkToMulti .IsChecked = false;
					MultiParentViewer = null;
					LinktoMultiParent = false;
				}
			}
			if ( linktype == "ALLLINKS" && value )
			{
				if ( LinkToAllRecords == true )
					LinkRecords .IsEnabled = true;
			}//
			 //else
			 //	LinkRecords . IsEnabled = false;
			#endregion HANDLERS for linkage checkboxes, inluding Thread montior

		}
		private void Window_MouseDown ( object sender , MouseButtonEventArgs e )
		{

		}

		private void Minimize_click ( object sender , RoutedEventArgs e )
		{
			this .WindowState = WindowState .Normal;
		}

		private void BankGrid_DragEnter ( object sender , DragEventArgs e )
		{
			e .Effects = ( DragDropEffects ) DragDropEffects .Move;
		}

		private void ExportBankCSV_Click ( object sender , RoutedEventArgs e )
		{
			string message = "";
			string part2 = "";
			string outstats = "Please check Output for failure details";
			// Export BANK DATA to CSV
			int count = BankCollection . ExportBankData ( @"C:\users\ianch\Documents\Bankb.csv", "BANKACCOUNT" );
			if ( count > 0 )
			{
				part2 = $"\n{count} records have been saved successully.";
				message = $"Bank Data Exported successfully.{part2}";
			}
			else
			{
				part2 = $"The data was NOT saved correctly...\n{outstats}";
				message = part2;
			}
			MessageBox .Show ( message );
		}
		private void ExportToJSON ( object sender , RoutedEventArgs e )
		{
			string path = @"C:\\Users\\Ianch\\Documents\\BankCollectiondata.json";
			var jsonresult = JsonConvert .SerializeObject ( TestBankviewerView  );
			JsonSupport .JsonSerialize ( jsonresult , path );
		}

		private void ImportFromJSON ( object sender , RoutedEventArgs e )		
		{
			string path = @"C:\\Users\\Ianch\\Documents\\BankCollectiondata.json";
			var result = JsonSupport .JsonDeserialize (  path );
		}		

		private void CompareFieldData ( )
		{
//			if ( SaveBttn == null )
//				return;
			//if (_bankno != Bankno.Text)
			//	SaveBttn.IsEnabled = true;
			//if (_custno != Custno.Text)
			//	SaveBttn.IsEnabled = true;
			//if (_actype != acType.Text)
			//	SaveBttn.IsEnabled = true;
			//if (_balance != balance.Text)
			//	SaveBttn.IsEnabled = true;
			//if (_odate != odate.Text)
			//	SaveBttn.IsEnabled = true;
			//if (_cdate != cdate.Text)
			//	SaveBttn.IsEnabled = true;

//			if ( SaveBttn .IsEnabled )
//				IsDirty = true;
		}

		private void ContextShowJson_Click ( object sender , RoutedEventArgs e )
		{
			//============================================//
			//MENU ITEM 'Read and display JSON File'
			//============================================//
			string Output = "";
			this .Refresh ( );
			////We need to save current Collectionview as a Json (binary) data to disk
			//// this is the best way to save persistent data in Json format
			////using tmp folder for interim file that we will then display
			BankAccountViewModel bvm = this.BankGrid.SelectedItem as BankAccountViewModel;
			Output = JsonSupport .CreateShowJsonText ( true , "BANKACCOUNT" , bvm , "BankAccountViewModel" );
			MessageBox .Show ( Output , "Currently selected record in JSON format" , MessageBoxButton .OK , MessageBoxImage .Information , MessageBoxResult .OK );

		}

		private async void ContextEdit_Click ( object sender , RoutedEventArgs e )
		{
			// handle flags to let us know WE have triggered the selectedIndex change
			//MainWindow . DgControl . SelectionChangeInitiator = 2; // tells us it is a EditDb initiated the record change
			BankAccountViewModel bvm = new BankAccountViewModel();
			int currsel = 0;
			DataGridRow RowData = new DataGridRow();
			bvm = this .BankGrid .SelectedItem as BankAccountViewModel;
			currsel = this .BankGrid .SelectedIndex;
			//int row = DataGridSupport . GetDataGridRowFromTree ( e, out RowData );
			//if ( row == -1 ) row = 0;
			RowInfoPopup rip = new RowInfoPopup("BANKACCOUNT", BankGrid);
			rip .Topmost = true;
			rip .DataContext = RowData;
			rip .BringIntoView ( );
			rip .Focus ( );
			rip .ShowDialog ( );

			//If data has been changed, update everywhere
			// Update the row on return in case it has been changed
			if ( rip .IsDirty )
			{
				this .BankGrid .ItemsSource = null;
				this .BankGrid .Items .Clear ( );
				await TestBankCollection .LoadBank ( TestBankcollection , "BANKDBVIEW" , 1 , true );
				this .BankGrid .ItemsSource = TestBankviewerView;
				// Notify everyone else of the data change
				//EventControl.TriggerViewerDataUpdated(TestBankviewerView,
				//	new LoadedEventArgs
				//	{
				//		CallerType = "BANKBVIEW",
				//		CallerDb = "BANKACCOUNT",
				//		DataSource = TestBankviewerView,
				//		RowCount = this.BankGrid.SelectedIndex
				//	});
			}
			else
				this .BankGrid .SelectedItem = RowData .Item;

			// This sets up the selected Index/Item and scrollintoview in one easy FUNC function call (GridInitialSetup is  the FUNC name)
			this .BankGrid .SelectedIndex = currsel;
			//Count.Text = $"{this.BankGrid.SelectedIndex} / { this.BankGrid.Items.Count.ToString()}";
			// This is essential to get selection activated again
			this .BankGrid .Focus ( );
		}

		private async void ContextSave_Click ( object sender , RoutedEventArgs e )
		{
			//============================================//
			//MENU ITEM 'Save current Grid Db data as JSON File'
			//============================================//
			object DbData = new object();
			string path = "";
			string jsonresult = "";
			// Get default text files viewer application from App resources
			string program = (string)Properties.Settings.Default["DefaultTextviewer"];

			//HOW to save current Collectionview as a Json (binary) data from disk
			// this is the best way to save persistent data in Json format
			//Save data (XXXXViewModel[]) as binary to disk file
			path = @"C:\\Users\\Ianch\\Documents\\BankCollectiondata.json";
			jsonresult = JsonConvert .SerializeObject ( TestBankcollection );
			JsonSupport .JsonSerialize ( jsonresult , path );
			MessageBox .Show ( $"The data from this Database has been saved\nfor you in 'Json' format successfully ...\n\nFile is : {path}" , "Data Persistence System" );
		}

		private void ContextDisplayJsonData_Click ( object sender , RoutedEventArgs e )
		{
			//============================================//
			//MENU ITEM 'Read and display JSON File'
			//============================================//
			JsonSupport .CreateShowJsonText ( false , "BANKACCOUNT" , TestBankcollection );

		}

		private void ContextSettings_Click ( object sender , RoutedEventArgs e )
		{
			Setup setup = new Setup();
			setup .Show ( );
			setup .BringIntoView ( );
			setup .Topmost = true;
			this .Focus ( );
		}

		private void ContextClose_Click ( object sender , RoutedEventArgs e )
		{
			Close ( );
		}

		private void BankGrid_Loaded ( object sender , RoutedEventArgs e )
		{
			int counter = 0;
			if ( BankGrid . Columns . Count == 0 )
			{
				DataGridUtilities . LoadDataGridColumns ( BankGrid , "DGMultiBankColumns" );
				DataGridUtilities . LoadDataGridTextColumns ( BankGrid , "DGMultiBankTextColumns" );
			}
			//Saved default Columns layout
			foreach ( var item in BankGrid . Columns )
			{
				DGBankColumnsCollection [ counter++ ] = item;
			}
			DataGridSupport . SortBankColumns ( BankGrid , DGBankColumnsCollection );

			//DataGridUtilities . LoadDataGridColumns ( BankGrid , "DGMultiBankColumns" );
			//DataGridUtilities . LoadDataGridTextColumns ( BankGrid , "DGMultiBankTextColumns" );
			//DataGridSupport . SortBankColumns ( BankGrid ,DGBankColumnsCollection );
		}
	}
}
