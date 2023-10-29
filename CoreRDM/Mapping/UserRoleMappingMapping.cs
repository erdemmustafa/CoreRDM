using CoreRDM.Entities;
using FluentNHibernate.Mapping;

namespace CoreRDM.Mapping
{
    public class UserRoleMappingMapping:ClassMap<UserRoleMapping>
    {
        public UserRoleMappingMapping()
        {
            Schema("RdmFlow.Dbo");
            Table("UserRoleMapping");
            Id(x => x.Id);
            Map(x => x.User_Id).Column("UserId");
            Map(x => x.Role_Id).Column("RoleId");
        }
    }
}
