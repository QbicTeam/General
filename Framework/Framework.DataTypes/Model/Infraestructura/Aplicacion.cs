using System.Collections.Generic;

namespace Framework.DataTypes.Model.Infraestructura
{
    public class Aplicacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Abr { get; set; }
        public string Descripcion { get; set; }
        public ICollection<AppMenu> Menu { get; set; }

    }
}