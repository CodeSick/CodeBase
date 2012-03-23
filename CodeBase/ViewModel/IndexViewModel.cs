using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeBase.Models;

namespace CodeBase.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
    }
}