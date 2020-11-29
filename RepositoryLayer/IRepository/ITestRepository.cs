using ModelLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.IRepository
{
 public  interface ITestRepository:IGenericRepository<Test>
    {
        IList<Test> GetAllTest();
        void Add(Test test);
    }
}
