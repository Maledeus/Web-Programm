using System;
using System.Collections.Generic;
using System.Text;
using WifiSD.Core.Entities.Movies;

namespace WifiSD.Core.Application.Results
{
    public class MovieDto : MovieBase
    {
        public string GenreName { get; set; }
        public string MediumTypeName { get; set; }

        public Ratings Rating { get; set; }

        public string LocalizedRating { get; set; }

        public static MovieDto MapFrom(Movie movie)
        {
            return new MovieDto
            {
                Id = movie.Id,
                Name = movie.Name,
                MediumTypeCode = movie.MediumTypeCode,
                Price = movie.Price,
                ReleaseDate = movie.ReleaseDate,
                GenreId = movie.GenreId,
                Rating = movie.Rating,

                GenreName = movie?.Genre?.Name,
                MediumTypeName = movie.MediumType?.Name
                

            };
        }

    }
}
