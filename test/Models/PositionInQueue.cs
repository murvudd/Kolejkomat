using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace test.Models
{
    [Table("Position")]
    public class PositionInQueue
    {
        [Key]
        public int Pos { get; set; }
        public DateTime Date { get; set; }
        public string Issue { get; set; }



        public virtual ICollection<User> Users { get; set; }

    }
}