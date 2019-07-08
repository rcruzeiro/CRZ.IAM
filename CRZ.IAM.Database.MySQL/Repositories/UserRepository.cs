using CRZ.Framework.Repository;
using CRZ.IAM.Platform.Domain.Account;

namespace CRZ.IAM.Database.MySQL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWorkAsync unitOfWork)
            : base(unitOfWork)
        { }
    }
}
