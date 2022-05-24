using System;
using System.Collections.Generic;
using System.Text;
using WifiSD.Common.Attributes;
using WifiSD.Core.Repositories.Movies;
using WifiSD.Persistence.Repositories.Base;

namespace WifiSD.Persistence.Repositories.Movies
{
    [MappServiceDependency(nameof(GenreRepository))]
    public class GenreRepository : BaseRepository, IGenreRepository
    {


    }
}
