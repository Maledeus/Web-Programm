using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WifiSD.Core.Entities.Movies
{
    public partial class MediumType : IEntity
    {
        public MediumType () 
        {
            this.Movies = new HashSet<Movie> ();
        }

        [Key]
        [MaxLength (8)]
        public string Code { get; set; }

        [Required]
        [MaxLength (32)]
        public string Name { get; set; }

        public ICollection<Movie> Movies { get;}
    }
}
