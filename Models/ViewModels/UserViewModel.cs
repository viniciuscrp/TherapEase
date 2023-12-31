﻿using TherapEase.Data.Entities;

namespace TherapEase.Models.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
