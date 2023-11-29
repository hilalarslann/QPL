using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QPL.BL.ManufacturerDomain;
using QPL.BL.ProductDomain;
using Microsoft.AspNetCore.Authorization;
using QPL.BL.ValidateDomain;
using QPL.DAL.Entities.Concrete;

namespace QPL.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "QPL_Uretici_Girisi")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManufacturerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<LoadResult> Get(DataSourceLoadOptions loadOptions)
        {

            var source = await _mediator.Send(new ManufacturerQuery());
            return await DataSourceLoader.LoadAsync(source.Manufacturers, loadOptions);
        }

        [HttpGet("{id:int}")]
        public async Task<ManufacturerByIdResponse> GetById(int id) => await _mediator.Send(new ManufacturerByIdQuery() { Id = id });

        [HttpPost]
        public async Task<CreateManufacturerResponse> Create([FromBody] CreateManufacturerCommand command) => await _mediator.Send(command);

        [HttpPatch("{id:int}")]
        public async Task<UpdateManufacturerResponse> Update(int id, [FromBody] UpdateManufacturerCommand command)
        {
            command.Id = id;

            return await _mediator.Send(command);
        }

        [HttpDelete("{id:int}")]
        public async Task<DeleteManufacturerResponse> Delete(int id) => await _mediator.Send(new DeleteManufacturerCommand(id));

        [HttpGet("{id:int}/product")]
        public async Task<LoadResult> GetProducts(int id, DataSourceLoadOptions loadOptions)
        {
            var source = await _mediator.Send(new ProductQuery() { ManufacturerId = id });
            return await DataSourceLoader.LoadAsync(source.Products, loadOptions);
        }

    }
}
