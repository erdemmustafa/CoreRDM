using System.Text.Json.Serialization;

namespace CoreRDM.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }
        public string? Message { get; internal set; }
    }
}
