﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClenoviSDSM.Shared.Models
{
    public class TokenSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public string EncKey { get; set; }
    }
}
