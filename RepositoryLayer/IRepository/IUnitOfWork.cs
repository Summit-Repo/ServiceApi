using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.IRepository
{
  public  interface IUnitOfWork:IDisposable
    {
       ITestRepository TestRepository { get;}
       IErrorDetailRepository ErrorDetailRepository { get; }

        IUsersRepository UsersRepository { get;  }
        
        IRoleRepository RoleRepository { get; }
        IUserRolesRepository UserRolesRepository { get;  }
        IUserClaimsRepository UserClaimsRepository { get;  }

        int Complete();
    }
}
