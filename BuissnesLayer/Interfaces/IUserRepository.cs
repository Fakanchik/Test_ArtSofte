using DataLayer.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuissnesLayer.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByPhone(string UserPhone);
        void CreateUser(User user);
        bool RegisteredUser(string Phone);
        bool RegisteredUserForRegister(string Phone, string Email);
        bool CorrectPassword(string Phone,string Password);
        void LoginDate(string Phone);

    }
}
