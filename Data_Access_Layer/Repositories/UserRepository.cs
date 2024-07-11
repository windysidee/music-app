using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Entity_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Data_Access_Layer.Repositories
{
    public class UserRepository : Interfaces.IUserRepository
    {
        MusicShelfContext _musicShelfContext;
        public UserRepository(MusicShelfContext context)
        {
            _musicShelfContext = context;
        }

        //gets all users
        public List<User> GetUsers()
        {
            return _musicShelfContext.Users.ToList();
        }
        public List<User> GetStandardUsers()
        {
            return _musicShelfContext.Users
                .Where(u => u.UserRole == UserType.Standard)
                .Where(u => u.IsApproved == true)
                .ToList();
        }

        //finds user by user name
        public User? FindUserByUserName(string userName)
        {
            return _musicShelfContext.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public User? GetUserById(int id)
        {
            var user = _musicShelfContext.Users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                Console.WriteLine($"User with ID {id} not found.");
            }

            return user;
        }


        //register user
        public bool RegisterUser(User user)
        {
            try
            {
                // Check if the username is already taken
                if (_musicShelfContext.Users.FirstOrDefault(u => u.UserName == user.UserName) == null)
                {
                    // User does not exist, proceed with registration
                    _musicShelfContext.Users.Add(user);
                    _musicShelfContext.SaveChanges();
                    return true;
                }
                else
                {
                    // If the username is already taken, return false without throwing an exception
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred while registering user: " + ex);
                return false;
            }
        }


        //for admin
        public List<User> GetPendingApprovals()
        {
            return _musicShelfContext.Users
                .Where(u => u.IsApproved == false)
                .ToList();
        }

        //admin
        public bool ApproveUser(int id)
        {
            try
            {   
                var user = GetUserById(id);
                user.IsApproved = true;
                _musicShelfContext.Users.Update(user);
                _musicShelfContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error approving user: {ex.Message}");
                return false;
            }
        }

        //admin
        public bool DeleteApprove(int id)
        {
            try
            {
                var user = GetUserById(id);
                _musicShelfContext.Users.Remove(user);
                _musicShelfContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting user: {ex.Message}");
                return false;
            }
        }
        public bool CreateModerator(int id )
        {
            try
            {
                var user = GetUserById(id);
                user.UserRole = UserType.Moderator;
                _musicShelfContext.Users.Update(user);
                _musicShelfContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message );
                return false;
            }
        }
        
    }
}