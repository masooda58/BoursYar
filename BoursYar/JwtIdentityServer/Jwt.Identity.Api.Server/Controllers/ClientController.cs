using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jwt.Identity.Api.Server.Resources;
using Jwt.Identity.Data.UnitOfWork;
using Jwt.Identity.Domain.Clients.Command;
using Jwt.Identity.Domain.Clients.Entity;

using Jwt.Identity.Framework.Response;
using Jwt.Identity.Framework.Tools.PersianErrorHandelSqlException;
using MediatR;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jwt.Identity.Api.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        public ClientController(UnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        // GET: api/<ClientController>
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Client>> GetAll()
        {

                return  _unitOfWork.ClientRepository.Get();
         
       
        
        }

        // GET api/<ClientController>/5
        //[HttpPost("GetByClientId")]
        //public async Task<Client> GetByClientId(int clientId)
        //{

        //    return Ok("await _clientService.FindByConditionAsync(c => c.ClientId == clientId);");
        //}
        [HttpPost("GetByClientName")]
        public async Task<ActionResult> GetByClientName(string clientName)
        {

            return Ok(await _unitOfWork.ClientRepository.GetByAsync(clientName));
        }


        // PUT api/<ClientController>/5
        [HttpPut("Add")]
        public async Task<ActionResult> AddClient([FromBody] UpSert client)
        {
            try
            {
               await _mediator.Send(client);
                 //await _unitOfWork.ClientRepository.InsertAsync(client);
                 //_unitOfWork.Save();
                return Ok(client);
            }
            catch (Exception e)
            {
                throw ExceptionMessage.GetPerisanSqlExceptionMessage(e);
            }
       
        }
        [HttpPut("Update")]
        public async Task<ActionResult> UpdateClient([FromBody] Client client)
        {
            _unitOfWork.ClientRepository.Update(client);
            return Ok("Client Update");
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("DeleteClient")]
        public async Task<ActionResult> DeleteClient(string clientName)
        {


            await _unitOfWork.ClientRepository.DeleteAsync(clientName);
            return Ok("delete");

        }
    }
}

