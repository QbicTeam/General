
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Framework.DataTypes.Model.Base;
using Framework.DataTypes.Model.Licenciamiento;
using Framework.DataTypes.Model.Infraestructura;

namespace SI_Admin.API.Data
{
    public class StoreRepository: IStoreRepository
    {
        private readonly DataContext _context;

        public StoreRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T: class
        {
            _context.Add(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<IEnumerable<Paquete>> GetPackages()
        {
           var result = await _context.Paquetes
            //.Include(a => a.Apps)
            .Where(p => p.Activo == true)
            //.FirstOrDefaultAsync(c => c.Id == id);
            //.OrderBy(c => c.NomCorto)
            .ToListAsync();

            return result;
            //Task<Paquete> GetPackage(int id);
        }
            
        public async Task<Paquete> GetPackage(int id)
        {
           var result = await _context.Paquetes
            //.Include(a => a.Apps)
            //.Where(p => p.Activo == true)
            .FirstOrDefaultAsync(p => p.Id == id);

            return result;
        }
        
    }
}