using KanbanList.Core.Models;
using KanbanList.Core.Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KanbanList.Core.Services.Implementations
{
    public class ValidationService : IValidationService
    {
        public ValidationModelResult ValidateModel(object model, bool validateAllProperties = true)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);

            if (!Validator.TryValidateObject(model, context, results, true))
            {
                Dictionary<string, string> errorMessages = results.ToDictionary(
                    x => x.MemberNames?.FirstOrDefault() ?? string.Empty,
                    x => x.ErrorMessage ?? string.Empty);

                return new ValidationModelResult
                {
                    IsValid = false,
                    ErrorMessages = errorMessages
                };
            }
            return new ValidationModelResult()
            {
                IsValid = true,
            };
        }
    }
}
