using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Application.GenreOperations.Commands.CreateGenre;
using MovieStoreWebApi.Application.GenreOperations.Commands.DeleteGenre;
using MovieStoreWebApi.Application.GenreOperations.Commands.UpdateGenre;
using MovieStoreWebApi.Application.GenreOperations.Queries.GetGenreDetail;
using MovieStoreWebApi.Application.GenreOperations.Queries.GetGenres;
using MovieStoreWebApi.DBOperations;
using System.Net;
using static MovieStoreWebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static MovieStoreWebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;
using static MovieStoreWebApi.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;

namespace MovieStoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet] // Tüm janraları getir
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")] // ID'ye göre janra getir
        public IActionResult GetById(int id)
        {
            GenreDetailViewModel result;
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = id;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
            return Ok(result);
        }

        [HttpPost] // Janra ekleme
        public IActionResult AddGenre([FromBody] CreateGenreViewModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = newGenre;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Hande();
            return Ok();

        }

        [HttpDelete] // Janra silme
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")] // Janra güncelleme
        public IActionResult UpdateGenre([FromBody] UpdateGenreViewModel updatedGenre, int id)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);
            command.GenreId = id;
            command.Model = updatedGenre;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }




    }
}
