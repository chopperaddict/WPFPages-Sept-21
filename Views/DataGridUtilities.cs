using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Windows . Controls;
using System . Windows;

namespace WPFPages . Views
{
        public static class DataGridUtilities
        {
                public static void LoadDataGridColumns ( object sender, RoutedEventArgs e )
                {
                        //This works just fine
                        // Load  the BANKACCOUNT columns definitions from the Resourcefile "BankDataGridColumns.xaml"
                        DataGrid dg = sender as DataGrid;
                        if ( dg . Columns . Count == 0 )
                        {
                                // Get columns setup from an x:Array stored in Resources somewhere.
                                DataGridColumn [ ] columns = dg . FindResource ( "DGColumns" ) as DataGridColumn [ ];
                                dg . Columns . Clear ( );
                                try
                                {
                                        foreach ( var item in columns )
                                        {
                                                dg . Columns . Add ( item );
                                        }
                                }
                                catch
                                {

                                }
                        }
                }
                public static void LoadDataGridColumns2 ( object sender, RoutedEventArgs e )
                {
                        //This works just fine
                        // Load  the CUSTOMER columns definitions from the Resourcefile "BankDataGridColumns.xaml"
                        DataGrid dg2 = sender as DataGrid;
                        if ( dg2 . Columns . Count == 0 )
                                dg2 . Items . Clear ( );
                        // Get columns setup from an x:Array stored in Resources somewhere.
                        DataGridColumn [ ] columns2 = dg2 . FindResource ( "DGColumns2" ) as DataGridColumn [ ];
                        dg2 . Columns . Clear ( );
                        try
                        {
                                foreach ( var item2 in columns2 )
                                {
                                        dg2 . Columns . Add ( item2 );
                                }
                        }
                        catch
                        {

                        }
                }
        }
}
