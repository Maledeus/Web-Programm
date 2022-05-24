using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using WifiSD.Resources;
using WifiSD.Resources.Attributes;

namespace WifiSD.Core.Entities.Movies
{
    public enum Ratings : byte
    {
        [LocalizedDescription(nameof(BasicRes.Rating_0))]
        Unrated = 0,

        [LocalizedDescription(nameof(BasicRes.Rating_1))]
        Bad = 1,

        [LocalizedDescription(nameof(BasicRes.Rating_3))]
        Medium = 3,

        [LocalizedDescription(nameof(BasicRes.Rating_5))]
        Great = 5
    }

    public partial class Movie : MovieBase,IEntity
    {
        DateTime CreateDate { get; set; } = DateTime.Now;
        DateTime? ModifyDate { get; set; }

        [ForeignKey(nameof(MovieBase.GenreId))]
        public Genre Genre { get; set; }

        //[JsonIgnore]
        [ForeignKey(nameof(MediumTypeCode))]
        public MediumType MediumType { get; set; } 

        public Ratings Rating { get; set; }
    }
}
