﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EntityProject.Models
{
    public enum Privileges
    {
        Admin, User, PrivilagedUser
    }

    public interface IGuid
    {
        Guid ID { get; }
    }
    [Table("Person")]
    public class Person :IGuid
    {

        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public Privileges Privileges { get; set; }


        
        public int PositionInQueuePos { get; set; }

        public virtual PositionInQueue PositionInQueues { get; set; }
    }
}