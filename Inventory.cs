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

        public void addItem(Item item)
        {
            this.items.Add(item);
           
        }
        public void GetItemsRoom()
        {

            string message = "In this room you find:";

            if (items.Count != 0)
            {

                for (int i = 0; i < items.Count; i++)
                {

                    message += "\n" + items[i].name;

                }

                Console.WriteLine(message);
            }
            else
            {

                Console.WriteLine("There are no items in this room");

            }

        }

        public Item Take(Inventory other, string item)
        {

            if (other == null)
            {

                Console.WriteLine("This inventory does not exist!");
                return null;

            }

            for (int i = Items.Count - 1; i >= 0; i--)
            {

                if (!this.Items.Any())
                {

                    Console.WriteLine("This item does not exist in this room!");
                    return null;

                }

                if (Items[i].name == item)
                {

                    other.Items.Add(Items[i]);
                    Console.WriteLine("You took a " + Items[i].name);
                    Items.Remove(Items[i]);

                }
                else
                {

                    Console.WriteLine("This item does not exist in this room!");

                }
            }
            return null;
        }

        public Item Drop(Inventory other, string item)
        {

            if (other == null)
            {

                Console.WriteLine("This inventory does not exist!");
                return null;

            }

            for (int i = Items.Count - 1; i >= 0; i--)
            {

                if (!this.Items.Any())
                {

                    Console.WriteLine("This item does not exist in your inventory!");
                    return null;

                }

                if (Items[i].name == item)
                {

                    other.Items.Add(Items[i]);
                    Console.WriteLine("You dropped a " + Items[i].name);
                    Items.Remove(Items[i]);

                }
                else
                {

                    Console.WriteLine("This item does not exist in your inventory!");

                }
            }
            return null;
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
        /*public Item GetKey(string name)
        {
            foreach(Key key in Items)
            {
                if(key.name == name)
                {
                    return key;
                }

            }
            return null;
        }
        */



    }    
}
