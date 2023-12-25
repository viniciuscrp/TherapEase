using TherapEase.Data.Entities.Base;

namespace TherapEase.Data.Entities
{
    public class UserRefreshToken : BaseEntity
    {
        public string Email { get; set; }

        public string RefreshToken { get; set; }

        public bool IsActive { get; set; }
    }
}
