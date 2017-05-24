using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models
{
    public class Comment
    {
        [Key]
        public int CommentId
        {
            get;
            set;
        }

        [ForeignKey("BlogPostId")]
        public int BlogPostId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [StringLength(2048)]
        public string Content
        {
            get;
            set;
        }
    }
}
