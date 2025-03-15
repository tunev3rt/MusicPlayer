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
        private readonly MusicPlayerService musicPlayerService;

        public event EventHandler? CanExecuteChanged;

        public LogoutCommand(NavigationStore navigationStore, MusicPlayerService musicPlayerService) 
        {
            this.navigationStore = navigationStore;
            this.musicPlayerService = musicPlayerService;
        }

        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter)
        {
            musicPlayerService.Stop();

            UserService userService = new UserService();

            navigationStore.CurrentViewModel = new LoginViewModel(navigationStore, userService);
        }
    }
}
