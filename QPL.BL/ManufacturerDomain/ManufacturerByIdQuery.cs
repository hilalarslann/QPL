using AutoMapper;
using MediatR;
using QPL.BL.Models;
using QPL.DAL.EntityFramework.Context;

namespace QPL.BL.ManufacturerDomain
{
    public class ManufacturerByIdQuery : IRequest<ManufacturerByIdResponse>
    {
        public ManufacturerByIdQuery() : this(int.MinValue)
        {

        }
        public ManufacturerByIdQuery(int id) 
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class ManufacturerByIdResponse : BaseHandlerResponse
    {
        public Manufacturer? Manufacturer { get; set; }
    }
    public class ManufacturerByIdQueryHandler : IRequestHandler<ManufacturerByIdQuery, ManufacturerByIdResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public ManufacturerByIdQueryHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<ManufacturerByIdResponse> Handle(ManufacturerByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ManufacturerByIdResponse();
            try
            {
                response.Manufacturer = _mapper.Map<Manufacturer>(_dataContext.Manufacturers.FirstOrDefault(x => x.Id == request.Id));
            }
            catch 
            {
                throw;
            }
            return response;
        }
    }
}
