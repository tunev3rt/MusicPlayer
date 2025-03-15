using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using MusicPlayer.Model;
using MusicPlayer.Services;
using MusicPlayer.Stores;
using MusicPlayer.Commands;
using System.Diagnostics;

namespace MusicPlayer.ViewModels
{
    public class SongSelectionViewModel : BaseViewModel
    {
        private readonly NavigationStore navigationStore;
        private readonly UserService user;
        private readonly MusicPlayerService musicPlayerService;

        public ICommand ReturnToHomeViewCommand { get; set; }

        public ICommand AddSongToPlaylistCommand { get; set; }

        public ObservableCollection<Song> Songs { get; set; }

        public Playlist SelectedPlaylist { get; set; }

        private Song _selectedSong;
        public Song SelectedSong
        {
            get => _selectedSong;
            set
            {
                if (_selectedSong != value)
                {
                    _selectedSong = value;
                    OnPropertyChanged();
                }
            }
        }



        public SongSelectionViewModel(NavigationStore navigationStore, UserService user, Playlist playlist, MusicPlayerService musicPlayerService) 
        {
            this.navigationStore = navigationStore;
            this.user = user;
            SelectedPlaylist = playlist;
            this.musicPlayerService = musicPlayerService;
            ReturnToHomeViewCommand = new NavigateCommand(new Services.NavigationService(navigationStore, () => new HomeViewModel(navigationStore, user, this.musicPlayerService)), user);
            Songs = new ObservableCollection<Song>(user.GetAllSongs());
            AddSongToPlaylistCommand = new AddSongToPlaylistCommand(user);

        }
    }
}
