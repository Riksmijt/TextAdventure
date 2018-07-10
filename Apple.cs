using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
    public class Apple:Item
    {
        public Apple()
        {
            this.name = "apple";
            this.description = "this apple can heal you";
            this.weight = 1;
        }
        public override void use(Object o)
        {
            if(o.GetType() == typeof(Player))
            {
                Player p = (Player) o;
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
