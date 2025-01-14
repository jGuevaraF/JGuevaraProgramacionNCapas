﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceUsuario" in both code and config file together.
    [ServiceContract]
    public interface IServiceUsuario
    {
        [OperationContract]
        SLWCF.Result Add(ML.Usuario usuario);

        [OperationContract]
        SLWCF.Result Update(ML.Usuario usuario);

        [OperationContract]
        SLWCF.Result Delete(int idUsuario);

        [OperationContract]
        [ServiceKnownType(typeof(ML.Usuario))]
        SLWCF.Result GetAll(ML.Usuario usuario);

        [OperationContract]
        [ServiceKnownType (typeof(ML.Usuario))]
        SLWCF.Result GetById(int id);

        [OperationContract]
        SLWCF.Result CambiarStatus(int idUsuario, bool status);
    }
}
