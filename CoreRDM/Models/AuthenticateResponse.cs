using CoreRDM.Entities;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace CoreRDM.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public List<Role> Role { get; set; }
        public string? ExpiryDate { get; set; }
        public string Message { get; set; }


        public AuthenticateResponse(Users user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Password = user.Password;
            Token = token;
            Role = user.Roles;
            ExpiryDate = System.DateTime.Now.ToString();
            Message = user.Message;
        }
    }
}
