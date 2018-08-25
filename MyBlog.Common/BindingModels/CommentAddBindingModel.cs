using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Common.BindingModels
{
    public class CommentAddBindingModel
    {
        public string Username { get; set; }

        [Required]
        [MinLength(4)]
        public string Comment { get; set; }

    }
}
