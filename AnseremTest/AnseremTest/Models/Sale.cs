using System.ComponentModel.DataAnnotations;

namespace AnseremTest.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Sale's name")]
        [Required]
        public string Name { get; set; }

        public int? СounterpartyId { get; set; }
        public virtual Counterparty Сounterparty { get; set; }

        public int? ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
