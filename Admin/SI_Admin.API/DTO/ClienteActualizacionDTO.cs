using System;
using System.Collections.Generic;

using Framework.DataTypes.Model.Base;
using Framework.DataTypes.Model.Licenciamiento;
using Framework.DataTypes.Model.Infraestructura;

namespace SI_Admin.API.DTO
{
    public class ClienteActualizacionDTO
    {
        public int Id { get; set; }
        // Enum: 1.Nuevo, 2.AddApp, 3.AddNegocio. 4.AddAppNegocio        
        public int Tipo { get; set; }
        public Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
        public ICollection<ClienteActualizacionApp> Apps { get; set; }
        public ICollection<ClienteActualizacionNegocio> Negocios { get; set; }
        public DateTime Fecha { get; set; }        
    }
}