using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EstadoEjemplo
    {
        public static (bool, string, ML.Estado, Exception) GetByIdPais(int idPais)
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
                        return (true, null, estado, null);

                    } else
                    {
                        return (false, "No hay estados", null, null);
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
