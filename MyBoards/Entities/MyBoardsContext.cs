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
                eb.Property(x => x.State).IsRequired();
                eb.Property(x => x.State).HasMaxLength(50);
                eb.Property(x => x.Area).HasColumnType("varchar200");
                eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
                eb.Property(wi => wi.EndDate).HasPrecision(3);
                eb.Property(wi => wi.Efford).HasColumnType("decimal(5,2)");
                eb.Property(wi => wi.Activity).HasMaxLength(200);
                eb.Property(wi => wi.RemaningWork).HasPrecision(14,2);
                eb.Property(wi => wi.Priority).HasDefaultValue(1);

                eb.HasMany(w => w.Comments).WithOne(c => c.WorkItem).HasForeignKey(c => c.WorkItemId);
                eb.HasOne(w => w.Author).WithMany(u => u.WorkItems).HasForeignKey(w => w.AuthorId);


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
            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");
                eb.Property(x => x.UpdateTime).ValueGeneratedOnUpdate();
            });

            // Relations
            modelBuilder.Entity<User>().HasOne(u => u.Address).WithOne(a => a.User).HasForeignKey<Address>(a => a.UserId);

            modelBuilder.Entity<State>(st =>
            {
                st.Property(x => x.Name).IsRequired().HasMaxLength(50);
            });
            

        }
        
    }
}
