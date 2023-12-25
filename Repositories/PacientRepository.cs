using TherapEase.Context;
using TherapEase.Data.Entities;
using TherapEase.Repositories.Interfaces;

namespace TherapEase.Repositories
{
    public class PacientRepository(ApiContext context) : Repository<Pacient>(context), IPacientRepository
    {
    }
}
