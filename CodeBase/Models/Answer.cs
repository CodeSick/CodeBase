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
        public int Id { get; set; }
        [Required]
        public User Author { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}