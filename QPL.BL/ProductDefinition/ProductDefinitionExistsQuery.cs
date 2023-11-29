using AutoMapper;
using MediatR;
using QPL.DAL.EntityFramework.Context;

namespace QPL.BL
{
    public class ProductDefinitionExistsQuery : IRequest<ProductDefinitionExistsResponse>
    {
        public ProductDefinitionExistsQuery()
        {
        }
        public int Id { get; set; }
    }

    public class ProductDefinitionExistsResponse : BaseHandlerResponse
    {
        public bool Exists { get; set; }
    }

    public class ProductDefinitionExistsQueryHandler : IRequestHandler<ProductDefinitionExistsQuery, ProductDefinitionExistsResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public ProductDefinitionExistsQueryHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ProductDefinitionExistsResponse> Handle(ProductDefinitionExistsQuery request, CancellationToken cancellationToken)
        {
            var response = new ProductDefinitionExistsResponse();

            try
            {
                response.Exists = (_dataContext.ProductDefinitions.Any(x => x.Id == request.Id));
            }
            catch
            {
                throw;
            }

            return response;
        }
    }
}
