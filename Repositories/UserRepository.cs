using TherapEase.Context;
using TherapEase.Data.Entities;
using TherapEase.Repositories.Interfaces;

namespace TherapEase.Repositories
{
    public class UserRepository(ApiContext context) : Repository<User>(context), IUserRepository
    {
    }
}
