using MediatR;
using QPL.DAL.Entities.Concrete;
using QPL.DAL.EntityFramework.Context;

namespace QPL.BL.ValidateDomain
{
    public class ValidateProductDefinitionQuery : IRequest<ValidateProductDefinitionResponse>
    {
        public List<ProductDefinition> _succeedProductDefinition = new List<ProductDefinition>();
        public List<ProductDefinition> _failedProductDefinitions = new List<ProductDefinition>();
        public List<string> Messages = new List<string>();
        public ValidateProductDefinitionQuery()
        {

        }
        public ValidateProductDefinitionQuery(ProductDefinition productDefinition)
        {
            this.productDefinition = productDefinition;
        }
        public ProductDefinition productDefinition { get; set; }
    }

    public class ValidateProductDefinitionResponse : BaseHandlerResponse
    {
        public List<ProductDefinition> succeedProductDefinition;
        public List<ProductDefinition> failedProductDefinitions;
        public List<string> Messages;
    }

    public class ValidateProductDefinitionQueryHandler : IRequestHandler<ValidateProductDefinitionQuery, ValidateProductDefinitionResponse>
    {
        private readonly DataContext _dataContext;
        public ValidateProductDefinitionQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ValidateProductDefinitionResponse> Handle(ValidateProductDefinitionQuery request, CancellationToken cancellationToken)
        {
            var response = new ValidateProductDefinitionResponse();
            response.succeedProductDefinition = request._succeedProductDefinition;
            response.failedProductDefinitions = request._failedProductDefinitions;
            response.Messages = request.Messages;

            var CPC = _dataContext.ProductDefinitions.Any(x => x.CPC == request.productDefinition.CPC);

            var CPCExcel = response.succeedProductDefinition?.Any(x => x.CPC == request.productDefinition.CPC) ?? false;

            if (CPC || CPCExcel)
            {
                response.failedProductDefinitions.Add(request.productDefinition);
                response.Messages.Add(request.productDefinition.CPC + " koda sahip kayıt zaten mevcut!");
                response.ResponseCode = HandlerResponseCode.BadRequest;

                return response;
            }
            else
            {
                response.succeedProductDefinition.Add(request.productDefinition);
                response.ResponseCode = HandlerResponseCode.Success;
            }

            return response;
        }
    }
}
