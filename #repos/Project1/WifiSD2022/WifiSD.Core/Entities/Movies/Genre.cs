using System;
using System.Collections.Generic;
using System.Text;

namespace WifiSD.Core.Entities.Movies
{
    public partial class Genre : IEntity
    {
        public Genre()
        {
            this.Movies = new HashSet<Movie>();
        }

        public Int32 Id { get; set; }
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; }
        
    }
}
