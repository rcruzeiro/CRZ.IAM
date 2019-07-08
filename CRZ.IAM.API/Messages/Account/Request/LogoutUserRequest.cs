using CRZ.Framework.Web.Messages;
using CRZ.IAM.Platform.Application.Account.Commands.Actions;

namespace CRZ.IAM.API.Messages.Account.Request
{
    public class LogouUserRequest : BaseRequest
    {
        public LogoutUserCommand Command { get; set; }
    }
}