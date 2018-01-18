using CQRS1.AllCommands;
using CQRS1.AllEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS1
{
    public class EventBroker
    {
        //1. All Events that happened.
        public IList<Event> AllEvents = new List<Event>();
        //2.Commands 
        public event EventHandler<Command> Commands;
        //3.Query
        public event EventHandler<Query> Queries;

        public void Command(Command c)
        {
            Commands?.Invoke(this, c);
        }
        //casting result to T
        public T Query<T>(Query q)
        {
            Queries?.Invoke(this, q);
            return (T) q.Result;
           

        }
        public void UndoLast()
        {
            var e = AllEvents.LastOrDefault();
            var ac = e as IdChangedEvent;
            if(ac != null)
            {
                Command(new ChangeIdCommand(ac.Target, ac.OldValue) { Register = false });
                AllEvents.Remove(e);
            }
        }
    }
}
