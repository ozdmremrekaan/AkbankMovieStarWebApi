using AutoMapper;
using MovieStoreWebApi.DBOperations;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.Application.ActorActressOperations.Commands.CreateActorActress
{
    public class CreateActorActressCommand
    {
        public CreateActorActressModel Model { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;


        public CreateActorActressCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var actorActress = _context.ActorActress.SingleOrDefault(x => x.Name == Model.Name);
            if (actorActress != null)
            {
                throw new InvalidOperationException("Böyle bir aktör zaten mevcut");
            }

            actorActress = _mapper.Map<ActorActress>(Model);


            //actorActress = new ActorActress();

            //actorActress.Name = Model.Name;
            //actorActress.Surname = Model.Surname;

            _context.ActorActress.Add(actorActress);
            _context.SaveChanges();
        }

        public class CreateActorActressModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }
}
