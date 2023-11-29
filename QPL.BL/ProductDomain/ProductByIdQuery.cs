using MediatR;
using Microsoft.EntityFrameworkCore;
using QPL.DAL.Entities.Concrete;
using QPL.DAL.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPL.BL.ProductDomain
{
    public class ProductByIdQuery : IRequest<ProductByIdResponse>
    {
        public ProductByIdQuery()
        {

        }
        public int Id { get; set; }
    }
    public class ProductByIdResponse : BaseHandlerResponse
    {
        public ProductItem product { get; set; } = new ProductItem();
        public class ProductItem
        {
            public int Id { get; set; }
            public int ManufacturerId { get; set; }
            public int ProductDefinitionId { get; set; }
            public string CPC { get; set; }
            public string? EngineeringStatus { get; set; }
            public string? ManufacturerCode { get; set; }
            public string PrevEngineeringStatus { get; set; }
            public DateTime? EntryDate { get; set; }
            public DateTime? ModifyDate { get; set; }
        }
    }
    public class ProductGetByIdQueryHandler : IRequestHandler<ProductByIdQuery, ProductByIdResponse>
    {
        private readonly DataContext _dataContext;
        public ProductGetByIdQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ProductByIdResponse> Handle(ProductByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ProductByIdResponse();

            try
            {
                var product = _dataContext.Products.FirstOrDefault(x => x.Id == request.Id);

                if (product != null)
                {
                    response.product.Id = product.Id;
                    response.product.PrevEngineeringStatus = product.PrevEngineeringStatus;
                    response.product.EngineeringStatus = product.EngineeringStatus;
                    response.product.ManufacturerCode = product.ManufacturerCode;
                    response.product.EntryDate = product.CreatedDate;
                    response.product.ModifyDate = product.UpdatedDate;
                    response.product.ManufacturerId = product.ManufacturerId;
                    response.product.ProductDefinitionId = product.ProductDefinitionId;

                    var product2 = _dataContext.Products
                    .Include(x => x.ProductDefinition)
                    .FirstOrDefault(x => x.Id == request.Id && x.ProductDefinitionId == x.ProductDefinition.Id);

                    response.product.CPC = product2?.ProductDefinition?.CPC;
                }
            }
            catch
            {
                throw;
            }

            return response;
        }
    }
}
