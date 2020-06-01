using Microsoft.EntityFrameworkCore;
using Framework.DataTypes.Model.Base;
using Framework.DataTypes.Model.Licenciamiento;
using Framework.DataTypes.Model.Infraestructura;

namespace SI_Admin.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) {}

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteNegocio> ClienteNegocios { get; set; }

        public DbSet<Aplicacion> Aplicaciones { get; set; }
        public DbSet<AppMenu> Menus { get; set; }
        
        public DbSet<ClienteActualizacion> ClienteActualizaciones { get; set; }
        public DbSet<ClienteActualizacionNegocio> ClienteActualizacionNegocios { get; set; }
        public DbSet<ClienteActualizacionApp> ClienteActualizacionApps { get; set; }
        public DbSet<Licencia> Licencias { get; set; }
        public DbSet<LicenciaApp> LicenciaApps { get; set; }
        public DbSet<Paquete> Paquetes { get; set; }
        public DbSet<PaqueteApp> PaqueteApps { get; set; }
       
    }
}