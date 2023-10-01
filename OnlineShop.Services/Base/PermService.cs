using OnlineShop.Core;
using OnlineShop.Core.Schemas.Base;

namespace OnlineShop.Services.Base
{

    public interface IPerm : IBaseService<PermSchema>
    { 
    }

    public class PermService : BaseService<PermSchema, DataContext>, IPerm
    {

        private readonly DataContext context;
        public PermService(DataContext _ctx) : base(_ctx)
        {
            context = _ctx;
        }

	 
	}
}
