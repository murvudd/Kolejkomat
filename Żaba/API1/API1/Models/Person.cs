using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API1.Models
{
    public class Person
    {
        //private string _firstname { get; set; }
        //private string _lastname { get; set; }
        //private string _mail { get; set; }
        private Guid _guid { get; set; }
        //private string _password { get; set; }
        //private int _privileges { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public Guid Guid { get; set; }
        public string Password { get; set; } 
        public int Privileges { get; set; }

        public Person()
        {
            _guid = Guid.NewGuid();
        }

        protected Person(Person other)
        {
            this._guid = other._guid;
        }
    }

}