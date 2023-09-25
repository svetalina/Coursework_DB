using Books.Models;

namespace Books.Interfaces
{
    public interface ISeriesRepository : IRepository<Series>
    {
		IEnumerable<Series> GetAllGuest();

		IEnumerable<Series> GetByNameGuest(string name);
		IEnumerable<Series> GetByName(string name);
	}
}
