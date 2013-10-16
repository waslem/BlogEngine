using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Models
{
    public class BlogImage
    {
        public int BlogImageId { get; set; }
        public string ImagePath { get; set; }
    }
}
