using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestIService;

namespace TestWeb
{
    public static  class TestHelper
    {
        public static string Test()
        {
            IUserService userService = DependencyResolver.Current.GetService<IUserService>();
            return userService.CheckLogin();
        }
    }
}