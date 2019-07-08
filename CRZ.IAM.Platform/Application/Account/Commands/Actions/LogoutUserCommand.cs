using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRZ.Framework.CQRS.Commands;

namespace CRZ.IAM.Platform.Application.Account.Commands.Actions
{
    public class LogoutUserCommand : ICommand
    {
        [Required]
        public string Token { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Token))
                yield return new ValidationResult($"{nameof(Token)} is a necessary field.", new[] { nameof(Token) });
        }
    }
}
