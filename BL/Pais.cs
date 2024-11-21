using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Pais
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    //ejecutamos el query
                    var query = context.PaisGetAll().ToList();
                    //verificamos si viene con informacion
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query) { 
                            //usamos el constructor para asignar los valores a las propiedades
                            ML.Pais pais = new ML.Pais(obj.IdPais, obj.Nombre);
                            result.Objects.Add(pais);
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

        public static (bool, string, List<object>, Exception) GetAll2()
        {
            ML.Pais pais = new ML.Pais();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    //ejecutamos el query
                    var query = context.PaisGetAll().ToList();
                    //verificamos si viene con informacion
                    if (query != null)
                    {
                        pais.Paises = new List<object>();
                        foreach (var obj in query)
                        {
                            //usamos el constructor para asignar los valores a las propiedades
                            ML.Pais paisobj = new ML.Pais(obj.IdPais, obj.Nombre);
                            pais.Paises.Add(pais);
                        }

                        return (true, null, pais.Paises, null);
                    } else
                    {
                        return (false, "No hay paises", null, null);
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
