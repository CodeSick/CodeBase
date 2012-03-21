using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeBase.Models
{
    public class File
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Article Article { get; set; }
        [Required]
        public string Filename { get; set; }
        public int Size { get; set; }
        [Required]
        public Byte[] Raw { get; set; }
    }
}