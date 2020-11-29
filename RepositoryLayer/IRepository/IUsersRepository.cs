using ModelLayer.Model;
using ModelLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.IRepository
{
  public  interface IUsersRepository:IGenericRepository<Users>
    {
        Users GetUserLogin(UsersDto usersDto);
    }
}
