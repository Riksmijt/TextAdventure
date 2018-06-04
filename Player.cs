using System;
namespace ZuulCS
{
    public class Player
    {
        private Room currentRoom;
        private int health;
        
        public Room CurrentRoom { get{ return currentRoom; } set { currentRoom = value; } }

        public Player()
        {
            
        }
        public void damage(uint amountDamage)
        {
            health = health - (int) amountDamage;
        }
        public void heal(int amountHeal) 
        {
            health = health + amountHeal;
        }

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
