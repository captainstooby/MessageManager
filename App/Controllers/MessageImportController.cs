using MessageManager.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MessageManager.Controllers
{
    [Produces("application/json")]
    [Route("api/MessageImport")]
    public class MessageImportController : Controller
    {
        private readonly IMessageImportService _messageImportService;
        private readonly IValidatorService _validatorService;

        public MessageImportController(IMessageImportService messageImportService,
            IValidatorService validatorService)
        {
            _messageImportService = messageImportService;
            _validatorService = validatorService;
        }

        // POST: api/MessageImport
        [HttpPost]
        public ObjectResult Post([FromBody]string value)
        {
            if (_validatorService.IsValidDirectory(value) == false)
                return StatusCode((int)HttpStatusCode.NotFound, "It appears the directory provided does not exist");

            var results = _messageImportService.ImportMessages(value);

            if (results.IsError)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, results);
            }

            return Ok(results.SuccessMessage);
        }
    }
}
