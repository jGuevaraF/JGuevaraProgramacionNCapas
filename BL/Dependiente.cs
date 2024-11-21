using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dependiente
    {
        
        public static ML.Result GetById (int idDependiente)
        {
            ML.Result result = new ML.Result ();
            try
            {

                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.DependienteGetById(idDependiente).SingleOrDefault();

                    if(query != null)
                    {
                        ML.Dependiente dependiente = new ML.Dependiente();
                        dependiente.Nombre = query.Nombre;
                        dependiente.ApellidoPaterno = query.ApellidoPaterno;
                        dependiente.ApellidoMaterno = query.ApellidoMaterno;
                        dependiente.FechaNacimiento = query.FechaNacimiento;
                        dependiente.EstadoCivil = query.EstadoCivil;
                        dependiente.Genero = query.GENERO;
                        dependiente.Telefono = query.Telefono;
                        dependiente.RFC = query.RFC;
                        result.Object = dependiente;

                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro ningun ID";
                    }

                }


            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetByNumeroEmpleado(string numeroEmpleado) //GETALL
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {

                    var query = context.DependienteGetByNumeroEmpleado(numeroEmpleado).ToList();

                    if(query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach(var item in query)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();

                            dependiente.IdDependiente = item.IdDependiente;
                            dependiente.Nombre = item.DependienteNombre;
                            dependiente.ApellidoPaterno = item.DependienteApellidoPaterno;
                            dependiente.ApellidoMaterno = item.DependienteApellidoMaterno;
                            dependiente.FechaNacimiento = item.DependienteFechaNacimiento;
                            dependiente.EstadoCivil = item.EstadoCivil;
                            dependiente.Genero = item.GENERO;
                            dependiente.Telefono = item.DependienteTelefono;
                            dependiente.RFC = item.DependienteRFC;

                            dependiente.Empleado = new ML.Empleado();
                            dependiente.Empleado.NumeroEmpleado = item.NumeroEmpleado;
                            dependiente.Empleado.Nombre = item.NombreEmpleado;
                            dependiente.Empleado.ApellidoPaterno = item.EmpleadoApellidoPaterno;
                            dependiente.Empleado.ApellidoMaterno = item.EmpleadoApellidoMaterno;
                            dependiente.Empleado.Telefono = item.TelefonoEmpleado;
                            dependiente.Empleado.Foto = item.Foto;

                            result.Objects.Add( dependiente );
                        }
                        result.Correct = true;
                    } else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "El empleado no tiene dependientes";
                    }

                }


            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Add (ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            try
            {

                using(DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {

                    var query = context.DependienteAdd(dependiente.Nombre, dependiente.ApellidoPaterno, dependiente.ApellidoMaterno, dependiente.FechaNacimiento, dependiente.EstadoCivil, dependiente.Genero, dependiente.Telefono, dependiente.RFC, dependiente.Empleado.NumeroEmpleado);

                    if(query > 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo dar de alta";
                    }

                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Update (ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {

                    var query = context.DependienteUpdate(dependiente.IdDependiente, dependiente.Nombre, dependiente.ApellidoPaterno, dependiente.ApellidoMaterno, dependiente.FechaNacimiento, dependiente.EstadoCivil, dependiente.Genero, dependiente.Telefono, dependiente.RFC);

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo Actualizar";
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Delete (int idDependiente)
        {
            ML.Result result = new ML.Result();

            try
            {

                using(DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.DependienteDelete(idDependiente);

                    if(query > 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar";
                    }
                }


            } catch(Exception ex) { 
                result.Correct =false;
                result.ErrorMessage= ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}
