using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Model
{
    public class Song
    {
        public int SongID { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public int Duration { get; set; }

        public string FilePath { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
