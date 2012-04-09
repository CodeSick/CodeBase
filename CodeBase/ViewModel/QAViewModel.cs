using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeBase.Models;

namespace CodeBase.ViewModel
{
    public class QAViewModel
    {
        public Question question { get; set; }
        public Answer answer { get; set; }
        public IEnumerable<Answer> answers { get; set; }
    }

    
}