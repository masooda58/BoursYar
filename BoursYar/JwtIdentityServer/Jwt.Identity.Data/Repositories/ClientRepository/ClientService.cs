//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Jwt.Identity.Data.Context;
//using Jwt.Identity.Domain.Interfaces.IRepository;
//using Jwt.Identity.Domain.Models.Client;
//using Microsoft.EntityFrameworkCore;

//namespace Jwt.Identity.Data.Repositories.ClientRepository
//{
    
//   public class ClientService:IRepositoryService
//   {
//       private readonly IdentityContext _context;

//       public ClientService(IdentityContext context)
//       {
//           _context = context;
//       }


//       public async Task<IEnumerable<Client>> GetAllAsync()
//       {
//           return await _context.Clients.ToListAsync();
//       }

//        public async Task<Client> GetByClientIdAsync(string clientId)
//        {
            
//            return await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);
//        }

//        public async Task<string> AddOrUpdateClient(Client client)
//        {
//            var clienExis = await GetByClientIdAsync(client.ClientId);
//            if (clienExis != null)
//            {
//                _context.Entry(clienExis).State = EntityState.Modified;
//                await _context.SaveChangesAsync();
//                return $"Update{client.ClientName}";
//            }

//            await  _context.Clients.AddAsync(client);
//            await _context.SaveChangesAsync();
//           return $"Add {client.ClientName}";
//        }

//        public async Task<bool> DeleteClient(string clientId)
//        {
//            var client = await GetByClientIdAsync(clientId);
//            if (client != null)
//            {
//                var deleteResult= _context.Clients.Remove(client);
//                if (deleteResult != null)
//                {
//                    return true;
//                }
//            }

         

//            return false;
//        }
//   }
//}
