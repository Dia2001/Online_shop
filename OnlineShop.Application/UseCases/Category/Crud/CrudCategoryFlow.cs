using OnlineShop.Application.UseCase;
using OnlineShop.Core.Schemas;
using OnlineShop.Services.Base;
using OnlineShop.Utils;

namespace OnlineShop.Application.UseCases.Category.Crud
{
	public class CrudCategoryFlow
	{
        private readonly IUnitOfWork uow;
        public CrudCategoryFlow(IUnitOfWork _uow)
        {
            uow = _uow;
        }
         
        public Response List()
        {
            var users = uow.Categories.FindAll();
            return new Response(Message.SUCCESS, users);
        }

        public async Task<Response> Create(CategorySchema category)
        {
            var result = uow.Categories.Create(category);
            return new Response(Message.SUCCESS, result);
        }

        public async Task<Response> Update(CategorySchema category)
        {
            var result = uow.Categories.Update(category);
            return new Response(Message.SUCCESS, result);
        }

        public Response Delete(int id)
        {
            var result = uow.Categories.Delete(id);
            return new Response(Message.SUCCESS, result);
        }

    }
}
