using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRZ.Framework.CQRS.Commands;

namespace CRZ.IAM.Platform.Application.Account.Commands.Actions
{
    public class CreateUserCommand : ICommand
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return new ValidationResult($"{nameof(Name)} is a necessary field.", new[] { nameof(Name) });

            if (string.IsNullOrWhiteSpace(Email))
                yield return new ValidationResult($"{nameof(Email)} is a necessary field.", new[] { nameof(Email) });

            if (string.IsNullOrWhiteSpace(Password))
                yield return new ValidationResult($"{nameof(Password)} is a necessary field.", new[] { nameof(Password) });
        }
    }
}
