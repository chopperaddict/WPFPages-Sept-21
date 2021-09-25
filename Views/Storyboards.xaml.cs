using System;
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

namespace WPFPages . Views
{

        /// <summary>
        /// Interaction logic for Storyboards.xaml
        /// </summary>
        public partial class Storyboards : Window
        {
                private NwOrderCollection NwOrders = new NwOrderCollection ( );
                private NwOrderCollection NwOrder1 = new NwOrderCollection ( );
                private NwOrderCollection NwOrder2 = new NwOrderCollection ( );
                private NwOrderCollection NwOrder3 = new NwOrderCollection ( );
                private NwOrderCollection NwOrder4 = new NwOrderCollection ( );
                private NwOrderCollection NwOrder5 = new NwOrderCollection ( );
                private NwOrderCollection NwGridOrder1 = new NwOrderCollection ( );
                private NwOrderCollection NwdummyView = new NwOrderCollection ( );

                private CollectionView orderview1 = new CollectionView ( "" );
                //private nwcustomer NwCustomer = new nwcustomer ( );
                private ObservableCollection<nwcustomer> NwCustomers = new ObservableCollection<nwcustomer> ( );
                private ObservableCollection<nwcustomer> NwCustomers1 = new ObservableCollection<nwcustomer> ( );
                private ObservableCollection<nwcustomer> NwCustomers2 = new ObservableCollection<nwcustomer> ( );
                private ObservableCollection<nwcustomer> NwCustomers3 = new ObservableCollection<nwcustomer> ( );
                private ObservableCollection<nwcustomer> NwCustomers4 = new ObservableCollection<nwcustomer> ( );
                private ObservableCollection<nwcustomer> NwCustomers5 = new ObservableCollection<nwcustomer> ( );

                private CollectionView view1 = null;
                private CollectionView view2 = null;
                private CollectionView view3 = null;
                private CollectionView view4 = null;
                private CollectionView view5 = null;
                private CollectionView view6 = null;
                nworder Nwview1 = new nworder ( );
                nworder Nwview2 = new nworder ( );
                nworder Nwview3 = new nworder ( );
                nworder Nwview4 = new nworder ( );
                nworder Nwview5 = new nworder ( );
                nworder Nwview6 = new nworder ( );
                nwcustomer nwcust = new nwcustomer ( );
                nwcustomer nwc = new nwcustomer ( );

                public Storyboards ( )
                {
                        InitializeComponent ( );
                        Utils . SetupWindowDrag ( this );
                }
                private void storyboard_Loaded ( object sender, RoutedEventArgs e )
                {
                        NwOrders . StdLoadOrders ( "" );
                        NwCustomers = nwc . GetNwCustomers ( );
                }

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
                private void UserListbox_Loaded ( object sender, RoutedEventArgs e )
                {
                        NwOrder1 . Clear ( );
                        NwOrder1 = NwOrders;
                        UserListbox . ListboxControl . Items . Clear ( );
                        view1 = new CollectionView ( NwOrder1 );
                        view1 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwOrder1 );
                        if ( view1 != ( CollectionView ) null && view1 . Count > 0 )
                                view1 . SortDescriptions . Clear ( );
                        view1 . SortDescriptions . Add ( new SortDescription ( "ShipName", ListSortDirection . Ascending ) );
                        UserListbox . ListboxControl . ItemsSource = view1;
                        UserListbox . ListboxControl . ItemTemplate = FindResource ( "NwordersDataTemplate1" ) as DataTemplate;
                        DataContext = view1;
                        UserListbox . UpdateLayout ( );
                        Nwview1 = view1 . CurrentItem as nworder;
                }

