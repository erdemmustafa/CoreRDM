using CoreRDM.Mapping;

namespace CoreRDM.Entities
{
    public class UserRoleMapping
    {
        public UserRoleMapping()
        {
            roles = new List<Role>();
        }
        public virtual int Id { get; set; }
        public virtual int User_Id { get; set; }
        public virtual int Role_Id { get; set; }
        public virtual Users Users { get; set; }
        public virtual IList<Role> roles { get; set; }
    }
}
