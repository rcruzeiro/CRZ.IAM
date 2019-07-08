using System;
using System.Threading;
using System.Threading.Tasks;
using CRZ.Framework.CQRS;
using CRZ.Framework.Web.Security;
using CRZ.IAM.Platform.Application.Account.Commands.Actions;
using CRZ.IAM.Platform.Domain.Account;
using CRZ.IAM.Platform.Domain.Account.Specifications;

namespace CRZ.IAM.Platform.Application.Account.Commands
{
    public class AccountCommandHandler : IAccountCommandHandler
    {
        readonly IUserRepository _userRepository;
        readonly ISecurityTokenService<User> _securityTokenService;

        public AccountCommandHandler(IUserRepository userRepository, ISecurityTokenService<User> securityTokenService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _securityTokenService = securityTokenService ?? throw new ArgumentNullException(nameof(securityTokenService));
        }

        public async Task ExecuteAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
        {
            command.Validate();

            var user = new User(command.Name, command.Email, command.Username, command.Password);

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task<string> ExecuteAsync(LoginUserCommand command, CancellationToken cancellationToken = default)
        {
            command.Validate();

            var spec = new GetUserByUsernameSpecification(command.Username);
            var user = await _userRepository.GetOneAsync(spec, cancellationToken);

            if (user == null) throw new ArgumentNullException(nameof(user));

            if (!user.ValidatePassword(command.Password)) throw new InvalidOperationException("Invalid password.");

            var token = _securityTokenService.CreateToken(user);

            user.Login(token);

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return token;
        }

        public async Task ExecuteAsync(LogoutUserCommand command, CancellationToken cancellationToken = default)
        {
            command.Validate();

            var spec = new GetUserByTokenSpecification(command.Token);
            var user = await _userRepository.GetOneAsync(spec, cancellationToken);

            if (user == null) throw new ArgumentNullException(nameof(user));

            user.Logout();

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
