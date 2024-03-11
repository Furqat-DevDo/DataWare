using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AviaSales.Shared.Extensions;

/// <summary>
/// Will extend Fluent Validation result class.
/// </summary>
public static class FluentValidationExt
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
    
    /// <summary>
    /// Will create new Problem details class to return more standart response.
    /// </summary>
    /// <param name="result">Fluent validation result.</param>
    public static ProblemDetails ToProblemDetails(this ValidationResult result)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad request",
            Detail = "One or more validation errors occurred.",
            Instance = nameof(ValidationResult)
        };

        foreach (var error in result.Errors)
        {
            problemDetails.Extensions.Add(error.PropertyName, new[] { error.ErrorMessage });
        }

        return problemDetails;
    }

}