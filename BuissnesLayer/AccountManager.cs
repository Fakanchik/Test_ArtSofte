using BuissnesLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuissnesLayer
{
    public class AccountManager
    {
        private IUserRepository _userRepository;

        public AccountManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IUserRepository Users { get { return _userRepository; } }
    }
}
