using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace test.Models
{
    public enum Privileges
    {
        User = 0,
        PrivilagedUser = 1,
        Admin = 2,
    }

    
    [Table("Person")]
    public class User 
    {

        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public Privileges Privileges { get; set; }



        public int? PositionInQueuePos { get; set; }

        public virtual PositionInQueue PositionInQueues { get; set; }

        //public Person()
        //{
        //    Id = Guid.NewGuid();
        //}
    }
}