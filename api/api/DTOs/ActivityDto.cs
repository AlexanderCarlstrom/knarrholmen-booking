namespace api.DTOs
{
    public class ActivityDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        // Open time in half hours starting at 0
        public int Open { get; set; }

        // Close time in half hours starting at 0
        public int Close { get; set; }
    }
}