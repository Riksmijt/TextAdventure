using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
    class AWP:Item
    {
        public AWP()
        {
            this.name = "awp";
            this.description = "one shot one kill";
            this.weight = 20;
        }
        public override void use(Object o)
        {
            if (o.GetType() == typeof(Player))
            {
                Player p = (Player)o;
                p.heal(10);
                Console.WriteLine("healed by 10");
            }
            else
            {
                Console.WriteLine("you can't use this");
            }
        }
    }
}
