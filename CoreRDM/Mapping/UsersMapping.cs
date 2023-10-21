using CoreRDM.Entities;
using FluentNHibernate.Mapping;

namespace CoreRDM.Mapping
{
    public class UsersMapping:ClassMap<Users>
    {
        public UsersMapping()
        {
            Table("Users");
            Id(x => x.Id);
            Map(x => x.Username).Column("Username");
            Map(x => x.Password).Column("Password");
            Map(x => x.FirstName).Column("PersonName");
            Map(x => x.LastName).Column("PersonSurname");
        }
    }
}
