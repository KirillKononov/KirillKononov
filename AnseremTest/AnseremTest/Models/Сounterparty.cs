using System.ComponentModel.DataAnnotations;

namespace AnseremTest.Models
{
    public class Counterparty
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Client organization")]
        public string Name { get; set; }

        public int? CityId { get; set; }
        public virtual City City { get; set; }
    }
}
