using FluentValidation;
using Teams.ApiMember.Service.Dtos;

namespace Teams.ApiMember.Service.Validations;

public class TeamMemberValidator : AbstractValidator<TeamMemberDto>
{
    public TeamMemberValidator()
    {
        RuleFor(m => m.Id)
            .NotEmpty()
            .WithMessage("The Id is required.");

        RuleFor(m => m.FirstName)
            .NotEmpty()
            .MaximumLength(50) // Maximum length of 50 characters
            .WithMessage("FirstName must be a valid string with a maximum length of 50 characters.");

        RuleFor(m => m.LastName)
            .NotEmpty()
            .MaximumLength(50) // Maximum length of 50 characters
            .WithMessage("LastName must be a valid string with a maximum length of 50 characters.");

        RuleFor(m => m.BirthDate)
            .NotEmpty()
            .Must(BeValidDate) // Custom validation function to check valid date
            .WithMessage("BirthDate must be a valid date.");

        RuleFor(m => m.Position)
            .NotEmpty()
            .MaximumLength(20) // Maximum length of 20 characters
            .WithMessage("Position must be a valid string with a maximum length of 20 characters.");

        RuleFor(m => m.Phone)
            .NotEmpty()
            .Must(BeValidPhoneNumber) // Custom validation function to check valid phone number
            .WithMessage("Phone must be a valid phone number.");

        RuleFor(m => m.TeamId)
            .NotEmpty()
            .WithMessage("The TeamId is required.");
    }

    private bool BeValidDate(DateTime date)
    {
        // Add custom logic to validate the date, e.g., it should be in the past or within a valid range.
        // Return true if valid, false if not.
        return date < DateTime.Now;
    }

    private static bool BeValidPhoneNumber(string phone)
    {
        // Add custom logic to validate the phone number, e.g., it should match a specific format.
        // Return true if valid, false if not.
        return IsValidPhoneNumberFormat(phone);
    }

    private static bool IsValidPhoneNumberFormat(string phone)
    {
        // Add logic to check if the phone number matches a valid format.
        // Return true if it matches the format, false if not.
        // Example format: (123) 456-7890
        // You can use regular expressions or other validation methods here.
        // For simplicity, we assume it's valid if it's not empty in this example.
        return !string.IsNullOrEmpty(phone);
    }
}