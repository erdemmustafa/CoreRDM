namespace CoreRDM.Entities
{
    public class UserRoleMapping
    {
        public virtual int Id { get; set; }
        public virtual int User_Id { get; set; }
        public virtual int Role_Id { get; set; }    
    }
}
