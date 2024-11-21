using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        int Suma(int num1, int num2); //estamos en la interface, por lo que hacemos la firma del metodo, el cual la logica se encuentra en el servicio

        [OperationContract]
        int Resta (int num1, int num2);

        [OperationContract]
        int Multiplicacion (int num1, int num2);

        [OperationContract]
        int Dividir (int num1, int num2);
    }

    
}
