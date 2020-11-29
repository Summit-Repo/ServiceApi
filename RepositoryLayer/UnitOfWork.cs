using RepositoryLayer.IRepository;
using RepositoryLayer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer
{
  public  class UnitOfWork : IUnitOfWork
    {
        private readonly Context context;
        private ErrorDetailRepository _ErrorDetailRepository;
        private TestRpository _TestRpository;
        private UsersRepository _usersRepository;
        private UserRoleRepository _userRoleRepository;
        private RoleRepository _roleRepository;
        private UserClaimsRepository _userClaimsRepository;
        //  private readonly Context context=new Context();

        public UnitOfWork(Context context)
        {
            this.context = context;
            TestRepository = new TestRpository(this.context);
            
        }
        public ITestRepository TestRepository
        {
            get
            {
                if (_TestRpository == null) { _TestRpository = new TestRpository(this.context); }
                return _TestRpository;
            }
            private set { }
        }
        public IErrorDetailRepository ErrorDetailRepository
        {
            get
            {
                if (_ErrorDetailRepository == null) { _ErrorDetailRepository = new ErrorDetailRepository(this.context); }
                return _ErrorDetailRepository;
            }
            private set { }
        }

        public IUsersRepository UsersRepository
        {
            get
            {
                if (_usersRepository == null) { _usersRepository = new UsersRepository(this.context); }
                return _usersRepository;
            }
            private set { }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null) { _roleRepository = new RoleRepository(this.context); }
                return _roleRepository;
            }
            private set { }
        }

        public IUserRolesRepository UserRolesRepository
        {
            get
            {
                if (_userRoleRepository == null) { _userRoleRepository = new UserRoleRepository(this.context); }
                return _userRoleRepository;
            }
            private set { }
        }

        public IUserClaimsRepository UserClaimsRepository
        {
            get
            {
                if (_userClaimsRepository == null) { _userClaimsRepository = new UserClaimsRepository(this.context); }
                return _userClaimsRepository;
            }
            private set { }
        }

        

        public int Complete()
        {
            return  context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
