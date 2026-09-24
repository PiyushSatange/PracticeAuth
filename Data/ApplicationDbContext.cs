using Microsoft.EntityFrameworkCore;
using PracticeAuth.Models.Entity;

namespace PracticeAuth.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
}