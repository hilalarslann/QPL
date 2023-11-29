using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QPL.DAL.EntityFramework.Context;

namespace QPL.BL.ManufacturerDomain
{
    public class DeleteManufacturerCommand : IRequest<DeleteManufacturerResponse>
    {
        public DeleteManufacturerCommand() : this(int.MinValue)
        {

        }
        public DeleteManufacturerCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class DeleteManufacturerResponse : BaseHandlerResponse
    {

    }

    public class DeleteManufacturerCommandHandler : IRequestHandler<DeleteManufacturerCommand, DeleteManufacturerResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public DeleteManufacturerCommandHandler(DataContext dataContext, IMapper mapper, IMediator mediator)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<DeleteManufacturerResponse> Handle(DeleteManufacturerCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteManufacturerResponse();
            try
            {
                var res = await _mediator.Send(new ManufacturerExistsQuery() { Id = request.Id });

                if (res.ResponseCode != HandlerResponseCode.Success)
                {
                    return (DeleteManufacturerResponse)(BaseHandlerResponse)res;
                }
                else if (res.Exists == false)
                {
                    response.Message = "Manufacturer is not found.";
                    response.ResponseCode = HandlerResponseCode.BadRequest;
                    return response;
                }

                _dataContext.Manufacturers
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
