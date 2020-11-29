using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.ViewModel
{
  public  class UserRoleDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UsersDto User { get; set; }


        public int RoleId { get; set; }
        public RoleDto Role { get; set; }
    }
}
