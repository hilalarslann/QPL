using MediatR;
using QPL.DAL.EntityFramework.Context;

namespace QPL.BL.ProductDomain
{
    public class ProductQuery : IRequest<ProductResponse>
    {
        public ProductQuery()
        {
        }
        public int? ProductDefinitionId { get; set; }
        public int? ManufacturerId { get; set; }
    }

    public class ProductResponse : BaseHandlerResponse
    {
        public IQueryable<Models.Product> Products { get; set; }
    }

    public class ProductQueryHandler : IRequestHandler<ProductQuery, ProductResponse>
    {
        private readonly DataContext _dataContext;
        public ProductQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ProductResponse> Handle(ProductQuery request, CancellationToken cancellationToken)
        {
            var response = new ProductResponse();

            try
            {
                var query = _dataContext.Products.AsQueryable();

                if (request.ProductDefinitionId.HasValue)
                    query = query.Where(x => x.ProductDefinitionId == request.ProductDefinitionId);

                if (request.ManufacturerId.HasValue)
                    query = query.Where(x => x.ManufacturerId == request.ManufacturerId);

                response.Products = query.Where(x => x.IsDeleted == false).Select(x => new Models.Product()
                {
                    Id = x.Id,
                    CPC = x.ProductDefinition.CPC,
                    EngineeringCode = x.ProductDefinition.EngineeringCode,
                    CurrentEngineeringStatus = x.EngineeringStatus,
                    PreviousEngineeringStatus = x.PrevEngineeringStatus,
                    ManufacturerId = x.ManufacturerId,
                    ManufacturerName = x.Manufacturer.Name,
                    ManufacturerCode = x.ManufacturerCode,
                    ProductDefinitionId = x.ProductDefinitionId,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate
                });

            }
            catch
            {
                throw;
            }

            return response;
        }
    }
}