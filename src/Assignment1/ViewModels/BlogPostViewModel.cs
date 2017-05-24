using Assignment1.Models;
using System.Collections.Generic;

namespace Assignment1.ViewModels
{
    public class BlogPostViewModel
    {
        public Models.BlogPost blogPost { get; set; }
        public List<CommentViewModel> comments { get; set; } // Set representing all the comments tied to the blog post
        public Models.User user { get; set; }
    }

    // Represents a single comment in the specific blog post
    public class CommentViewModel
    {
        public Comment comment { get; set; }
        public string authorName { get; set; }
    }
}
