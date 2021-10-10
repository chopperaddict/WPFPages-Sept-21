using System . Windows;
using System . Windows . Controls;



namespace WPFPages . Views
{
        public class ExpandedState : Expander
        {
                public ExpandedState ( ) : base ( ) { }
                public bool IsItemExpanded
                {
                        get
                        {
                                return ( bool ) this . GetValue ( IsItemExpandedProperty );
                        }
                        set
                        {
                                this . SetValue ( IsItemExpandedProperty, value );
                        }
                }

                public static readonly DependencyProperty
                        IsItemExpandedProperty = DependencyProperty .
                        Register ( "IsItemExpanded", typeof ( bool ), typeof ( Expander ), new PropertyMetadata ( true ) );

        }

}
