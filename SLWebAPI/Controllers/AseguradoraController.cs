using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SLWebAPI.Controllers
{
    [RoutePrefix("api/aseguradora")]
    public class AseguradoraController : ApiController
    {
        [Route("")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]ML.Aseguradora aseguradora)
        {
            ML.Result result = BL.Aseguradora.Add(aseguradora);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            } else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Aseguradora.GetAll();
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("{idAseguradora}")]
        [HttpGet]
        public IHttpActionResult GetAll(int idAseguradora)
        {
            ML.Result result = BL.Aseguradora.GetById(idAseguradora);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("{idAseguradora}")]
        [HttpDelete]
        public IHttpActionResult Delete(int idAseguradora)
        {
            ML.Result result = BL.Aseguradora.Delete(idAseguradora);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("{idAseguradora}")]
        [HttpPut]
        public IHttpActionResult Delete(int idAseguradora, [FromBody]ML.Aseguradora aseguradora)
        {
            aseguradora.IdAseguradora = idAseguradora;

            ML.Result result = BL.Aseguradora.Update(aseguradora);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
    }
}
