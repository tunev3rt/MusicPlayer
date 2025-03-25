using MusicPlayer.Services;
using MusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MusicPlayer.Commands
{
    public class AddSongCommand : ICommand
    {

        private readonly UserService user;
        private readonly Services.NavigationService navigationService;

        public AddSongCommand(UserService user, NavigationService navigationService)
        {
            this.user = user;
            this.navigationService = navigationService;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object? parameter)
        {
            bool canExecute = false;
            if (parameter is SongConfigurationViewModel scv)
            {
                if (!string.IsNullOrEmpty(scv.NewSong.Title) && !string.IsNullOrEmpty(scv.NewSong.Artist) && !string.IsNullOrEmpty(scv.NewSong.Album) &&!string.IsNullOrEmpty(scv.NewSong.FilePath))
                {
                    canExecute = true;
                }
            }
            return canExecute;
        }
        public void Execute(object? parameter)
        {
            if (parameter is SongConfigurationViewModel scv)
            {
                user.AddSong(scv.NewSong);
                MessageBox.Show($"The song: {scv.NewSong.Title} has been added.");
                navigationService.Navigate();
            }
        }
    }
}
