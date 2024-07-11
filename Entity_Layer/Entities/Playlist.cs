using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer.Entities
{
    public class Playlist
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlaylistId { get; set; }
        public string PlaylistName { get; set; }

        //playlist'i oluşturan user
        public int UserId { get; set; }

        //navigation property
        public User User { get; set; }

        //şarkılar 
        public IEnumerable<Song> Songs { get; set;}

    }
}
