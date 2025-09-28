using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGE.Core.Entities;

namespace SGE.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    
     protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configuration Department
        builder.Entity<Department>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
            entity.Property(d => d.Code).IsRequired().HasMaxLength(10);
            entity.HasIndex(d => d.Code).IsUnique();
        });

        // Configuration Employee
        builder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            
            entity.HasIndex(e => e.Email).IsUnique();
            
            entity.HasOne(e => e.Department)
                  .WithMany(d => d.Employees)
                  .HasForeignKey(e => e.DepartmentId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuration Attendance
        builder.Entity<Attendance>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.WorkedHours).HasColumnType("decimal(5,2)");
            entity.Property(a => a.OvertimeHours).HasColumnType("decimal(5,2)");
            
            entity.HasOne(a => a.Employee)
                  .WithMany(e => e.Attendances)
                  .HasForeignKey(a => a.EmployeeId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasIndex(a => new { a.EmployeeId, a.Date }).IsUnique();
        });

        // Configuration LeaveRequest
        builder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(lr => lr.Id);
            entity.Property(lr => lr.Reason).HasMaxLength(500);
            entity.Property(lr => lr.ManagerComments).HasMaxLength(500);
            entity.Property(lr => lr.LeaveType).IsRequired().HasConversion<string>();
            entity.Property(lr => lr.Status).IsRequired().HasConversion<string>();

            entity.HasOne(lr => lr.Employee)
                  .WithMany(e => e.LeaveRequests)
                  .HasForeignKey(lr => lr.EmployeeId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(u => u.FirstName).HasMaxLength(50);
            entity.Property(u => u.LastName).HasMaxLength(50);
            entity.Property(u => u.CreatedAt);
        });
        
        builder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(rt => rt.Id);
            entity.Property(rt => rt.Token).IsRequired().HasMaxLength(500);
            entity.Property(rt => rt.CreatedAt);
                
            entity.HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                      
            entity.HasIndex(rt => rt.Token).IsUnique();
            entity.HasIndex(rt => new { rt.UserId, rt.CreatedAt });
        });
        
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "Manager", NormalizedName = "MANAGER" },
            new IdentityRole { Id = "3", Name = "User", NormalizedName = "USER" }
        );

    }
}