using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class MunicipioEjemplo
    {
        public  static (bool, string, List<ML.Municipio>, Exception) GetByIdEstado (int idEstado)
        {
            ML.Municipio municipio = new ML.Municipio ();
            try
            {
                using(DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var result = context.MunicipioGetByIdEstado (idEstado).ToList();

                    if(result.Count > 0)
                    {
                        municipio.Muncipios2 = new List<ML.Municipio>();
                        foreach(var registro  in result)
                        {
                            ML.Municipio municipioObj = new ML.Municipio();
                            municipioObj.Nombre = registro.Nombre;
                            municipioObj.IdMunicipio = registro.IdMunicipio;

                            municipio.Muncipios2.Add(municipioObj);
                        }

                        return (true, null, municipio.Muncipios2, null);
                    } else
                    {
                        return (false, "No hay registros", null,  null);
                    }
                }

            } catch (Exception ex)
            {
                return (false,  ex.Message, null, ex);
            }
        }
    }
}
