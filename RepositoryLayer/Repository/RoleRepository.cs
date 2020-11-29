using ModelLayer.Model;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Repository
{
   public class RoleRepository:GenericRepository<Role>,IRoleRepository
    {
        private readonly Context context;

        public RoleRepository(Context context) : base(context)
        {
            this.context = context;
        }
    }
}
