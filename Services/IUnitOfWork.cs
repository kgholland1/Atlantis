﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Services
{
    public interface IUnitOfWork
    {
        Task<bool> CompleteAsync();
    }
}
