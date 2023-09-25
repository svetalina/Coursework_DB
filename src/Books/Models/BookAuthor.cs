using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Books.Models
{
    [PrimaryKey(nameof(IdBook), nameof(IdAuthor))]
    public class BookAuthor
    {
        [ForeignKey("Book")]
        public int IdBook { get; set; }

        [ForeignKey("Author")]
        public int IdAuthor { get; set; }
    }
}
