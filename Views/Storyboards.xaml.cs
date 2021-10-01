using System;
using System . Collections;
using System . Collections . Generic;
using System . Collections . ObjectModel;
using System . ComponentModel;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Windows;
using System . Windows . Controls;
using System . Windows . Controls . Primitives;
using System . Windows . Data;
using System . Windows . Documents;
using System . Windows . Input;
using System . Windows . Media;
using System . Windows . Media . Animation;
using System . Windows . Media . Imaging;
using System . Windows . Shapes;
using System . Xml . Schema;

using WPFPages . Commands;
using WPFPages . UserControls;
using WPFPages . ViewModels;

using static WPFPages . Views . nworder;

namespace WPFPages . Views
{

        /// <summary>
        /// Interaction logic for Storyboards.xaml
        /// </summary>
        public partial class Storyboards : Window
        {
                private bool DataSwitched = false;
                private  NwOrderCollection NwOrders = new NwOrderCollection ( );
                private  NwOrderCollection NwOrder1 = new NwOrderCollection ( );
                private  NwOrderCollection NwOrder2 = new NwOrderCollection ( );
                private  NwOrderCollection NwOrder3 = new NwOrderCollection ( );
                private  NwOrderCollection NwOrder4 = new NwOrderCollection ( );
                private  NwOrderCollection NwOrder5 = new NwOrderCollection ( );
                private  NwOrderCollection NwGridOrder1 = new NwOrderCollection ( );
                private  NwOrderCollection NwdummyView = new NwOrderCollection ( );


                private CollectionView orderview1 = new CollectionView ( "" );
                //private nwcustomer NwCustomer = new nwcustomer ( );
                private  ObservableCollection<nwcustomer> NwCustomers = new ObservableCollection<nwcustomer> ( );
                private  ObservableCollection<nwcustomer> NwCustomers1 = new ObservableCollection<nwcustomer> ( );
                private  ObservableCollection<nwcustomer> NwCustomers2 = new ObservableCollection<nwcustomer> ( );
                private  ObservableCollection<nwcustomer> NwCustomers3 = new ObservableCollection<nwcustomer> ( );
                private  ObservableCollection<nwcustomer> NwCustomers4 = new ObservableCollection<nwcustomer> ( );
                private  ObservableCollection<nwcustomer> NwCustomers5 = new ObservableCollection<nwcustomer> ( );
                private  ObservableCollection<nwcustomer> NwCustomers6 = new ObservableCollection<nwcustomer> ( );

                private  CollectionView view1 = null;
                private  CollectionView view2 = null;
                private  CollectionView view3 = null;
                private  CollectionView view4 = null;
                private  CollectionView view5 = null;
                private  CollectionView view6 = null;
                private  CollectionView view7 = null;
                nworder Nwview1 = new nworder ( );
                nworder Nwview2 = new nworder ( );
                nworder Nwview3 = new nworder ( );
                nworder Nwview4 = new nworder ( );
                nworder Nwview5 = new nworder ( );
                nworder Nwview6 = new nworder ( );
                nwcustomer nwcust = new nwcustomer ( );
                nwcustomer nwc = new nwcustomer ( );


                 //Handle click event  from 3DButton
                public event RoutedEventHandler Click;

                public Storyboards ( )
                {
                        InitializeComponent ( );
                        Utils . SetupWindowDrag ( this );
                }
                private void storyboard_Loaded ( object sender, RoutedEventArgs e )
                {
                        if ( NwOrders . Count > 0 )
                                NwOrders . Clear ( );
                        NwOrders . StdLoadOrders ( "" );
                        if ( NwCustomers . Count > 0 )
                                NwCustomers . Clear ( );
                        NwCustomers = nwc . GetNwCustomers ( );
                        //Handle click event  from 3DButton
                        Click += new RoutedEventHandler(Threedbtn_Click);
                }


