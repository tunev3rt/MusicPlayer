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
using System.Windows.Input;
using MusicPlayer.Commands;

namespace MusicPlayer.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ICommand AddPlaylistCommand { get; set; }

        public ICommand LogoutCommand { get; set; }

        private Playlist _selectedPlaylist;
        public Playlist SelectedPlaylist
        {
            get => _selectedPlaylist;
            set
            {
                if (_selectedPlaylist != value)
                {
                    _selectedPlaylist = value;
                    OnPropertyChanged();
                }
            }
        }

        public UserService User { get; set; }

        public ObservableCollection<Playlist> Playlists { get; set; }

        public ObservableCollection<Song> Songs { get; set; }

        public HomeViewModel(NavigationStore navigationStore, UserService user)
        {
            AddPlaylistCommand = new AddPlaylistCommand(user);
            LogoutCommand = new LogoutCommand(navigationStore);
            User = user;
            Playlists = new ObservableCollection<Playlist>(User.GetPlaylistsForUser());
            Songs = new ObservableCollection<Song>(User.GetAllSongs());
        }
    }
}
