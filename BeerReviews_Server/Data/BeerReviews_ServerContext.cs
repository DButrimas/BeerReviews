using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeerReviews_Server.Models;

namespace BeerReviews_Server.Models
{
    public class BeerReviews_ServerContext : DbContext
    {
        public BeerReviews_ServerContext (DbContextOptions<BeerReviews_ServerContext> options)
            : base(options)
        {
        }

        public DbSet<BeerReviews_Server.Models.Beer> Beer { get; set; }

        public DbSet<BeerReviews_Server.Models.Comment> Comment { get; set; }

        public DbSet<BeerReviews_Server.Models.User> User { get; set; }
    }
}
