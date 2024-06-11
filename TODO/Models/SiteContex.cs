using Microsoft.EntityFrameworkCore;

namespace TODO.Models
{
    public class SiteContex : DbContext
    {
        public DbSet<TaskForList> TaskForLists { get; set; }
        public DbSet<Category> Categories { get; set; }
        public SiteContex(DbContextOptions<SiteContex> options)
            : base(options)
        {

        }
    }
}
