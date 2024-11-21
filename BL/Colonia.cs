using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result ColoniaGetByIdMunicipio (int idMunicipio)
        {
            ML.Result result = new ML.Result ();
            try
            {

                using(DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities ())
                {
                    var query = context.ColoniaGetByIdMunicipio(idMunicipio).ToList();

                    if(query != null)
                    {
                        result.Objects = new List<object> ();
                        foreach(var obj in query)
                        {
                            ML.Colonia colonia = new ML.Colonia ();
                            colonia.IdColonia = obj.IdColonia;
                            colonia.Nombre = obj.Nombre;
                            colonia.CodigoPostal = obj.CodigoPostal;
                            result.Objects.Add (colonia);
                        }

                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static (bool, string, List<object>, Exception) ColoniaGetByIdMunicipio2(int idMunicipio)
        {
            ML.Colonia colonia = new ML.Colonia();
            try
            {

                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.ColoniaGetByIdMunicipio(idMunicipio).ToList();

                    if (query != null)
                    {
                        colonia.Colonias = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Colonia coloniaObj = new ML.Colonia();
                            coloniaObj.IdColonia = obj.IdColonia;
                            coloniaObj.Nombre = obj.Nombre;
                            coloniaObj.CodigoPostal = obj.CodigoPostal;
                            colonia.Colonias.Add(coloniaObj);
                        }

                        return (true, null, colonia.Colonias, null);
                    } else
                    {
                        return (false, "No hay colonias", null, null);
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
