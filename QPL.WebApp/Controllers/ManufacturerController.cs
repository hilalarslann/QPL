using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPL.BL.ManufacturerDomain;
using QPL.WebApp.Models.Manufacturer;

namespace QPL.WebApp.Controllers
{
    //[Authorize(Roles = "QPL_Uretici_Girisi")]
    public class ManufacturerController : Controller
    {
        private readonly IMediator _mediator;

        public ManufacturerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Manufacturer/Add")]
        public IActionResult Add()
        {
            var model = new EditManufacturerViewModel();

            return View("Editor",model);
        }

        [Route("Manufacturer/{id:int}/Edit")]
        public IActionResult Edit(int id)
        {
            var model = new EditManufacturerViewModel()
            {
                Id = id
            };
            return View("Editor", model);
        }

        [Route("Manufacturer/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var res = await _mediator.Send(new ManufacturerByIdQuery(id));

            if (res.Manufacturer == null)
            {
                return NotFound();
            }

            var model = new ManufacturerDetailsViewModel()
            {
                Id = id,
                Name= res.Manufacturer.Name,
                Code = res.Manufacturer.Code
            };

            return View("Details", model);
        }
    }
}
