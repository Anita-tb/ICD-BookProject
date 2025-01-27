using ICD.Framework.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace ICD.BookProject.Data
{
    public class BaseDbContext : DataContext
    {
        public BaseDbContext(DbContextOptions options)
            :base(options)
        {

        }
    }
}
