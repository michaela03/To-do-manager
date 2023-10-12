using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ToDoManagerContext : DbContext
    {
        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Task> Tasks { get; set; }

        public virtual DbSet<WorkLog> WorkLogs { get; set; }
        public object Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Projects)
                .WithOne(e => e.Lead)
                .HasForeignKey(e => e.LeadId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tasks)
                .WithOne(e => e.Assignee)
                .HasForeignKey(e => e.AssigneeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasMany(e => e.WorkLogs)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Tasks)
                .WithOne(e => e.Project)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>()
                .HasData(new User()
                {
                    Id = 1,
                    FirstName = "Administrator",
                    LastName = "Administrator",
                    Username = "admin",
                    Password = "adminpass",
                    IsAdmin = true
                });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Dat Source=(localdb)\ProjectsV13;Initial Catalog=ToDoManagerDB;");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
