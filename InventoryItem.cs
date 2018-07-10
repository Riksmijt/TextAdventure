using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
    public class InventoryItem
    {
        
        public string name { get; set; }
        public int weight { get; set; }
        public string description { get; set; }
        public bool badItem { get; set; }
        public bool healing { get; set; }

       public InventoryItem(string name, int weight, string description, bool badItem, bool healing)
        {
            this.name = name;
            this.weight = weight;
            this.description = description;
            this.badItem = badItem;
            this.healing = healing;
           
        }
        public string toString()
        {
            return name + ", " + description + ", " + weight + " kg";
        }
    }
}
