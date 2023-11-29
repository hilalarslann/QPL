using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPL.BL;
using QPL.BL.ProductDomain;

namespace QPL.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductDefinitionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductDefinitionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<LoadResult> Get(DataSourceLoadOptions loadOptions)
        {

            var source = await _mediator.Send(new ProductDefinitionQuery());

            return await DataSourceLoader.LoadAsync(source.ProductDefinitions, loadOptions);
        }


        [HttpGet("{id:int}")]
        public async Task<ProductDefinitionByIdResponse> GetById(int id) => await _mediator.Send(new ProductDefinitionByIdQuery() { Id = id });

        [HttpPost]
        public async Task<CreateProductDefinitionResponse> Create([FromBody] CreateProductDefinitionCommand command) => await _mediator.Send(command);

        [HttpPatch("{id:int}")]
        public async Task<UpdateProductDefinitionResponse> Update(int id, [FromBody] UpdateProductDefinitionCommand command)
        {
            command.Id = id;

            return await _mediator.Send(command);
        }

        [HttpDelete("{id:int}")]
        public async Task<DeleteProductDefinitionResponse> Delete(int id) => await _mediator.Send(new DeleteProductDefinitionCommand(id));

        [HttpGet("{id:int}/product")]
        public async Task<LoadResult> GetProducts(int id, DataSourceLoadOptions loadOptions)
        {

            var source = await _mediator.Send(new ProductQuery() { ProductDefinitionId = id });

            return await DataSourceLoader.LoadAsync(source.Products, loadOptions);
        }
    }
}
