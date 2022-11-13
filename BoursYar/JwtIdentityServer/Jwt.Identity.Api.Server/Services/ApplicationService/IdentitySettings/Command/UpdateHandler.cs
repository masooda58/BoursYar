using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jwt.Identity.Api.Server.Resources;
using Jwt.Identity.Data.UnitOfWork;
using Jwt.Identity.Domain.IdentityPolicy.Command;
using Jwt.Identity.Domain.IdentityPolicy.Entity;
using Jwt.Identity.Framework.Response;
using Jwt.Identity.Framework.Tools;
using MediatR;

namespace Jwt.Identity.Api.Server.Services.ApplicationService.IdentitySettings.Command
{
    public class UpdateHandler:IRequestHandler<UpdatePolicy,ResultResponse>
    {
        private readonly UnitOfWork _unitOfWork;

        public UpdateHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ResultResponse> Handle(UpdatePolicy request, CancellationToken cancellationToken)
        {

            if (_unitOfWork.IdentitySettingPolicy.SettingExist(request.IdentitySetting.Id))
            {
                _unitOfWork.IdentitySettingPolicy.UpdateSetting(request.IdentitySetting);
                _unitOfWork.Save();
                return new ResultResponse(true, "Update Error", request.IdentitySetting);

            }

            throw new Exception(ErrorRes.EntityNotExist);



        }
    }
}
