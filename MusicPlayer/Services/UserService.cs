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

        private UserRepo userRepo;

        public string Username { get {return username; } set { OnPropertyChanged(nameof(Username)); username = value; } }
        private string username;

        public UserService()
        {
            userRepo = new UserRepo();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool Register(string username, string password)
        {
            return userRepo.Register(username, password);
        }

        public List<Song> GetAllSongs()
        {
            return userRepo.GetAllSongs();
        }

        public List<Playlist> GetPlaylistsForUser()
        {
            return userRepo.GetPlaylistsForUser(Username);
        }

        public void CreateNewPlaylist(Playlist playlist)
        {
            userRepo.CreatePlaylist(Username, playlist);
        }
        
        public List<Song> GetSongsFromPlaylist(Playlist playlist)
        {
            return userRepo.GetSongsFromPlaylist(playlist);
        }

        public void AddSongToPlaylist(Song song, Playlist playlist)
        {
            userRepo.AddSongToPlaylist(song, playlist);
        }

        public void AddSong(Song song)
        {
            userRepo.AddSong(song);
        }
        public void DeletePlaylist(Playlist playlist)
        {
            userRepo.DeletePlaylist(playlist);
        }
    }
}
