using MusicPlayer.Services;
using MusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicPlayer.Commands
{
    public class DeletePlaylistCommand : ICommand
    {

        private UserService user;

        public DeletePlaylistCommand(UserService user)
        {
            this.user = user;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            bool canExecute = false;
            if (parameter is HomeViewModel hvm)
            {
                if (hvm.SelectedPlaylist != null)
                {
                    canExecute = true;
                }
            }
            return canExecute;
        }

        public void Execute(object? parameter)
        {
            if (parameter is HomeViewModel hvm)
            {
                user.DeletePlaylist(hvm.SelectedPlaylist);
                hvm.Playlists.Remove(hvm.SelectedPlaylist);
            }
        }
    }
}
