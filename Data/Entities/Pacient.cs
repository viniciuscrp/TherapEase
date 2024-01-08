using System.ComponentModel.DataAnnotations.Schema;
using TherapEase.Data.Entities.Base;

namespace TherapEase.Data.Entities
{
    public class Pacient : BaseEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        public int TherapistId { get; set; }

        public Therapist Therapist { get; set; }
    }
}
