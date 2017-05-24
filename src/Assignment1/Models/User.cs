using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment1.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [ForeignKey("RoleId")]
        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public String FirstName
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public String LastName
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public String EmailAddress
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public string Password
        {
            get;
            set;
        }
    }
}
