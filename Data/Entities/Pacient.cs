using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TherapEase.Data.Entities.Base;

namespace TherapEase.Data.Entities
{

    [Index(nameof(Email), IsUnique = true)]
    public class Pacient : BaseEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        public int TherapistId { get; set; }

        public Therapist Therapist { get; set; }
    }
}
