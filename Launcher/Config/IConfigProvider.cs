﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Configuration
{
    public abstract class IConfigProvider
    {
        protected virtual bool Validate()
        {
            return true;
        }
    }
}
