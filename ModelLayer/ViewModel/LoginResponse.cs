using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.ViewModel
{
    public class LoginResponse
    {
        public string token { get; set; }

        public UsersDto users {get;set;}
    }
}
