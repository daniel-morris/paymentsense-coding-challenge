using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Paymentsense.Coding.Challenge.Api.Services.Models;

namespace Paymentsense.Coding.Challenge.Api.ErrorHandling.ActionFilters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception is Exception ex)
            {
                context.Result = new ObjectResult(new EmptyOperationResult { Success = false, Message = "Oops, an error ocurred." });
                // In production errors would be logged and potentially have our own error codes returning to the UI, depending on decided approach
                context.ExceptionHandled = true;
            }
        }
    }
}
