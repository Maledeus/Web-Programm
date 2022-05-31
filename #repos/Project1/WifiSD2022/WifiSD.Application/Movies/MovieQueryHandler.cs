using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiSD.Common.Attributes;
using WifiSD.Common.Extensions.SD.Common.Extensions;
using WifiSD.Core.Application.Queries;
using WifiSD.Core.Application.Results;
using WifiSD.Core.Entities.Movies;
using WifiSD.Core.Repositories.Movies;

namespace WifiSD.Application.Movies
{
    [MappServiceDependency(nameof(MovieQueryHandler))]
    public class MovieQueryHandler : IRequestHandler<GetMovieDtosQuery, IEnumerable<MovieDto>>,
                                     IRequestHandler<GetMovieDtoQuery, MovieDto>,
                                     IRequestHandler<GetGenresQuery, IEnumerable<Genre>>,
                                     IRequestHandler<GetMediumTypesQuery, IEnumerable<MediumType>>
    {
        private readonly IMovieRepository movieRepository;

        public MovieQueryHandler(IServiceProvider serviceProvider)
        {
            this.movieRepository = serviceProvider.GetRequiredService<IMovieRepository>();
        }

        public async Task<MovieDto> Handle(GetMovieDtoQuery query, CancellationToken cancellationToken)
        {
            var result = await this.GetMovieQueryComplete()
                                   .Where(w => w.Id == query.Id)
                                   .Select(s => MovieDto.MapFrom(s)).FirstOrDefaultAsync(cancellationToken);

            //var result = await this.movieRepository.QueryFrom<Movie>(w => w.Id == query.Id)
            //                                       .Select(s => MovieDto.MapFrom(s)).FirstOrDefaultAsync(cancellationToken);

            result.LocalizedRating = result.Rating.GetDescription();

            return result;
        }

        private IQueryable<Movie> GetMovieQueryComplete()
        {
            return this.movieRepository.QueryFrom<Movie>()
                                       .Include(nameof(Genre))
                                       .Include(nameof(MediumType));
        }

        public async Task<IEnumerable<MovieDto>> Handle(GetMovieDtosQuery query, CancellationToken cancellationToken)
        {
            //var movieQuery = this.movieRepository.QueryFrom<Movie>();

            var movieQuery = this.GetMovieQueryComplete();

            if (!string.IsNullOrWhiteSpace(query.MediumTypeCode))
            {
                movieQuery = movieQuery.Where(w => w.MediumTypeCode == query.MediumTypeCode);
            }

            if (query.GenreId.HasValue)
            {
                movieQuery = movieQuery.Where(w => w.GenreId == query.GenreId.Value);
            }

            if (query.Take > 0)
            {
                movieQuery = movieQuery.Skip(query.Skip).Take(query.Take); /* Take / Skip für die Pagination */
            }

            var result = await movieQuery.Select(s => MovieDto.MapFrom(s)).ToListAsync (cancellationToken); 
            result.ForEach(r =>
            {
                r.LocalizedRating = r.Rating.GetDescription();
            }
             );

            return result;

            
        }

        public async Task<IEnumerable<Genre>> Handle(GetGenresQuery query, CancellationToken cancellationToken)
        {
            var result = await this.movieRepository.QueryFrom<Genre>().ToListAsync(cancellationToken);
            return result;
        }

        public async Task<IEnumerable<MediumType>> Handle(GetMediumTypesQuery query, CancellationToken cancellationToken)
        {
            var result = await this.movieRepository.QueryFrom<MediumType>().ToListAsync(cancellationToken);
            return result;
        }


    }

    
}
