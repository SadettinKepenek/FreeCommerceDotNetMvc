﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeCommerceDotNet.Models.DbModels
{
    public class Users
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string EMail { get; set; }

        public string Role { get; set; }
    }
}