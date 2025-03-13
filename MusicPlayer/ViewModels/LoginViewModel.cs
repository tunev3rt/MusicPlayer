using MusicPlayer.Services;
using MusicPlayer.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly UserService userService;

        public LoginViewModel(NavigationStore navigationStore, UserService userService)
        {
            this.userService = userService;
        }
    }
}
