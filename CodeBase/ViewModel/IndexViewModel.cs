using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeBase.Models;

namespace CodeBase.ViewModel
{
    public class IndexViewModel
    {
        public String Message { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<UserWithCount> Users { get; set; }
    }

    public class UserWithCount
    {
        public User User { get; set; }
        public int Count { get; set; }
    }
}