using Microsoft.EntityFrameworkCore;
using MyDigitalResumee.Model.Entity;

namespace MyDigitalResumee.Api.Model
{
    public class MyDigitalResumeeContext : DbContext
    {
        public MyDigitalResumeeContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User>? Users { get; set; }
    }
}