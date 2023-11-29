using AutoMapper;
using MediatR;
using QPL.DAL.Entities.Concrete;
using QPL.DAL.EntityFramework.Context;

namespace QPL.BL
{
    public class CreateProductDefinitionCommandProfile : Profile
    {
        public CreateProductDefinitionCommandProfile()
        {
            CreateMap<CreateProductDefinitionCommand, ProductDefinition>();
        }
    }


    public class CreateProductDefinitionCommand : IRequest<CreateProductDefinitionResponse>
    {
        public CreateProductDefinitionCommand() : this(string.Empty)
        {

        }
        public CreateProductDefinitionCommand(string CPC)
        {
            CPC = CPC;
        }
        public string CPC { get; set; }
        public string? NtCpc { get; set; }
        public string? EngCode { get; set; }
        public string? SpecNo { get; set; }
        public string? Description { get; set; }
        public string? Comments { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? TDate { get; set; }
        public string? Concept { get; set; }
        public string? PackageType { get; set; }
        public string? RoshStatus { get; set; }
        public string? FlammabilityRating { get; set; }
        public string? RadiassionRating { get; set; }
        public bool? DisassembledOrReusable { get; set; }
    }

    public class CreateProductDefinitionResponse : BaseHandlerResponse
    {
        public int Id { get; set; }
    }

    public class CreateProductDefCommandHandler : IRequestHandler<CreateProductDefinitionCommand, CreateProductDefinitionResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public CreateProductDefCommandHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<CreateProductDefinitionResponse> Handle(CreateProductDefinitionCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateProductDefinitionResponse();

            try
            {
                var CPC = _dataContext.ProductDefinitions.Any(x => x.CPC == request.CPC);
                if (CPC)
                {
                    response.Message = "Aynı Cpc koda sahip kayıt zaten mevcut!";
                    response.ResponseCode = HandlerResponseCode.BadRequest;
                    return response;
                }
                var main = _mapper.Map<ProductDefinition>(request);

                main.CreatedDate = DateTime.UtcNow.AddHours(3);
                main.UpdatedDate = DateTime.UtcNow.AddHours(3);

                _dataContext.ProductDefinitions.Add(main);
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