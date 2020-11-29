using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;
using System;


namespace RepositoryLayer
{
    public class Context:DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options) { }

        public virtual DbSet<Test> Test { get; set; }

        public virtual DbSet<ErrorDetail> ErrorDetails { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        public virtual DbSet<UserRole>  UserRoles { get; set; }

    }
}
