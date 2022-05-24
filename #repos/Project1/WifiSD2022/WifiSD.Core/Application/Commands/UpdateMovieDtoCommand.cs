using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using WifiSD.Core.Application.Results;

namespace WifiSD.Core.Application.Commands
{
    public class UpdateMovieDtoCommand : IRequest<MovieDto>

    {
        [FromRoute]
        public Guid Id { get; set; }

        [FromBody]
        public MovieDto MovieDto { get; set; }

    }
}
