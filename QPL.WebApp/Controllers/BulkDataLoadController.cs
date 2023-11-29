using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.DataValidation;
using QPL.BL;
using QPL.BL.ValidateDomain;
using QPL.DAL.Entities.Concrete;
using QPL.WebApp.Models.BulkDataLoad;
using QPL.WebApp.Models.Manufacturer;

namespace QPL.WebApp.Controllers
{
    //[Authorize]
    public class BulkDataLoadController : Controller
    {
        ValidateProductDefinitionQuery validatePDQuery = new ValidateProductDefinitionQuery();
        private readonly IMediator _mediator;
        public int CPCIndex { get; set; }
        public ProductDefinitionIndex pDefIndex = new ProductDefinitionIndex();
        public BulkDataLoadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult DataLoadSource()
        {
            return View();
        }
        public class ProductDefinitionIndex
        {
            public int CPC { get; set; }
            public int NortalCpc { get; set; }
            public int EngineeringCode { get; set; }
            public int SpecNo { get; set; }
            public int Description { get; set; }
            public int Comments { get; set; }
            public int Concept { get; set; }
            public int PackageType { get; set; }
            public int RoshStatus { get; set; }
            public int FlammabilityRating { get; set; }
            public int RadiassionRating { get; set; }
            public int DisassembledOrReusable { get; set; }
        }
        public void SetIndex(string[] values)
        {
            pDefIndex.CPC = Array.IndexOf(values, nameof(ProductDefinition.CPC));
            pDefIndex.NortalCpc = Array.IndexOf(values, nameof(ProductDefinition.NortelCpc));
            pDefIndex.EngineeringCode = Array.IndexOf(values, nameof(ProductDefinition.EngineeringCode));
            pDefIndex.SpecNo = Array.IndexOf(values, nameof(ProductDefinition.SpecNo));
            pDefIndex.Description = Array.IndexOf(values, nameof(ProductDefinition.Description));
            pDefIndex.Comments = Array.IndexOf(values, nameof(ProductDefinition.Comments));
            pDefIndex.Concept = Array.IndexOf(values, nameof(ProductDefinition.Concept));
            pDefIndex.PackageType = Array.IndexOf(values, nameof(ProductDefinition.PackageType));
            pDefIndex.RoshStatus = Array.IndexOf(values, nameof(ProductDefinition.RoshStatus));
            pDefIndex.FlammabilityRating = Array.IndexOf(values, nameof(ProductDefinition.FlammabilityRating));
            pDefIndex.RadiassionRating = Array.IndexOf(values, nameof(ProductDefinition.RadiassionRating));
            pDefIndex.DisassembledOrReusable = Array.IndexOf(values, nameof(ProductDefinition.DisassembledOrReusable));
        }


        [HttpPost]
        public async Task<IActionResult> ProductDefinitionExcelUpload(IFormFile file)
        {
            BulkProductViewModel model = new BulkProductViewModel();
            Task<ValidateProductDefinitionResponse> productDefinitionResponse = _mediator.Send(new ValidateProductDefinitionQuery());
            if (file != null && file.Length > 0 && file.ContentType.Contains("csv"))
            {
                using (var stream = file.OpenReadStream())
                using (var reader = new StreamReader(stream))
                {
                    var headerLine = reader.ReadLine();
                    string[] nonCommaValues = headerLine.Split(',');
                    string[] nonSemiColonValues = headerLine.Split(';');

                    char symbol = nonCommaValues.Length > nonSemiColonValues.Length ? symbol = ',' : symbol = ';';
                    string[] headerLineValues = nonCommaValues.Length > nonSemiColonValues.Length ? headerLineValues = nonCommaValues : headerLineValues = nonSemiColonValues;

                    SetIndex(headerLineValues);

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        var values = line.Split(symbol);

                        if (values[pDefIndex.CPC] == null)
                        {
                            ViewBag.Message = "Boş girilen cpc alanları mevcut";
                        }

                        var productDefinition = new ProductDefinition
                        {
                            CPC = values[pDefIndex.CPC],
                            NortelCpc = values[pDefIndex.NortalCpc],
                            EngineeringCode = values[pDefIndex.EngineeringCode],
                            SpecNo = values[pDefIndex.SpecNo],
                            Description = values[pDefIndex.Description],
                            Comments = values[pDefIndex.Comments],
                            Concept = values[pDefIndex.Concept],
                            PackageType = values[pDefIndex.PackageType],
                            RoshStatus = values[pDefIndex.RoshStatus],
                            FlammabilityRating = values[pDefIndex.FlammabilityRating],
                            RadiassionRating = values[pDefIndex.RadiassionRating],
                            DisassembledOrReusable = Convert.ToBoolean(values[pDefIndex.DisassembledOrReusable]),
                            IsActive = true,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedDate = DateTime.UtcNow,
                        };
                        validatePDQuery.productDefinition = productDefinition;
                        var res = await _mediator.Send(validatePDQuery);
                        model.succeedProductDefinition = res.succeedProductDefinition;
                        model.failedProductDefinitions = res.failedProductDefinitions;
                        model.Messages = res.Messages;
                        model.HeaderLine = nonCommaValues;

                        //productDefinitionResponse = ValidationControl(productDefinition);
                    }
                }
                return View("DataValidation", model);
            }
            else
            {
                ViewBag.Message = "Lütfen bir CSV dosyası yükleyin.";
            }

            return View("DataLoadSource");
        }

        public async Task<ValidateProductDefinitionResponse> ValidationControl(ProductDefinition pd)
        {
            validatePDQuery.productDefinition = pd;
            ValidateProductDefinitionResponse response = await _mediator.Send(validatePDQuery);
            return response;
        }
    }
}
