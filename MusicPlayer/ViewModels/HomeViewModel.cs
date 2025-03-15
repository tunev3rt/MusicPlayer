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

        private readonly MusicPlayerService musicPlayerService;

        public ICommand AddPlaylistCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand AddSongToPlaylistCommand { get; set; }


        private Playlist selectedPlaylist;
        public Playlist SelectedPlaylist
        {
            get => selectedPlaylist;
            set
            {
                if (selectedPlaylist != value)
                {
                    selectedPlaylist = value;
                    OnPropertyChanged(); // Notify that SelectedPlaylist changed
                    OnPropertyChanged(nameof(SelectedPlaylistSongs)); // Also notify that the songs have changed
                }
            }
        }


        public List<Song> SelectedPlaylistSongs
        {
            get
            {
                if (SelectedPlaylist != null)
                {
                    return User.GetSongsFromPlaylist(SelectedPlaylist);
                }
                return new List<Song>();
            }
        }

        private Song selectedSongFromPlaylist;
        public Song SelectedSongFromPlaylist
        {
            get => selectedSongFromPlaylist;
            set
            {
                if (selectedSongFromPlaylist != value)
                {
                    selectedSongFromPlaylist = value;
                    if (selectedSongFromPlaylist != null)
                    {
                        musicPlayerService.Play(selectedSongFromPlaylist);
                    }
                    OnPropertyChanged();
                }
            }
        }

        public UserService User { get; set; }

        public ObservableCollection<Playlist> Playlists { get; set; }

        public ObservableCollection<Song> Songs { get; set; }

        public HomeViewModel(NavigationStore navigationStore, UserService user, MusicPlayerService musicPlayerService)
        {
            // Set the commands
            AddPlaylistCommand = new AddPlaylistCommand(user);
            LogoutCommand = new LogoutCommand(navigationStore);
            AddSongToPlaylistCommand = new NavigateCommand(new NavigationService(navigationStore, () => new SongSelectionViewModel(navigationStore, user, SelectedPlaylist, musicPlayerService)), user);

            // Set the user and music player service
            User = user;
            this.musicPlayerService = musicPlayerService;

            // Load the playlists and songs
            Playlists = new ObservableCollection<Playlist>(User.GetPlaylistsForUser());
            Songs = new ObservableCollection<Song>(User.GetAllSongs());
        }
    }
}
