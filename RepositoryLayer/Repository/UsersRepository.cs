using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;
using ModelLayer.ViewModel;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Repository
{
  public  class UsersRepository:GenericRepository<Users>,IUsersRepository
    {
        private readonly Context context;

        public UsersRepository(Context context):base(context)
        {
            this.context = context;
        }

        public Users GetUserLogin(UsersDto usersDto)
        {
         Users users= context.Users.Include(u=>u.UserRoles).ThenInclude(r=>r.Role).Include(u=>u.UserClaims).SingleOrDefault(u => u.UserName == usersDto.UserName);
            return users;
        }
    }
}
