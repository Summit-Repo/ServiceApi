using ModelLayer.Model;
using ModelLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.IBusiness
{
  public  interface ILoginAccess
    {
       LoginResponse VerifyLogin(UsersDto users);

    }
}
