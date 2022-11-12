using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jwt.Identity.Data.UnitOfWork;
using Jwt.Identity.Domain.Clients.Command;
using Jwt.Identity.Framework.Response;
using MediatR;

namespace Jwt.Identity.Api.Server.Services.ApplicationService.Clients.Command
{
    public class DeletClientHandler:IRequestHandler<DeletClient,bool>
    {
        private readonly UnitOfWork _unitOfWork;

        public DeletClientHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletClient request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ClientRepository.DeleteAsync(request.ClientName);
            _unitOfWork.Save();
            return true;

        }
    }
}
