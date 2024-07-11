using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer.Entities
{
    public enum UserType : Int16
    {
        Admin = 0,
        Moderator = 1,
        Standard = 2,
        
    }
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //unique
        public string UserName { get; set; }

        //token oluştururken kullanıcam sonra
        public UserType UserRole { get; set; } = UserType.Standard;
        //default false
        public bool IsApproved { get; set; } = false;
     

        //user birden fazla playlist oluşturabilir
        public IEnumerable<Playlist> Playlists { get; set; }
    }
}
