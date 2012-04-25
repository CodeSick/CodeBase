using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeBase.ViewModel
{
    public class ArticleEditModel
    {
        [Required]
        public int ArticleId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}