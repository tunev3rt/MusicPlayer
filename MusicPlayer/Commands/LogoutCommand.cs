using MusicPlayer.Services;
using MusicPlayer.Stores;
using MusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicPlayer.Commands
{
    class LogoutCommand : ICommand
    {

        private readonly NavigationStore navigationStore;

        public event EventHandler? CanExecuteChanged;

        public LogoutCommand(NavigationStore navigationStore) 
        {
            this.navigationStore = navigationStore;
        }

        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter)
        {
            UserService userService = new UserService();

            navigationStore.CurrentViewModel = new LoginViewModel(navigationStore, userService);
        }
    }
}
