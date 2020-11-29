using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ModelLayer.Model;

namespace RepositoryLayer.Repository
{
    public class TestRpository : GenericRepository<Test>, ITestRepository
    {
        private readonly Context context;

        public TestRpository(Context context) : base(context)
        {
            this.context = context;
        }

        public IList<Test> GetAllTest()
        {
            IQueryable<Test> q = context.Test;
            return q.ToList();
        }
    }
}
