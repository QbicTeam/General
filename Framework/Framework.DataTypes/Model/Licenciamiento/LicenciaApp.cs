using Framework.DataTypes.Model.Infraestructura;

namespace Framework.DataTypes.Model.Licenciamiento
{
    public class LicenciaApp
    {
        public int Id { get; set; }
        public Aplicacion App { get; set; }
        public int AppId { get; set; }
    }
}