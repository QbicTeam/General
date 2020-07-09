using Framework.DataTypes.Model.Infraestructura;
using System.Collections.Generic;

namespace Framework.DataTypes.Model.Licenciamiento
{
    public class Paquete
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int NumUsuarios { get; set; }
        public int NumNegocios { get; set; }
        public decimal Costo { get; set; }
        public ICollection<PaqueteApp> Apps { get; set; }        
        public string ContenidoCorto { get; set; }
        public string ContenidoCompleto { get; set; }
        public bool Activo { get; set; }
        public string ClaseIcono { get; set; }
        public string RutaLogo { get; set; }
    }
}