using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{

    class Inventory
    {
       private List<Item> items = new List<Item>();
        public List<Item> Items { get { return items; } set { items = value; } }



        //public string item;
        public Inventory()
        {
            
            
        }

        public void addItem(InventoryItem item)
        {
            //this.items.Add(item);
           
        }

        public void Drop(Item item)
        {
            items.Remove(item);
        }
       public void Take(Item item)
        {
            items.Add(item);
            //currentRoom.Items.Remove(item);
        }
      
        public string showInv()
        {
            string output = "";
            foreach (Item item in items)
            {
                output += item.name + "\r\n";
            }
            return output;

        }

        public Item GetFirstPlayerItem(string name)
        {
            foreach (Item item in Items)
            {
                if (item.name == name)
                {
                    return item;
                }
            }
            return null;
        }




    }    
}
