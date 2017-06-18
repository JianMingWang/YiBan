using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//引入命名空间
using System.Web.Http;
using X_1_FirstWebAPI.Model;

namespace X_1_FirstWebAPI.Controller
{
    public class UserController : ApiController
    {

        public List<Users> GetUser()
        {
            var userList = new List<Users> { 
            new Users{ Id=1,UName="张三",UAge=12,UAddress="海淀区"},
            new Users{Id=2,UName="李四",UAge=23,UAddress="昌平区"},
            new Users{Id=3,UName="王五",UAge=34,UAddress="朝阳区"}
            };

            var temp = (from u in userList
                        select u).ToList();
            return temp;
        }

    }
}