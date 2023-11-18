using CoreRDM.Entities;
using FluentNHibernate.Mapping;

namespace CoreRDM.Mapping
{
    public class RoleMapping:ClassMap<Role>
    {
        public RoleMapping()
        {
            Schema("RdmFlow.dbo");
            Table("Roles");
            Id(x=>x.Id).Column("Id");
            Map(x => x.Name).Column("Name");
            References(x => x.RoleMapping).Column("Id").ReadOnly().LazyLoad();

        }
    }
}
