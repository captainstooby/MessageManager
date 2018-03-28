using System.Collections.Generic;
using MessageManager.Domain.Import;
using MessageManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace MessageManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Messages")]
    public class MessagesController : Controller
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        // GET: api/Messages
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            return _messageService.GetAllMessages();
        }

        // GET: api/Messages/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Messages
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Messages/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
