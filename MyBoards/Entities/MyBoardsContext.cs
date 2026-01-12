using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace MyBoards.Entities
{
    public class MyBoardsContext : DbContext
    {
        public MyBoardsContext(DbContextOptions<MyBoardsContext> options) : base(options) 
        {
            
        }

        public DbSet<WorkItem> WorkItemsDb{ get; set; }
        public DbSet<User> UsersDb { get; set; }
        public DbSet<Tag> TagsDb { get; set; }
        public DbSet<Comment> CommentsDb { get; set; }
        public DbSet<Address> AddressesDb { get; set; }

        
    }
}
