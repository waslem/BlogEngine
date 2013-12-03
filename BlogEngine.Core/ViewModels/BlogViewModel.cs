using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BlogEngine.Core.Models;
using System.Web;

namespace BlogEngine.Core.ViewModels
{
    public class BlogViewModel
    {
        public int UserId { get; set; }
        public int BlogId { get; set; }

        public DateTime DateCreated { get; set; }
        public string SelectedCategory { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public List<TagCheckViewModel> Tags { get; set; }

        [Required(ErrorMessage="A short blog description is required")]
        public string BlogShortDescription { get; set; }

        [Required(ErrorMessage="The blog entry is required")]
        [AllowHtml]
        //[UIHint("tinymce_jquery_full"), AllowHtml]
        public string BlogEntryText { get; set; }

        [Required(ErrorMessage="A blog title is required")]
        public string BlogTitle { get; set; }

        public HttpPostedFileBase BlogImage { get; set; }

    }
}
