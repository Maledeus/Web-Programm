using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WifiSD.Core.Application.Results;

namespace WifiSD.Core.Application.Queries
{
    public class GetMovieDtosQuery : IRequest<IEnumerable<MovieDto>>
    {
        public int? GenreId { get; set; }
        public string MediumTypeCode { get; set; }

        public int Take { get; set; }
        public int Skip { get; set; }

    }
}
