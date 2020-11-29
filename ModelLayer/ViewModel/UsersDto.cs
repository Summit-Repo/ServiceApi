using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.ViewModel
{
  public  class UsersDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public bool IsLocked { get; set; }

        public ICollection<UserRoleDto> UserRoles { get; set; }
        public ICollection<UserClaimsDto> UserClaims { get; set; }
    }
}
