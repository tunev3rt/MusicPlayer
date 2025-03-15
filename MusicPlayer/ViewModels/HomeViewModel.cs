﻿using MusicPlayer.Services;
using MusicPlayer.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public ICommand PlayPauseCommand { get; set; }


        private Playlist selectedPlaylist;
        public Playlist SelectedPlaylist
        {
            get => selectedPlaylist;
            set
            {
                if (selectedPlaylist != value)
                {
                    selectedPlaylist = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SelectedPlaylistSongs));
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
                        musicPlayerService.ChooseSong(value);
                        musicPlayerService.Play();
                    }
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TrackLength));
                    OnPropertyChanged(nameof(TrackPosition));
                }
            }
        }

        public UserService User { get; set; }

        public ObservableCollection<Playlist> Playlists { get; set; }

        public ObservableCollection<Song> Songs { get; set; }

        public HomeViewModel(NavigationStore navigationStore, UserService user, MusicPlayerService musicPlayerService)
        {
            AddPlaylistCommand = new AddPlaylistCommand(user);
            LogoutCommand = new LogoutCommand(navigationStore);
            AddSongToPlaylistCommand = new NavigateCommand(new NavigationService(navigationStore, () => new SongSelectionViewModel(navigationStore, user, SelectedPlaylist, musicPlayerService)), user);
            PlayPauseCommand = new PlayPauseCommand(musicPlayerService);


            User = user;
            this.musicPlayerService = musicPlayerService;
            this.musicPlayerService.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(MusicPlayerService.TrackLength))
                {
                    OnPropertyChanged(nameof(TrackLength));
                }
                if (args.PropertyName == nameof(MusicPlayerService.TrackPosition))
                    OnPropertyChanged(nameof(TrackPosition));

                if (args.PropertyName == nameof(MusicPlayerService.IsPlayingText))
                    OnPropertyChanged(nameof(IsPlayingText));
            };


            Playlists = new ObservableCollection<Playlist>(User.GetPlaylistsForUser());
            Songs = new ObservableCollection<Song>(User.GetAllSongs());
        }


        // Music Player Controls

        public double TrackLength => musicPlayerService.TrackLength;

        public double TrackPosition
        {
            get => musicPlayerService.TrackPosition;
            set
            {
                musicPlayerService.Seek(value);
                OnPropertyChanged();
            }
        }

        public double Volume
        {
            get => musicPlayerService.Volume;
            set
            {
                musicPlayerService.Volume = value;
                OnPropertyChanged();
            }
        }

        public string IsPlayingText => musicPlayerService.IsPlayingText;
    }
}
