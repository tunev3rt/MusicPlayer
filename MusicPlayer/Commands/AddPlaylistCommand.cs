using MusicPlayer.Services;
using MusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MusicPlayer.Model;
using System.Diagnostics;

namespace MusicPlayer.Commands
{
    public class AddPlaylistCommand : ICommand
    {
        private readonly UserService user;

        public AddPlaylistCommand(UserService user) 
        {
            this.user = user;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is HomeViewModel hvm)
            {
                Playlist newPlaylist = new Playlist
                {
                    PlaylistName = "New Playlist"
                };
                user.CreateNewPlaylist(newPlaylist);
                hvm.Playlists.Add(newPlaylist);
            }

        }
    }
}
