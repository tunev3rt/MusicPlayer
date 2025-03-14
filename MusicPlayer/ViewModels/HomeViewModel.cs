using MusicPlayer.Services;
using MusicPlayer.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public readonly UserViewModel uvm;

        public UserService User { get; set; }

        public string Username { get; set; }

        public HomeViewModel(NavigationStore navigationStore, UserService user)
        {
            User = user;
        }
    }
}
