using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace PLMVC.Controllers
{
    public class CargaMasivaController : Controller
    {
        // GET: CargaMasiva
        public ActionResult Cargar()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            return View(result);
        }

        [HttpPost]
        public ActionResult Cargar(ML.Result result)
        {
            if (Session["pathExcel"] == null) //verificamos si la session esta nula, si es nula, quiere decir que no hay ningun archivo en validacion
            {
                HttpPostedFileBase file = Request.Files["Excel"]; //guardamos el archivo de excel con el id del input Excel
                if (file != null) //si encontramos el archivo
                {

                    string extensionArchivo = Path.GetExtension(file.FileName).ToLower(); //obteniendo la extension
                    string extesionValida = ConfigurationManager.AppSettings["TipoExcel"]; //obtenemos la extension de nuestra key en appSettings

                    if (extensionArchivo == extesionValida) //validamos si la extension del archivo que nos pasaron coincide con la que estamos esperando
                    {
                        string filePath = Server.MapPath("~/CargaMasiva/") + Path.GetFileNameWithoutExtension(file.FileName) + '-' + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx"; //obtnego la ruta y el nombre del archivo donde hare la copia

                        if (!System.IO.File.Exists(filePath))
                        {

                            file.SaveAs(filePath); //guardamos el archivo en la ruta filePath

                            //Session -C#
                            string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"] + filePath; //pasamos la conexion, junto con la ruta del archivo que queremos abrir
                            ML.Result resultUsuarios = BL.Usuario.LeerExcel(connectionString);

                            if (resultUsuarios.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultUsuarios.Objects); // le pasamos los rows que se obtuvieron del excel que nos paso el usuario
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true; //se coloca correct para que en la vista se pueda controlar el boton de insertar
                                    Session["pathExcel"] = filePath; //guardamos la ruta en una sesion con el nombre de pathExcel
                                }

                                return View(resultValidacion);
                            }
                            else
                            {
                                ViewBag.Message = "El excel no contiene registros";
                                return PartialView("Modal");
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Favor de seleccionar un archivo .xlsx, Verifique su archivo";
                        return PartialView("Modal");
                    }
                }
                else
                {
                    ViewBag.Message = "No selecciono ningun archivo, Seleccione uno correctamente";
                }
                return View(result);
            }
            else //ya habia un archivo cargado
            {
                string filepath = Session["pathExcel"].ToString(); //obtenemos la ruta del archivo desde la sesion

                if (filepath != null) //validar si la ruta trae informacion
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"] + filepath; //establecemos la conexion con el archivo para leerlo
                    ML.Result resultUsuarios = BL.Usuario.LeerExcel(connectionString); //como ya validamos el excel, lo volvemos a leer para obtener los datos

                    if (resultUsuarios.Correct) //si todas las asignaciones se hicieron correctas en la capa BL, todo esta correcto
                    {
                        ML.Result resultErrores = new ML.Result(); //vamos a guardar los errores
                        resultErrores.Objects = new List<object>();
                        foreach (ML.Usuario usuario in resultUsuarios.Objects) //iteramos todos los usuarios de la lista resultUsuarios.Objects
                        {
                            ML.Result result1 = BL.Usuario.AddEF(usuario); //como todo esta "bien", hacemos el insert

                            if (!result1.Correct) //podria haber errores a la hora del insert, debido a un tipo de dato diferente o cosas asi
                            {
                                string error = "***Error al insertar el usuario, con Username "+usuario.UserName+".  " + result1.ErrorMessage;
                                resultErrores.Objects.Add(error); //creamos el mensaje de error y lo guardamos en la lista
                            } 

                        }

                        if(resultErrores.Objects.Count > 0)
                        {
                            string pathTxt = Server.MapPath(@"~\Files\logErrores")+ "-"+ DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"; //creamos el archivo de errores, y le colocamos la fecha de la carga masiva 

                            using (StreamWriter escribir = new StreamWriter(pathTxt))
                            {
                                foreach(string linea in resultErrores.Objects)
                                {
                                    escribir.WriteLine(linea);
                                }
                            }
                        }
                    }

                    Session["pathExcel"] = null; //hasta este punto, dejamos de trabajar con el excel, ya sea que todo esta bien o hubo errores, el proceso termina, y colocamos la ruta en null para que el usuario pueda ingresar otro archivo y este se valide desde 0

                }
                else
                {

                }


            }
            return View(result);



        }


    }
}