                 private void Threedbtn_Click ( object sender, RoutedEventArgs e )
                {
                        ObservableCollection<nwcustomer> NwCust = new ObservableCollection<nwcustomer> ( );

                        if (DataSwitched )
                        {
                                if ( NwCustomers . Count == 0 )
                                        NwCustomers = nwc . GetNwCustomers ( );

                                //NwCustomers6 . Clear ( );
                                // assign to our Obs collection
                                NwCustomers6 = NwCustomers;
                                LView2 . ItemsSource = null;
                                //LView2 . ListviewControl . Items . Clear ( );
                                LView2 . ItemsSource = NwCustomers6;

                                CollectionView view2;
                                // do  The sortng with CollectionView
                                view2 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwCustomers6 );
                                if ( view2 != ( CollectionView ) null && view2 . Count > 0 )
                                        view2 . SortDescriptions . Clear ( );
                                view2 . SortDescriptions . Add ( new SortDescription ( "Country", ListSortDirection . Ascending ) );
                                view2 . SortDescriptions . Add ( new SortDescription ( "Region", ListSortDirection . Ascending ) );
                                view2 . SortDescriptions . Add ( new SortDescription ( "City", ListSortDirection . Ascending ) );
                                //NwCustomers6 = view2;
                                LView2 . ItemsSource = NwCustomers6;
                                
                                nwc = view2 . CurrentItem as nwcustomer;
                                DataSwitched = false;
                                return;
                        }

                        var Customers = NwCustomers6 . AsEnumerable ( ) . GroupBy ( d => new
                        {
                                d . Country,
                                d . City
                        } )
                        . Select ( gg => new
                        {
                                Country = gg . Key . Country,
                                _City = gg . Key . City,
                                //pcode = gg.Key.PostalCode
                        } )
                        . ToList ( );

                        // By here, I have a list of countries + Cities alone in no particular order
                        //so organise theentire Db into ordered sequence by Countries and then Cities
                        int y = Customers . Count ( );

                        foreach ( var country in Customers )
                        {
                                foreach ( var item in NwCustomers6 )
                                {
                                        if ( item . Country == country . Country )
                                        {
                                                foreach ( var city in item . Country )
                                                {
                                                        NwCust . Add ( item );
                                                        //Console . WriteLine ( $"{item . Country}, {item . City}, {item . CompanyName}" );
                                                }
                                        }
                                }
                        }

                        // We now have a list arranged by City's inside Country's in our "Wrapped" ListView
                        LView2 . ItemsSource = NwCust;
                        DataSwitched = true;
                }

                //                public event RoutedEventHandler Click;

                #region COMMANDS
                BreakCommand _breakCommand = new BreakCommand ( );
                public BreakCommand Breakcommand
                {
                        get
                        {
                                return _breakCommand;
                        }
                        set
                        {
                                _breakCommand = value;
                        }
                }
                #endregion COMMANDS


                public nworder nwOrder = new nworder ( );

                #region Data Handling

