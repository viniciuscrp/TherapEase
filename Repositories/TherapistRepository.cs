using TherapEase.Context;
using TherapEase.Data.Entities;
using TherapEase.Repositories.Interfaces;

namespace TherapEase.Repositories
{
    public class TherapistRepository(ApiContext context) : Repository<Therapist>(context), ITherapistRepository
    {
    }
}
