using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyBoards.Entities.WorkItems;
using System.Reflection.Metadata.Ecma335;
using Task = MyBoards.Entities.WorkItems.Task;

namespace MyBoards.Entities
{
    public class MyBoardsContext : DbContext
    {
        public MyBoardsContext(DbContextOptions<MyBoardsContext> options) : base(options) 
        {
            
        }

        public DbSet<WorkItem> WorkItemsDb{ get; set; }
        public DbSet<Epic> EpicDb{ get; set; }
        public DbSet<Task> TaskDb{ get; set; }
        public DbSet<Issue> IssueDb { get; set; }
        public DbSet<User> UsersDb { get; set; }
        public DbSet<Tag> TagsDb { get; set; }
        public DbSet<Comment> CommentsDb { get; set; }
        public DbSet<Address> AddressesDb { get; set; }
        public DbSet<State> StatesDb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.Property(x => x.Area).HasColumnType("varchar200");
                eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
                eb.Property(wi => wi.Priority).HasDefaultValue(1);

                eb.HasMany(w => w.Comments).WithOne(c => c.WorkItem).HasForeignKey(c => c.WorkItemId);
                eb.HasOne(w => w.Author).WithMany(u => u.WorkItems).HasForeignKey(w => w.AuthorId);
                eb.HasOne(s => s.State).WithMany(u => u.WorkItems).HasForeignKey(w =>w.StateId);

                eb.HasMany(w => w.Tags).WithMany(t => t.WorkItems).UsingEntity<WorkItemTag>(
                    w => w.HasOne(wit => wit.Tag).WithMany().HasForeignKey(w => w.TagId),
                    w => w.HasOne(wit => wit.WorkItemT).WithMany().HasForeignKey(w => w.WorkItemId),
                    wit =>
                    {
                        wit.HasKey(x => new { x.TagId, x.WorkItemId });
                        wit.Property(x => x.PublicationDate).HasDefaultValueSql("getutcdate()");
                    }
                    );
            });
            modelBuilder.Entity<Epic>(e =>
            {
                e.Property(x => x.EndDate).HasPrecision(3);
            });
            modelBuilder.Entity<Task>(t =>
            {
                t.Property(x => x.Activity).HasMaxLength(200);
                t.Property(x => x.RemaningWork).HasPrecision(14,2);
            });
            modelBuilder.Entity<Issue>(i =>
            {
                i.Property(x => x.Efford).HasColumnType("decimal(5,2)");
            });
            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");
                eb.Property(x => x.UpdateTime).ValueGeneratedOnUpdate();
            });

            // Relations
            modelBuilder.Entity<User>().HasOne(u => u.Address).WithOne(a => a.User).HasForeignKey<Address>(a => a.UserId);

            modelBuilder.Entity<State>(st =>
            {
                st.Property(x => x.Value).IsRequired().HasMaxLength(50);
            });
            

        }
        
    }
}
