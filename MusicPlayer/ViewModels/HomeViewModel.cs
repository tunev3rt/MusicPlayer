using MusicPlayer.Services;
using MusicPlayer.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer.Model;

namespace MusicPlayer.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public UserService User { get; set; }

        public ObservableCollection<Playlist> Playlists { get; set; }

        public ObservableCollection<Song> Songs { get; set; }

        public HomeViewModel(NavigationStore navigationStore, UserService user)
        {
            User = user;
            Playlists = new ObservableCollection<Playlist>(User.GetPlaylistsForUser());
            Songs = new ObservableCollection<Song>(User.GetAllSongs());
        }
    }
}
