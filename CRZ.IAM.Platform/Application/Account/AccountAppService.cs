using System;
using System.Threading;
using System.Threading.Tasks;
using CRZ.IAM.Platform.Application.Account.Commands;
using CRZ.IAM.Platform.Application.Account.Commands.Actions;

namespace CRZ.IAM.Platform.Application.Account
{
    public class AccountAppService : IAccountAppService
    {
        readonly IAccountCommandHandler _accountCommandHandler;

        public AccountAppService(IAccountCommandHandler accountCommandHandler)
        {
            _accountCommandHandler = accountCommandHandler ?? throw new ArgumentNullException(nameof(accountCommandHandler));
        }

        public async Task CreateUser(CreateUserCommand command, CancellationToken cancellationToken = default)
        {
            await _accountCommandHandler.ExecuteAsync(command, cancellationToken);
        }

        public async Task<string> LoginUser(LoginUserCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _accountCommandHandler.ExecuteAsync(command, cancellationToken);

            return result;
        }

        public async Task LogoutUser(LogoutUserCommand command, CancellationToken cancellationToken = default)
        {
            await _accountCommandHandler.ExecuteAsync(command, cancellationToken);
        }
    }
}
