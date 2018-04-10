
namespace my8.Api.Models.Neo4j
{
    public class Page
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public string Url { get; set; }
        public double Rate { get; set; }
        public int Follows { get; set; }
        public int PageIPoint { get; set; } //Page interaction point
        public string Title { get; set; }
    }
}
