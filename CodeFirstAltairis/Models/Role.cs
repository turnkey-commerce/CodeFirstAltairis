using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstAltairis.Models {
    public class Role {
        [Key]
        [Display(Name = "Role Name")]
        [Required(ErrorMessage = "Role Name is required")]
        [MaxLength(100)]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}