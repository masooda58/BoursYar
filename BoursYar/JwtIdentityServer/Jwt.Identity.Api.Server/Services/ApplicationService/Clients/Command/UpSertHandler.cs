using Jwt.Identity.Data.UnitOfWork;
using Jwt.Identity.Domain.Clients.Command;
using Jwt.Identity.Framework.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jwt.Identity.Api.Server.Services.ApplicationService.Clients.Command
{
    public class UpSertHandler : IRequestHandler<UpSert, ResultResponse>
    {
        private readonly UnitOfWork _unitOfWork;

        public UpSertHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ResultResponse> Handle(UpSert request, CancellationToken cancellationToken)
        {
            var existUser = await _unitOfWork.ClientRepository.GetByIdNotraking(request.Client.ClientId);

            if (existUser != null)
            {

                _unitOfWork.ClientRepository.Update(request.Client);
                _unitOfWork.Save();
                return new ResultResponse(true, $"Update client ={request.Client.ClientName}");
            }
            await _unitOfWork.ClientRepository.InsertAsync(request.Client);
            _unitOfWork.Save();
            return new ResultResponse(true, $"Add client={request.Client.ClientName}", request.Client);
        }
    }
}
