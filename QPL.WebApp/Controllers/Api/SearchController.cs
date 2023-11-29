using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPL.BL;
using QPL.BL.Search;

namespace QPL.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SearchController : Controller
    {
        private readonly IMediator _mediator;
        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<LoadResult> Get(DataSourceLoadOptions loadOptions)
        {
            var source = await _mediator.Send(new SearchQuery());
            return await DataSourceLoader.LoadAsync(source.products, loadOptions);
        }

        [HttpGet("{searchText}/getbyexcelsearch")]
        public async Task<LoadResult> GetByExcelSearch(string searchText, DataSourceLoadOptions loadOptions)
        {
            var source = await _mediator.Send(new SearchQuery() { UserId = 1, SearchText = searchText });
            return await DataSourceLoader.LoadAsync(source.products, loadOptions);
        }


    }
}



