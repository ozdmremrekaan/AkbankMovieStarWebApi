using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebApi.Application.CustomerOperations.Commands.CreateToken;
using MovieStoreWebApi.Application.CustomerOperations.Commands.RefreshToken;
using MovieStoreWebApi.DBOperations;
using MovieStoreWebApi.TokenOperations.Models;
using static MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer.CreateCustomerCommand;
using static MovieStoreWebApi.Application.CustomerOperations.Commands.CreateToken.CreateTokenCommand;

namespace MovieStoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class CustomerController :ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;
        public CustomerController(IMovieStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CreateCustomerModel newCustomer)
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
            command.Model = newCustomer;
            command.Handle();

            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_dbContext, _mapper,_configuration);
            command.Model = login;
            var token = command.Handle();
            return token;
        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_dbContext, _configuration);
            command.RefreshToken = token;
            var resultToken = command.Handle();
            return resultToken;
        }
        


    }
}
