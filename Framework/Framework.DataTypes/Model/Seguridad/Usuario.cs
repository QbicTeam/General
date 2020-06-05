using System.Collections.Generic;

namespace Framework.DataTypes.Model.Seguridad
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string PWD { get; set; }
        public Rol Rol { get; set; }
        public string Foto { get; set; }
        public string Telefono { get; set; }
        public ICollection<UsuarioPregunta> Preguntas { get; set; }
        // Enum: 1.Invitado, 2.Activo, 3.Bloqueado, 4.Suspendido, 5.Baja
        public string Status { get; set; }
    }
}