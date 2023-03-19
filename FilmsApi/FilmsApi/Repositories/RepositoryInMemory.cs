using FilmsApi.Entities; 

namespace FilmsApi.Repositories
{
    public class RepositoryInMemory : IRepository
    {
        private List<Genre> _genres;

        public RepositoryInMemory(ILogger<RepositoryInMemory> logger)
        {
            _genres = new List<Genre>()
            {
                new Genre(){Id = 1, Name = "Comedy"},
                new Genre(){Id = 2, Name = "Action"}
            };

            _guid = Guid.NewGuid(); // 123456-DFKSLDF-SLK3M4324-DSADALKSKDM
        }
        public Guid _guid;

        public List<Genre> GetAllGenres()
        {
            return _genres;
        }

        public async Task<Genre> GetById(int Id)
        {
            await Task.Delay(1);

            return _genres.FirstOrDefault(x => x.Id == Id);
        }

        public Guid GetGUID()
        {
            return _guid;
        }

        public void CreateGenre(Genre genero)
        {
            genero.Id = _genres.Count() + 1;
            _genres.Add(genero);
        }
    }
}
