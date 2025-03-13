using MusicPlayer.Authentication;
using MusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicPlayer.Commands
{
    public class LoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            bool canExecute = false;
            if (parameter is UserViewModel uvm)
            {
                canExecute = !string.IsNullOrEmpty(uvm.Username) && !string.IsNullOrEmpty(uvm.Password);
            }
            return canExecute;
        }
        public void Execute(object parameter)
        {
            if (parameter is UserViewModel uvm)
            {
                bool LoggedIn = LoginHandler.Login(uvm.Username, uvm.Password);
                if (LoggedIn)
                {
                    // Navigate to the next page
                }
                else
                {
                    // Show an error message
                }
            }
        }
    }
}
