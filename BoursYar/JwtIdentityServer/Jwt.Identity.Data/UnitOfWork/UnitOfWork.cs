using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwt.Identity.Data.Context;
using Jwt.Identity.Data.IntialData;
using Jwt.Identity.Data.Repositories.ClientRepository;
using Jwt.Identity.Domain.Clients.Data;

namespace Jwt.Identity.Data.UnitOfWork
{
    public class UnitOfWork:IDisposable
    {
        private readonly IdentityContext _context;
        private ClientRepositoryService _client;

        public UnitOfWork(IdentityContext context)
        {
            _context = context;
        }

        public ClientRepositoryService ClientRepository
        {
            get
            {
                if (_client == null)
                {
                    _client = new ClientRepositoryService(_context);
                }

                return _client;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
