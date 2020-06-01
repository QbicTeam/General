using System;
using System.Collections.Generic;

using Framework.DataTypes.Model.Licenciamiento;
using Framework.DataTypes.Model.Infraestructura;

namespace SI_Admin.API.DTO
{
    public class ClienteActualizacionParaActualizacionDTO
    {
        public int Status { get; set; }
        public ICollection<Aplicacion> Apps { get; set; }
        public ICollection<ClienteActualizacionNegocio> Negocios { get; set; }
    }
}