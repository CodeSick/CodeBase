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
        public int Id { get; set; }
        [Required]
        public User Author { get; set; }
        [Required]
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}