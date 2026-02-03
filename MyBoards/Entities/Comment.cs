using MyBoards.Entities.WorkItems;

namespace MyBoards.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateTime { get; set; }

        public WorkItem WorkItem { get; set; }
        public int WorkItemId { get; set; }


        public Guid AuthorId { get; set; }
        public User Author { get; set; }
    }
}