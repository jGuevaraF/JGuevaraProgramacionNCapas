using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class DependienteController : Controller
    {
        // GET: Dependiente
        [HttpGet]
        public ActionResult GetAll()
        {
            Session["numeroEmpleado"] = null;

            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result resultEmpleado = BL.Empleado.GetAll(empleado);

            if (resultEmpleado.Correct)
            {
                empleado.Empleados = resultEmpleado.Objects;
                return View(empleado);
            } else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetByNumeroEmpleado(string numeroEmpleado) {
            Session["numeroEmpleado"] = numeroEmpleado;

            ML.Dependiente dependiente = new ML.Dependiente();
            dependiente.Empleado = new ML.Empleado();

            ML.Result resultDependiete = BL.Dependiente.GetByNumeroEmpleado(numeroEmpleado);//obtenemos dependiente
            ML.Result resultGetEmpleado = BL.Empleado.GetById(numeroEmpleado); //obtenemos el empleado

            dependiente.Empleado = (ML.Empleado)resultGetEmpleado.Object; //Pasamos el empleado


            if (resultDependiete.Correct) //tiene dependientes
            {
                dependiente.Dependientes = resultDependiete.Objects;

                return View(dependiente);
            } else //no tiene dependientes
            {
                return View(dependiente);
            }
        }


        [HttpGet]
        public ActionResult Form(int? idDependiente)
        {
            if (Session["numeroEmpleado"] != null)
            {
                if (idDependiente != null)
                {
                    ML.Dependiente dependiente = new ML.Dependiente();

                    ML.Result result = BL.Dependiente.GetById(idDependiente.Value);
                    dependiente = (ML.Dependiente)result.Object;

                    return View(dependiente);
                }
                else
                {
                    return View();
                }

            } else
            {
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult Form(ML.Dependiente dependiente)
        {
            dependiente.Empleado = new ML.Empleado();
            dependiente.Empleado.NumeroEmpleado = (string)Session["numeroEmpleado"];

            if (dependiente.IdDependiente == 0) //Add
            {
                
                ML.Result result = BL.Dependiente.Add(dependiente);

                if (result.Correct)
                {
                    ViewBag.Message = "El dependiente se dio de alta correctamente";
                    return RedirectToAction("GetByNumeroEmpleado", "Dependiente", new { numeroEmpleado = Session["numeroEmpleado"] });

                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("Modal");
                }
            } else //update
            {
                ML.Result resultUpdate = BL.Dependiente.Update(dependiente);
                if (resultUpdate.Correct)
                {
                    return RedirectToAction("GetByNumeroEmpleado", "Dependiente", new { numeroEmpleado = Session["numeroEmpleado"] });
                } else
                {
                    ViewBag.Message = resultUpdate.ErrorMessage;
                    return PartialView("Modal");
                }
            }
        }

        [HttpGet]
        public ActionResult Delete (int idDependiente)
        {
            ML.Result delete = BL.Dependiente.Delete(idDependiente);
            if (delete.Correct)
            {
                return RedirectToAction("GetByNumeroEmpleado", "Dependiente", new { numeroEmpleado = Session["numeroEmpleado"] });
            }
            else
            {
                ViewBag.Message = delete.ErrorMessage;
                return PartialView("Modal");
            }
        }

            
    }
}