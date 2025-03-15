using MusicPlayer.Model;
using MusicPlayer.Services;
using MusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MusicPlayer.Commands
{
    public class AddSongToPlaylistCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly UserService user;

        public AddSongToPlaylistCommand(UserService userService)
        {
            user = userService;
        }

        public bool CanExecute(object? parameter)
        {
            bool canExecute = false;
            if (parameter is SongSelectionViewModel ssv)
            {
                if (ssv.SelectedSong != null && ssv.SelectedPlaylist != null)
                {
                    canExecute = true;
                }
            }
            return canExecute;
        }
        public void Execute(object? parameter)
        {
            if (parameter is SongSelectionViewModel ssv)
            {
                user.AddSongToPlaylist(ssv.SelectedSong, ssv.SelectedPlaylist);
                MessageBox.Show($"The song: {ssv.SelectedSong.Title} has been added to your playlist: {ssv.SelectedPlaylist.PlaylistName}");
            }
        }
    }
}
