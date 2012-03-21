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
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }

    }
}