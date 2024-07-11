using Microsoft.AspNetCore.Mvc;
using Entity_Layer.Models;
using Entity_Layer.Entities;
using Data_Access_Layer.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace Web_API.Controllers
{

    [Route("api/users")]
    [EnableCors]
    
    public class UserController : Controller
    {
        UserRepository _userRepository;
        IMapper _mapper;
        TokenService _tokenService;


        public UserController(UserRepository userRepository, IMapper mapper, TokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;

        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserModel model)
        {
            try
            {
                var user = _userRepository.FindUserByUserName(model.UserName);

                //model -> entity
                var userModel = _mapper.Map<LoginUserModel>(user);

                //token service class'ı açıldı
                if (userModel == null)
                {
                    return NotFound("User not found!");

                }
                else if (user.IsApproved == false)
                {
                    return BadRequest("User is not approved!");
                }


                else
                {
                    var token = _tokenService.GenerateToken("music_shelf_backend", "music_shelf_frontend", user.UserName, user.UserRole);
                    Response.Headers.Append("Authorization", $"Bearer {token}");
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while login attempt: {ex.Message}");
                return StatusCode(500, "An error occurred while login.");
            }
            


        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserModel model)
        {
            try
            {
                var user = _mapper.Map<User>(model);
                bool isRegistered = _userRepository.RegisterUser(user);

                if (isRegistered == true)
                {
                    return Accepted("Your login request has been sent!");
                }
                else
                {
                    return BadRequest("Username already taken!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while registering the user: {ex.Message}");
                return StatusCode(500, "An error occurred while registering the user.");
            }
        }


        [HttpPost("approve/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Approve(int id)
        {
            try
            {
                if (_userRepository.ApproveUser(id))
                {
                    return Ok("Users have been approved successfully.");

                }
                else return BadRequest("Users cannot approved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error approving users: {ex.Message}");
                return StatusCode(500, "An error occurred while approving users.");
            }
        }
        [HttpDelete("delete/{id}" )]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteApprove(int id)
        {
            try
            {
                if (_userRepository.DeleteApprove(id))
                {
                    return Ok("Users have been approved successfully.");

                }
                else return BadRequest("Users cannot deleted.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error approving users: {ex.Message}");
                return StatusCode(500, "An error occurred while approving users.");
            }
        }
        [HttpGet("pending-approvals")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetPendingApprovals()
        {
            try
            {
                var users = _userRepository.GetPendingApprovals();
                if (users == null)
                {
                    return BadRequest();
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting users: {ex.Message}");
                return StatusCode(500, "An error occurred while getting users.");
            }
        }

        [HttpGet("standard-users")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetStandardUsers()
        {
            try
            {
                var users = _userRepository.GetStandardUsers();
                if (users == null)
                {
                    return BadRequest();
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting users: {ex.Message}");
                return StatusCode(500, "An error occurred while getting users.");
            }
        }
        [HttpPost("create-moderator/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateModerator(int id)
        {
            try
            {
                if (_userRepository.CreateModerator(id))
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Error creating moderator: {ex.Message}");
                return StatusCode(500, "An error occurred while creating moderator.");
            }
            
        }

    }
}
