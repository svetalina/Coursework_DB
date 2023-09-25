using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Books.Models
{
    [PrimaryKey(nameof(IdBookshelf), nameof(IdBook))]
    public class BookshelfBook
    {

        [ForeignKey("Bookshelf")]
        public int IdBookshelf { get; set; }

        [ForeignKey("Book")]
        public int IdBook { get; set; } 
        
        public bool IsRead { get; set; }
    }
}
