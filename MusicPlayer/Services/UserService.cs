﻿using MusicPlayer.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
    }
}
