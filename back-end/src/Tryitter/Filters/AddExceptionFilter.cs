using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.ExceptionHandled)
        {
            return;
        }

        int statusCode = (int)HttpStatusCode.InternalServerError;
        if (context.Exception is StudentNotFound)
            statusCode = (int)HttpStatusCode.NotFound;
        else if (context.Exception is IncorrectPassword)
            statusCode = (int)HttpStatusCode.BadRequest;
        else if (context.Exception is NoContent)
            statusCode = (int)HttpStatusCode.NoContent;
        
        var response = new
        {
            Error = context.Exception.Message,
            StackTrace = context.Exception.StackTrace
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}