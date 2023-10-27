using CoreRDM.Entities;
using FluentNHibernate.Mapping;

namespace CoreRDM.Mapping
{
    public class UsersMapping:ClassMap<Users>
    {
        public UsersMapping()
        {
            Schema("RdmFlow.dbo");
            Table("Users");
            Id(x => x.User_Id);
            Map(x => x.UserCode).Column("UserCode");
            Map(x => x.Password).Column("Password");
            Map(x => x.Name).Column("Name");
            Map(x => x.SurName).Column("SurName");
            Map(x => x.Email).Column("Email");
            Map(x => x.Gsm).Column("GSM");
        }
    }
}
