using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer.Connection;

namespace MusicPlayer.Persistance
{
    public class UserRepo
    {
        private readonly string connectionString;

        public UserRepo()
        {
            connectionString = DBConnectionManager.GetConnectionString();
        }


    }
}
