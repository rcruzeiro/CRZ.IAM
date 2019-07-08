using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRZ.Framework.CQRS.Commands;

namespace CRZ.IAM.Platform.Application.Account.Commands.Actions
{
    public class LoginUserCommand : ICommand
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Username))
                yield return new ValidationResult($"{nameof(Username)} is a necessary field.", new[] { nameof(Username) });

            if (string.IsNullOrWhiteSpace(Password))
                yield return new ValidationResult($"{nameof(Password)} is a necessary field.", new[] { nameof(Password) });
        }
    }
}
