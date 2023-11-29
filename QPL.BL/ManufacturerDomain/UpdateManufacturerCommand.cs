using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QPL.BL;
using QPL.BL.ManufacturerDomain;
using QPL.DAL.Entities.Concrete;
using QPL.DAL.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPL.BL.ManufacturerDomain
{
    public class UpdateManufacturerCommand : IRequest<UpdateManufacturerResponse>
    {
        public UpdateManufacturerCommand() : this(int.MinValue, string.Empty, string.Empty)
        {

        }
        public UpdateManufacturerCommand(int id, string name, string code)
        {
            Id = id;
            Name = name;
            Code = code;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class UpdateManufacturerResponse : BaseHandlerResponse
    {

    }

    public class UpdateManufacturerCommandHandler : IRequestHandler<UpdateManufacturerCommand, UpdateManufacturerResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public UpdateManufacturerCommandHandler(DataContext dataContext, IMapper mapper, IMediator mediator)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<UpdateManufacturerResponse> Handle(UpdateManufacturerCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateManufacturerResponse();
            try
            {
                var res = await _mediator.Send(new ManufacturerExistsQuery() { Id = request.Id });

                if (res.ResponseCode != HandlerResponseCode.Success)
                {
                    return (UpdateManufacturerResponse)(BaseHandlerResponse)res;
                }
                else if (res.Exists == false)
                {
                    response.Message = "Manufacturer is not found.";
                    response.ResponseCode = HandlerResponseCode.BadRequest;
                    return response;
                }

                _dataContext.Manufacturers
                    .Where(x => x.Id == request.Id)
                    .ExecuteUpdate(c =>
                        c
                        .SetProperty(x => x.Name, request.Name)
                        .SetProperty(x => x.Code, request.Code)
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
