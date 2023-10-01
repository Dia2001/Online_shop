using OnlineShop.Core.Schemas;
using OnlineShop.Core;
using OnlineShop.Services.Base; 

namespace OnlineShop.Services
{ 
    public interface ICategory : IBaseService<CategorySchema>
    {
    }

    public class CategoryService : BaseService<CategorySchema, DataContext>, ICategory
    {

        private readonly DataContext context;

        public CategoryService(DataContext _ctx) : base(_ctx)
        {
            this.context = _ctx;
        }
    }    
}
