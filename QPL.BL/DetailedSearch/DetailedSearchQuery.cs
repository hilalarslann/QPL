using MediatR;
using Microsoft.EntityFrameworkCore;
using QPL.DAL.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace QPL.BL.DetailedSearch
{
    public class DetailedSearchQuery : IRequest<DetailedSearchResponse>
    {
        public DetailedSearchQuery()
        {

        }

    }

    public class DetailedSearchResponse : BaseHandlerResponse
    {
        public IQueryable<ListItem> products { get; set; }


        public class ListItem
        {
            public int Id { get; set; }
            public string CPC { get; set; } = string.Empty;
            public string EngineeringCode { get; set; } = string.Empty;
            public string Concept { get; set; } = string.Empty;
            public string SpecNo { get; set; } = string.Empty;
            public string ManufacturerCode { get; set; } = string.Empty;
            public string NortelCpc { get; set; } = string.Empty;
            public string RoshStatus { get; set; } = string.Empty;
            public string PackageType { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Comments { get; set; } = string.Empty;
            public string RadiassionRating { get; set; } = string.Empty;
        }
    }
    public class DetailedSearchQueryHandler : IRequestHandler<DetailedSearchQuery, DetailedSearchResponse>
    {
        private readonly DataContext _dataContext;
        public DetailedSearchQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<DetailedSearchResponse> Handle(DetailedSearchQuery request, CancellationToken cancellationToken)
        {
            var response = new DetailedSearchResponse();
            try
            {
                response.products = _dataContext.Products
                .Include(x => x.ProductDefinition)
                .Select(r => new DetailedSearchResponse.ListItem
                {
                    Id = r.ProductDefinitionId,
                    CPC = r.ProductDefinition.CPC,
                    NortelCpc = r.ProductDefinition.NortelCpc,
                    EngineeringCode = r.ProductDefinition.EngineeringCode,
                    Concept = r.ProductDefinition.Concept,
                    Description = r.ProductDefinition.Description,
                    RoshStatus = r.ProductDefinition.RoshStatus,
                    PackageType = r.ProductDefinition.PackageType,
                    ManufacturerCode = r.ManufacturerCode,
                    RadiassionRating = r.ProductDefinition.RadiassionRating,
                    Comments = r.ProductDefinition.Comments,
                    SpecNo= r.ProductDefinition.SpecNo,

                }).AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }

    }
    

}


//todo : DataGrid'de DisassembledOrReusable true seçeneği listelenmiyor. 
