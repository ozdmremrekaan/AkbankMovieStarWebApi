﻿using FluentValidation;

namespace MovieStoreWebApi.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryValidator :AbstractValidator<GetMovieDetailQuery>
    {
        public GetMovieDetailQueryValidator()
        {
            RuleFor(query => query.MovieId).GreaterThan(0);
        }
    }
}
