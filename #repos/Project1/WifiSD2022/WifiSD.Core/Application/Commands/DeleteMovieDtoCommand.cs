using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using WifiSD.Core.Application.Results;

namespace WifiSD.Core.Application.Commands
{
    public class DeleteMovieDtoCommand : IRequest<bool>

    {
        [FromRoute]
        public Guid Id { get; set; }

    }
}
