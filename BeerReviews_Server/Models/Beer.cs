using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerReviews_Server.Models
{
    public class Beer
    {
        public long Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Brand { get; set; }
        [Required]
        public double Alcohol { get; set; }
        [Required]
        public double Size { get; set; }
        [Required]
        public double Price { get; set; }
        public string LogoUrl { get; set; }
        [StringLength(250)]
        public string Description { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
