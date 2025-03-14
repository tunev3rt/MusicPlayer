using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
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

        public bool Register(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Passcode) VALUES (@Username, @Passcode)", con))
                {
                    cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
                    cmd.Parameters.Add("@Passcode", SqlDbType.NVarChar).Value = password;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected == 1;
                }
            }
        }


    }
}
