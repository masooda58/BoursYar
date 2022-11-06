using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jwt.Identity.Api.Server.Resources;
using Jwt.Identity.Data.UnitOfWork;
using Jwt.Identity.Domain.Clients.Entity;

using Jwt.Identity.Framework.Response;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jwt.Identity.Api.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public ClientController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<ClientController>
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Client>> GetAll()
        {

                return await _unitOfWork.ClientRepository.GetAllAsync();
         
       
        
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

            return Ok(await _unitOfWork.ClientRepository.GetByClientNameAsync(clientName));
        }


        // PUT api/<ClientController>/5
        [HttpPut("Add")]
        public async Task<ActionResult> AddClient([FromBody] Client client)
        {
            var addResult = await _unitOfWork.ClientRepository.Add(client);
            return addResult.Successed ? Ok(addResult) : Conflict(addResult);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> UpdateClient([FromBody] Client client)
        {
            return Ok(await _unitOfWork.ClientRepository.Update(client));
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("DeleteClient")]
        public async Task<ActionResult> DeleteClient(string clientName)
        {


            var deleteClient = await _unitOfWork.ClientRepository.DeleteClientNameAsync((clientName));
                return deleteClient.Successed ? Ok(deleteClient) : Conflict(deleteClient); ;

        }
    }
}

