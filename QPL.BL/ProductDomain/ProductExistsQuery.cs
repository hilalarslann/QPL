using AutoMapper;
using MediatR;
using QPL.DAL.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPL.BL.ProductDomain
{
    public class ProductExistsQuery : IRequest<ProductExistsResponse>
    {
        public ProductExistsQuery()
        {
            
        }
        public int Id { get; set; }
    }
    public class ProductExistsResponse : BaseHandlerResponse
    {
        public bool Exists { get; set; }
    }
    public class ProductExistsQueryHandler : IRequestHandler<ProductExistsQuery, ProductExistsResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public ProductExistsQueryHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ProductExistsResponse> Handle(ProductExistsQuery request, CancellationToken cancellationToken)
        {
            var response = new ProductExistsResponse();

            try
            {
                response.Exists = (_dataContext.Products.Any(x => x.Id == request.Id));
            }
            catch
            {
                throw;
            }

            return response;
        }
    }
}
