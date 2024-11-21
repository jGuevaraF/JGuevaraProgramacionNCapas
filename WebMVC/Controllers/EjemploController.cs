using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class EjemploController : Controller
    {
        public ActionResult Sesion()
        {
            ML.Usuario usus = new ML.Usuario();
            Session["nombreSesion"] = usus; //puede guardar cualquier tipo de dato (Primitivo o Complejo)
            Session["nombreSesion"] = null; //limpiar sesion

            if (Session["nombreSesion"]!= null)
            {


            } else //session viene vacia
            {

            }
            return View();
        }

        // GET: Ejemplo
        public ActionResult Form(int? id)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Imagen = "";
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
                ML.Result result = BL.Usuario.GetByIdEF(id.Value);

                usuario = (ML.Usuario)result.Object;
                //Pasamos la lista de roles
                usuario.Rol.Roles = resultRol.Objects;


                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                usuario.Direccion.Colonia.Municipio.Estado.Estados = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais).Objects;

                //usuario.Direccion.Colonia.Municipio.Muncipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado).Objects;

                return View(usuario);
            }

            return View(usuario);
        }

        public ActionResult GetAll()
        {

            ML.Usuario usuario = new ML.Usuario();

            ML.Result result = BL.Usuario.GetAllEF(usuario);

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
            if (usuario.Nombre == null)
            {
                usuario.Nombre = "";
            }
            else
            {
                usuario.Nombre = usuario.Nombre;
            }

            usuario.Nombre = (usuario.Nombre == null) ? usuario.Nombre = "" : usuario.Nombre;

            ML.Result result = BL.Usuario.GetAllEF(usuario);

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
        public ActionResult Form(ML.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["Imagen"];
                if (file.ContentLength > 0)
                {
                    usuario.Imagen = ConvertirABase64(file);
                }


                //resto del codigo para add
            } else
            {
                var roles = BL.Rol.GetAll2();
                var pais = BL.Pais.GetAll2();
                var estado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                var municipio = BL.Municipio.GetByIdEstado2(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                var colonia = BL.Colonia.ColoniaGetByIdMunicipio2(usuario.Direccion.Colonia.Municipio.IdMunicipio);

                usuario.Rol.Roles = roles.Item3;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = pais.Item3;
                usuario.Direccion.Colonia.Municipio.Estado.Estados = estado.Objects;
                usuario.Direccion.Colonia.Municipio.Muncipios = municipio.Item3;
                usuario.Direccion.Colonia.Colonias = colonia.Item3;

                return View(usuario);
            }



            return View();
        }

        [HttpPost]
        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.GetByIdPais(IdPais);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(IdEstado);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
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
        public JsonResult CambiarStatus(int IdUsuario, bool Status)
        {
            var result = BL.Usuario.CambioStatus(IdUsuario, Status);
            if (result.Item1)
            {
                return Json(result.Item1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result.Item2, JsonRequestBehavior.AllowGet);
            }
        }



    }
}

//BOXING -- almacenar cualquier tipo de dato dentro de un Object
//UNBOXING -- extraer cualquier tipo de dato desde un Object