﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Configuration
{
    public class UserConfigProvider : IConfigProvider
    {
        /// <summary>
        /// Saved username
        /// </summary>
        public string Username { get; set; }
    }
}
