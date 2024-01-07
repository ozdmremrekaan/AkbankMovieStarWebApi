﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.DBOperations;

namespace MovieStoreWebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public UpdateGenreViewModel Model { get; set; }
        public int GenreId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateGenreCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }


        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre == null)
            {
                throw new InvalidOperationException("Böyle bir janra bulunamadı");
            }

            _mapper.Map(Model,genre);
            _dbContext.SaveChanges();
        }


        public class UpdateGenreViewModel
        {
            public string GenreTitle { get; set; }
        }
    }
}
