using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WifiSD.Common.Attributes;
using WifiSD.Core.Application.Commands;
using WifiSD.Core.Application.Results;
using WifiSD.Core.Entities.Movies;
using WifiSD.Core.Repositories.Movies;

namespace WifiSD.Application.Movies
{
    [MappServiceDependency(nameof(MovieCommandHandler))]
    public class MovieCommandHandler : BaseCommandHandler, IRequestHandler<CreateMovieDtoCommand, MovieDto>,
                                                           IRequestHandler<UpdateMovieDtoCommand, MovieDto>,
                                                           IRequestHandler<DeleteMovieDtoCommand, bool>

    {
        private IMovieRepository movieRepository;


        public MovieCommandHandler(IServiceProvider serviceProvider)
        {
            this.movieRepository = serviceProvider.GetService<IMovieRepository>();
        }


        public Task<MovieDto> Handle(CreateMovieDtoCommand request, CancellationToken cancellationToken)
        {
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Name = "n/a",
                GenreId = 1,
                MediumTypeCode = "BR"

            };

            return Task.FromResult(MovieDto.MapFrom(movie));

        }

        public async Task<MovieDto> Handle(UpdateMovieDtoCommand request, CancellationToken cancellationToken)
        {
            /* ID im DTO mit ID aus Route überschreiben */
            request.MovieDto.Id = request.Id;

            var movie = await this.movieRepository.QueryFrom<Movie>().Where(w => w.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            var isNew = false;

            /* Insert*/
            if (movie == null)
            {
                movie = new Movie();
                isNew = true;
            }
            /* Update */

            base.MapEntityProperties<MovieDto, Movie>(request.MovieDto, movie);

            if (isNew)
            {
                await this.movieRepository.AddAsync(movie, true, cancellationToken);
            }
            else
            {
                await this.movieRepository.UpdateAsync(movie, movie.Id, true, cancellationToken);
            }

            return request.MovieDto;

        }

        public async Task<bool> Handle(DeleteMovieDtoCommand request, CancellationToken cancellationToken)
        {
            await movieRepository.RemoveByKeyAsync<Movie>(request.Id, true, cancellationToken);
            return true;
        }

        

    }
}
