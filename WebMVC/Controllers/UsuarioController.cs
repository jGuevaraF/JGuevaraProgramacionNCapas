using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["Imagen"];

                if (file.ContentLength > 0)
                {
                    usuario.Imagen = ConvertirABase64(file);
                }

                if (usuario.IdUsuario == 0) //Agregar
                {
                    //ML.Result result = BL.Usuario.AddEF(usuario);
                    ServiceReferenceUsuario.ServiceUsuarioClient usuarioAdd = new ServiceReferenceUsuario.ServiceUsuarioClient();

                    var result = usuarioAdd.Add(usuario);

                    if (result.Correct)
                    {
                        ViewBag.Add = true;
                        ViewBag.Message = "El usuario se dio de alta correctamente";
                    }
                    else
                    {
                        ViewBag.Add = false;
                        ViewBag.Message = "ERROR " + result.ErrorMessage;
                    }
                }
                else //Actualizar
                {
                    //ML.Result result = BL.Usuario.GetByIdEF(usuario.IdUsuario);
                    //usuario = (ML.Usuario)result.Object;
                    //ML.Result result = BL.Usuario.UpdateEF(usuario);
                    ServiceReferenceUsuario.ServiceUsuarioClient updateUsuario = new ServiceReferenceUsuario.ServiceUsuarioClient();

                    var result = updateUsuario.Update(usuario);

                    if (result.Correct)
                    {
                        ViewBag.Message = "Registro actualizado";
                    }
                    else
                    {
                        ViewBag.Message = "ERROR " + result.ErrorMessage;
                    }
                }

                return PartialView("Modal");
            } else
            {
                //obtenemos todos los Drop down list
                ML.Result roles = BL.Rol.GetAll();
                ML.Result paises = BL.Pais.GetAll();

                usuario.Rol.Roles = roles.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = paises.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Estados = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais).Objects;

                //usuario.Direccion.Colonia.Municipio.Muncipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado).Objects;

                usuario.Direccion.Colonia.Colonias = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio).Objects;

                return View(usuario);
            }
        }

        [HttpGet]
        public ActionResult Form(int? id)
        {
            ML.Usuario usuario = new ML.Usuario();

            //Instanciamos todas las referencias
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            ML.Result resultRol = BL.Rol.GetAll();
            //Obtenemos los paises
            ML.Result resultPais = BL.Pais.GetAll();

            if (id == null) //Agregar
            {
                //paso los paises
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                usuario.Rol.Roles = resultRol.Objects;
            }
            else //Update
            {
                //ML.Result result = BL.Usuario.GetByIdEF(id.Value);
                ServiceReferenceUsuario.ServiceUsuarioClient getIdUsuario = new ServiceReferenceUsuario.ServiceUsuarioClient();
                var result = getIdUsuario.GetById(id.Value);

                usuario = (ML.Usuario)result.Object;
                //Pasamos la lista de roles
                usuario.Rol.Roles = resultRol.Objects;


                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                usuario.Direccion.Colonia.Municipio.Estado.Estados = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais).Objects;

                usuario.Direccion.Colonia.Municipio.Muncipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado).Objects;

                usuario.Direccion.Colonia.Colonias = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio).Objects;

                return View(usuario);
            }

            return View(usuario);
        }

        public ActionResult Delete(int id)
        {
            //ML.Result result = BL.Usuario.DeleteEF(id);
            ServiceReferenceUsuario.ServiceUsuarioClient deleteUsuario = new ServiceReferenceUsuario.ServiceUsuarioClient();
            var result = deleteUsuario.Delete(id);

            if (result.Correct)
            {
                ViewBag.Message = "El usuario se eliminó correctamente";
            }
            else
            {
                ViewBag.Message = "ERROR";
            }
            return PartialView("Modal");
        }

        [HttpGet]
        // GET: Usuario
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            //ML.Result result = BL.Usuario.GetAllEF(usuario);
            ServiceReferenceUsuario.ServiceUsuarioClient getAll = new ServiceReferenceUsuario.ServiceUsuarioClient();

            var result = getAll.GetAll(usuario);


            if (result.Correct)
            {
                usuario.Usuarios = result.Objects.ToList();
                return View(usuario);
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            //ML.Result result = BL.Usuario.GetAllEF(usuario);
            ServiceReferenceUsuario.ServiceUsuarioClient getAll = new ServiceReferenceUsuario.ServiceUsuarioClient();
            var result = getAll.GetAll(usuario);

            usuario = new ML.Usuario();
            usuario.Usuarios = result.Objects.ToList();
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {

            ML.Result loginResult = BL.Usuario.Login(email);

            if(loginResult.Correct) //iniciar sesion
            {
                ML.Usuario usuario = (ML.Usuario)loginResult.Object;
                if(usuario.Password == password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Login = false;
                    ViewBag.Message = "La contraseña es incorrecta";
                    return PartialView("Modal");
                }

                
            } else
            {
                ViewBag.Login = false;
                ViewBag.Message = loginResult.ErrorMessage;
                return PartialView("Modal");
            }
        }

        [HttpGet]
        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.GetByIdPais(IdPais);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(IdEstado);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.ColoniaGetByIdMunicipio(IdMunicipio);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        public string ConvertirABase64(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            string imagen = Convert.ToBase64String(data);
            return imagen;
        }

        [HttpPost]
        public JsonResult ChangeStatus(int IdUsuario, bool Status)
        {
            ML.Result result = BL.Usuario.CambiarStatus(IdUsuario, Status);
            //ServiceReferenceUsuario.ServiceUsuarioClient status = new ServiceReferenceUsuario.ServiceUsuarioClient();
            //var result = status.CambiarStatus(IdUsuario, Status);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}