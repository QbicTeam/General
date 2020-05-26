using Framework.DataTypes.Model.Base;
using Framework.DataTypes.Model.Infraestructura;
using System.Collections.Generic;
using System;

namespace Framework.DataTypes.Model.Licenciamiento
{
    public class ClienteActualizacion
    {
        public int Id { get; set; }
        // Enum: 1.Nuevo, 2.AddApp, 3.AddNegocio. 4.AddAppNegocio        
        public int Tipo { get; set; }
        public Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
        public ICollection<Aplicacion> Apps { get; set; }
        public ICollection<ClienteActualizacionNegocio> Negocios { get; set; }
        public DateTime Fecha { get; set; }
        // Enum: 1.Por Procesar, 2.Procesado
        public int Status { get; set; }
    }
}