//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Jwt.Identity.Api.Server.Resources;
//using Jwt.Identity.Domain.Client;
//using Jwt.Identity.Domain.Interfaces.IRepository;
//using Jwt.Identity.Framework.Response;
//using Newtonsoft.Json;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace Jwt.Identity.Api.Server.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ClientController : ControllerBase
//    {
//        private readonly IRepositoryService<Client> _clientService;
//        public ClientController(IRepositoryService<Client> clientService)
//        {
//            _clientService = clientService;
//        }
//        // GET: api/<ClientController>
//        [HttpGet("GetAll")]
//        public async Task<IEnumerable<Client>> GetAll()
//        {
//            return await _clientService.GetAllAsync();
//        }

//        // GET api/<ClientController>/5
//        [HttpPost("GetByClientId")]
//        public async Task<Client> GetByClientId(int clientId)
//        {

//            return await _clientService.FindByConditionAsync(c=>c.ClientId==clientId);
//        }
//        [HttpPost("GetByClientName")]
//        public async Task<Client> GetByClientName(string clientName)
//        {

//            return await _clientService.FindByConditionAsync(c=>c.ClientName==clientName);
//        }


//        // PUT api/<ClientController>/5
//        [HttpPut("Add")]
//        public async Task<ActionResult> AddClient([FromBody]Client client)
//        {
//            var addResult=await _clientService.Add(client);
//            return addResult.Successed ? Ok(addResult) : Conflict(addResult);
//        }
//        [HttpPut("Update")]
//        public async Task<ActionResult> UpdateClient([FromBody]Client client)
//        {
//            var clientExist = await _clientService.FindByConditionAsync(c => c.ClientId == client.ClientId);
//            if (clientExist != null)
//            {
//             var updatClient= await _clientService.Update(client) ;
//             return updatClient.Successed ? Ok(updatClient): Conflict(updatClient);
//            }

//            return  BadRequest(new ResultResponse(false,MessageRes.ClientNoExist));
//        }

//        // DELETE api/<ClientController>/5
//        [HttpDelete("DeleteClient")]
//        public async  Task<ActionResult> DeleteClient( int clientId)
//        {
//            var client = await _clientService.FindByConditionAsync(c => c.ClientId == clientId);
//           if (client!=null)
//           {
//               var deleteClient = await _clientService.Delete(client);
//             return deleteClient.Successed ? Ok(deleteClient): Conflict(deleteClient);  ;

//           }
//            return   BadRequest(new ResultResponse(false,MessageRes.ClientNoExist));
//        }
//    }
//}

