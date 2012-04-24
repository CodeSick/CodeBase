using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace CodeBase.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        public string Gravatar { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true,
       DataFormatString = "{0:d/M/yyyy}")]
        public DateTime? JoinDate { get; set; }

        [NotMapped]
        public MembershipUser MembershipUser { get { return Membership.GetUser(Username); } }
        [NotMapped]
        public IEnumerable<String> Roles { get { return System.Web.Security.Roles.GetRolesForUser(Username); } }



        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Question> Questions { get; set; }

        [InverseProperty("Subscribers")]
        public virtual ICollection<Question> SubscritionQuestions { get; set; }
        [InverseProperty("Subscribers")]
        public virtual ICollection<Article> SubscriptionArticles { get; set; }

        //Optional Facebook user id
        public int FbId { get; set; }
    }


}