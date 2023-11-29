using AutoMapper;
using MediatR;
using QPL.DAL.EntityFramework.Context;

namespace QPL.BL.ManufacturerDomain
{
    public class ManufacturerExistsQuery : IRequest<ManufacturerExistsResponse>
    {
        public ManufacturerExistsQuery()
        {
            
        }
        public int Id { get; set; }
    }

    public class ManufacturerExistsResponse : BaseHandlerResponse
    {
        public bool Exists { get; set; }
    }
    public class ManufacturerExistsQueryHandler : IRequestHandler<ManufacturerExistsQuery, ManufacturerExistsResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public ManufacturerExistsQueryHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<ManufacturerExistsResponse> Handle(ManufacturerExistsQuery request, CancellationToken cancellationToken)
        {
            var response = new ManufacturerExistsResponse();
            try
            {
                response.Exists = _dataContext.Manufacturers.Any(x => x.Id == request.Id);
            }
            catch 
            {
                throw;
            }
            return response;
        }
    }
}
