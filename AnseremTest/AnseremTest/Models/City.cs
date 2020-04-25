using System.ComponentModel.DataAnnotations;

namespace AnseremTest.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Client city")]
        public string Name { get; set; }
    }
}
