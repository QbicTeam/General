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
        public int PadreId { get; set; }
    }
}