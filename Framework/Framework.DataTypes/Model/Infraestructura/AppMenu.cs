namespace Framework.DataTypes.Model.Infraestructura
{
    public class AppMenu
    {
        public int Id { get; set; }
        public int AplicacionId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string URL { get; set; }
        public AppMenu Padre { get; set; }
        public int? PadreId { get; set; }
        public string ClaseIcono { get; set; }
        /// Breadcrum
        public string RutaNavegacion { get; set; }
        public string Notas { get; set; }
        public string NotasTipo { get; set; }
        public string NotasPie { get; set; }
        public string Componente { get; set; }
        /// Aun sin uso, los permisos se deberan separar por un delimitador (por definir)
        public string PermisosEspeciales { get; set; }
        public string Estatus { get; set; }
    }
}