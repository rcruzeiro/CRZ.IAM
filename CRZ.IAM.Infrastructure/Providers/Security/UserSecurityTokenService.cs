using System.Collections.Generic;
using System.Security.Claims;
using CRZ.Framework.Web.Security;
using CRZ.IAM.Platform.Domain.Account;

namespace CRZ.IAM.Infrastructure.Providers.Security
{
    public class UserSecurityTokenService : BaseSecurityTokenService<User>
    {
        public UserSecurityTokenService(SigninConfiguration signinConfiguration, TokenConfiguration tokenConfiguration)
            : base(signinConfiguration, tokenConfiguration)
        { }

        protected override string GetGenericIdentity(User data)
        {
            return data.Username;
        }

        protected override IEnumerable<Claim> GetClaims(User data)
        {
            return new[]
            {
                new Claim("username", data.Username),
                new Claim("name", data.Name)
            };
        }
    }
}
