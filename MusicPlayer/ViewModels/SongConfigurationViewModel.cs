using MusicPlayer.Commands;
using MusicPlayer.Services;
using MusicPlayer.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MusicPlayer.Model;

namespace MusicPlayer.ViewModels
{
    public class SongConfigurationViewModel : BaseViewModel
    {
        public ICommand AddSongCommand { get; set; }
        public ICommand SelectFileCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public Song NewSong { get; set; }

        public SongConfigurationViewModel(NavigationStore navigationStore, UserService user, MusicPlayerService musicPlayerService) 
        {
            // Properties
            NewSong = new Song();

            // Commands
            AddSongCommand = new AddSongCommand(user, new NavigationService(navigationStore, () => new HomeViewModel(navigationStore, user, musicPlayerService)));
            SelectFileCommand = new SelectFileCommand();
            CancelCommand = new NavigateCommand(new NavigationService(navigationStore, () => new HomeViewModel(navigationStore, user, musicPlayerService)), user);
        }
    }
}
