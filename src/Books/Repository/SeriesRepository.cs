using Books.Interfaces;
using Books.Models;

namespace Books.Repository
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly AppDBContext _appDBContext;

        public SeriesRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void Add(Series model)
        {
            try
            {
                _appDBContext.Series.Add(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при добавлении серии");
            }
        }

        public void Update(Series model)
        {
            try
            {
                _appDBContext.Series.Update(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при обновлении серии");
            }
        }

        public void Delete(int id)
        {
            try
            {
                Series series = _appDBContext.Series.Find(id);

                if (series != null)
                {
                    _appDBContext.Series.Remove(series);
                    _appDBContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при удалении серии");
            }
        }

		public IEnumerable<Series> GetAllGuest()
		{
			return _appDBContext.Series
								.Select(series => new Series
								{
									Name = series.Name,
									Genre = series.Genre,
									Rating = series.Rating
								})
								.ToList();
		}

		public IEnumerable<Series> GetAll()
		{
			return _appDBContext.Series.ToList();
		}

		public Series GetById(int id)
        {
            return _appDBContext.Series.Find(id);
        }

		public IEnumerable<Series> GetByNameGuest(string name)
		{
			return _appDBContext.Series
								.Where(elem => elem.Name == name)
								.Select(series => new Series
								{
									Name = series.Name,
									Genre = series.Genre,
									Rating = series.Rating
								})
								.ToList();
		}

		public IEnumerable<Series> GetByName(string name)
        {
            return _appDBContext.Series.Where(elem => elem.Name == name).ToList();
        }
    }
}
