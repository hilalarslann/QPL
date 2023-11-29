using AutoMapper;
using QPL.BL.ManufacturerDomain;
using QPL.DAL.Entities.Concrete;

namespace QPL.BL.Models
{
    public class ManufacturerProfile : Profile
    {
        public ManufacturerProfile()
        {
            CreateMap<DAL.Entities.Concrete.Manufacturer, Manufacturer>().ReverseMap();
            CreateMap<CreateManufacturerCommand, DAL.Entities.Concrete.Manufacturer>();
            CreateMap<UpdateManufacturerCommand, DAL.Entities.Concrete.Manufacturer>();
        }
    }
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}