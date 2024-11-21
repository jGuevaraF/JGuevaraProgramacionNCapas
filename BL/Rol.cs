using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        //Creamos otra clase, debido a que el usuario no tendra control a Rol
        //Como se añadio la propiedad de navegacion a la clase Usuario, podemos acceder a esta clase

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var roles = context.RolGetAll().ToList();
                    result.Objects = new List<object>();

                    if(roles.Count > 0)
                    {
                        foreach (var valor in roles)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.idRol = valor.IdRol;
                            rol.Nombre = valor.Nombre;

                            result.Objects.Add(rol);
                        }

                        result.Correct = true;
                    }
                }

            } catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }
            return result;
        }

        public static (bool, string, List<object>, Exception) GetAll2()
        {
            ML.Rol rol = new ML.Rol();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var roles = context.RolGetAll().ToList();
                    rol.Roles = new List<object>();

                    if (roles.Count > 0)
                    {
                        foreach (var valor in roles)
                        {
                            ML.Rol rolObj = new ML.Rol();
                            rolObj.idRol = valor.IdRol;
                            rolObj.Nombre = valor.Nombre;

                            rol.Roles.Add(rolObj);
                        }

                        return (true, null, rol.Roles, null);
                    } else
                    {
                        return (false, "No hay roles",  null, null);
                    }
                }

            }
            catch (Exception e)
            {
                return (false, e.Message, null, e);   
            }
        }

    }
}
