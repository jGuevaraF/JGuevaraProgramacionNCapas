using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.EmpleadoAdd(empleado.NumeroEmpleado, empleado.RFC, empleado.Nombre, empleado.ApellidoPaterno, empleado.ApellidoMaterno, empleado.Email, empleado.Telefono, empleado.FechaNacimiento, empleado.NSS, empleado.Foto, empleado.Empresa.IdEmpresa);

                    if(query != 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "No se pudo hacer el insert de Empleado";
                    }

                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {

                    var query = context.EmpleadoUpdate(empleado.NumeroEmpleado, empleado.RFC, empleado.Nombre, empleado.ApellidoPaterno, empleado.ApellidoMaterno, empleado.Email, empleado.Telefono, empleado.FechaNacimiento, empleado.NSS, empleado.Foto, empleado.Empresa.IdEmpresa);

                    if(query != 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "No se pudo actualizar";
                    }

                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetAll(ML.Empleado buscarEmp)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.EmpleadoGetAll(buscarEmp.Empresa.IdEmpresa, buscarEmp.Nombre).ToList();

                    if(query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach(var obj in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            empleado.RFC = obj.RFC;
                            empleado.Nombre = obj.NombreEmpleado;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.TelefonoEmpleado;
                            empleado.FechaNacimiento = (DateTime)obj.FechaNacimiento;
                            empleado.NSS = obj.NSS;
                            empleado.FechaIngreso = (DateTime)obj.FechaIngreso;
                            empleado.Foto = obj.Foto;

                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa;
                            empleado.Empresa.Nombre = obj.NombreEmpresa;
                            empleado.Empresa.Telefono = obj.EmpresaTelefono;

                            result.Objects.Add(empleado);
                        }

                        result.Correct = true;
                    }

                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetById(string numeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {

                    var query = context.EmpleadoGetById(numeroEmpleado).SingleOrDefault();

                    if(query != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.NumeroEmpleado = query.NumeroEmpleado;
                        empleado.RFC = query.RFC;
                        empleado.Nombre = query.NombreEmpleado;
                        empleado.ApellidoPaterno = query.ApellidoPaterno;
                        empleado.ApellidoMaterno = query.ApellidoMaterno;
                        empleado.Email = query.Email;
                        empleado.Telefono = query.TelefonoEmpleado;
                        empleado.FechaNacimiento = (DateTime)query.FechaNacimiento;
                        empleado.NSS = query.NSS;
                        empleado.FechaIngreso = (DateTime)query.FechaIngreso;
                        empleado.Foto = query.Foto;

                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = query.IdEmpresa;
                        empleado.Empresa.Nombre = query.NombreEmpresa;
                        empleado.Empresa.Telefono = query.EmpresaTelefono;

                        result.Object = empleado;
                        result.Correct = true;
                    }

                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Delete(string numeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {

                    var query = context.EmpleadoDelete(numeroEmpleado);

                    if(query>0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo borrar el Empleado";
                    }

                }

            } catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}
