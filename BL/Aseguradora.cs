using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Aseguradora
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.AseguradoraGetAll().ToList();
                    result.Objects = new List<object>();

                    if (query.Count > 0)
                    {
                        foreach (var obj in query)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();
                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre = obj.NombreAseguradora;
                            aseguradora.FechaCreacion = (DateTime)obj.FechaCreacion;
                            aseguradora.FechaModificacion = (DateTime)obj.FechaModificacion;
                            
                            aseguradora.Usuario = new ML.Usuario();
                            aseguradora.Usuario.IdUsuario = obj.IdUsuario;
                            aseguradora.Usuario.Nombre = obj.NombreUsuario;
                            aseguradora.Usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            aseguradora.Usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            aseguradora.Usuario.Imagen = obj.Imagen;

                            result.Objects.Add(aseguradora);

                        }

                        result.Correct = true;
                    }
                    else { 
                        result.Correct = false;
                        result.ErrorMessage = "Error al obtener los datos";
                    }
                }

            } catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetById (int idAseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {

                    var query = context.AseguradoraGetById(idAseguradora).FirstOrDefault();
                    if (query != null)
                    {
                        ML.Aseguradora aseguradora = new ML.Aseguradora();
                        aseguradora.IdAseguradora = query.IdAseguradora;
                        aseguradora.Nombre = query.Nombre;

                        aseguradora.Usuario = new ML.Usuario();
                        aseguradora.Usuario.IdUsuario = query.IdUsuario.Value;
                        result.Object = aseguradora;

                        result.Correct = true;
                    }

                }

            }catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Add (ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.AseguradoraAdd(aseguradora.Nombre, aseguradora.Usuario.IdUsuario);

                    if(query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                       result.Correct= false;
                        result.ErrorMessage = "No se hizo el add";
                    }

                }

            } catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Update (ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities ())
                {
                    var query = context.AseguradoraUpdate(aseguradora.IdAseguradora, aseguradora.Nombre, aseguradora.Usuario.IdUsuario);

                    if(query > 0)
                    {
                        result.Correct =true;
                    } else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "Error al actualizar";
                    }
                }

            } catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Delete (int idAseguradora)
        {
            ML.Result result = new ML.Result ();
            try
            {
                using(DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.AseguradoraDelete(idAseguradora);

                    if(query > 0 ) result.Correct =true;
                    else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "No se pudo borrar";
                    }

                }

            } catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}
