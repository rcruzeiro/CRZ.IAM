using System.Threading;
using System.Threading.Tasks;
using CRZ.IAM.Platform.Application.Account.Commands.Actions;

namespace CRZ.IAM.Platform.Application.Account
{
    public interface IAccountAppService
    {
        Task CreateUser(CreateUserCommand command, CancellationToken cancellationToken = default);
        Task<string> LoginUser(LoginUserCommand command, CancellationToken cancellationToken = default);
        Task LogoutUser(LogoutUserCommand command, CancellationToken cancellationToken = default);
    }
}
