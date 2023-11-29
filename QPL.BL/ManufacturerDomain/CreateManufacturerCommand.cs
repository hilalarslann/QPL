using AutoMapper;
using MediatR;
using QPL.DAL.EntityFramework.Context;
using QPL.DAL.Entities.Concrete;


namespace QPL.BL.ManufacturerDomain
{
    public class CreateManufacturerCommand : IRequest<CreateManufacturerResponse>
    {
        public CreateManufacturerCommand() : this(string.Empty, string.Empty)
        {

        }
        public CreateManufacturerCommand(string name, string code)
        {
            Name = name;
            Code = code;
        }
        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class CreateManufacturerResponse : BaseHandlerResponse
    {
        public int Id { get; set; }
    }

    public class CreateManufacturerCommandHandler : IRequestHandler<CreateManufacturerCommand, CreateManufacturerResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public CreateManufacturerCommandHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<CreateManufacturerResponse> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateManufacturerResponse();
            try
            {

                var main = _mapper.Map<Manufacturer>(request);
                var manufacturerCheck = _dataContext.Manufacturers.Any(x => x.Name == main.Name || x.Code == main.Code);
                if (manufacturerCheck)
                {
                    response.Message = main.Name + " ismine veya " + main.Code + " koduna ait kayıt zaten mevcut!";
                    response.ResponseCode = HandlerResponseCode.BadRequest;
                    return response;
                }

                main.CreatedDate = DateTime.UtcNow.AddHours(3);
                main.UpdatedDate = DateTime.UtcNow.AddHours(3);

                _dataContext.Manufacturers.Add(main);
                _dataContext.SaveChanges();
                
                response.Id = main.Id;
            }
            catch 
            {

                throw;
            }
            return response;
        }

    }
}
