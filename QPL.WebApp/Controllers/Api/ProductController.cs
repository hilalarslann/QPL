using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QPL.BL.ManufacturerDomain;
using QPL.BL.ProductDomain;
using Microsoft.AspNetCore.Authorization;

namespace QPL.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}")]
        public async Task<ProductByIdResponse> GetById(int id) => await _mediator.Send(new ProductByIdQuery() { Id = id });

        [HttpPost("{productDefinitionId:int}")]
        public async Task<CreateProductResponse> CreateProduct(int productDefinitionId, [FromBody] CreateProductCommand command)
        {
            command.ProductDefinitionId = productDefinitionId;
            return await _mediator.Send(command);
        }


        [HttpPatch("{id:int}")]
        public async Task<UpdateProductResponse> Update(int id, [FromBody] UpdateProductCommand command)
        {
            command.Id = id;

            return await _mediator.Send(command);
        }
        [HttpDelete("{id:int}")]
        public async Task<DeleteProductResponse> Delete(int id) => await _mediator.Send(new DeleteProductCommand(id));
    }
}
