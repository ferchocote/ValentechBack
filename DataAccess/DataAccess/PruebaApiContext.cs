using Microsoft.EntityFrameworkCore;
using PruebaAppApi.DataAccess.Entities;

namespace PruebaAppApi.DataAccess.DataAccess
{
    public class PruebaApiContext : ValentechCoreContext
    {
        public PruebaApiContext(CustomDbSettings options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



        }
    }
}
