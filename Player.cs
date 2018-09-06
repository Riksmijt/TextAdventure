using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ZuulCS
{
    class Player
    {
        
        private Room currentRoom;
        private Inventory playerInventory;
        private Parser pars;
        private int health;
       
        
        public Room CurrentRoom { get{ return currentRoom; } set { currentRoom = value; } }
        // private List<InventoryItem> playerItems = new List<InventoryItem>();
        //  public List<InventoryItem> PlayerItems { get { return playerItems; } set { playerItems = value; } }

        public Inventory PlayerInventory { get { return playerInventory; } set { playerInventory = value; } }
        public Player()
        {

            pars = new Parser();
            playerInventory = new Inventory();
            
            
            health = 50;
            
        }
     
        public void damage(uint amountDamage)
        {
            health = health - (int) amountDamage;
            Console.WriteLine("current health  " + health);
        }
        public void heal(int amountHeal) 
        {
            health = health + amountHeal;
        }

       /* public void take(InventoryItem item)
        {
            playerItems.Add(item);
            currentRoom.Items.Remove(item);
            if (item.badItem)
            {
                this.damage(3);
                Console.WriteLine("that item was not good for you -3 health");
            }
        }
       public void drop(InventoryItem item)
        {
            playerItems.Remove(item);
            currentRoom.Items.Add(item);
        }*/
      
        

        public bool isAlive()
        {
            if (health <= 0)
            {
             
                return false;
                
            }

            return true;
        }
    }
}
