using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeBase.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime? Date { get; set; }

        [ForeignKey("Author")]
        public int UserId { get; set; }
        public virtual User Author { get; set; }
        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}