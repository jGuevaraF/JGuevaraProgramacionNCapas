using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {

        public static ML.Result GetByIdEstado (int idEstado)
        {
            ML.Result result = new ML.Result ();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.MunicipioGetByIdEstado(idEstado).ToList();

                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.IdMunicipio = obj.IdMunicipio;
                            municipio.Nombre = obj.Nombre;
                            result.Objects.Add(municipio);
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

        public static (bool, string, List<object>, Exception) GetByIdEstado2(int idEstado)
        {
            ML.Municipio municipio = new ML.Municipio();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.MunicipioGetByIdEstado(idEstado).ToList();

                    if (query != null)
                    {
                        municipio.Muncipios = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Municipio municipioObj = new ML.Municipio();
                            municipioObj.IdMunicipio = obj.IdMunicipio;
                            municipioObj.Nombre = obj.Nombre;
                            municipio.Muncipios.Add(municipioObj);
                        }

                        return (true, null, municipio.Muncipios,  null);

                    } else
                    {
                        return (false, "No hay municipios", null,  null);
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
