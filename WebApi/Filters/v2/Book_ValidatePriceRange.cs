using Library.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Library.WebApi.Filters.v2
{
    public class Book_ValidatePriceRange: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var book = context.ActionArguments["book"] as Book;
            if (book != null && !book.ValidatePrice())
            {
                context.ModelState.AddModelError("Price", "Zero not allowed");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
