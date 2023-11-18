using System.Text.Json.Serialization;

namespace CoreRDM.Entities
{
    public class Users
    {

        public Users()
        {

            UserRoleMapping = new List<UserRoleMapping>();
        }

        public virtual int User_Id { get; set; }
        public virtual string? UserCode { get; set; }
        public virtual string? SurName { get; set; }
        public virtual string? Name { get; set; }

        [JsonIgnore]
        public virtual string? Password { get; set; }
        public virtual string? Message { get; set; }
        public virtual string? Email { get; set; }

        public virtual string? Gsm { get; set; }

        public virtual IList<UserRoleMapping> UserRoleMapping { get; set; }
    }
}
