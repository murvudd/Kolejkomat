using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EntityProject.Models
{
    public enum Privileges
    {
        User = 0,
        PrivilagedUser = 1,
        Admin = 2,
    }

    public interface IGuid
    {
        Guid Id { get; }
    }


    [Table("Person")]
    public class Person : IGuid
    {

        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public Privileges Privileges { get; set; }



        public int? PositionInQueuePos { get; set; }

        public virtual PositionInQueue PositionInQueues { get; set; }

        public Person()
        {
        }
        public Person(string mail, string password, string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            Mail = mail;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Privileges = Privileges.User;
            PositionInQueuePos = null;
        }
    }
}