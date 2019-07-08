using CRZ.Framework.Domain;

namespace CRZ.IAM.Platform.Domain.Account.Specifications
{
    public class GetUserByTokenSpecification : BaseSpecification<User>
    {
        public GetUserByTokenSpecification(string token)
             : base(u => u.Token == token)
        { }
    }
}
