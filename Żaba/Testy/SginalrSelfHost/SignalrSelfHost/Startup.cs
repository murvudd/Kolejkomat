using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace SignalrSelfHost
{
    public partial class Startup
    {
        public void Configurations(IAppBuilder app)
        {
            AuthConfig(app);
        }
    }
}
