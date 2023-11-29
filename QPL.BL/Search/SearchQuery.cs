using MediatR;
using Microsoft.EntityFrameworkCore;
using QPL.DAL.Entities.Concrete;
using QPL.DAL.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPL.BL.Search
{
    public class SearchQuery : IRequest<SearchResponse>
    {
        public SearchQuery()
        {
            SearchText = string.Empty;
        }
        public int? UserId { get; set; }
        public string SearchText { get; set; }        
    }

    public class SearchResponse : BaseHandlerResponse
    {
        public IQueryable<Items> products { get; set; }

        public class Items
        {
            public int Id { get; set; }
            public string CPC { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string ManufacturerName { get; set; } = string.Empty;
            public string ManufacturerCode { get; set; } = string.Empty;
            public string PrevEngineeringStatus { get; set; } = string.Empty;
            public string EngineeringStatus { get; set; } = string.Empty;
        }

    }

    public class SearchQueryHandler : IRequestHandler<SearchQuery, SearchResponse>
    {
        private readonly DataContext _dataContext;
        public SearchQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InsertOrUpdateUser(int userId, string activeQPLSearch)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(r => r.Id == userId);
            if (user == null)
                throw new Exception();

            user.ActiveQPLSearch = activeQPLSearch;
            _dataContext.Users.Entry(user).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }
        public async Task<SearchResponse> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            var response = new SearchResponse();
            try
            {
                await InsertOrUpdateUser(1, request.SearchText);
                var qplSearch = _dataContext.Users.FirstOrDefault(x => x.Id == 1)?.ActiveQPLSearch?.Split(';', StringSplitOptions.RemoveEmptyEntries);

               var rp = _dataContext.Products
                    .Include(x => x.ProductDefinition)
                    .Include(p => p.Manufacturer).AsQueryable();

                if (qplSearch?.Count() > 0)
                    rp = rp.Where(x => qplSearch.Contains(x.ProductDefinition.CPC));

                response.products = rp
                    .Select(r => new SearchResponse.Items
                    {
                        Id = r.ProductDefinitionId,
                        CPC = r.ProductDefinition.CPC,
                        Description = r.ProductDefinition.Description,
                        ManufacturerName = r.Manufacturer.Name,
                        ManufacturerCode = r.ManufacturerCode,
                        PrevEngineeringStatus = r.PrevEngineeringStatus,
                        EngineeringStatus = r.EngineeringStatus

                    }).AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }



    }
}



