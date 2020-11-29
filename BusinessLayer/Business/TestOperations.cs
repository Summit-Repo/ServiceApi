using AutoMapper;
using BusinessLayer.IBusiness;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ModelLayer.ViewModel;
using ModelLayer.Model;

namespace BusinessLayer.Business
{
  public  class TestOperations : ITestOpertaions
    {
       // private readonly ITestRepository itest;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public TestOperations(IMapper mapper,IUnitOfWork unitOfWork) //ITestRepository Itest,
        {
           // itest = Itest;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        //public IList<TestDto> getTestList()
        //{
        //    var test=  itest.GetAllTest();
        //   return mapper.Map<IList<Test> ,IList<TestDto>>(test);
        //}


        //Using UnitOfWork
        public IEnumerable<TestDto> getTestList()
        {
            var getTest = unitOfWork.TestRepository.GetAll();
            return mapper.Map<IEnumerable<Test>, IEnumerable<TestDto>>(getTest);
        }

        IEnumerable<TestDto> ITestOpertaions.getTestList()
        {
        //    throw  new Exception("Error OOccured");
            
                Test test = new Test() { testName = "Test2" };
                unitOfWork.TestRepository.Add(test);
                unitOfWork.Complete();
                var getTest = unitOfWork.TestRepository.GetAll();
            return mapper.Map<IEnumerable<Test>, IEnumerable<TestDto>>(getTest);
        }
    }
}
