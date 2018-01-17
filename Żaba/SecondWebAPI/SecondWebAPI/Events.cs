using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondWebAPI
{
    public interface IEvent
    {
        Guid Id { get; }
    }

    public class AccountCreateEvent : IEvent
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public AccountCreateEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class FundsDespoitedEvent : IEvent
    {
        public Guid Id { get; private set; }
        public Decimal Amount { get; private set; }
        public FundsDespoitedEvent(Guid id, Decimal amount)
        {
            Id = id;
            Amount = amount;
        }
    }

    public class FundsWithdrawedEvent : IEvent
    {
        public Guid Id { get; private set; }
        public Decimal Amount { get; private set; }
        public FundsWithdrawedEvent(Guid id, Decimal amount)
        {
            Id = id;
            Amount = amount;
        }
    }
}