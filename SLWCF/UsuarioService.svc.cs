using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceUsuario" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceUsuario.svc or ServiceUsuario.svc.cs at the Solution Explorer and start debugging.
    public class ServiceUsuario : IServiceUsuario
    {
        public SLWCF.Result Add(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario); //ejecutamos el metodo de la capa BL
            return new SLWCF.Result
            {
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex
            };
        }

        public SLWCF.Result Update(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.UpdateEF(usuario);

            return new SLWCF.Result
            {
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex
            };
        }

        public SLWCF.Result Delete(int idUsuario)
        {
            ML.Result result = BL.Usuario.DeleteEF(idUsuario);

            return new SLWCF.Result
            {
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex
            };
        }

        public SLWCF.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAllEF(usuario);

            return new SLWCF.Result
            {
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex
            };
        }

        public SLWCF.Result GetById (int id)
        {
            ML.Result result = BL.Usuario.GetByIdEF(id);

            return new SLWCF.Result
            {
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex
            };
        }

        public SLWCF.Result CambiarStatus (int idUsuario, bool status)
        {
            ML.Result result = BL.Usuario.CambiarStatus(idUsuario, status);

            return new SLWCF.Result
            {
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex
            };

        }

    }
}
