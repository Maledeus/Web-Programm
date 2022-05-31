using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WifiSD.Core.Entities.Movies;

namespace WifiSD.Core.Application.Queries
{
    public class GetGenresQuery : IRequest<IEnumerable<Genre>>
    {
    }
}
