namespace OpenAICloneApi.Models
{
    public class Assistant
    {
        public string Id { get; set; }
        public string Object => "assistant";
        public string Name { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string Instructions { get; set; }
        public List<string> Tools { get; set; }
        public long CreatedAt { get; set; }
    }

    public class AssistantRequest
    {
        public string Model { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public List<string> Tools { get; set; }
    }
}