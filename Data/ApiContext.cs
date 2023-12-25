using Microsoft.EntityFrameworkCore;
using TherapEase.Data.Entities;

namespace TherapEase.Context
{
    public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
    {
        public virtual DbSet<User> Therapists { get; set; }

        public virtual DbSet<Pacient> Pacients { get; set;}

        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}
