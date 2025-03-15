using MusicPlayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MusicPlayer.Services
{
    public class MusicPlayerService
    {
        private readonly MediaPlayer mediaPlayer;
        private Song currentSong;
        public event Action SongEnded;

        public double TrackPosition => mediaPlayer.Position.TotalSeconds;
        public double TrackLength => mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;

        public double Volume
        {
            get => mediaPlayer.Volume * 100;
            set => mediaPlayer.Volume = value / 100;
        }

        public MusicPlayerService()
        {
            mediaPlayer = new MediaPlayer();
            mediaPlayer.MediaEnded += (sender, args) => SongEnded?.Invoke();
        }

        public void Play(Song song)
        {
            currentSong = song;
            mediaPlayer.Open(new Uri(song.FilePath));
            mediaPlayer.Play();
        }

        public void Pause()
        {
            mediaPlayer.Pause();
        }
        public void TogglePlayPause()
        {
            if (mediaPlayer.CanPause && mediaPlayer.Position.TotalSeconds < mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds)
            {
                Pause();
            }
            else
            {
                Play(currentSong);
            }
        }
        public void Stop()
        {
            mediaPlayer.Stop();
        }
        public void Seek(double positionInSeconds)
        {
            mediaPlayer.Position = TimeSpan.FromSeconds(positionInSeconds);
        }

    }
}
