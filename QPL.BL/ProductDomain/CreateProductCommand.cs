using AutoMapper;
using MediatR;
using QPL.DAL.Entities.Concrete;
using QPL.DAL.EntityFramework.Context;

namespace QPL.BL.ProductDomain
{
    public class CreateProductCommandProfile : Profile
    {
        public CreateProductCommandProfile()
        {
            CreateMap<CreateProductCommand, Product>();
        }
    }
    public class CreateProductCommand : IRequest<CreateProductResponse>
    {
        public CreateProductCommand() : this(string.Empty)
        {
            
        }
        public int ProductDefinitionId { get; set; }
        public CreateProductCommand(string CPC)
        {
           
        }
        public int ManufacturerId { get; set; }
        public string? EngineeringStatus { get; set; }
        public string? ManufacturerCode { get; set; }
        public string? PrevEngineeringStatus { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }

    public class CreateProductResponse : BaseHandlerResponse
    {

    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateProductResponse();

            try
            {

                var product = _mapper.Map<Product>(request);

                product.PrevEngineeringStatus = request.EngineeringStatus;
                product.IsActive = true;
                product.CreatedDate = DateTime.UtcNow.AddHours(3);
                product.UpdatedDate = DateTime.UtcNow.AddHours(3);
                _dataContext.Products.Add(product);
                _dataContext.SaveChanges();
            }
            catch
            {
                throw;
            }

            return response;
        }
    }
}
