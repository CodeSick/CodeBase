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
        public int FileId { get; set; }
        [Required]
        public string Filename { get; set; }
        public int Size { get; set; }
        public Byte[] Raw { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}