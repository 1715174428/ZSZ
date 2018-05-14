using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestIService;

namespace TestService
{
    public class UserService : IUserService
    {
        public string CheckLogin()
        {
            
            return "登录成功";
        }
    }
}
