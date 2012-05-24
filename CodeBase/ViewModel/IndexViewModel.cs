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
        public IEnumerable<QuestionsWithCount> Questions { get; set; }
        public IEnumerable<ArticleRating> ArticlesRating { get; set; }
    }

    public class UserWithCount
    {
        public User User { get; set; }
        public int Count { get; set; }
    }

    public class QuestionsWithCount
    {
        public Question Question { get; set; }
        public int Count { get; set; }
    }

    public class ArticleRating
    {
        public Article Article { get; set; }
        public float Rating { get; set; }
    }
}