                // Orange Background
                private void UserListbox_Loaded2 ( object sender, RoutedEventArgs e )
                {
                        NwOrder2 . Clear ( );
                        NwOrder2 = NwOrders;
                        UserListbox2 . ListboxControl . Items . Clear ( );
                        view2 = new CollectionView ( NwOrder2 );
                        view2 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwOrder2 );
                        if ( view2 != ( CollectionView ) null && view2 . Count > 0 )
                                view2 . SortDescriptions . Clear ( );
                        view2 . SortDescriptions . Add ( new SortDescription ( "CustomerId", ListSortDirection . Ascending ) );
                        UserListbox2 . ListboxControl . ItemsSource = view2;
                        UserListbox2 . ListboxControl . ItemTemplate = FindResource ( "NwordersDataTemplate2" ) as DataTemplate;
                        DataContext = view2;
                        UserListbox2 . UpdateLayout ( );
                        Nwview2 = view2 . CurrentItem as nworder;
                }

                // Red Background
                private void UserListbox_Loaded3 ( object sender, RoutedEventArgs e )
                {
                        NwCustomers3 . Clear ( );
                        NwCustomers3 = NwCustomers;
                        UserListbox3 . ListboxControl . Items . Clear ( );
                        view3 = new CollectionView ( NwCustomers3);
                        view3 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwCustomers3 );
                        if ( view3 != ( CollectionView ) null && view3 . Count > 0 )
                                view3 . SortDescriptions . Clear ( );
                        view3 . SortDescriptions . Add ( new SortDescription ( "CompanyName", ListSortDirection . Ascending ) );
                        UserListbox3 . ListboxControl . ItemsSource = view3;
                        UserListbox3 . ListboxControl . ItemTemplate = FindResource ( "NwCustomersDataTemplate3" ) as DataTemplate;
                        DataContext = view3;
                        UserListbox3 . UpdateLayout ( );
                        Nwview3 = view3 . CurrentItem as nworder;
                }

                // Yellow Background
                private void UserListbox_Loaded4 ( object sender, RoutedEventArgs e )
                {
                        NwOrder4 . Clear ( );
                        NwOrder4 = NwOrders;
                        UserListbox4 . ListboxControl . Items . Clear ( );
                        view4 = new CollectionView ( NwOrder4 );
                        view4 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwOrder4 );
                        if ( view4 != ( CollectionView ) null && view4 . Count > 0 )
                                view4 . SortDescriptions . Clear ( );
                        view4 . SortDescriptions . Add ( new SortDescription ( "Country", ListSortDirection . Ascending ) );
                        UserListbox4 . ListboxControl . ItemsSource = view4;
                        UserListbox4 . ListboxControl . ItemTemplate = FindResource ( "NwordersDataTemplate4" ) as DataTemplate;
                        DataContext = view4;
                        UserListbox4 . UpdateLayout ( );
                        Nwview4 = view4 . CurrentItem as nworder;
                }

                // Magenta Background
                private void UserListbox_Loaded5 ( object sender, RoutedEventArgs e )
                {
                        NwCustomers5 . Clear ( );
                        NwCustomers5 = NwCustomers;
                        UserListbox5 . ListboxControl . Items . Clear ( );
                        view5 = new CollectionView ( NwCustomers5);
                        view5 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwCustomers5 );
                        if ( view5 != ( CollectionView ) null && view5 . Count > 0 )
                                view5 . SortDescriptions . Clear ( );
                        view5 . SortDescriptions . Add ( new SortDescription ( "CustomerId", ListSortDirection . Ascending ) );
                        UserListbox5 . ListboxControl . ItemsSource = view5;
                        UserListbox5 . ListboxControl . ItemTemplate = FindResource ( "NwCustomersDataTemplate5" ) as DataTemplate;
                        DataContext = view5;
                        UserListbox5 . UpdateLayout ( );
                        Nwview5 = view5 . CurrentItem as nworder;
                }

               // Black Background Grid view
                private void GridListbox1_Loaded ( object sender, RoutedEventArgs e )
                {
                        ListViewControl lbgc = sender as ListViewControl;
                        if ( NwOrders . Count == 0 )
                                NwOrders . StdLoadOrders ( "" );
                        NwGridOrder1 =NwOrders;                        
                        lbgc . ListviewControl . Items . Clear ( );
//                        view6 = new CollectionView (NwGridOrder1 );
                        view6 = ( CollectionView ) CollectionViewSource . GetDefaultView ( NwGridOrder1 );
                        if ( view6 != (CollectionView)null &&  view6 . Count > 0 )
                                view6 . SortDescriptions . Clear ( );
                        view6 . SortDescriptions . Add ( new SortDescription ( "ShipName", ListSortDirection . Descending ) );
                        lbgc . ListviewControl . ItemsSource = view6;
                        lbgc . ListviewControl . ItemTemplate = FindResource ( "NwOrdersDataGridTemplate1" ) as DataTemplate;
                        DataContext = view6;
                        Nwview6 = view6 . CurrentItem as nworder;
                }
                //private void Lv1_loaded ( object sender, RoutedEventArgs e )
                //{
                //        if ( NwOrder . Count == 0 )
                //        {
                //                NwOrder . Clear ( );
                //                NwOrder . StdLoadOrders ( "" );
                //        }
                //        Lv1 . Items . Clear ( );
                //        Lv1 . Items . CurrentChanging += Items_CurrentChanging;
                //        Lv1 . ItemsSource = NwOrder;
                //        DataContext = nwOrder;
                //        Lv1 . UpdateLayout ( );
                //        CollectionView view = ( CollectionView ) CollectionViewSource . GetDefaultView ( Lv1 . ItemsSource );
                //        view . SortDescriptions . Add ( new SortDescription ( "OrderId3", ListSortDirection . Ascending ) );
                //        Nwview3 = view . CurrentItem as nworder;
                //}
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

        }
}

