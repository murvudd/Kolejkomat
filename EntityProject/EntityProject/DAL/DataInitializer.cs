using EntityProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityProject.DAL
{
    public class DataInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            //var positionInQueues = new List<PositionInQueue>
            //{
            //    new PositionInQueue{Date=DateTime.Now,Issue="beka" },
            //    new PositionInQueue{Date=DateTime.Now,Issue="beka2" },
            //    new PositionInQueue{Date=DateTime.Now,Issue="beka3" }



            //};
            //positionInQueues.ForEach(s => context.PositionInQueues.Add(s));
            //context.SaveChanges();

            //var persons = new List<Person>
            //{
            //    new Person{FirstName="Krystian", LastName="Kurylowicz",Mail="k@rylo.com",Password="waflo",Privileges=Privileges.Admin,PositionInQueuePos=1},
            //    new Person{FirstName="Adam", LastName="Kurylowicz2",Mail="k@rylo2.com",Password="waflo2",Privileges=Privileges.User,PositionInQueuePos=2},
            //    new Person{FirstName="Justynka", LastName="Kurylo",Mail="k@ryl3.com",Password="waflo3",Privileges=Privileges.PrivilagedUser,PositionInQueuePos=3}
            //};
            //persons.ForEach(s => context.Persons.Add(s));
            //context.SaveChanges();
        }
    }
}