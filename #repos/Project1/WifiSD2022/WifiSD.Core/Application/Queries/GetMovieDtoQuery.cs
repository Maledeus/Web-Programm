using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WifiSD.Core.Application.Results;

namespace WifiSD.Core.Application.Queries
{
    public class GetMovieDtoQuery : IRequest<MovieDto>
    {
        public Guid Id { get; set; }
    }
}
