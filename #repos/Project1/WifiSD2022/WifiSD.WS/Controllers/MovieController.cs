using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WifiSD.Core.Application.Commands;
using WifiSD.Core.Application.Queries;
using WifiSD.Core.Application.Results;

namespace WifiSD.WS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : BaseController
    {
        /*
         * FromRiute: api/movie/movieDto/12345
         * FromQuery: api/movie/movieDto?genreId=1&mediumTypeCode=DVD
         */

        private const string ID_PARAMETER = " /{Id}";

        
        [HttpGet(nameof(MovieDto) + ID_PARAMETER)]
        public async Task<MovieDto> GetMovieDto([FromRoute]GetMovieDtoQuery query, CancellationToken cancellationToken)
        {
            return await this.Mediator.Send(query, cancellationToken);
        }

        [HttpGet(nameof(MovieDto))]
        public async Task<IEnumerable<MovieDto>> GetMovieDtos([FromQuery] GetMovieDtosQuery query, CancellationToken cancellationToken)
        {
            return await this.Mediator.Send(query, cancellationToken);
        
        }

        [ProducesResponseType(typeof(MovieDto), (int)HttpStatusCode.Created)]
        [HttpPost(nameof(MovieDto))]

        public async Task<MovieDto>CreateMovieDto(CancellationToken cancellationToken)
        {

            var command = new CreateMovieDtoCommand();
            var result = await base.Mediator.Send(command, cancellationToken);

            //Todo: Location header implementieren
            return base.SetLocationURI<MovieDto>(result, result.Id.ToString());
        }

        [HttpPut(nameof(MovieDto) + ID_PARAMETER)]
        public async Task<MovieDto> UpdateMovieDto([FromQuery]UpdateMovieDtoCommand command, CancellationToken cancellationToken)
        {
            return await base.Mediator.Send(command, cancellationToken);
        }

        [HttpDelete(nameof(MovieDto) + ID_PARAMETER)]
        public async Task<bool> DeleteMovieDto([FromQuery] DeleteMovieDtoCommand command, CancellationToken cancellationToken)
        {
            return await base.Mediator.Send(command, cancellationToken);
        }

    }
}
