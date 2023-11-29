using MediatR;
using Microsoft.EntityFrameworkCore;
using QPL.DAL.EntityFramework.Context;

namespace QPL.BL.ProductDomain
{
    public class DeleteProductCommand : IRequest<DeleteProductResponse>
    {
        public DeleteProductCommand() : this(int.MinValue)
        {

        }
        public DeleteProductCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class DeleteProductResponse : BaseHandlerResponse
    {

    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMediator _mediator;
        public DeleteProductCommandHandler(DataContext dataContext, IMediator mediator)
        {
            _dataContext = dataContext;
            _mediator = mediator;
        }
        public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteProductResponse();
            try
            {
                var res = await _mediator.Send(new ProductExistsQuery() { Id = request.Id });

                if (res.ResponseCode != HandlerResponseCode.Success)
                {
                    return (DeleteProductResponse)(BaseHandlerResponse)res;
                }
                else if (res.Exists == false)
                {
                    response.Message = "Product is not found.";
                    response.ResponseCode = HandlerResponseCode.BadRequest;
                    return response;
                }

                _dataContext.Products
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
