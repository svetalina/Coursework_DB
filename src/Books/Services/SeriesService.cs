using Books.Interfaces;
using Books.Models;
using Books.Repository;
using Books.ViewModels;

namespace Books.Services
{
    public class SeriesService : ISeriesService
    {

		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ISeriesRepository _seriesRepository;

        public SeriesService(IHttpContextAccessor httpContextAccessor,
                             ISeriesRepository seriesRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _seriesRepository = seriesRepository;
        }


        private bool IsExist(Series series)
        {
            return _seriesRepository.GetAll().FirstOrDefault(elem =>
                    elem.Name == series.Name) != null;
        }

        private bool IsNotExist(int id)
        {
            return _seriesRepository.GetById(id) == null;
        }


        public void Add(Series series)
        {
            if (IsExist(series))
                throw new Exception("Такая пользователь уже существует");

            _seriesRepository.Add(series);
        }

        public void Update(Series series)
        {
            if (IsNotExist(series.Id))
                throw new Exception("Такой пользователь не существует");

            _seriesRepository.Update(series);
        }

        public void Delete(Series series)
        {
            if (IsNotExist(series.Id))
                throw new Exception("Такой пользователь не существует");

            _seriesRepository.Delete(series.Id);
        }


        public IEnumerable<Series> GetAll()
        {
			var curUser = _httpContextAccessor.HttpContext.User;

			if (!curUser.Identity.IsAuthenticated)
				return _seriesRepository.GetAllGuest();

			return _seriesRepository.GetAll();
		}

        public Series GetById(int id)
        {
            return _seriesRepository.GetById(id);
        }

        public IEnumerable<Series> GetByName(string name)
        {
			var curUser = _httpContextAccessor.HttpContext.User;

			if (!curUser.Identity.IsAuthenticated)
				return _seriesRepository.GetByNameGuest(name);

			return _seriesRepository.GetByName(name);
		}

		public IEnumerable<Series> GetByParameters(Series parameters,
												SearchFlag yearFlag,
												SearchFlag ratingFlag)
		{
			IEnumerable<Series> series;

			var curUser = _httpContextAccessor.HttpContext.User;

			if (!curUser.Identity.IsAuthenticated)
				series = _seriesRepository.GetAllGuest();
			else
				series = _seriesRepository.GetAll();

			if (series.Count() != 0 && parameters.Name != null)
				series = series.Where(elem => elem.Name == parameters.Name);

			if (series.Count() != 0 && parameters.Genre != null)
				series = series.Where(elem => elem.Genre == parameters.Genre);

			if (series.Count() != 0 && parameters.Publisher != null)
				series = series.Where(elem => elem.Publisher == parameters.Publisher);

			if (series.Count() != 0 && parameters.Year != 0)
			{
				if (yearFlag == SearchFlag.Equal)
					series = series.Where(elem => elem.Year == parameters.Year);
				if (yearFlag == SearchFlag.More)
					series = series.Where(elem => elem.Year >= parameters.Year);
				if (yearFlag == SearchFlag.Less)
					series = series.Where(elem => elem.Year <= parameters.Year);
			}

			if (series.Count() != 0 && parameters.Rating != 0)
			{
				if (ratingFlag == SearchFlag.Equal)
					series = series.Where(elem => elem.Rating == parameters.Rating);
				if (ratingFlag == SearchFlag.More)
					series = series.Where(elem => elem.Rating >= parameters.Rating);
				if (ratingFlag == SearchFlag.Less)
					series = series.Where(elem => elem.Rating <= parameters.Rating);
			}

			return series;
		}
    }
}
