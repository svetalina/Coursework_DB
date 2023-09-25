using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
    public class Bookshelf
    {
        [Key]
        public int Id { get; set; }

		[ForeignKey("User")]
		public int IdUser { get; set; }

        public int Number { get; set; }

        public double Rating { get; set; }
	}
}
