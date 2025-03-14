using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Model
{
    public class Playlist
    {
        public int PlaylistID { get; set; }

        public string PlaylistName { get; set; }

        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
