using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Common.ViewModels
{
    public class FeedbackViewModel
    {
        public FeedbackViewModel()
        {
            this.New = new List<FeedbackDetailsViewModel>();
            this.Read = new List<FeedbackDetailsViewModel>();
        }

        public ICollection<FeedbackDetailsViewModel> New { get; set; }

        public ICollection<FeedbackDetailsViewModel> Read { get; set; }
    }
}
