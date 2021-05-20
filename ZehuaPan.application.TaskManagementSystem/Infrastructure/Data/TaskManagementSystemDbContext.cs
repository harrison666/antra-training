using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Infrastructure.Data
{
    public class TaskManagementSystemDbContext : DbContext
    {
        public TaskManagementSystemDbContext(DbContextOptions<TaskManagementSystemDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Task>(ConfigureTask);
            modelBuilder.Entity<TaskHistory>(ConfigureTaskHistory);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email).HasColumnType("varchar(50)");
            builder.Property(u => u.Password).HasColumnType("varchar(10)").IsRequired();
            builder.Property(u => u.Fullname).HasColumnType("varchar(50)");
            builder.Property(u => u.Mobileno).HasColumnType("varchar(50)");
        }

        private void ConfigureTask(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Title).HasColumnType("varchar(50)");
            builder.Property(t => t.Description).HasColumnType("varchar(500)");
            builder.Property(t => t.DueDate).HasColumnType("datetime");
            builder.Property(t => t.Priority).HasColumnType("char(1)");
            builder.Property(t => t.Remarks).HasColumnType("varchar(500)");

            builder.HasOne(t => t.User).WithMany(t => t.Tasks).HasForeignKey(t => t.userid);

        }

        private void ConfigureTaskHistory(EntityTypeBuilder<TaskHistory> builder)
        {
            builder.ToTable("Tasks History");
            builder.HasKey(th => th.TaskId );
            builder.Property(th => th.Title).HasColumnType("varchar(50)");
            builder.Property(th => th.Description).HasColumnType("varchar(500)");
            builder.Property(th => th.DueDate).HasColumnType("datetime");
            builder.Property(th => th.Completed).HasColumnType("datetime");
            builder.Property(th => th.Remarks).HasColumnType("varchar(500)");

            builder.HasOne(th => th.User).WithMany(th => th.TaskHistories).HasForeignKey(th => th.UserId);
        }
    }
}