                // Blue Background
                private void LView1_Loaded ( object sender, RoutedEventArgs e )
                {
                        NwOrder1 . Clear ( );
                        NwOrder1 = NwOrders;
                        LView1 .  Items . Clear ( );
                        LView1 . ItemsSource = null;
                        view1 = new CollectionView ( NwOrder1 );
                        view1 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwOrder1 );
                        if ( view1 != ( CollectionView ) null && view1 . Count > 0 )
                                view1 . SortDescriptions . Clear ( );
                        view1 . SortDescriptions . Add ( new SortDescription ( "ShipName", ListSortDirection . Ascending ) );
                        LView1 . ItemsSource = view1;
                       DataContext = view1;
                        LView1 . UpdateLayout ( );
                        Nwview1 = view1 . CurrentItem as nworder;
                }

                // Yellow Background
                private void LView2_Loaded ( object sender, RoutedEventArgs e )
                {
                        NwOrder4 . Clear ( );
                        NwOrder4 = NwOrders;
                        LView2 . Items . Clear ( );
                        view4 = new CollectionView ( NwOrder4 );
                        view4 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwOrder4 );
                        if ( view4 != ( CollectionView ) null && view4 . Count > 0 )
                                view4 . SortDescriptions . Clear ( );
                        view4 . SortDescriptions . Add ( new SortDescription ( "ShipCountry", ListSortDirection . Ascending ) );
                        LView2 . ItemsSource = view4;
                        DataContext = view4;
                        LView2 . UpdateLayout ( );
                        Nwview4 = view4 . CurrentItem as nworder;
                  }

                // Magenta Background
                private void LView3_Loaded ( object sender, RoutedEventArgs e )
                {
                        NwCustomers5 . Clear ( );
                        NwCustomers5 = NwCustomers;
                        LView3 . Items . Clear ( );
                        view5 = new CollectionView ( NwCustomers5 );
                        view5 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwCustomers5 );
                        if ( view5 != ( CollectionView ) null && view5 . Count > 0 )
                                view5 . SortDescriptions . Clear ( );
                        view5 . SortDescriptions . Add ( new SortDescription ( "CustomerId", ListSortDirection . Ascending ) );
                        LView3 . ItemsSource = view5;
                        DataContext = view5;
                        LView3 . UpdateLayout ( );
                        Nwview5 = view5 . CurrentItem as nworder;
                }

                // Red Background
                private void LView4_Loaded ( object sender, RoutedEventArgs e )
                {
                        NwCustomers3 . Clear ( );
                        NwCustomers3 = NwCustomers;
                        LView4 . Items . Clear ( );
                        view3 = new CollectionView ( NwCustomers3 );
                        view3 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwCustomers3 );
                        if ( view3 != ( CollectionView ) null && view3 . Count > 0 )
                                view3 . SortDescriptions . Clear ( );
                        view3 . SortDescriptions . Add ( new SortDescription ( "CompanyName", ListSortDirection . Ascending ) );
                        LView4 . ItemsSource = view3;
                        DataContext = view3;
                        LView4 . UpdateLayout ( );
                        Nwview3 = view3 . CurrentItem as nworder;
                }

                //// Orange Background
                //private void LView5_Loaded ( object sender, RoutedEventArgs e )
                //{
                //        NwOrder2 . Clear ( );
                //        NwOrder2 = NwOrders;
                //        LView5 . Items . Clear ( );
                //        view2 = new CollectionView ( NwOrder2 );
                //        view2 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwOrder2 );
                //        if ( view2 != ( CollectionView ) null && view2 . Count > 0 )
                //                view2 . SortDescriptions . Clear ( );
                //        view2 . SortDescriptions . Add ( new SortDescription ( "CustomerId", ListSortDirection . Ascending ) );
                //        LView5 . ItemsSource = view2;
                //        DataContext = view2;
                //        LView5 . UpdateLayout ( );
                //        Nwview2 = view2 . CurrentItem as nworder;
                //}

                //// Black Background Grid view
                //private void LView6_Loaded ( object sender, RoutedEventArgs e )
                //{
                //        if ( NwOrders . Count == 0 )
                //                NwOrders . StdLoadOrders ( "" );
                //        NwGridOrder1 = NwOrders;
                //        LView6 . Items . Clear ( );
                        
                //        // Get a view and handle sorting
                //        CollectionView view9;
                //        view9 =  (CollectionView)CollectionViewSource . GetDefaultView ( NwGridOrder1 );
                //       // view6 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwGridOrder1 );
                //        if ( view9 != ( CollectionView ) null && view9 . Count > 0 )
                //                view9 . SortDescriptions . Clear ( );
                //       // view6 . SortDescriptions . Add ( new SortDescription ( "ShipName", ListSortDirection . Descending ) );

                //        view9 . SortDescriptions . Add ( new SortDescription ( "ShipName", ListSortDirection . Descending ) );
                //        //Assign our sorted data to our ItemsSource
                //        LView6 . ItemsSource = view9;
                //        // Set up current item pointer 
                //       // Nwview6 = view6 . CurrentItem as nworder;
                //        // set up our DataContext for this ListBox
                //        ListViewControl lbc1 = sender as ListViewControl;
                //        //lbc1 . DataContext = view9;
                //}

                //Gray default
                // This has independent nworder data due to the sorting performed in the method
                private void LView7_Loaded ( object sender, RoutedEventArgs e )
                {
                        NwCustomers6 . Clear ( );
                        // assign to our Obs collection
                        NwCustomers6 = NwCustomers;

                        LView2 . Items . Clear ( );
                        LView2 . ItemsSource = NwCustomers;

                       // do  The sortng with CollectionView
                        view2 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwCustomers6 );
                        if ( view2 != ( CollectionView ) null && view2 . Count > 0 )
                                view2 . SortDescriptions . Clear ( );
                        view2 . SortDescriptions . Add ( new SortDescription ( "Country", ListSortDirection . Ascending ) );
                        view2 . SortDescriptions . Add ( new SortDescription ( "Region", ListSortDirection . Ascending ) );
                        view2 . SortDescriptions . Add ( new SortDescription ( "City", ListSortDirection . Ascending ) );
                        //NwCustomers6 = view2;
                        LView2 . ItemsSource = NwCustomers6;
                        nwcustomer nwcust = new nwcustomer ( );

                        nwc = view2 . CurrentItem as nwcustomer;

                          nwcustomer custsort = new nwcustomer ( );
                       
                }

                 #endregion

                private void Items_CurrentChanging ( object sender, System . ComponentModel . CurrentChangingEventArgs e )
                {
                        // This gets called when loading the grid !!!
                }

                private void Lv1_SelectionChanged ( object sender, SelectionChangedEventArgs e )
                {
                        //Save current data itrem selection to nworder class
                        //nwOrder = Lv1 . Items [ Lv1 . SelectedIndex ] as nworder;
                        //nwOrder . SelectedItem = nwOrder;
                        //nwOrder . SelectedIndex = Lv1 . SelectedIndex;
                }

                private void ThreeDeeBtnControl_MouseDown ( object sender, MouseButtonEventArgs e )
                {
                        var Customers = NwCustomers6 . AsEnumerable ( ) . GroupBy ( d => new
                        {
                                d . Country,
                                d . City
                        } )
                        . Select ( gg => new
                        {
                                Country = gg . Key . Country,
                                _City = gg . Key . City,
                                //pcode = gg.Key.PostalCode
                        } )
                        . ToList ( );

                        ObservableCollection<nwcustomer> NwCust = new ObservableCollection<nwcustomer> ( );
                        // By here, I have a list of countries + Cities alone in no particular order
                        //so organise theentire Db into ordered sequence by Countries and then Cities
                        int y = Customers . Count ( );

                        foreach ( var country in Customers )
                        {
                                foreach ( var item in NwCustomers6 )
                                {
                                        if ( item . Country == country . Country )
                                        {
                                                foreach ( var city in item . Country )
                                                {
                                                        NwCust . Add ( item );
                                                        //Console . WriteLine ( $"{item . Country}, {item . City}, {item . CompanyName}" );
                                                }
                                        }
                                }
                        }

                        // We now have a list arranged by City's inside Country's in our "Wrapped" ListView
                        LView2 . ItemsSource = NwCust;
                }

                private void ThreeDeeBtnControl_MouseDown ( object sender, RoutedEventArgs e )
                {
                        var Customers = NwCustomers6 . AsEnumerable ( ) . GroupBy ( d => new
                        {
                                d . Country,
                                d . City
                        } )
                        . Select ( gg => new
                        {
                                Country = gg . Key . Country,
                                _City = gg . Key . City,
                                //pcode = gg.Key.PostalCode
                        } )
                        . ToList ( );

                        ObservableCollection<nwcustomer> NwCust = new ObservableCollection<nwcustomer> ( );
                        // By here, I have a list of countries + Cities alone in no particular order
                        //so organise theentire Db into ordered sequence by Countries and then Cities
                        int y = Customers . Count ( );

                        foreach ( var country in Customers )
                        {
                                foreach ( var item in NwCustomers6 )
                                {
                                        if ( item . Country == country . Country )
                                        {
                                                foreach ( var city in item . Country )
                                                {
                                                        NwCust . Add ( item );
                                                        //Console . WriteLine ( $"{item . Country}, {item . City}, {item . CompanyName}" );
                                                }
                                        }
                                }
                        }

                        // We now have a list arranged by City's inside Country's in our "Wrapped" ListView
                        LView2 . ItemsSource = NwCust;

                }

                private void StdListview_SelectionChanged ( object sender, SelectionChangedEventArgs e )
                {
                        ListView lv = sender as ListView;
                }

                private void APTestingControl_Loaded ( object sender, RoutedEventArgs e )
                {

                }

                private void ShadowLabelControl_PreviewMouseDoubleClick ( object sender, MouseButtonEventArgs e )
                {
                        this.Close ( );
                }

