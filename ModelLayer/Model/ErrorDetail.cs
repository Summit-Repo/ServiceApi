using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer.Model
{
  public  class ErrorDetail
    {
        [Key]
        public int Id { get; set; }

        public string Source { get; set; }
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string Message { get; set; }

        [MaxLength]
        public string StackTrace { get; set; }

        public bool ModelState { get; set; }
        public int StatusCode { get; set; }
    }
}
