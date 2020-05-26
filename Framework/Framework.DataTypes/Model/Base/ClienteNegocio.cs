namespace Framework.DataTypes.Model.Base
{
    public class ClienteNegocio
    {
        // TODO: Definir donde se almacena el Script de las tablas
        public int Id { get; set; }
        public string Server { get; set; }
        public string DataBase { get; set; }
        public string UserDB { get; set; }
        public string PWD { get; set; }
        public string Puerto { get; set; }
        public string NombreNegocio { get; set; }
        public int NegocioID { get; set; }
        public bool IsDBControl { get; set; }
    }
}