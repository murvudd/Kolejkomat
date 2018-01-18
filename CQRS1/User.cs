using CQRS1.AllCommands;
using CQRS1.AllEvents;
using CQRS1.AllQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS1
{
    public class User
    {
        private int id;

        EventBroker broker;

        public User(EventBroker broker)
        {
            this.broker = broker;
            //listing to commnads

            broker.Commands += Broker_Commands;

            broker.Queries += Broker_Querries;
        }

        private void Broker_Querries(object sender, Query query)
        {
            var iq = query as IdQuery;
            if(iq !=null && iq.Target == this)
            {
                iq.Result = id;
            }
        }

        private void Broker_Commands(object sender, Command command)
        {
            var cic = command as ChangeIdCommand;
            if(cic!=null && cic.Target == this)
            {
                if(cic.Register) broker.AllEvents.Add(new IdChangedEvent(this, id, cic.Id));
                id = cic.Id;
            }
        }
    }
}
