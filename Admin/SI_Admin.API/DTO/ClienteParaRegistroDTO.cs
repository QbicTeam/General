using System;
using System.Collections.Generic;

using Framework.DataTypes.Model.Base;
using Framework.DataTypes.Model.Licenciamiento;

namespace SI_Admin.API.DTO
{
    public class ClienteParaRegistroDTO
    {

        // Cliente
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string email { get; set; }
        // public ICollection<ClienteNegocio> Negocios { get; set; }
        // public Licencia Licencia { get; set; }
        public string NomEmpresa { get; set; }
        public string NomCorto { get; set; }
        public string RFC { get; set; }
        public string Domicilio { get; set; }

        // Paquete
        public int PaqueteId { get; set; }
    }
}