using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.Property(eb => eb.State).IsRequired();
                eb.Property(eb => eb.Area).HasColumnType("varchar200");
                eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
                eb.Property(wi => wi.EndDate).HasPrecision(3);
                eb.Property(wi => wi.Efford).HasColumnType("decimal(5,2)");
                eb.Property(wi => wi.Activity).HasMaxLength(200);
                eb.Property(wi => wi.RemaningWork).HasPrecision(14,2);

            });

        }
        
    }
}
