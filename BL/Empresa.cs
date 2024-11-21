using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.EmpresaGetAll().ToList();
                    //inicializamos la lista
                    result.Objects = new List<object>();

                    if(query.Count > 0)
                    {
                        foreach (var emp in query)
                        {
                            ML.Empresa empresa = new ML.Empresa();
                            empresa.IdEmpresa = emp.IdEmpresa;
                            empresa.Nombre = emp.Nombre;
                            empresa.Telefono = emp.Telefono;

                            result.Objects.Add(empresa);
                        }

                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros en la tabla Empresa";
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
    }
}
