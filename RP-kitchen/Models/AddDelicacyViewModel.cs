using System.ComponentModel.DataAnnotations.Schema;

namespace RP_kitchen.Models
{
    public class AddDelicacyViewModel
    {
        

        public string Catagory { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
    }
}
