using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entity_Layer.Entities;
using Data_Access_Layer.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Web_API.Controllers
{

    [Route("api/songs")]
    [EnableCors]
    public class SongController : Controller
    {
        SongRepository _songRepository;
        UserRepository _userRepository;
        public SongController(SongRepository songRepository, UserRepository userRepository) 
        {
            _songRepository = songRepository;
            _userRepository = userRepository;
        }

        [HttpGet("get-songs")]
        public IActionResult GetSongs()
        {
            try
            {
                var songs = _songRepository.GetSongs();              
                return Ok(songs);  
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }



        [HttpPost("add-song")]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult AddSong()
        {
            //şarkıyla user'ı bağlama
            int id = 0;
              
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                var username = jsonToken?.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
                var user = _userRepository.FindUserByUserName(username);
                id = user.UserId;
            }
            catch(Exception ex)
            {

            }

            //gelen form elemanını songa çevir
            //repository'ye gönder
            var uploadedFile = Request.Form.Files["audioFile"];
            var songName = Request.Form["songName"];
            var artist = Request.Form["artist"];
            var genre = Request.Form["genre"];
            var coverUrl = Request.Form["coverUrl"];
            byte[] audioBytes;
            
            using (MemoryStream memoryStream = new MemoryStream())
            {
                uploadedFile.CopyTo(memoryStream);
                audioBytes = memoryStream.ToArray();
            }
            string audioDataUrl = "data:audio/mp3;base64," + Convert.ToBase64String(audioBytes);
            byte[] byteArray = Encoding.ASCII.GetBytes(audioDataUrl);
            Song song = new Song
            {
                Name = songName,
                Artist = artist,
                Genre = genre,
                CoverUrl = coverUrl,
                AudioFile = audioBytes,
                UserId = id
            };
            try
            {
                if (_songRepository.AddSong(song))
                {
                    return Ok();
                }
                return BadRequest("Song repository error");
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while adding song.");
            }

        }
    }
}
