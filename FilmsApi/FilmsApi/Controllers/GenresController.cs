
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using FilmsApi.Entities;
using FilmsApi.Filters;
using FilmsApi.Repositories; 

namespace FilmsApi.Controllers
{
    [Route("api/Genres")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenresController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly WeatherForecastController weatherForecastController;
        private readonly ILogger<GenresController> logger;

        public GenresController(IRepository repository,
            WeatherForecastController weatherForecastController,
            ILogger<GenresController> logger)
        {
            this.repository = repository;
            this.weatherForecastController = weatherForecastController;
            this.logger = logger;
        }

        [HttpGet] // api/Genres
        [HttpGet("list")] // api/Genres/list
        [HttpGet("/listGenres")] // /listGenres
        [ResponseCache(Duration = 60)]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult<List<Genre>> Get()
        {
            logger.LogInformation("Let's show the genres");
            return repository.GetAllGenres();
        }

        [HttpGet("guid")] // api/Genres/guid
        public ActionResult<Guid> GetGUID()
        {
            return Ok(new
            {
                GUID_GenresController = repository.GetGUID(),
                GUID_WeatherForecastController = weatherForecastController.GetGUIDWeatherForecastController()
            });
        }


        [HttpGet("{Id:int}")] // api/Genres/3/felipe
        public async Task<ActionResult<Genre>> Get(int Id, [FromHeader] string name)
        {

            logger.LogDebug($"Getting a gender by id {Id}");

            var Genre = await repository.GetById(Id);

            if (Genre == null)
            {
                throw new ApplicationException($"The gender of ID {Id} it was not found");
                logger.LogWarning($"We couldn't find the gender of id {Id}");
                return NotFound();
            }

            //return "felipe";
            //return Ok("felipe");
            //return Ok(DateTime.Now);
            return Genre;
        }

    
        [HttpPost("CreateGenre")]  
        public ActionResult Post([FromBody] Genre Genre)
        {
            repository.CreateGenre(Genre);
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put([FromBody] Genre Genre)
        {
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return NoContent();
        }

    }
}
