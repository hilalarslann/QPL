using AutoMapper;
using MediatR;
using QPL.DAL.Entities.Concrete;
using QPL.DAL.EntityFramework.Context;


namespace QPL.BL
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<TestCommand, ProductDefinition>();
        }
    }


    public class TestCommand : IRequest<TestResponse>
    {
        public TestCommand(ProductDefinition productDefinition) 
        {
            pDef = productDefinition;
        }
        public ProductDefinition pDef;
    }

    public class TestResponse : BaseHandlerResponse
    {
        public int Id { get; set; }
    }

    public class TestHandler : IRequestHandler<TestCommand, TestResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public TestHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<TestResponse> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            var response = new TestResponse();

            try
            {
                var CPC = _dataContext.ProductDefinitions.Any(x => x.CPC == request.pDef.CPC);
                if (CPC)
                {
                    response.Message = "Aynı Cpc koda sahip kayıt zaten mevcut!";
                    response.ResponseCode = HandlerResponseCode.BadRequest;
                    return response;
                }

                var main = _mapper.Map<ProductDefinition>(request.pDef);

                //main.CreatedDate = DateTime.UtcNow.AddHours(3);
                //main.UpdatedDate = DateTime.UtcNow.AddHours(3);

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
