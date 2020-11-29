using ModelLayer.Model;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Repository
{
    class UserRoleRepository:GenericRepository<UserRole>,IUserRolesRepository
    {
        private readonly Context context;

        public UserRoleRepository(Context context):base(context)
        {
            this.context = context;
        }
    }
}
