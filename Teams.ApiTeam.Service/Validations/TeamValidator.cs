using FluentValidation;
using Teams.ApiTeam.Service.Dtos;

namespace Teams.ApiTeam.Service.Validations;

public class TeamValidator : AbstractValidator<TeamDto>
{
    public TeamValidator()
    {
        RuleFor(m => m.Id)
            .NotEmpty()
            .WithMessage("The Id is required.");

        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(50) // Maximum length of 50 characters
            .Must(BeValidString) // Validate that the string is not null or empty and that it contains only letters or white spaces
            .WithMessage("Name must be a valid string with a maximum length of 50 characters.");

        RuleFor(m => m.Coach)
            .NotEmpty()
            .MaximumLength(50) // Maximum length of 50 characters 
            .Must(BeValidString) // Validate that the string is not null or empty and that it contains only letters or white spaces
            .WithMessage("Coach must be a valid string with a maximum length of 50 characters.");

        RuleFor(m => m.Conference)
            .MaximumLength(20)
            .WithMessage("The maximum length of Conference is 20 characters.");
    }

    private static bool BeValidString(string value)
    {
        // Validate that the string is not null or empty and that it contains only letters or white spaces
        return !string.IsNullOrEmpty(value) && value.All(char.IsLetterOrDigit);
    }
}