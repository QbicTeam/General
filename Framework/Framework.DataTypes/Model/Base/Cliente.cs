using Framework.DataTypes.Model.Licenciamiento;
using System.Collections.Generic;

namespace Framework.DataTypes.Model.Base
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string email { get; set; }
        public ICollection<ClienteNegocio> Negocios { get; set; }
        public Licencia Licencia { get; set; }
        public string NomEmpresa { get; set; }
        public string NomCorto { get; set; }
        public string RFC { get; set; }
        public string Domicilio { get; set; }
    }
}