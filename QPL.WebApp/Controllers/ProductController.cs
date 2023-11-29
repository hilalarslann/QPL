using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPL.WebApp.Models.Manufacturer;
using QPL.WebApp.Models.Product;
using QPL.WebApp.Models.ProductDefinition;

namespace QPL.WebApp.Controllers
{
    //[Authorize]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("Product/Add/{productDefinitionId:int}")]
        public IActionResult Add(int productDefinitionId)
        {
            var model = new EditProductViewModel()
            {
                ProductDefinitionId = productDefinitionId
            };
            return View("Editor", model);
        }

        [Route("Product/{id:int}/Edit/{productDefinitionId:int}")]
        public IActionResult Edit(int id, int productDefinitionId)
        {
            var model = new EditProductViewModel()
            {
                Id = id,
                ProductDefinitionId = productDefinitionId,
            };
            return View("Editor", model);
        }
    }
}
