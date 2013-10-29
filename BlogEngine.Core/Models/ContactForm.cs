using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Models
{
    public class ContactForm
    {
        [Required]
        [EmailAddress(ErrorMessage = "You must enter a valid email address")]
        public string From { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
