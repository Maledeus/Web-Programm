using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WifiSD.Resources;

namespace WifiSD.Core.Entities.Movies
{
    public abstract class MovieBase
    {
        /*[Key] Nicht notwendig, da konform */
        public Guid Id { get; set; }

        [Display(Name = nameof(Name), ResourceType = typeof(BasicRes))]
        [MaxLength(256)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Display(Name = nameof(Price), ResourceType = typeof(BasicRes))]
        public decimal Price { get; set; } = 0M;

        [Display(Name = nameof(ReleaseDate), ResourceType = typeof(BasicRes))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime ReleaseDate { get; set; }

        public int GenreId { get; set; }
        
        [MaxLength(8)]
        public string MediumTypeCode { get; set; }

    }
}
