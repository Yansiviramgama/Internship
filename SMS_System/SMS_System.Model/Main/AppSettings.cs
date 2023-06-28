using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_System.Model.Main
{
    public class AppSettings
    {
        public string JWTSecretKey { get; set; }
        public int JWTValidityMinutes { get; set; }
    }
}
