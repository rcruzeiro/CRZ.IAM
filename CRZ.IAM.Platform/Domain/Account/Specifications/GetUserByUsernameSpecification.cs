using CRZ.Framework.Domain;

namespace CRZ.IAM.Platform.Domain.Account.Specifications
{
    public class GetUserByUsernameSpecification : BaseSpecification<User>
    {
        public GetUserByUsernameSpecification(string username)
            : base(u => u.Username == username)
        { }
    }
}
