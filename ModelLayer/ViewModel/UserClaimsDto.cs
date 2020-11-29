using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.ViewModel
{
   public class UserClaimsDto
    {
        public int UsersId { get; set; }
        public UsersDto Users { get; set; }

        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