//                private void listviewclass_Loaded ( object sender, RoutedEventArgs e )
//                {
//                        //                        if ( NwCustomers. Count == 0 )
//                        //                             NwCustomers .( "" );
//                        BankCollection nwo1 = new BankCollection ( );
//                        nwo1 = BankCollection.LoadBank ( nwo1, ""  );
//                        listviewclass . Items . Clear ( );
//                        // Get a view and handle sorting
//                        CollectionView view10;
//                        view10 = ( CollectionView ) CollectionViewSource . GetDefaultView ( nwo1 );
//                        // view6 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwGridOrder1 );
//                        if ( view10 != ( CollectionView ) null && view10 . Count > 0 )
//                                view10 . SortDescriptions . Clear ( );
//                        // view6 . SortDescriptions . Add ( new SortDescription ( "ShipName", ListSortDirection . Descending ) );

//                        view10 . SortDescriptions . Add ( new SortDescription ( "BankNo", ListSortDirection . Descending ) );
//                        view10 . SortDescriptions . Add ( new SortDescription ( "CustNo", ListSortDirection . Descending ) );
//                        //Assign our sorted data to our ItemsSource
//                        listviewclass . ItemsSource = view10;
//                        // Set up current item pointer 
//                        // Nwview6 = view6 . CurrentItem as nworder;
//                        // set up our DataContext for this ListBox
////ListViewControl lbc1 = sender as ListViewControl;
////lbc1 . DataContext = view10;
//                }

                //private void subclassed_Loaded ( object sender, RoutedEventArgs e )
                //{
                //        ObservableCollection<nwcustomer> NwCust = new ObservableCollection<nwcustomer> ( );
                //        // assign to our Obs collection
                //        NwCust = NwCustomers;

                //        subclassed . Items . Clear ( );
                //        subclassed . ItemsSource = NwCustomers;

                //        // do  The sortng with CollectionView
                //        CollectionView vw1;
                //        vw1 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwCust );
                //        if ( vw1 != ( CollectionView ) null && view2 . Count > 0 )
                //                vw1 . SortDescriptions . Clear ( );
                //        vw1 . SortDescriptions . Add ( new SortDescription ( "Country", ListSortDirection . Ascending ) );
                //        vw1 . SortDescriptions . Add ( new SortDescription ( "Region", ListSortDirection . Ascending ) );
                //        vw1 . SortDescriptions . Add ( new SortDescription ( "City", ListSortDirection . Ascending ) );
                //        //NwCustomers6 = view2;
                //        subclassed . ItemsSource = NwCust;
                //        nwcustomer nwcust = new nwcustomer ( );

                //        nwc = vw1 . CurrentItem as nwcustomer;

                //}
        }
}

