using System.ComponentModel.DataAnnotations;

namespace AnseremTest.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Organization contact person")]
        public string Name { get; set; }

        [Display(Name = "Responsible for the sale")]
        public string ResponsibleForSale { get; set; }
    }
}
