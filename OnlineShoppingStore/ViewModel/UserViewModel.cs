
using Microsoft.AspNetCore.Http;
using OnlineShopEdmx.Model;
using OnlineShopping.Models;
using OnlineShopping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModel
{
    public class UserViewModel
    {
        public UserDetail dbModel { get; set; }
        public IEnumerable<PairModel> ddlIsActive { get; set; }
        public IEnumerable<IntStringPairModel> ddlCategory { get; set; }

    }
    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
    public class UserViewModelLst
    {
        public IEnumerable<UserDetail> dbModelLst { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int id { get; set; }

    }
}
