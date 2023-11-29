using AutoMapper;
using MediatR;
using QPL.BL.Models;
using QPL.DAL.EntityFramework.Context;

namespace QPL.BL
{
    public class ProductDefinitionByIdQuery : IRequest<ProductDefinitionByIdResponse>
    {
        public ProductDefinitionByIdQuery() : this(int.MinValue)
        {

        }
        public ProductDefinitionByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class ProductDefinitionByIdResponse : BaseHandlerResponse
    {
        public ProductDefinition? ProductDefinition { get; set; }
    }

    public class ProductDefinitionByIdQueryHandler : IRequestHandler<ProductDefinitionByIdQuery, ProductDefinitionByIdResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public ProductDefinitionByIdQueryHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ProductDefinitionByIdResponse> Handle(ProductDefinitionByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ProductDefinitionByIdResponse();

            try
            {
                response.ProductDefinition = _mapper.Map<ProductDefinition>(_dataContext.ProductDefinitions.FirstOrDefault(x => x.Id == request.Id));
            }
            catch
            {
                throw;
            }

            return response;
        }
    }
}