using System.Text.Json.Serialization;
using TherapEase.Data.Entities;

namespace TherapEase.Models.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(Therapist user)
        {
            Id = user.Id;
            Name = user.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
