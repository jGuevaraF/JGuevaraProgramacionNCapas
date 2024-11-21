using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Services;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceEmpleado" in both code and config file together.
    [ServiceContract]
    public interface IServiceEmpleado
    {
        [OperationContract]
        SLWCF.Result Add(ML.Empleado empleado); // se hace el result en SLWCF para que pueda hacer la serializacion y la deserealizacion, si usamos el Result desde el ML, no sabra como se serializa o deserealiza

        [OperationContract]
        SLWCF.Result Update(ML.Empleado empleado);

        [OperationContract]
        SLWCF.Result Delete(string numeroEmpleado);

        [OperationContract]
        [ServiceKnownType(typeof(ML.Empleado))]//le especificamos que lo debe deserealizar como un ML.Usuario
        SLWCF.Result GetAll(ML.Empleado empleado);

        [OperationContract]
        [ServiceKnownType (typeof(ML.Empleado))]
        SLWCF.Result GetById(string numeroEmpleado);

    }
}
