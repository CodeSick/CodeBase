using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeBase.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User Author { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public Category Category { get; set; }

        public DateTime? Date { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}