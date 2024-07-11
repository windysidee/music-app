using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity_Layer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositories
{
   
    public class SongRepository
    {
        MusicShelfContext _musicShelfContext;
        public SongRepository(MusicShelfContext context)
        {
            _musicShelfContext = context;
        }
        public List<Song> GetSongs() 
        {
            return _musicShelfContext.Songs.ToList();
        }
        public Song? GetSongByName(string name)
        {
            return _musicShelfContext.Songs.FirstOrDefault(s => s.Name == name);
        }
        public bool AddSong(Song song)
        {
            try
            {
                if(GetSongByName(song.Name) == null)
                {
                    _musicShelfContext.Songs.Add(song);
                    _musicShelfContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
