using BuissnesLayer.Interfaces;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BuissnesLayer.Implementations
{
    public class EFUsersRepository : IUserRepository
    {
        private EFDBContext context;
        public EFUsersRepository(EFDBContext context)
        {
            this.context = context;
        }
        private User GetUserData (string Phone)
        {
            return context.User.FirstOrDefault(x => x.Phone == Phone);
        }
        public User GetUserByPhone(string UserPhone)
        {
            return GetUserData(UserPhone);
        }

        public void CreateUser(User user)
        {
            context.User.Add(user);
            context.SaveChanges();
        }
        public bool RegisteredUser(string Phone)
        {
            User _checkPhone = GetUserData(Phone);
            if (_checkPhone != null) return true;
            else return false;
        }
        public bool RegisteredUserForRegister(string Phone, string Email)
        {
            bool _checkPhone = RegisteredUser(Phone);
            User _checkEmail = context.User.FirstOrDefault(x => x.Email == Email);
            if ((_checkPhone)||(_checkEmail!=null)) return true;
            else return false;
        }
        public bool CorrectPassword(string Phone,string Password)
        {
            User checkPassword = GetUserByPhone(Phone); 
            if (checkPassword.Password == Password) return true;
            else return false;
        }
        public void LoginDate(string Phone)
        {
            User checkPhone = GetUserData(Phone);
            checkPhone.LastLogin = DateTime.Now;
            context.SaveChanges();
            
        }
    }
}
