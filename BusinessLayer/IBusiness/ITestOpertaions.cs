using System;
using System.Collections.Generic;
using System.Text;
using ModelLayer.ViewModel;

namespace BusinessLayer.IBusiness
{
  public  interface ITestOpertaions
    {
        IEnumerable<TestDto> getTestList();
    }
}
