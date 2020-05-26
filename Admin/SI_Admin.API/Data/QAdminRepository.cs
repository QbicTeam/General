
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Framework.DataTypes.Model.Base;
using Framework.DataTypes.Model.Licenciamiento;
using Framework.DataTypes.Model.Infraestructura;


namespace SI_Admin.API.Data
{
    public class QAdminRepository: IQAdminRepository
    {
        private readonly DataContext _context;

        public QAdminRepository(DataContext context)
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


        public async Task<Cliente> GetCliente(int id)
        {
           var cliente = await _context.Clientes
            .Include(n => n.Negocios)
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync(c => c.Id == id);

            return cliente;
        }

        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            var clientes = await _context.Clientes
            .Include(n => n.Negocios)
            //.Where(c => c.Id == id)
            .OrderBy(c => c.NomCorto)
            .ToListAsync();

            return clientes;
        }

        public async Task<IEnumerable<ClienteActualizacion>> GetActualizacionesClientes(int status)
        {
            var actualizaciones = await _context.ClienteActualizaciones
            .Include(c => c.Cliente)
            .Where(a => a.Status == status)
            .OrderBy(c => c.Cliente.NomCorto)
            .ToListAsync();

            return actualizaciones;
        }

        public async Task<ClienteActualizacion> GetActualizacionCliente(int id)
        {
            var actualizacion = await _context.ClienteActualizaciones
            .Include(c => c.Cliente)
            .Include(a => a.Apps)
            .Include(n => n.Negocios)
            //.Where(ca => ca.Id == id)
            .OrderBy(ca => ca.Cliente.NomCorto)
            .FirstOrDefaultAsync(ca => ca.Id == id);

            return actualizacion;
        }
    }
}