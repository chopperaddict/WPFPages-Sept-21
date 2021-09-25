using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Windows;
using System . Windows . Controls;
using System . Windows . Input;

namespace WPFPages . Commands
{
        /// <summary>
        /// Generic ICommand handler to simplify later commands 
        /// </summary>
        class RelayCommand : ICommand

        {   
                //public Action<object> executeAction;
                //public Func<object, bool> canExecute;
                //bool canExecuteCache;

               public RelayCommand ( Action<object> executeaction, Func<object, bool> canExecute, bool canExecuteCache )
                {
                }

                public event EventHandler CanExecuteChanged;

                public bool CanExecute ( object parameter )
                {
                        return true;
                }

                //        this . canExecute = canExecute;
                //        this . executeAction = executeAction;
                //        canExecuteCache = canExecuteCache;
                //}
                //public bool CanExecute ( object parameter )
                //{
                //        if ( canExecute == null )
                //                return true;
                //        else
                //                return canExecute ( parameter );
                //}

                public void Execute ( object parameter )
                {
                        //        if(parameter != null)
                        //                executeAction ( parameter );
                        MessageBox . Show ( "eeeeeeeeeeeeekkkkk" );
                }

//                public event EventHandler CanExecuteChanged
   //             {
                        
                //        add
                //        {
                //                CommandManager . RequerySuggested += value;
                //        }
                //        remove
                //        {
                //                CommandManager . RequerySuggested -= value;
                //        }
      //          }


        }
}
