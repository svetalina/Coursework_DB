using Books.Models;
using Books.ViewModels;

namespace Books.Interfaces
{
    public interface ISeriesService : IService<Series>
    {
        IEnumerable<Series> GetByName(string name);

        IEnumerable<Series> GetByParameters(Series parameters,
                                            SearchFlag yearFlag,
                                            SearchFlag ratingFlag);
    }
}
