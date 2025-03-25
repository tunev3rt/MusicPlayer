using MusicPlayer.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer.Model;

namespace MusicPlayer.Services
{
    public class UserService : INotifyPropertyChanged
    {

        private UserRepo _userRepo;

        public string Username { get {return _username; } set { OnPropertyChanged(nameof(Username)); _username = value; } }
        private string _username;

        public UserService()
        {
            _userRepo = new UserRepo();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool Register(string username, string password)
        {
            return _userRepo.Register(username, password);
        }

        public List<Song> GetAllSongs()
        {
            return _userRepo.GetAllSongs();
        }

        public List<Playlist> GetPlaylistsForUser()
        {
            return _userRepo.GetPlaylistsForUser(Username);
        }

        public void CreateNewPlaylist(Playlist playlist)
        {
            _userRepo.CreatePlaylist(Username, playlist);
        }
        
        public List<Song> GetSongsFromPlaylist(Playlist playlist)
        {
            return _userRepo.GetSongsFromPlaylist(playlist);
        }

        public void AddSongToPlaylist(Song song, Playlist playlist)
        {
            _userRepo.AddSongToPlaylist(song, playlist);
        }

        public void AddSong(Song song)
        {
            _userRepo.AddSong(song);
        }

    }
}
