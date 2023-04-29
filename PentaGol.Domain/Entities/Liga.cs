using PentaGol.Domain.Commons;

#pragma warning disable
namespace PentaGol.Domain.Entities
{
    public class Liga : Auditable
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}
