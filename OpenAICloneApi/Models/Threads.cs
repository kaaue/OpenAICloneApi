namespace OpenAICloneApi.Models
{
    public class ConversationThread
    {
        public string Id { get; set; }
        public string Object => "thread";
        public long CreatedAt { get; set; }
        public Dictionary<string, string> Metadata { get; set; }

        // Construtor padrão
        public ConversationThread()
        {
            Id = $"thread_{Guid.NewGuid().ToString()}";
            CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Metadata = new Dictionary<string, string>();
        }
    }

    public class Message
    {
        public string Id { get; set; }
        public string Object => "thread.message";
        public long CreatedAt { get; set; }
        public string ThreadId { get; set; }
        public string Role { get; set; }
        public string Content { get; set; }

        // Construtor padrão
        public Message(string threadId, string role, string content)
        {
            Id = $"msg_{Guid.NewGuid().ToString()}";
            CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            ThreadId = threadId;
            Role = role;
            Content = content;
        }
    }
}