
using Microsoft.AspNetCore.Mvc;
using OpenAICloneApi.Models;

namespace OpenAICloneApi.Controllers
{
    [Route("v1/assistants")]
    [ApiController]
    public class AssistantsController : ControllerBase
    {
        private static readonly List<Assistant> _assistants = new();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Assistant> CreateAssistant([FromBody] AssistantRequest request)
        {
            var assistant = new Assistant
            {
                Id = $"asst_{Guid.NewGuid().ToString()}",
                Name = request.Name,
                Description = request.Description,
                Model = request.Model,
                Instructions = request.Instructions,
                Tools = request.Tools,
                CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };
            _assistants.Add(assistant);
            return Ok(assistant);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<object> ListAssistants(
            [FromQuery] int? limit = 20,
            [FromQuery] string order = "desc",
            [FromQuery] string? after = null,
            [FromQuery] string? before = null)
        {
            var response = new
            {
                @object = "list",
                data = _assistants,
                first_id = _assistants.FirstOrDefault()?.Id,
                last_id = _assistants.LastOrDefault()?.Id,
                has_more = false
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Assistant> GetAssistant(string id)
        {
            var assistant = _assistants.FirstOrDefault(a => a.Id == id);
            if (assistant == null) return NotFound();
            return Ok(assistant);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Assistant> ModifyAssistant(string id, [FromBody] AssistantRequest request)
        {
            var assistant = _assistants.FirstOrDefault(a => a.Id == id);
            if (assistant == null) return NotFound();

            assistant.Name = request.Name ?? assistant.Name;
            assistant.Description = request.Description ?? assistant.Description;
            assistant.Model = request.Model ?? assistant.Model;
            assistant.Instructions = request.Instructions ?? assistant.Instructions;
            assistant.Tools = request.Tools ?? assistant.Tools;

            return Ok(assistant);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<object> DeleteAssistant(string id)
        {
            var assistant = _assistants.FirstOrDefault(a => a.Id == id);
            if (assistant == null) return NotFound();

            _assistants.Remove(assistant);
            return Ok(new { id = id, @object = "assistant", deleted = true });
        }
    }
}