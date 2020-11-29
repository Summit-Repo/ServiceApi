using ModelLayer.Model;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Repository
{
   public class ErrorDetailRepository:GenericRepository<ErrorDetail>,IErrorDetailRepository
    {
        private readonly Context context;

        public ErrorDetailRepository(Context context) : base(context)
        {
            this.context = context;
        }
}
}
