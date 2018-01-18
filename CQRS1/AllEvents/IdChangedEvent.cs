using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS1.AllEvents
{
    class IdChangedEvent : Event
    {
        public User Target;
        public int OldValue, NewValue;

        public IdChangedEvent(User target, int oldValue, int newValue )
        {
            Target = target;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public override string ToString()
        {
            return $"Id Changed from {OldValue} to {NewValue}";
        }

    }
}
