using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.ViewModel
{
 public   class JwtDto
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
