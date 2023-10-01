using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.UseCase;
using OnlineShop.Application.UseCases.Customer.Crud;
using OnlineShop.Application.UseCases.User.Crud.Presenter;
using OnlineShop.Core.Schemas;
using OnlineShop.Utils;

namespace OnlineShop.Application.UseCases.Product.Crud
{
	[ApiController]
	[Route("api/products")]
	public class CrudProductCtrl : ControllerBase
	{
		private readonly IMapper _mapper;
		CrudProductFlow workFlow;

		[HttpPost(Name = "CreateProduct_1")]
		public async Task<IActionResult> Create([FromBody] CreateProductPresenter model)
		{
			ProductSchema product = _mapper.Map<ProductSchema>(model);
			Response response = await workFlow.Create(product);
			if (response.Status == Message.ERROR)
			{
				return BadRequest();
			}
			return Ok(response);
		}
	}
}

