using OnlineShop.Core;
using OnlineShop.Core.Schemas.Base;

namespace OnlineShop.Services.Base
{

    public interface IGroupPerm : IBaseService<GroupPerm>
    {
        List<GroupPerm> Creates(List<GroupPerm> perms);
    }

    public class GroupPermService : BaseService<GroupPerm, DataContext>, IGroupPerm
    {

        private readonly DataContext context;
        public GroupPermService(DataContext _ctx) : base(_ctx)
        {
            context = _ctx;
        }

        public List<GroupPerm> Creates(List<GroupPerm> groupsPerms)
        {
            context.GroupsPerms.AddRange(groupsPerms);
            context.SaveChanges();
            return groupsPerms;
        }
    }
}
