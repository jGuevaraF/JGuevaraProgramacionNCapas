using ML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace WebMVC.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        //sin WCF
        public ActionResult GetAllsinWCF()
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result resultGetAll = BL.Empleado.GetAll(empleado);
            ML.Result resultGetAllEmpresas = BL.Empresa.GetAll();

            if (resultGetAll.Correct)
            {
                empleado.Empleados = resultGetAll.Objects;
                //empleado.Empresa = new ML.Empresa();
                empleado.Empresa.Empresas = resultGetAllEmpresas.Objects;
                return View(empleado);
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult GetAllconWCF()//WCF
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            //ML.Result resultGetAll = BL.Empleado.GetAll(empleado);
            ServiceReferenceEmpleado.ServiceEmpleadoClient resultGetAll = new ServiceReferenceEmpleado.ServiceEmpleadoClient(); //creamos un objeto hacia nuestro servicio web, ahora en vez de tener un BL tenemos un servicio web

            var resultadoGetAll = resultGetAll.GetAll(empleado);//guardamos el resultado del return que tiene el servicio

            ML.Result resultGetAllEmpresas = BL.Empresa.GetAll(); //pasamos la lista de las empresas

            if (resultadoGetAll.Correct)
            {
                empleado.Empleados = resultadoGetAll.Objects.ToList(); //le pasamos los resultados obtenidos en el servicio, hacia la lista de Empleados
                //empleado.Empresa = new ML.Empresa();
                empleado.Empresa.Empresas = resultGetAllEmpresas.Objects;
                return View(empleado);
            }
            else
            {
                return View();
            }

}

        [HttpGet]
        public ActionResult GetAll()//REST
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result result = new ML.Result();
            ML.Result resultGetAllEmpresas = BL.Empresa.GetAll();
            result.Objects = new List<object>();

            using (var getAll = new HttpClient())
            {
                string URLapi = ConfigurationManager.AppSettings["URLapi"];

                getAll.BaseAddress = new Uri(URLapi); //crea la Uri de la API REST
                empleado.Nombre = (empleado.Nombre == null) ? " " : empleado.Nombre; //no acepta null, por lo que se condiciona que si viene nulo, lo cambie por un espacio
                var responseTaks = getAll.GetAsync("empleado/"+ empleado.Nombre + '/' + empleado.Empresa.IdEmpresa); //TE esta tomando una doble /
                //Le indica que la peticion es de tipo GET y de Empleado
                responseTaks.Wait(); //se ejecuta la logica de la api

                var resultGetAll = responseTaks.Result; //se guarda el Result del servicio web en la variable

                empleado.Nombre = empleado.Nombre.Replace(" ", ""); //le quito el espacio

                if (resultGetAll.IsSuccessStatusCode)
                {
                    var leerEmpleados = resultGetAll.Content.ReadAsAsync<ML.Result>(); //va a guardar lo que trae en leerEmpleados
                    leerEmpleados.Wait();

                    foreach(var consulta in leerEmpleados.Result.Objects) //accedemos a cada elemento de Objects para asignarlo a un objeto de tipo empleado
                    {
                        ML.Empleado resultados = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(consulta.ToString()); //deserealiza el objeto de Objects y lo pasa a un objeto de tipo empleado
                        result.Objects.Add(resultados);
                    }

                    empleado.Empleados = result.Objects;
                    empleado.Empresa.Empresas = resultGetAllEmpresas.Objects;
                    return View(empleado);
                }

            }

            return View();
        }

        [HttpPost]
        public ActionResult GetAllWCF(ML.Empleado buscarEmp) //busqueda abierta
        {
            //ML.Result resultGetAll = BL.Empleado.GetAll(buscarEmp);

            //WCF
            ServiceReferenceEmpleado.ServiceEmpleadoClient resultBusqueda = new ServiceReferenceEmpleado.ServiceEmpleadoClient();
            var resultados = resultBusqueda.GetAll(buscarEmp);


            ML.Result resultGetAllEmpresas = BL.Empresa.GetAll();
            buscarEmp = new ML.Empleado();
            buscarEmp.Empresa = new ML.Empresa();

            buscarEmp.Empleados = resultados.Objects.ToList();
            buscarEmp.Empresa.Empresas = resultGetAllEmpresas.Objects;
            return View(buscarEmp) ;
        }

        [HttpPost]
        public ActionResult GetAll(ML.Empleado buscarEmp)//REST
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result result = new ML.Result();
            ML.Result resultGetAllEmpresas = BL.Empresa.GetAll();
            result.Objects = new List<object>();

            using (var getAll = new HttpClient())
            {
                string URLapi = ConfigurationManager.AppSettings["URLapi"];

                getAll.BaseAddress = new Uri(URLapi); //crea la Uri de la API REST
                buscarEmp.Nombre = (buscarEmp.Nombre == null) ? " " : buscarEmp.Nombre; //no acepta null, por lo que se condiciona que si viene nulo, lo cambie por un espacio
                var responseTaks = getAll.GetAsync("empleado/"+buscarEmp.Nombre+"/"+buscarEmp.Empresa.IdEmpresa); //Le indica que la peticion es de tipo GET y de Empleado
                responseTaks.Wait(); //se ejecuta la logica de la api

                var resultGetAll = responseTaks.Result; //se guarda el Result del servicio web en la variable
                buscarEmp.Nombre = buscarEmp.Nombre.Replace(" ", ""); //le quito el espacio
                if (resultGetAll.IsSuccessStatusCode)
                {
                    var leerEmpleados = resultGetAll.Content.ReadAsAsync<ML.Result>(); //va a guardar lo que trae en leerEmpleados
                    leerEmpleados.Wait();

                    foreach (var consulta in leerEmpleados.Result.Objects) //accedemos a cada elemento de Objects para asignarlo a un objeto de tipo empleado
                    {
                        ML.Empleado resultados = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(consulta.ToString()); //deserealiza el objeto de Objects y lo pasa a un objeto de tipo empleado
                        result.Objects.Add(resultados);
                    }

                    empleado.Empleados = result.Objects;
                    empleado.Empresa.Empresas = resultGetAllEmpresas.Objects;
                    return View(empleado);
                }

            }

            return View();
        }

        [HttpGet]
        public ActionResult FormWCF(string numeroEmpleado) 
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();

            ML.Result resultEmpresa = BL.Empresa.GetAll();
            

            if (numeroEmpleado != null) //update
            {
                //    ML.Result resultById = BL.Empleado.GetById(numeroEmpleado);
                //    empleado = (ML.Empleado)resultById.Object;
                //    empleado.Empresa.Empresas = resultEmpresa.Objects;
                //    empleado.Accion = "Update";

                ServiceReferenceEmpleado.ServiceEmpleadoClient getById = new ServiceReferenceEmpleado.ServiceEmpleadoClient();
                var result = getById.GetById(numeroEmpleado);
                empleado = (ML.Empleado)result.Object;
                empleado.Empresa.Empresas = resultEmpresa.Objects;
                empleado.Accion = "Update";

            } else //ADD
            {
                empleado.Empresa.Empresas = resultEmpresa.Objects;
                empleado.Accion = "Add";
            }

            return View(empleado);
        }

        [HttpGet]
        public ActionResult Form(string numeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();

            ML.Result resultEmpresa = BL.Empresa.GetAll();


            if (numeroEmpleado != null) //update
            {
                
                using (var empleadoGetById = new HttpClient())
                {
                    string URLapi = ConfigurationManager.AppSettings["URLapi"];

                    empleadoGetById.BaseAddress = new Uri(URLapi); //crea la Uri de la API REST

                    var responseTaks = empleadoGetById.GetAsync("empleado/" + numeroEmpleado); //ejecuta el comando Add de nuestro servicio
                    responseTaks.Wait(); //se ejecuta la logica de la api

                    var resultGetById = responseTaks.Result; //se guarda el Result del servicio web en la variable

                    if (resultGetById.IsSuccessStatusCode)
                    {
                        var readTask = resultGetById.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Empleado resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(readTask.Result.Object.ToString());
                        empleado = resultItemList;
                        empleado.Accion = "Update";
                        empleado.Empresa.Empresas = resultEmpresa.Objects;
                    }

                }

            }
            else //ADD
            {
                empleado.Empresa.Empresas = resultEmpresa.Objects;
                empleado.Accion = "Add";
            }

            return View(empleado);
        }



        [HttpPost]
        public ActionResult FormWCF(ML.Empleado empleado)
        {
            if(ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["Imagen"];

                if (file.ContentLength > 0)
                {
                    empleado.Foto = ConvertirABase64(file);
                }

                //ML.Result buscarNumeroEmpleado = BL.Empleado.GetById(empleado.NumeroEmpleado); no es lo optimo, pues ocupamos recursos innecesarios
                //en este caso, como sabemos en que momento se hace un ADD o un Update, podemos manejar una bandera que controle la situacion

                if(empleado.Accion.Equals("Add")) //agregar
                {
                    //ML.Result resultAdd = BL.Empleado.Add(empleado); BL
                    ServiceReferenceEmpleado.ServiceEmpleadoClient addEmpleado = new ServiceReferenceEmpleado.ServiceEmpleadoClient();
                    var resultAdd = addEmpleado.Add(empleado);

                    if (resultAdd.Correct)
                    {
                        ViewBag.Message = "El empleado se dio de alta correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "ERROR " + resultAdd.ErrorMessage;
                    }
                } 
                else //actualizar
                {
                    //ML.Result resultUpdate = BL.Empleado.Update(empleado);
                    ServiceReferenceEmpleado.ServiceEmpleadoClient updateEmpleado = new ServiceReferenceEmpleado.ServiceEmpleadoClient();
                    var resultUpdate = updateEmpleado.Update(empleado);

                    if (resultUpdate.Correct)
                    {
                        ViewBag.Message = "El empleado se actualizó correctamente";
                    }
                }
                return PartialView("Modal");

            } else //ModelState false
            {
                ML.Result resultEmpresa = BL.Empresa.GetAll();
                empleado.Empresa.Empresas = resultEmpresa.Objects;
                return View(empleado);
            }

        }

        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["Imagen"];

                if (file.ContentLength > 0)
                {
                    empleado.Foto = ConvertirABase64(file);
                }

                if (empleado.Accion.Equals("Add")) //agregar
                {
                    using(var empleadoAdd = new HttpClient()){
                        string URLapi = ConfigurationManager.AppSettings["URLapi"];

                        empleadoAdd.BaseAddress = new Uri(URLapi); //crea la Uri de la API REST

                        var responseTaks = empleadoAdd.PostAsJsonAsync<ML.Empleado>("empleado", empleado); //ejecuta el comando Add de nuestro servicio
                        responseTaks.Wait(); //se ejecuta la logica de la api

                        var resultAdd = responseTaks.Result; //se guarda el Result del servicio web en la variable

                        if (resultAdd.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "El empleado se dio de alta correctamente";
                        }
                    }

                    
                }
                else //actualizar
                {
                    using (var empleadoUpdate = new HttpClient())
                    {
                        string URLapi = ConfigurationManager.AppSettings["URLapi"];
                        empleadoUpdate.BaseAddress = new Uri(URLapi);

                        var responseTaks = empleadoUpdate.PutAsJsonAsync<ML.Empleado>("empleado/" + empleado.NumeroEmpleado, empleado); //ejecuta el comando Add de nuestro servicio
                        responseTaks.Wait(); //se ejecuta la logica de la api


                        var resultUpdate = responseTaks.Result;

                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "El empleado se actualizó correctamente";
                        }
                    }
                       
                }
                return PartialView("Modal");

            }
            else //ModelState false
            {
                ML.Result resultEmpresa = BL.Empresa.GetAll();
                empleado.Empresa.Empresas = resultEmpresa.Objects;
                return View(empleado);
            }

        }


        public ActionResult DeleteWCF(string numeroEmpleado)
        {
            //ML.Result resultDelete = BL.Empleado.Delete(numeroEmpleado);
            ServiceReferenceEmpleado.ServiceEmpleadoClient deleteEmpleado = new ServiceReferenceEmpleado.ServiceEmpleadoClient();
            var resultDelete = deleteEmpleado.Delete(numeroEmpleado);

            if (resultDelete.Correct)
            {
                ViewBag.Message = "El Empleado se ha borrado correctamente.";
            } else
            {
                ViewBag.Message = resultDelete.ErrorMessage;
            }

            return PartialView("Modal");
        }

        public ActionResult Delete(string numeroEmpleado)//REST
        {
            using (var deleteEmpleado = new HttpClient())
            {
                string URLapi = ConfigurationManager.AppSettings["URLapi"];
                deleteEmpleado.BaseAddress = new Uri(URLapi);

                var respuesta = deleteEmpleado.DeleteAsync("empleado/"+numeroEmpleado);
                var resultDelete = respuesta.Result;
                if (resultDelete.IsSuccessStatusCode)
                {
                    ViewBag.Message = "El Empleado se ha borrado correctamente.";
                }
            }

            return PartialView("Modal");
        }


        public string ConvertirABase64(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            string imagen = Convert.ToBase64String(data);
            return imagen;
        }
    }
}