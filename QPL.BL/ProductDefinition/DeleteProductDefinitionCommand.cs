using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QPL.DAL.EntityFramework.Context;

namespace QPL.BL
{
    public class DeleteProductDefinitionCommand : IRequest<DeleteProductDefinitionResponse>
    {
        public DeleteProductDefinitionCommand() : this(int.MinValue)
        {

        }
        public DeleteProductDefinitionCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
    public class DeleteProductDefinitionResponse : BaseHandlerResponse
    {

    }

    public class DeleteProductDefinitionCommandHandler : IRequestHandler<DeleteProductDefinitionCommand, DeleteProductDefinitionResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public DeleteProductDefinitionCommandHandler(DataContext dataContext, IMapper mapper, IMediator mediator)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<DeleteProductDefinitionResponse> Handle(DeleteProductDefinitionCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteProductDefinitionResponse();
            try
            {
                var res = await _mediator.Send(new ProductDefinitionExistsQuery() { Id = request.Id });

                if (res.ResponseCode != HandlerResponseCode.Success)
                {
                    return (DeleteProductDefinitionResponse)(BaseHandlerResponse)res;
                }
                else if (res.Exists == false)
                {
                    response.Message = "Product Definition is not found.";
                    response.ResponseCode = HandlerResponseCode.BadRequest;
                    return response;
                }

                _dataContext.ProductDefinitions
                    .Where(x => x.Id == request.Id)
                    .ExecuteUpdate(x => x.SetProperty(c => c.IsDeleted, true));
            }
            catch
            {

                throw;
            }
            return response;
        }
    }
}
