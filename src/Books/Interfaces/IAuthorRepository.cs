using Books.Models;

namespace Books.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
		IEnumerable<Author> GetAllGuest();

		IEnumerable<Author> GetByNameGuest(string name);
		IEnumerable<Author> GetByName(string name);
	}
}
