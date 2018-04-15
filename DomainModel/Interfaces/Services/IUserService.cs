using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces.Services
{
    public interface IUserService
    {
        void CreateUser(User user);
        void UpdateUser(User user);
        User GetUser(User user);
        IEnumerable<User> GetAllUsers();
    }
}
