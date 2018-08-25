using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Common.BindingModels
{
    public class PostCreationBindingModel
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        public string Body { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
