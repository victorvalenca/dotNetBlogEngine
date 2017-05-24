using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment1.Models
{
    public class Role
    {
        [Key]
        public int RoleId
        {
            get;
            set;
        }

        [StringLength(50)]
        public string name
        {
            get; set;
        }
    }
}
