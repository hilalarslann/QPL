using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QPL.BL.ManufacturerDomain;
using QPL.DAL.Entities.Concrete;
using QPL.DAL.EntityFramework.Context;

namespace QPL.BL
{
    public class UpdateProductDefinitionCommandProfile : Profile
    {
        public UpdateProductDefinitionCommandProfile()
        {
            CreateMap<UpdateProductDefinitionCommand, ProductDefinition>();
        }
    }


    public class UpdateProductDefinitionCommand : IRequest<UpdateProductDefinitionResponse>
    {
        public UpdateProductDefinitionCommand()
        {

        }
        public int Id { get; set; }
        public string CPC { get; set; }
        public string NortelCpc { get; set; }
        public string EngineeringCode { get; set; }
        public string SpecNo { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Concept { get; set; }
        public string PackageType { get; set; }
        public string RoshStatus { get; set; }
        public string FlammabilityRating { get; set; }
        public string RadiassionRating { get; set; }
        public bool DisassembledOrReusable { get; set; }
    }

    public class UpdateProductDefinitionResponse : BaseHandlerResponse
    {
    }

    public class UpdateProductDefinitionCommandHandler : IRequestHandler<UpdateProductDefinitionCommand, UpdateProductDefinitionResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IMediator _mediator;
        public UpdateProductDefinitionCommandHandler(DataContext dataContext, IMediator mediator)
        {
            _dataContext = dataContext;
            _mediator = mediator;
        }

        public async Task<UpdateProductDefinitionResponse> Handle(UpdateProductDefinitionCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateProductDefinitionResponse();

            try
            {
                var res = await _mediator.Send(new ProductDefinitionExistsQuery() { Id = request.Id });

                if (res.ResponseCode != HandlerResponseCode.Success)
                {
                    return (UpdateProductDefinitionResponse)(BaseHandlerResponse)res;
                }

                else if (res.Exists == false)
                {
                    response.Message = "ProductDefinition is not found.";
                    response.ResponseCode = HandlerResponseCode.BadRequest;
                    return response;
                }
                _dataContext.ProductDefinitions
                    .Where(x => x.Id == request.Id)
                    .ExecuteUpdate(c =>
                        c
                        .SetProperty(x => x.CPC, request.CPC)
                        .SetProperty(x => x.NortelCpc, request.NortelCpc)
                        .SetProperty(x => x.EngineeringCode, request.EngineeringCode)
                        .SetProperty(x => x.SpecNo, request.SpecNo)
                        .SetProperty(x => x.Description, request.Description)
                        .SetProperty(x => x.Comments, request.Comments)
                        .SetProperty(x => x.Concept, request.Concept)
                        .SetProperty(x => x.PackageType, request.PackageType)
                        .SetProperty(x => x.RoshStatus, request.RoshStatus)
                        .SetProperty(x => x.FlammabilityRating, request.FlammabilityRating)
                        .SetProperty(x => x.RadiassionRating, request.RadiassionRating)
                        .SetProperty(x => x.DisassembledOrReusable, request.DisassembledOrReusable)
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