using System.Collections.Generic;

namespace Framework.DataTypes.Model.Seguridad
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<RolOpcion> Opciones { get; set; }
    }
}