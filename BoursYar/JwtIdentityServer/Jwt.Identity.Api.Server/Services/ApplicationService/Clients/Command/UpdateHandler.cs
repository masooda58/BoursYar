using System.Threading;
using System.Threading.Tasks;
using Common.FrameWork.Domain.ApplicationServices;
using Jwt.Identity.Data.UnitOfWork;
using Jwt.Identity.Domain.Clients.Command;
using Jwt.Identity.Domain.Clients.Entity;
using MediatR;

namespace Jwt.Identity.Api.Server.Services.ApplicationService.Clients.Command
{
    public class UpdateHandler:AsyncRequestHandler<UpSert>
    {
        private readonly UnitOfWork _unitOfWork;

        public UpdateHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        protected override async Task Handle(UpSert request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ClientRepository.InsertAsync(request.Client);
            _unitOfWork.Save();
        }

    
    }
}
