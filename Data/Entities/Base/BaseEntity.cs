using System.ComponentModel.DataAnnotations.Schema;

namespace TherapEase.Data.Entities.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}
