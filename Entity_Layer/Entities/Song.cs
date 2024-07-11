using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer.Entities
{
    public class Song
    {
        /*Şarkı adı
        Sanatçı
        Tür
        Kapak Resmi
        Ses Dosyası
        Şarkıyı Platforma Ekleyen Kullanıcı*/

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SongId { get; set; }

        public string Artist { get; set; }  
        public string Name { get; set;}
        public string Genre { get; set;}

        //link 
        public string CoverUrl { get; set;}

        //link yaptım değiştirilebilir.
        public byte[] AudioFile { get; set;}

        //şarkıyı platforma ekleyecek user
        public int UserId { get; set; }

        //navigation property
        public User User { get; set;}

    }
}
