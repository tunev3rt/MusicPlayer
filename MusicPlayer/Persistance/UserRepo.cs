﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MusicPlayer.Connection;
using MusicPlayer.Model;

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
                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected == 1;
                    }
                    catch (SqlException ex)
                    {
                        return false;
                    }
                }
            }
        }
        public List<Song> GetAllSongs()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Songs", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Song> songs = new List<Song>();
                        while (reader.Read())
                        {
                            Song song = new Song
                            {
                                SongID = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Artist = reader.GetString(2),
                                Album = reader.GetString(3),
                                Duration = reader.GetInt32(4),
                                FilePath = reader.GetString(5),
                                DateAdded = reader.GetDateTime(6)
                            };
                            songs.Add(song);
                        }
                        return songs;
                    }
                }
            }
        }
        public List<Playlist> GetPlaylistsForUser(string username)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Playlists WHERE Username = @Username", con))
                {
                    cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Playlist> playlists = new List<Playlist>();
                        while (reader.Read())
                        {
                            Playlist playlist = new Playlist
                            {
                                PlaylistID = reader.GetInt32(0),
                                PlaylistName = reader.GetString(1),
                                Username = reader.GetString(2),
                                DateCreated = reader.GetDateTime(3)
                            };
                            playlists.Add(playlist);
                        }
                        return playlists;
                    }
                }
            }
        }
        public void CreatePlaylist(string username, Playlist playlist)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Playlists (PlaylistName, Username) VALUES (@PlaylistName, @Username); SELECT SCOPE_IDENTITY();", con))
                {
                    cmd.Parameters.Add("@PlaylistName", SqlDbType.NVarChar).Value = playlist.PlaylistName;
                    cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
                    playlist.DateCreated = DateTime.Now;
                    playlist.PlaylistID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public void AddSongToPlaylist(Song song, Playlist playlist)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO PlaylistSongs (PlaylistID, SongID) VALUES (@PlaylistID, @SongID)", con))
                {
                    cmd.Parameters.Add("@PlaylistID", SqlDbType.Int).Value = playlist.PlaylistID;
                    cmd.Parameters.Add("@SongID", SqlDbType.Int).Value = song.SongID;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Song> GetSongsFromPlaylist(Playlist playlist)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Songs.SongID, Title, Artist, Album, Duration, FilePath, Songs.DateAdded FROM Songs JOIN PlaylistSongs ON Songs.SongID = PlaylistSongs.SongID WHERE PlaylistID = @PlaylistID", con))
                {
                    cmd.Parameters.Add("@PlaylistID", SqlDbType.Int).Value = playlist.PlaylistID;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Song> songs = new List<Song>();
                        while (reader.Read())
                        {
                            Song song = new Song
                            {
                                SongID = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Artist = reader.GetString(2),
                                Album = reader.GetString(3),
                                Duration = reader.GetInt32(4),
                                FilePath = reader.GetString(5),
                                DateAdded = reader.GetDateTime(6)
                            };
                            songs.Add(song);
                        }
                        return songs;
                    }
                }
            }
        }
        public void AddSong(Song song)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Songs (Title, Artist, Album, Duration, FilePath) VALUES (@Title, @Artist, @Album, @Duration, @FilePath); SELECT SCOPE_IDENTITY();", con))
                {
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = song.Title;
                    cmd.Parameters.Add("@Artist", SqlDbType.NVarChar).Value = song.Artist;
                    cmd.Parameters.Add("@Album", SqlDbType.NVarChar).Value = song.Album;
                    cmd.Parameters.Add("@Duration", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@FilePath", SqlDbType.NVarChar).Value = song.FilePath;

                    song.SongID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public void DeletePlaylist(Playlist playlist)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        // Step 1: Delete related records in PlaylistSongs
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM PlaylistSongs WHERE PlaylistID = @PlaylistID", con, transaction))
                        {
                            cmd.Parameters.Add("@PlaylistID", SqlDbType.Int).Value = playlist.PlaylistID;
                            cmd.ExecuteNonQuery();
                        }

                        // Step 2: Delete the playlist
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Playlists WHERE PlaylistID = @PlaylistID", con, transaction))
                        {
                            cmd.Parameters.Add("@PlaylistID", SqlDbType.Int).Value = playlist.PlaylistID;
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error deleting playlist", ex);
                    }
                }
            }
        }



    }
}
