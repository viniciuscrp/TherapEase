using TherapEase.Context;
using TherapEase.Data.Entities;
using TherapEase.Repositories.Interfaces;

namespace TherapEase.Repositories
{
    public class TherapistRepository(ApiContext context) : Repository<User>(context), ITherapistRepository
    {
    }
}
