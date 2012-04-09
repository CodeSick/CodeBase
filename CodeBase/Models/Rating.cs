using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeBase.Models
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }
        [Required]
        public int Value { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true,
       DataFormatString = "{0:d/M/yyyy}")]
        public DateTime? Date { get; set; }

        [ForeignKey("Author")]
        public int UserId { get; set; }
        public virtual User Author { get; set; }
        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}