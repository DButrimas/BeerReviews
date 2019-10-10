using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerReviews_Server.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
    }
}
