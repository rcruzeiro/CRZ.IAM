using CRZ.Framework.CQRS.Commands;
using CRZ.IAM.Platform.Application.Account.Commands.Actions;

namespace CRZ.IAM.Platform.Application.Account.Commands
{
    public interface IAccountCommandHandler :
        ICommandHandlerAsync<CreateUserCommand>,
        ICommandHandlerAsync<LoginUserCommand, string>,
        ICommandHandlerAsync<LogoutUserCommand>
    { }
}
