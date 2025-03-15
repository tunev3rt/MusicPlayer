using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MusicPlayer.Services;

namespace MusicPlayer.Commands
{
    public class PlayPauseCommand : ICommand
    {
        private readonly MusicPlayerService musicPlayerService;

        public event EventHandler CanExecuteChanged;

        public PlayPauseCommand(MusicPlayerService musicPlayerService)
        {
            this.musicPlayerService = musicPlayerService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            musicPlayerService.TogglePlayPause();
        }
    }
}
