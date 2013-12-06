using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace BlogEngine.Core.Models
{
    
    public class User
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<BlogEntry> BlogEntries { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        //public User()
        //{
        //    Blogs = new List<BlogEntry>();
        //}
    }
}
