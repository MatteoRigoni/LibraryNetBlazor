using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Validation
{
    public class Book_ValidatePublishDate : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var book = validationContext.ObjectInstance as Book;
            var res = book.ValidatePublishDate();
            if (res)
                return ValidationResult.Success;
            else
                return new ValidationResult("Check date range");
            
        }
    }
}
