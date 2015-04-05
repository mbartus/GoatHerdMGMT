using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatMGMT.Models
{
    public class UserViewModel
    {
        public List<UserList> userlist = new List<UserList>();
    }
    public class UserList
    {
        public string username;
        public int animalcount;
        public DateTime creationDate;
        public string accountType;
    }
}