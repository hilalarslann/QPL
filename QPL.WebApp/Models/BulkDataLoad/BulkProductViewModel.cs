
namespace QPL.WebApp.Models.BulkDataLoad
{
    public class BulkProductViewModel
    {
        public List<DAL.Entities.Concrete.ProductDefinition> succeedProductDefinition { get; set; }
        public List<DAL.Entities.Concrete.ProductDefinition> failedProductDefinitions { get; set; }
        public List<string> Messages = new List<string>();
        public string[] HeaderLine { get; set; }
    }
}
