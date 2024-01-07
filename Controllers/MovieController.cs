using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using MovieStoreWebApi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStoreWebApi.Application.GenreOperations.Commands.DeleteGenre;
using MovieStoreWebApi.Application.GenreOperations.Commands.UpdateGenre;
using MovieStoreWebApi.Application.MovieOperations.Commands.CreateMovie;
using MovieStoreWebApi.Application.MovieOperations.Commands.DeleteMovie;
using MovieStoreWebApi.Application.MovieOperations.Commands.UpdateMovie;
using MovieStoreWebApi.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStoreWebApi.Application.MovieOperations.Queries.GetMovies;
using MovieStoreWebApi.DBOperations;
using static MovieStoreWebApi.Application.MovieOperations.Commands.CreateMovie.CreateMovieCommand;
using static MovieStoreWebApi.Application.MovieOperations.Commands.UpdateMovie.UpdateMovieCommand;
using static MovieStoreWebApi.Application.MovieOperations.Queries.GetMovieDetail.GetMovieDetailQuery;

namespace MovieStoreWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public MovieController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet] //Tüm filmleri getir
        public IActionResult GetMovies()
        {
            GetMoviesQuery query = new GetMoviesQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")] // ID'ye göre film getir
        public IActionResult GetById(int id)
        {
            MovieDetailViewModel result;
            GetMovieDetailQuery query = new GetMovieDetailQuery(_context, _mapper);
            query.MovieId = id;
            GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
            return Ok(result);
        }

        [HttpPost] // Film ekleme
        public IActionResult AddMovie([FromBody] CreateMovieViewModel newMovie)
        {
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            command.Model = newMovie;
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")] // Film güncelle
        public IActionResult UpdateMovie([FromBody] UpdateMovieViewModel updatedMovie, int id)
        {
            UpdateMovieCommand command = new UpdateMovieCommand(_context, _mapper);
            command.MovieId = id;
            command.Model = updatedMovie;
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpDelete] // Film silme
        public IActionResult DeleteMovie(int id)
        {
            DeleteMovieCommand command = new DeleteMovieCommand(_context, _mapper);
            command.MovieId = id;
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
