using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Common.ViewModels
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }

        public ICollection<SearchDetailsViewModel> Results { get; set; }
    }
}
