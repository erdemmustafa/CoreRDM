using System.Text.Json.Serialization;

namespace CoreRDM.Entities
{
    public class Users
    {
        public virtual int Id { get; set; }
        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }
        public virtual string? Username { get; set; }

        [JsonIgnore]
        public virtual string? Password { get; set; }
        public virtual string? Message { get; set; }

        public virtual List<Role> Roles { get; set; }
    }
}
