using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceAseguradora" in both code and config file together.
    [ServiceContract]
    public interface IServiceAseguradora
    {
        [OperationContract]
        SLWCF.Result Add(ML.Aseguradora aseguradora); //hacemos la firma del metodo que esta en nuestro servicio

        [OperationContract] //le decimos que es parte del servicio
        SLWCF.Result Update(ML.Aseguradora aseguradora);

        [OperationContract]
        SLWCF.Result Delete (int idAseguradora);

        [OperationContract]
        [ServiceKnownType(typeof (ML.Aseguradora))]
        SLWCF.Result GetAll();

        [OperationContract]
        [ServiceKnownType(typeof(ML.Aseguradora))]
        SLWCF.Result GetById(int idAseguradora);
    }
}
