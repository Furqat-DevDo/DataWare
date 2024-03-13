using FluentValidation;

namespace AviaSales.UseCases.Passenger;

public class PassengerValidator : AbstractValidator<CreatePassengerDto>
{
    public PassengerValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(p => p.Fullname).NotNull().NotEmpty();
        
        RuleFor(p => p.Phone).NotNull().NotEmpty()
            .Matches(@"^(\+\d{1,2}\s?)?(\(\d{1,4}\)|\d{1,4})[-.\s]?\d{1,10}$")
            .WithMessage("Invalid phone number format. Please enter a valid phone number.");
        
        RuleFor(p => p.Email).NotNull().NotEmpty().EmailAddress();
    }
}