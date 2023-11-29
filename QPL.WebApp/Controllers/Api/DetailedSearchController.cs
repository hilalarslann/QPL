using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QPL.BL;
using QPL.BL.DetailedSearch;
using Microsoft.AspNetCore.Authorization;

namespace QPL.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DetailedSearchController : Controller
    {
        private readonly IMediator _mediator;
        public DetailedSearchController( IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<LoadResult> Get(DataSourceLoadOptions loadOptions)
        {

            var source = await _mediator.Send(new DetailedSearchQuery());

            return await DataSourceLoader.LoadAsync(source.products, loadOptions);
        }
    }
}




