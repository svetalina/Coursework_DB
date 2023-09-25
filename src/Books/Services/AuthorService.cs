using Books.Models;
using Books.Interfaces;
using Books.ViewModels;
using static System.Reflection.Metadata.BlobBuilder;

namespace Books.Services
{
    public class AuthorService : IAuthorService
    {
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IAuthorRepository _authorRepository;

        public AuthorService(IHttpContextAccessor httpContextAccessor,
                             IAuthorRepository authorRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorRepository = authorRepository;
        }


        private bool IsExist(Author author)
        {
            return _authorRepository.GetAll().FirstOrDefault(elem =>
                    elem.Name == author.Name) != null;
        }

        private bool IsNotExist(int id)
        {
            return _authorRepository.GetById(id) == null;
        }


        public void Add(Author author)
        {
            if (IsExist(author))
                throw new Exception("Такой автор уже существует");

            _authorRepository.Add(author);
        }

        public void Update(Author author)
        {
            if (IsNotExist(author.Id))
                throw new Exception("Такой автор не существует");

            _authorRepository.Update(author);
        }

        public void Delete(Author author)
        {
            if (IsNotExist(author.Id))
                throw new Exception("Такой автор не существует");

            _authorRepository.Delete(author.Id);
        }


        public IEnumerable<Author> GetAll()
        {
            return _authorRepository.GetAll();
        }

        public Author GetById(int id)
        {
            return _authorRepository.GetById(id);
        }


        public IEnumerable<Author> GetByName(string name)
        {
			var curUser = _httpContextAccessor.HttpContext.User;

			if (!curUser.Identity.IsAuthenticated)
				return _authorRepository.GetByNameGuest(name);
			
            return _authorRepository.GetByName(name);
        }


        public IEnumerable<Author> GetByParameters(Author parameters,
												   SearchFlag yearBirthFlag,
												   SearchFlag yearDeathFlag)
        {
            IEnumerable<Author> authors;

			var curUser = _httpContextAccessor.HttpContext.User;

			if (!curUser.Identity.IsAuthenticated)
				authors = _authorRepository.GetAllGuest();
            else
                authors = _authorRepository.GetAll();

            if (authors.Count() != 0 && parameters.Name != null)
                authors = authors.Where(elem => elem.Name == parameters.Name);

			if (authors.Count() != 0 && parameters.Genre != null)
				authors = authors.Where(elem => elem.Genre == parameters.Genre);

			if (authors.Count() != 0 && parameters.Country != null)
				authors = authors.Where(elem => elem.Country == parameters.Country);

			if (authors.Count() != 0 && parameters.YearBirth != 0)
			{
				if (yearBirthFlag == SearchFlag.Equal)
					authors = authors.Where(elem => elem.YearBirth == parameters.YearBirth);
				if (yearBirthFlag == SearchFlag.More)
					authors = authors.Where(elem => elem.YearBirth >= parameters.YearBirth);
				if (yearBirthFlag == SearchFlag.Less)
					authors = authors.Where(elem => elem.YearBirth <= parameters.YearBirth);
			}

			if (authors.Count() != 0 && parameters.YearDeath != null)
			{
				if (yearDeathFlag == SearchFlag.Equal)
					authors = authors.Where(elem => elem.YearDeath == parameters.YearDeath);
				if (yearDeathFlag == SearchFlag.More)
					authors = authors.Where(elem => elem.YearDeath >= parameters.YearDeath);
				if (yearDeathFlag == SearchFlag.Less)
					authors = authors.Where(elem => elem.YearDeath <= parameters.YearDeath);
			}

			return authors;
        }
    }
}
