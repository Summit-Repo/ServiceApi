using ModelLayer.Model;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Repository
{
  public  class UserClaimsRepository:GenericRepository<UserClaims>,IUserClaimsRepository
    {
        private readonly Context context;

        public UserClaimsRepository(Context context) : base(context)
        {
            this.context = context;
        }
    }
}
