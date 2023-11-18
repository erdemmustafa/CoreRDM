using CoreRDM.Entities;
using FluentNHibernate.Utils;
using Microsoft.AspNetCore.Identity;
using NHibernate.Linq;
using NHibernate.Util;
using System.Data;
using System.Text.Json.Serialization;

namespace CoreRDM.Models
{
    public class AuthenticateResponse
    {

        public AuthenticateResponse()
        {
            Role = new List<Role>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        [JsonIgnore]
        public List<Role> Role { get; set; }
        public string? ExpiryDate { get; set; }
        public string Message { get; set; }


        public AuthenticateResponse(Users user, string token,string Expire)
        {
            Id = user.User_Id;
            FirstName = user.Name;
            LastName = user.SurName;
            Username = user.UserCode;
            Password = user.Password;
            Token = token;
            Role = user.UserRoleMapping.SelectMany(x => x.roles).ToList();
            ExpiryDate = Expire;
            Message = user.Message;
        }
    }
}
