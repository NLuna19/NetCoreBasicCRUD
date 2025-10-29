using BECommentsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BECommentsAPI
{
    public class AplicationDbContext: DbContext
    {
        public DbSet<Comment> Comment {  get; set; }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }

    }
}
