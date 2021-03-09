using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace View.Infrastructure.Commands.Base
{
   internal abstract class Command : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }


        /// <summary>
        /// Определяет, может ли команда выполнятьс
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public abstract bool CanExecute(object parameter);


        /// <summary>
        ///  Выполняет логику команды
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object parameter);
      
    }
}
