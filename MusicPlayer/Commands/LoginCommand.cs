﻿using MusicPlayer.Authentication;
using MusicPlayer.Services;
using MusicPlayer.Stores;
using MusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace MusicPlayer.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly Services.NavigationService navigationService;

        private UserService user;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public LoginCommand(Services.NavigationService navigationService, UserService user)
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
                bool LoggedIn = LoginHandler.Login(lvm.Username, lvm.Password);
                if (LoggedIn)
                {
                    user.Username = lvm.Username;
                    navigationService.Navigate();
                }
                else
                {
                    MessageBox.Show("Login mislykkedes. Tryk på Registrer for at registrere din konto.");
                }
            }
        }
    }
}
