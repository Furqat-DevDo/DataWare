using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AviaSales.API.Extensions;

/// <summary>
/// Will extend Model State class.
/// </summary>
public static class ModelStateExtension
{
    /// <summary>
    /// Will extend model state dictionary and add fluent validation errors to model state.
    /// </summary>
    /// <param name="result">Validation result.</param>
    /// <param name="modelState">Model state .</param>
    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState) 
    {
        foreach (var error in result.Errors) 
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
}