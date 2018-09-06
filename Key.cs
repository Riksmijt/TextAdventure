using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
    class Key:Item
    {
        public Key()
        {
            this.name = "key";
            this.description = "this can open a door";
            this.weight = 3;
        }
        public override void use(object o)
        {
            if (o.GetType() == typeof(Room))
            {

                Room r = (Room)o;


                r.unlock();
            }
            else
            {
                Console.WriteLine("you can't use this item on this object");
            }
        }
    }
}
