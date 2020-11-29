using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer.Model
{
  public  class UserClaims
    {
        [Key]
        public int Id { get; set; }

        public int UsersId { get; set; }
        public Users Users { get; set; }

        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

    }
}
