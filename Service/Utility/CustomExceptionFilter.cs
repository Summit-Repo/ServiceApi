using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using ModelLayer.Model;
using ModelLayer.ViewModel;
using RepositoryLayer;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.Utility
{
    public class CustomExceptionFilter : ExceptionFilterAttribute, IAsyncExceptionFilter
    {
         private readonly IUnitOfWork unitOfWork;

        public CustomExceptionFilter(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public override Task OnExceptionAsync(ExceptionContext context)
        {
                LogError(context);

            //Business exception-More generics for external world
            var error = new ErrorDisplay()
            {
                StatusCode = 500,
                Message = "Something went wrong! Internal Server Error."
            };
            //Logs your technical exception with stack trace below

            context.Result = new JsonResult(error);
            return Task.CompletedTask;
        }

        public void LogError(ExceptionContext context)
        {
            ControllerActionDescriptor actionDescription = context.ActionDescriptor as ControllerActionDescriptor;

            ErrorDetail errorDetail = new ErrorDetail();
            errorDetail.Source = context.Exception.Source;
            errorDetail.ControllerName = actionDescription.ControllerName;
            errorDetail.ActionName = actionDescription.ActionName;
            errorDetail.Message = context.Exception.Message;
            errorDetail.StackTrace = context.Exception.StackTrace;
            errorDetail.ModelState = context.ModelState.IsValid;
            errorDetail.StatusCode = context.HttpContext.Response.StatusCode;

            unitOfWork.ErrorDetailRepository.Add(errorDetail);
            unitOfWork.Complete();


         


        }


    }


    

}
