using OnlineShop.Core;
using OnlineShop.Core.Schemas.Base;

namespace OnlineShop.Services.Base
{

    public interface IGroup : IBaseService<GroupSchema>
    {
    }

    public class GroupService : BaseService<GroupSchema, DataContext>, IGroup
    {

        private readonly DataContext context;
        public GroupService(DataContext _ctx) : base(_ctx)
        {
            context = _ctx;
        }
         
	}
}
