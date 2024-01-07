﻿using MovieStoreWebApi.DBOperations;

namespace MovieStoreWebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }

        private readonly IMovieStoreDbContext _dbContext;
        public DeleteGenreCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre == null)
            {
                throw new InvalidOperationException("Böyle bir janra bulunamadı");
            }

            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
        }


    }
}
