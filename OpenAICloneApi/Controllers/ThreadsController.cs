using Microsoft.AspNetCore.Mvc;
using OpenAICloneApi.Models;

namespace OpenAICloneApi.Controllers
{
    [Route("v1/threads")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private static readonly List<ConversationThread> _threads = new();
        private static readonly List<Message> _messages = new();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ConversationThread> CreateThread()
        {
            var thread = new ConversationThread();
            _threads.Add(thread);
            return Ok(thread);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ConversationThread> GetThread(string id)
        {
            var thread = _threads.FirstOrDefault(t => t.Id == id);
            if (thread == null) return NotFound();
            return Ok(thread);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ConversationThread> ModifyThread(string id, [FromBody] Dictionary<string, string> metadata)
        {
            var thread = _threads.FirstOrDefault(t => t.Id == id);
            if (thread == null) return NotFound();

            thread.Metadata = metadata ?? new Dictionary<string, string>();
            return Ok(thread);
        }

        [HttpPost("{threadId}/messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Message> CreateMessage(string threadId, [FromBody] Message messageRequest)
        {
            var thread = _threads.FirstOrDefault(t => t.Id == threadId);
            if (thread == null) return NotFound();

            var message = new Message(threadId, messageRequest.Role, messageRequest.Content);
            _messages.Add(message);
            return Ok(message);
        }

        [HttpGet("{threadId}/messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<object> ListMessages(string threadId)
        {
            var messages = _messages.Where(m => m.ThreadId == threadId).ToList();
            return Ok(new
            {
                @object = "list",
                data = messages,
                first_id = messages.FirstOrDefault()?.Id,
                last_id = messages.LastOrDefault()?.Id,
                has_more = false
            });
        }
    }
}