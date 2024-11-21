using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace SLWebAPI.Controllers
{
    [RoutePrefix("api/empleado")] //como en todas las peticiones se repite esta parte de la URL, la colocamos de manera global, quiero decir que todos los metodos que esten dentro de esta clase, se les quedara esta ruta de manera automática
    public class EmpleadoController : ApiController
    {
        [Route("")] //se le asigna la ruta de la api, esta vacia porque ya tenemos la ruta base en el RoutePrefix, y como no vamos a enviar ningun parametro, esta se queda vacia
        [HttpPost] //como es un add, es de tipo post
        public IHttpActionResult Add (ML.Empleado empleado) //es de tipo IHttpActionResult porque regresaremos un resultado de HTTP
        {
            ML.Result result = BL.Empleado.Add (empleado); //ejecutamos la logica

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result); //regresamos el estado HTTP OK, indicando que todo salio bien, y regresamos el result para que se pueda ver en el resultado del JSON
            } else
            {
                return Content(HttpStatusCode.BadRequest, result); //si algo salio mal, regresamos el error 400, que es parte del Cliente, pudo haber enviado un valor erroneo
            }


        }

        [Route("{numeroEmpleado}")]//especificamos que vamos a recibir un parametro por HEAD, y lo vamos a recibir como numeroEmpleado. Esta es la forma correcta de que a la hora de actualizar o eliminar, se envie el ID del registro que vamos a actualizar o eliminar
        [HttpPut] //metodo PUT, por actualizar un valor existente
        public IHttpActionResult Update(string numeroEmpleado, [FromBody] ML.Empleado empleado) //en nuestro metodo, vamos a recibir 2 parametros. 1 lo recibimos por el HEAD y el otro lo recibimos por el BODY. Siempre en el BODY es donde se envia la informacion con mas peso y se le especifica con el decorador [FromBody]
        {
            empleado.NumeroEmpleado = numeroEmpleado; //como el numeroEmpleado lo estamos recibiendo aparte, lo tenemos que asignar a nuestro objeto, ya que en nuestro BL cuando hacemos un UPDATE buscamos el numeroEmpleado en el modelo
            ML.Result result = BL.Empleado.Update (empleado); //ejecutamos la logica de negocio
            
            if(result.Correct)
            {
                return Content(HttpStatusCode.OK, result); //regresamos el estado OK, y el contenido de result
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result); //regresamos el error 400, parte del cliente, pudo haber un error a la hora de recibir la informacio
            }
        }

        [Route("{numeroEmpleado}")]
        [HttpDelete]
        public IHttpActionResult Delete (string numeroEmpleado)
        {
            ML.Result result = BL.Empleado.Delete(numeroEmpleado);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result); //regresamos el estado OK, y el contenido de result
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result); //regresamos el error 400, parte del cliente, pudo haber un error a la hora de recibir la informacio
            }
        }


        [Route("{nombre?}/{idEmpresa?}")]
        [HttpGet]
        public IHttpActionResult GetAll(string nombre, int idEmpresa)
        {

            ML.Empleado empleado = new ML.Empleado();
            empleado.Nombre = nombre;

            empleado.Empresa = new ML.Empresa();
            empleado.Empresa.IdEmpresa = idEmpresa;
            ML.Result result = BL.Empleado.GetAll(empleado);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result); //regresamos el estado OK, y el contenido de result
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result); //regresamos el error 400, parte del cliente, pudo haber un error a la hora de recibir la informacio
            }
        }

        [Route("{numeroEmpleado}")]
        [HttpGet]
        public IHttpActionResult GetById(string numeroEmpleado)
        {
            ML.Result result = BL.Empleado.GetById(numeroEmpleado);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result); //regresamos el estado OK, y el contenido de result
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result); //regresamos el error 400, parte del cliente, pudo haber un error a la hora de recibir la informacio
            }
        }

    }
}
