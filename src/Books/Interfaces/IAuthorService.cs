using Books.Models;
using Books.ViewModels;

namespace Books.Interfaces
{
    public interface IAuthorService : IService<Author>
    {
        IEnumerable<Author> GetByName(string name);

        IEnumerable<Author> GetByParameters(Author parameters,
                                            SearchFlag yearBirthFlag = SearchFlag.Equal,
                                            SearchFlag yearDeathFlag = SearchFlag.Equal);
    }
}
