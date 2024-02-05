using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.AuthModels
{
    public class Registration
    {
        public string Username { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
