using DomainModel.Interfaces.Services;
using System.Collections.Generic;
using DomainModel.Entities;
using Data.Repositories;
using DomainModel.Interfaces.Repositories;
using System;

namespace DomainServices.Services
{

    public class UserServices : IUserService
    {

        private IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

   

        public void CreateUser(User user)
        {
            _userRepository.Add(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUser(User user)
        {
            return _userRepository.Get(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }
    }
}
