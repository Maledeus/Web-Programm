using Microsoft.AspNetCore.Mvc.Rendering;
using WifiSD.Core.Application.Results;

namespace WifiSD.WebAPP.Models
{
    public class MovieModel
    {
        public MovieDto MovieDto { get; set; }
        public SelectList Genres { get; set; }
        public SelectList MediumTypes { get; set; }
        public SelectList Ratings { get; set; }
    }
}
