using MusicPlayer.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace MusicPlayer.Services
{
    public class MusicPlayerService : BaseService
    {
        private readonly MediaPlayer mediaPlayer;
        private readonly DispatcherTimer positionTimer;

        private double trackPosition;
        private double trackLength;
        private bool isPlaying;
        private string isPlayingText;

        public Song CurrentSong { get; private set; }
        public event Action SongEnded;
        public event Action<double> OnTrackLengthUpdated;

        public double TrackLength
        {
            get => trackLength;
            private set
            {
                if (trackLength != value)
                {
                    trackLength = value;
                    OnPropertyChanged();
                }
            }
        }
        public double TrackPosition
        {
            get => trackPosition;
            private set
            {
                if (trackPosition != value)
                {
                    trackPosition = value;
                    OnPropertyChanged();
                }
            }
        }


        public double Volume
        {
            get => mediaPlayer.Volume * 100;
            set => mediaPlayer.Volume = value / 100;
        }


        public bool IsPlaying
        {
            get => isPlaying;
            private set
            {
                if (isPlaying != value)
                {
                    isPlaying = value;
                    OnPropertyChanged();
                    IsPlayingText = isPlaying ? "Pause" : "Play";
                }
            }
        }

        public string IsPlayingText
        {
            get => isPlayingText;
            private set
            {
                if (isPlayingText != value)
                {
                    isPlayingText = value;
                    OnPropertyChanged(); 
                }
            }
        }

        public MusicPlayerService()
        {
            mediaPlayer = new MediaPlayer();
            positionTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
            positionTimer.Tick += (sender, args) => TrackPosition = mediaPlayer.Position.TotalSeconds;


            mediaPlayer.MediaEnded += (sender, args) =>
            {
                SongEnded?.Invoke();
                TrackPosition = 0;
                positionTimer.Stop();
            };


            mediaPlayer.MediaOpened += (sender, args) =>
            {
                if (mediaPlayer.NaturalDuration.HasTimeSpan)
                {
                    TrackLength = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                }
            };
        }

        public void ChooseSong(Song song)
        {
            try
            {
                mediaPlayer.Open(new Uri(song.FilePath));
                CurrentSong = song;
                TrackPosition = 0;
            }
            catch (Exception e)
            {
                MessageBox.Show($"ERROR: {e.Message}");
                CurrentSong = song;
            }
        }

        public void Play()
        {
            if (CurrentSong != null)
            {
                mediaPlayer.Play();
                IsPlaying = true;
                positionTimer.Start();
            }
        }

        public void Pause()
        {
            if (CurrentSong != null)
            {
                mediaPlayer.Pause();
                IsPlaying = false;
                positionTimer.Stop();
            }
        }
        public void TogglePlayPause()
        {
            if (CurrentSong != null)
            {
                if (IsPlaying)
                {
                    Pause();
                }
                else
                {
                    Play();
                }
            }
        }

        public void Stop()
        {
            if (CurrentSong != null)
            {
                mediaPlayer.Stop();
                IsPlaying = false;
                positionTimer.Stop();
            }
        }
        public void Seek(double positionInSeconds)
        {
            if (CurrentSong != null)
            {
                mediaPlayer.Position = TimeSpan.FromSeconds(positionInSeconds);
            }
        }

    }
}
