using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS1.AllCommands
{
    public class ChangeIdCommand : Command
    {
        public User Target;
        public int Id;
        public ChangeIdCommand(User target, int id)
        {
            Target = target;
            Id = id;
        }
    }
}
