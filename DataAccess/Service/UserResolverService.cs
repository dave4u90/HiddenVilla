using System;
using Common;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Service
{
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor _accessor;

        public UserResolverService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string GetCurrentUserName()
        {
           return _accessor?.HttpContext?.User?.Identity?.Name;
        }
    }
}
