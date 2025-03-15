using MusicPlayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace MusicPlayer.Commands
{
    public class NavigateCommand : ICommand
    {
        private readonly Services.NavigationService navigationService;

        private UserService user;

        public NavigateCommand(Services.NavigationService navigationService, UserService user)
        {
            this.navigationService = navigationService;
            this.user = user;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            navigationService.Navigate();
        }
    }
}
