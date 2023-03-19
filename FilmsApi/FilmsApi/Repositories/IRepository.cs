using FilmsApi.Entities; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Repositories
{
    public interface IRepository
    {
        void CreateGenre(Genre genero);
        Guid GetGUID();
        Task<Genre> GetById(int Id);
        List<Genre> GetAllGenres();
    }
}
