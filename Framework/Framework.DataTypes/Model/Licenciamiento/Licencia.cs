using Framework.DataTypes.Model.Infraestructura;
using Framework.DataTypes.Model.Licenciamiento;
using System.Collections.Generic;
using System;

namespace Framework.DataTypes.Model.Licenciamiento
{
    public class Licencia
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Paquete PaqueteInicial { get; set; }
        public decimal CostoInicial { get; set; }
        public int NumUsuariosTotal { get; set; }
        public int NumNegociosTotal { get; set; }
        public ICollection<LicenciaApp> Apps { get; set; }
        public decimal CostoTotalActual { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime UltActualizacion { get; set; }
    }
}