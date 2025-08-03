using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace application.Interfaces
{
    public interface IUserAccessor
    {
        string GetUserId();
        Task<User> GetUserAsync();
        Task<User> GetUserWithPhotosAsync();
    }
}