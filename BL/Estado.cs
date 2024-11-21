using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetByIdPais(int idPais)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.EstadoGetByIdPais(idPais).ToList();
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var obj in query)
                        {
                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = obj.IdEstado;
                            estado.Nombre = obj.Nombre;
                            result.Objects.Add(estado);
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

        public static (bool, string, List<object>, Exception) GetByIdPais2(int idPais)
        {
            ML.Estado estado = new ML.Estado();

            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.EstadoGetByIdPais(idPais).ToList();
                    if (query != null)
                    {
                        estado.Estados = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Estado estadoObj = new ML.Estado();
                            estadoObj.IdEstado = obj.IdEstado;
                            estadoObj.Nombre = obj.Nombre;
                            estado.Estados.Add(estadoObj);
                        }
                        return (true, null, estado.Estados, null);
                    } else
                    {
                        return (false, "No hay estados",  null, null);
                    }

                }

            }
            catch (Exception ex)
            {
                return (false, ex.Message, null, ex);
            }

        }

    }
}
