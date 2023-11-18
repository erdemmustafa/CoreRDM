namespace CoreRDM.Entities
{
    public class Role
    {
        public virtual UserRoleMapping RoleMapping { get; set; }
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}
