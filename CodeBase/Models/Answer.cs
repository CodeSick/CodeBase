using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeBase.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        public int UserId { get; set; }
        [Required]
        public virtual User Author { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime? Date { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}