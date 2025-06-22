using Microsoft.EntityFrameworkCore;
using Models.Client.DTOs;
using Models.Data;
using Models.Data.Entities;
using ServiceCore.Interfaces.Repositories;
using ServiceCore.Interfaces.Services;

namespace ServiceCore.Services
{
    public class AuthService
    {
        private readonly IUnitOfWork<AppDbContext> _uow;
        private readonly IJwtService _jwtService;

        public AuthService(IUnitOfWork<AppDbContext> uow, IJwtService jwtService)
        {
            _uow = uow;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var repo = _uow.GetRepository<User>();
            var users = await repo.Get(u => u.UserName == request.UserName);
            var user = users.FirstOrDefault();
            if (user == null) return null;

            // password check omitted
            var token = _jwtService.GenerateToken(user.Id.ToString(), user.UserName);
            return new LoginResponse { Token = token };
        }
    }
}
