using MusicPlayer.Commands;
using MusicPlayer.Services;
using MusicPlayer.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicPlayer.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; set; }

        public ICommand RegisterCommand { get; set; }

        private readonly UserService user;

        public string Username { get; set; }
        public string Password { get; set; }

        public LoginViewModel(NavigationStore navigationStore, UserService user)
        {
            this.user = user;
            LoginCommand = new LoginCommand(new NavigationService(navigationStore, () => new HomeViewModel(navigationStore, this.user, new MusicPlayerService())), this.user);
            RegisterCommand = new RegisterCommand(new NavigationService(navigationStore, () => new HomeViewModel(navigationStore, this.user, new MusicPlayerService())), this.user);


        }
    }
}
