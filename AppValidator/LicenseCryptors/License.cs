﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppValidator.LicenseCryptors
{
    [Serializable]
    public class License
    {
        public int PlatformHash { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
