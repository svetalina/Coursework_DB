using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class Series
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public double Rating { get; set; }
    }
}
