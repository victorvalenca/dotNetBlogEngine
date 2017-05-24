using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models
{
    public class BlogPost
    {
        [Key]
        public int BlogPostId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title
        {
            get;
            set;
        }

        [Required]
        [StringLength(4000)]
        public string Content
        {
            get;
            set;
        }

        [DataType(DataType.Date)]
        public DateTime Posted
        {
            get;
            set;
        }
    }
}
