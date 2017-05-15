using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodFinder.API.Authentication
{
    /// <summary>
    /// Middleware to implement application key as security
    /// </summary>
    public class ApplicationKeyAttributem : ActionFilterAttribute
    {
        private readonly string _applicationKey;
        private readonly string _authenticationKey;

        public ApplicationKeyAttributem()
        {
            _applicationKey = "Agrajag";
            _authenticationKey = "FC-APPLICATION-KEY";
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!ValidateKey(context.HttpContext.Request.HttpContext.Request))
                context.Result =  new UnauthorizedResult();
            else
            base.OnActionExecuting(context);
        }

        private bool ValidateKey(HttpRequest httpContextRequest)
        {

            string test = httpContextRequest.Headers[_authenticationKey];

            return test != null && (test.Equals(_applicationKey, StringComparison.Ordinal));
        }
    }
}