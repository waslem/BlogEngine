using System;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Core.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
