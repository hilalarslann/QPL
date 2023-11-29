using MediatR;
using QPL.BL.Models;
using QPL.DAL.EntityFramework.Context;

namespace QPL.BL.ManufacturerDomain
{
    public class ManufacturerQuery : IRequest<ManufacturerQueryResponse>
    {
        public ManufacturerQuery()
        {
            
        }
    }

    public class ManufacturerQueryResponse : BaseHandlerResponse
    {
        public IQueryable<Manufacturer> Manufacturers { get; set; }
    }

    public class ManufacturerQueryHandler : IRequestHandler<ManufacturerQuery, ManufacturerQueryResponse>
    {
        private readonly DataContext _datacontext;
        public ManufacturerQueryHandler(DataContext datacontext)
        {
            _datacontext = datacontext;
        }

        public async Task<ManufacturerQueryResponse> Handle(ManufacturerQuery request, CancellationToken cancellationToken)
        {
            var response = new ManufacturerQueryResponse();
            try
            {
                response.Manufacturers = _datacontext.Manufacturers.Where(x => x.IsDeleted == false).Select(x => new Manufacturer() { Id = x.Id, Name = x.Name, Code = x.Code, CreatedDate = x.CreatedDate, UpdatedDate = x.UpdatedDate });
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
    }
}
