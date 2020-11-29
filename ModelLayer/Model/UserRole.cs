using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer.Model
{
 public   class UserRole
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; }


        public int RoleId { get; set; }
        public Role Role { get; set; }
        
    }
}
