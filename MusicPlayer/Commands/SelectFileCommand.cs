using Microsoft.Win32;
using MusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicPlayer.Commands
{
    public class SelectFileCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object? parameter)
        {
            bool canExecute = false;
            if (parameter is SongConfigurationViewModel scv)
            {
                Trace.WriteLine(scv.NewSong.FilePath);
                if (string.IsNullOrEmpty(scv.NewSong.FilePath))
                {
                    canExecute = true;
                }
            }
            return canExecute;
        }
        public void Execute(object? parameter)
        {
            if (parameter is SongConfigurationViewModel scv)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "MP3 Files (*.mp3)|*.mp3";
                fileDialog.Title = "Select song file...";

                bool? success = fileDialog.ShowDialog();
                if (success == true)
                {
                    scv.NewSong.FilePath = fileDialog.FileName;
                }
            }
        }
    }
}
