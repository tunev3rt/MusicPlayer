using Microsoft.Data.SqlClient;
using MusicPlayer.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Authentication
{
    public static class LoginHandler
    {
        private static readonly string ConnectionString = DBConnectionManager.GetConnectionString();

        public static bool Login(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @Username AND Passcode = @Passcode", con))
                {
                    cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
                    cmd.Parameters.Add("@Passcode", SqlDbType.NVarChar).Value = password;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
    }
}
