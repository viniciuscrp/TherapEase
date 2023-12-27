﻿using TherapEase.Models.ViewModels;

namespace TherapEase.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserViewModel> Login(string email, string password);

        Task RefreshAccessToken(string accessToken, string refreshToken);
    }
}
