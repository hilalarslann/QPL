using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using OfficeOpenXml;
using QPL.BL;
using QPL.DAL.Entities.Concrete;
using QPL.WebApp.Models.ProductDefinition;
using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;

namespace QPL.WebApp.Controllers
{
    //[Authorize]
    public class ProductDefinitionController : Controller
    {
        private readonly IMediator _mediator;

        public ProductDefinitionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("ProductDefinition/Add")]
        public IActionResult Add()
        {
            var model = new EditProductDefinitionViewModel();
            return View("Editor", model);
        }

        [Route("ProductDefinition/{id:int}/Edit")]
        public IActionResult Edit(int id)
        {
            var model = new EditProductDefinitionViewModel()
            {
                Id = id
            };
            return View("Editor", model);
        }


        [Route("ProductDefinition/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var res = await _mediator.Send(new ProductDefinitionByIdQuery(id));

            if (res.ProductDefinition == null)
            {
                return NotFound();
            }

            return View("Details", res.ProductDefinition);
        }

      
    }
}
