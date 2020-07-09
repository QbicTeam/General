using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.DataTypes.Model.Base;
using Framework.DataTypes.Model.Licenciamiento;
using Framework.DataTypes.Model.Infraestructura;

namespace SI_Admin.API.Data
{
    public interface IStoreRepository
    {
         void Add<T>(T entity) where T: class;
         Task<bool> SaveAll();
         // Task<IEnumerable<User>> GetUsers();
         //Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<IEnumerable<Paquete>> GetPackages();
         Task<Paquete> GetPackage(int id);
    }
}