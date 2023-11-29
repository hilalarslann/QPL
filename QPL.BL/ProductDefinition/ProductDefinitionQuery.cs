using MediatR;
using QPL.BL.Models;
using QPL.DAL.EntityFramework.Context;

namespace QPL.BL
{
    public class ProductDefinitionQuery : IRequest<ProductDefinitionResponse>
    {
        public ProductDefinitionQuery()
        {

        }
    }

    public class ProductDefinitionResponse : BaseHandlerResponse
    {
        public IQueryable<ProductDefinition> ProductDefinitions { get; set; } = new List<ProductDefinition>().AsQueryable();
    }

    public class ProductDefinitionQueryHandler : IRequestHandler<ProductDefinitionQuery, ProductDefinitionResponse>
    {
        private readonly DataContext _dataContext;
        public ProductDefinitionQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ProductDefinitionResponse> Handle(ProductDefinitionQuery request, CancellationToken cancellationToken)
        {
            var response = new ProductDefinitionResponse();

            try
            {
                response.ProductDefinitions = _dataContext.ProductDefinitions.Where(x => x.IsDeleted == false).Select(x => new ProductDefinition() { Id = x.Id, CPC = x.CPC, NortelCpc = x.NortelCpc, EngineeringCode = x.EngineeringCode, SpecNo = x.SpecNo, Description = x.Description, Comments = x.Comments, Concept = x.Concept, PackageType = x.PackageType, RoshStatus = x.RoshStatus, FlammabilityRating = x.FlammabilityRating, RadiassionRating = x.RadiassionRating, DisassembledOrReusable = x.DisassembledOrReusable, CreatedDate = x.CreatedDate, UpdatedDate = x.UpdatedDate });
            }
            catch
            {
                throw;
            }

            return response;
        }
    }
}