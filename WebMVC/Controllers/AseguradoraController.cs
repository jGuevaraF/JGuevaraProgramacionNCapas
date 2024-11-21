using ML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class AseguradoraController : Controller
    {

        public ActionResult GetAll()
        {
            using(var httpclient = new HttpClient())
            {
                string endPoint = " https://api.themoviedb.org/3/movie/popular";
                httpclient.BaseAddress = new Uri(endPoint);
                httpclient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0ZmE1MGQ1NmU1ZDVmNGNhYTBjZDdkYTUyODVmODJkNSIsIm5iZiI6MTcyNDc5NjUxMS4xMjk0MDQsInN1YiI6IjY1MWIzOWNhOWQ1OTJjMDEwMmMwZDM3ZiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.6Xw564qQ3oI-VgGEZHMtG3hKPTUU-3mRUt5u7S70pfk");
                var response = httpclient.GetAsync("");

                response.Wait();

                var resultServicio = response.Result;


                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsAsync<ML.TheMovieDB>();
                    readTask.Wait();

                }

            }
            return View();
        }












        // GET: Aseguradora
        [HttpGet]
        public ActionResult GetAllWCF()
        {
            //ML.Result result = BL.Aseguradora.GetAll();
            ServiceReferenceAseguradora.ServiceAseguradoraClient getAll = new ServiceReferenceAseguradora.ServiceAseguradoraClient();

            var result = getAll.GetAll();

            ML.Aseguradora aseguradora = new ML.Aseguradora();

            if (result.Correct)
            {
                aseguradora.Aseguradoras = result.Objects.ToList();
                return View(aseguradora);
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetAll45()
        {

            ML.Aseguradora aseguradora = new ML.Aseguradora();

            ML.Result result = GetAllWebAPI();

            if (result.Correct)
            {
                aseguradora.Aseguradoras = result.Objects;
                return View(aseguradora);
            }

            return View();
        } //REST


        public ML.Result GetAllWebAPI()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["URLAseguradora"].ToString();
                    client.BaseAddress = new Uri(url);

                    var responseTaks = client.GetAsync("aseguradora");
                    responseTaks.Wait();

                    var resultServicio = responseTaks.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        result.Objects = new List<object>();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Aseguradora resultados = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Aseguradora>(resultItem.ToString());
                            result.Objects.Add(resultados);
                        }

                        result.Correct = true;


                    }




                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        [HttpGet]
        public ActionResult FormWCF (int? idUsuario) {

            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.Usuario = new ML.Usuario();

            ML.Usuario usuario = new ML.Usuario();

            //ML.Result todosUsuarios = BL.Usuario.GetAllEF(usuario);
            ServiceReferenceUsuario.ServiceUsuarioClient getAll = new ServiceReferenceUsuario.ServiceUsuarioClient();
            var todosUsuarios = getAll.GetAll(usuario);

            if (idUsuario == null)//agregar
            {
                aseguradora.Usuario.Usuarios = todosUsuarios.Objects.ToList();
                

            } else //actualizar
            {
                //ML.Result resultUpdate = BL.Aseguradora.GetById(idUsuario.Value);
                ServiceReferenceAseguradora.ServiceAseguradoraClient getById = new ServiceReferenceAseguradora.ServiceAseguradoraClient();
                var resultGetById = getById.GetById(idUsuario.Value);

                aseguradora = (ML.Aseguradora)resultGetById.Object;
                aseguradora.Usuario.Usuarios = todosUsuarios.Objects.ToList();
            }
            return View(aseguradora);
        }


        [HttpGet]
        public ActionResult Form(int? idUsuario)
        {

            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.Usuario = new ML.Usuario();

            ML.Usuario usuario = new ML.Usuario();

            //ML.Result todosUsuarios = BL.Usuario.GetAllEF(usuario);
            ServiceReferenceUsuario.ServiceUsuarioClient getAll = new ServiceReferenceUsuario.ServiceUsuarioClient();
            var todosUsuarios = getAll.GetAll(usuario);

            if (idUsuario == null)//agregar
            {
                aseguradora.Usuario.Usuarios = todosUsuarios.Objects.ToList();


            }
            else //actualizar
            {
                //ML.Result resultUpdate = BL.Aseguradora.GetById(idUsuario.Value);
                ServiceReferenceAseguradora.ServiceAseguradoraClient getById = new ServiceReferenceAseguradora.ServiceAseguradoraClient();

                var resultGetById = getById.GetById(idUsuario.Value);

                aseguradora = (ML.Aseguradora)resultGetById.Object;
                aseguradora.Usuario.Usuarios = todosUsuarios.Objects.ToList();
            }
            return View(aseguradora);
        }




        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            ServiceReferenceAseguradora.ServiceAseguradoraClient aseguradoraWCF = new ServiceReferenceAseguradora.ServiceAseguradoraClient();

            if (aseguradora.IdAseguradora == 0)
            {
                //ML.Result resultAdd = BL.Aseguradora.Add(aseguradora);
                
                var resultAdd = aseguradoraWCF.Add(aseguradora);

                if (resultAdd.Correct)
                {
                    ViewBag.Message = "EL usuario se dio de alta correctamente";
                }
                else
                {
                    ViewBag.Message = "ERROR" + resultAdd.ErrorMessage;
                }
            } else
            {
                //ML.Result resultUpdate = BL.Aseguradora.Update(aseguradora);
                var resultUpdate = aseguradoraWCF.Update(aseguradora);
                if (resultUpdate.Correct)
                {
                    ViewBag.Message = "EL usuario se actualizó correctamente";
                }
                else
                {
                    ViewBag.Message = "ERROR" + resultUpdate.ErrorMessage;
                }
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int idAseguradora) {
            ServiceReferenceAseguradora.ServiceAseguradoraClient delete = new ServiceReferenceAseguradora.ServiceAseguradoraClient();

            //ML.Result resultDelete = BL.Aseguradora.Delete(idAseguradora);
            var resultDelete = delete.Delete(idAseguradora);

            if (resultDelete.Correct)
            {

                ViewBag.Message = "El usuario se eliminó correctamente";
            }
            else
            {
                ViewBag.Message = "ERROR";
            }
            return PartialView("Modal");
        }

    }
}