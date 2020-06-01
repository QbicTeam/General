using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.DataTypes.Model.Base;
using Framework.DataTypes.Model.Licenciamiento;
using Framework.DataTypes.Model.Infraestructura;

//using Reviews.API.Models;


namespace SI_Admin.API.Data
{
    public interface IQAdminRepository
    {
         
         void Add<T>(T entity) where T: class;
         Task<bool> SaveAll();
         // Task<IEnumerable<User>> GetUsers();
         //Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<Cliente> GetCliente(int id);
         Task<IEnumerable<Cliente>> GetClientes();
         Task<IEnumerable<ClienteActualizacion>> GetActualizacionesClientes(int status);
         Task<ClienteActualizacion> GetActualizacionCliente(int id);         
         Task<Paquete> GetPaquete(int id);
         Task<IEnumerable<Paquete>> GetPaquetes();
    }
}