using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QPL.BL.ManufacturerDomain;
using QPL.DAL.Entities.Concrete;
using QPL.DAL.EntityFramework.Context;
using System.Net.Mail;

namespace QPL.BL.ProductDomain
{
    public class UpdateProductCommand : IRequest<UpdateProductResponse>
    {
        public UpdateProductCommand() 
        {

        }
        public int Id { get; set; }
        public int? ManufacturerId { get; set; }
        public string? EngineeringStatus { get; set; }
        public string? ManufacturerCode { get; set; }
    }

    public class UpdateProductResponse : BaseHandlerResponse
    {

    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMediator _mediator;
        public UpdateProductCommandHandler(DataContext dataContext, IMediator mediator)
        {
            _dataContext = dataContext;
            _mediator = mediator;
        }
        public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateProductResponse();

            try
            {
                var res = await _mediator.Send(new ProductByIdQuery() { Id = request.Id });

                if (res.ResponseCode != HandlerResponseCode.Success)
                {
                    return (UpdateProductResponse)(BaseHandlerResponse)res;
                }
                else if (res == null)
                {
                    response.Message = "Product is not found.";
                    response.ResponseCode = HandlerResponseCode.BadRequest;
                    return response;
                }

                if (res.product.EngineeringStatus != request.EngineeringStatus)
                {
                    res.product.PrevEngineeringStatus = res.product.EngineeringStatus;
                }

                _dataContext.Products
                .Where(x => x.Id == request.Id)
                .ExecuteUpdate(c =>
                    c
                    .SetProperty(x => x.EngineeringStatus, request.EngineeringStatus)
                    .SetProperty(x => x.PrevEngineeringStatus, res.product.PrevEngineeringStatus)
                    .SetProperty(x => x.ManufacturerCode, request.ManufacturerCode)
                    .SetProperty(x => x.ManufacturerId, request.ManufacturerId)
                    .SetProperty(x => x.UpdatedDate, DateTime.UtcNow.AddHours(3))
                   );
            }
            catch
            {
                throw;
            }

            return response;
        }
    }
}
