using MusicPlayer.Authentication;
using MusicPlayer.Services;
using MusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MusicPlayer.Commands
{
    class RegisterCommand : ICommand
    {
        private readonly Services.NavigationService navigationService;

        private UserService user;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RegisterCommand(Services.NavigationService navigationService, UserService user)
        {
            this.navigationService = navigationService;
            this.user = user;
        }

        public bool CanExecute(object? parameter)
        {
            bool canExecute = false;
            if (parameter is LoginViewModel lvm)
            {
                canExecute = !string.IsNullOrEmpty(lvm.Username) && !string.IsNullOrEmpty(lvm.Password);
            }
            return canExecute;
        }
        public void Execute(object? parameter)
        {
            if (parameter is LoginViewModel lvm)
            {
                bool LoggedIn = user.Register(lvm.Username, lvm.Password);
                if (LoggedIn)
                {
                    user.Username = lvm.Username;
                    Trace.WriteLine("User logged in");
                    navigationService.Navigate();
                }
                else
                {
                    MessageBox.Show("Konto kan ikke registreres, muligvis findes der en konto med samme brugernavn.");
                }
            }
        }
    }
}
