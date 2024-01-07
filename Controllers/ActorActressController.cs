using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Application.ActorActressOperations.Commands.CreateActorActress;
using MovieStoreWebApi.Application.ActorActressOperations.Commands.UpdateActorActress;
using MovieStoreWebApi.Application.ActorActressOperations.Queries.GetActorActressDetail;
using MovieStoreWebApi.Application.ActorActressOperations.Queries.GetActorActress;
using MovieStoreWebApi.DBOperations;
using static MovieStoreWebApi.Application.ActorActressOperations.Commands.CreateActorActress.CreateActorActressCommand;
using static MovieStoreWebApi.Application.ActorActressOperations.Commands.UpdateActorActress.UpdateActorActressCommand;
using static MovieStoreWebApi.Application.ActorActressOperations.Queries.GetActorActressDetail.GetActorActressDetailQuery;
using MovieStoreWebApi.Application.ActorActressOperations.Commands.DeleteActorActress;

namespace MovieStoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class ActorActressController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public ActorActressController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet] // Tüm aktörleri getir
        public IActionResult GetActorActress()
        {
            GetActorActressQuery query = new GetActorActressQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")] // ID'ye göre aktör getir
        public IActionResult GetById(int id)
        {
            ActorActressDetailViewModel result;
            GetActorActressDetailQuery query = new GetActorActressDetailQuery(_context, _mapper);
            query.ActorActressId = id;
            GetActorActressDetailQueryValidator validator = new GetActorActressDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
            return Ok(result);
        }

        [HttpPost] // Aktör ekle
        public IActionResult AddActorActress([FromBody] CreateActorActressModel newActorActress)
        {
            CreateActorActressCommand command = new CreateActorActressCommand(_context, _mapper);
            command.Model = newActorActress;
            CreateActorActressCommandValidator validator = new CreateActorActressCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")] // Aktör güncelle
        public IActionResult UpdateActorActress(int id, [FromBody] UpdateActorActressModel updatedActorActress)
        {
            UpdateActorActressCommand command = new UpdateActorActressCommand(_mapper, _context);
            command.ActorActressId = id;
            command.Model = updatedActorActress;
            UpdateActorActressCommandValidator validator = new UpdateActorActressCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")] // Aktör silme
        public IActionResult DeleteActorActress(int id)
        {
            DeleteActorActressCommand command = new DeleteActorActressCommand(_context);
            command.ActorActressId = id;
            DeleteActorActressCommandValidator validator = new DeleteActorActressCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

    }
}
