﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Services.Interfaces
{
    internal interface IUserService
    {
        Task GetAllInfoAboutUser(int id);
    }
}
