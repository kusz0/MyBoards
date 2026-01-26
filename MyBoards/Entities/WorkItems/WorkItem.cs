using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

namespace MyBoards.Entities.WorkItems
{
    public abstract class WorkItem
    {
        [Key]
        public int Id { get; set; }
        public string Area { get; set; }

        public string IterationPath { get; set; }
        public int Priority { get; set; }


        public List<Comment> Comments { get; set; } = new List<Comment>();

        public User Author { get; set; }
        public Guid AuthorId { get; set; }

        public List<Tag> Tags { get; set; }

        public int StateId { get; set; }
        public State State { get; set; }

    }
}
