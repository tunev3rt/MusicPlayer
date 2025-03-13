using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Connection
{
    public static class DBConnectionManager
    {
        static IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        private static readonly string ConnectionString = config.GetConnectionString("MyDBConnection");

        public static string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}
