using CQRS1.AllCommands;
using CQRS1.AllQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS1
{
    class Program
    {
        static void Main(string[] args)
        {
            int id;
            var eb = new EventBroker();
            var p = new User(eb);
            eb.Command(new ChangeIdCommand(p, 123));

            foreach(var e in eb.AllEvents)
            {
                Console.WriteLine(e);
            }
            


            id = eb.Query<int>(new IdQuery { Target = p });
            Console.WriteLine(id);


            eb.UndoLast();

            foreach (var e in eb.AllEvents)
            {
                Console.WriteLine(e);
            }

            id = eb.Query<int>(new IdQuery { Target = p });
            Console.WriteLine(id);

            Console.ReadKey();

        }
    }
}
