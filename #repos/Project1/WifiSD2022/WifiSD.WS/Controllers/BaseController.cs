using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;

namespace WifiSD.WS.Controllers
{
    public class BaseController : Controller
    {
        private IMediator mediator;

        protected IMediator Mediator => this.mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected IMediator Mediator1
        {
            get
            {
                if (this.mediator == null)
                {
                    return HttpContext.RequestServices.GetService<IMediator>();
                }
                else
                {
                    return this.mediator;
                }
            }
        }

        protected T SetLocationURI<T>(T result, string id)
        {
            if (result == null|| string.IsNullOrWhiteSpace(id))
            {
                throw new HttpRequestException("Resource is null");
            }

            /* Aktueller Url ermitteln */
            var baseUrl = Request.HttpContext.Request.GetEncodedUrl();

            /*Base Url bis zum ersten Parameter, falls vorhanden, kürzen */
            var length = baseUrl.IndexOf('?') > 0 ? baseUrl.IndexOf('?') : baseUrl.Length;
            var uri = baseUrl.Substring(0, length);

            /* ID an den gekürtzten URL anhängen => URI der neuen Ressource */
            uri = string.Concat(uri.EndsWith("/") ? string.Empty : "/", id);

            /* Location header hinzufügen */
            HttpContext.Response.Headers.Add("Location", uri);
            /* Http-Status Code 201 - Created setzen */
            HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

            return result;

        }


    }
}
