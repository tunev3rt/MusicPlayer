using MusicPlayer.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace MusicPlayer.Services
{
    public class MusicPlayerService : BaseService
    {
        private readonly MediaPlayer mediaPlayer;
        private readonly DispatcherTimer positionTimer;
        public Song CurrentSong { get; private set; }
        public event Action SongEnded;
        public event Action<double> OnTrackLengthUpdated;

        private double trackLength;
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

        private double trackPosition;
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

        private bool isPlaying;
        private string isPlayingText;

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
            CurrentSong = song;
            mediaPlayer.Open(new Uri(CurrentSong.FilePath));
            TrackPosition = 0;
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
