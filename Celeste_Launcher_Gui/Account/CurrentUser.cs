using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celeste_Launcher_Gui.Account
{
    internal class CurrentUser
    {
        internal string Email { get; set; }

        internal UserConfig Configuration { get; }

        public CurrentUser()
        {
            Configuration = new UserConfig();
        }
    }
}
