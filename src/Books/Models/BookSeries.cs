using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Books.Models
{
    [PrimaryKey(nameof(IdBook), nameof(IdSeries))]
    public class BookSeries
    {
        [ForeignKey("Book")]
        public int IdBook { get; set; }

        [ForeignKey("Series")]
        public int IdSeries { get; set; }
    }
}
